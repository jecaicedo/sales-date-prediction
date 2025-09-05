import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CustomerService, CustomerPrediction } from '../../services/customer.service';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatSortModule, Sort } from '@angular/material/sort';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { OrdersModalComponent } from '../orders-modal/orders-modal.component';
import { NewOrderModalComponent } from '../new-order-modal/new-order-modal.component';
import { debounceTime, distinctUntilChanged, Subject } from 'rxjs';

@Component({
  selector: 'app-sales-prediction',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatButtonModule,
    MatInputModule,
    MatIconModule,
    MatProgressSpinnerModule,
    OrdersModalComponent,
    NewOrderModalComponent
  ],
  templateUrl: './sales-prediction.component.html',
  styleUrls: ['./sales-prediction.component.css']
})
export class SalesPredictionComponent implements OnInit {
  customers = signal<CustomerPrediction[]>([]);
  filteredCustomers = signal<CustomerPrediction[]>([]);
  displayedColumns: string[] = ['customerName', 'lastOrderDate', 'nextPredictedOrder', 'actions'];
  
  pageSize = 10;
  pageIndex = 0;
  totalItems = 0;
  
  sortField = 'customerName';
  sortDirection: 'asc' | 'desc' = 'asc';
  
  searchTerm = '';
  private searchSubject = new Subject<string>();
  isLoading = signal(false);
  
  showOrdersModal = false;
  showNewOrderModal = false;
  selectedCustomer: CustomerPrediction | null = null;

  constructor(private customerService: CustomerService) {
    this.searchSubject.pipe(
      debounceTime(300),
      distinctUntilChanged()
    ).subscribe(searchTerm => {
      console.log('Search term processed:', searchTerm);
      this.searchTerm = searchTerm;
      this.loadCustomers();
    });
  }

  ngOnInit(): void {
    this.loadCustomers();
  }

  loadCustomers(): void {
    this.isLoading.set(true);
    console.log('Loading customers with search term:', this.searchTerm);
    this.customerService.getCustomerPredictions(this.searchTerm).subscribe({
      next: (data) => {
        console.log('Customers received:', data.length, 'items');
        this.customers.set(data);
        this.applySortingAndPagination();
        this.isLoading.set(false);
      },
      error: (error) => {
        console.error('Error loading customers:', error);
        this.isLoading.set(false);
      }
    });
  }

  onSearchChange(event: Event): void {
    const target = event.target as HTMLInputElement;
    console.log('Search input changed:', target.value);
    this.searchSubject.next(target.value);
  }

  applySortingAndPagination(): void {
    let data = [...this.customers()];
    
    data.sort((a, b) => {
      const aValue = this.getFieldValue(a, this.sortField);
      const bValue = this.getFieldValue(b, this.sortField);
      
      if (aValue < bValue) return this.sortDirection === 'asc' ? -1 : 1;
      if (aValue > bValue) return this.sortDirection === 'asc' ? 1 : -1;
      return 0;
    });
    
    this.totalItems = data.length;
    const startIndex = this.pageIndex * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    this.filteredCustomers.set(data.slice(startIndex, endIndex));
  }

  getFieldValue(customer: CustomerPrediction, field: string): any {
    switch (field) {
      case 'customerName':
        return customer.customerName.toLowerCase();
      case 'lastOrderDate':
        return new Date(customer.lastOrderDate);
      case 'nextPredictedOrder':
        return new Date(customer.nextPredictedOrder);
      default:
        return '';
    }
  }

  onSortChange(sort: Sort): void {
    this.sortField = sort.active;
    this.sortDirection = sort.direction as 'asc' | 'desc';
    this.applySortingAndPagination();
  }

  onPageChange(event: PageEvent): void {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.applySortingAndPagination();
  }

  formatDate(dateString: string): string {
    const date = new Date(dateString);
    return date.toLocaleDateString('en-US', {
      month: 'numeric',
      day: 'numeric',
      year: 'numeric'
    });
  }

  viewOrders(customer: CustomerPrediction): void {
    this.selectedCustomer = customer;
    this.showOrdersModal = true;
  }

  newOrder(customer: CustomerPrediction): void {
    this.selectedCustomer = customer;
    this.showNewOrderModal = true;
  }

  closeModals(): void {
    this.showOrdersModal = false;
    this.showNewOrderModal = false;
    this.selectedCustomer = null;
  }
}
