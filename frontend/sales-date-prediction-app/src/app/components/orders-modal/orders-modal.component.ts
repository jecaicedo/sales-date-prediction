import { Component, Input, Output, EventEmitter, OnInit, OnChanges, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatSortModule, Sort } from '@angular/material/sort';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { CustomerPrediction, Order, CustomerService } from '../../services/customer.service';

@Component({
  selector: 'app-orders-modal',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './orders-modal.component.html',
  styleUrls: ['./orders-modal.component.css']
})
export class OrdersModalComponent implements OnInit, OnChanges {
  @Input() customer: CustomerPrediction | null = null;
  @Input() isOpen = false;
  @Output() modalClose = new EventEmitter<void>();
  
  allOrders = signal<Order[]>([]);
  displayedOrders = signal<Order[]>([]);
  displayedColumns: string[] = ['orderID', 'requiredDate', 'shippedDate', 'shipName', 'shipAddress', 'shipCity'];
  
  pageSize = 10;
  pageIndex = 0;
  totalItems = 0;
  
  sortField = 'orderID';
  sortDirection: 'asc' | 'desc' = 'asc';
  
  isLoading = signal(false);

  constructor(private customerService: CustomerService) {}

  ngOnInit(): void {
  }

  ngOnChanges(): void {
    if (this.customer && this.isOpen) {
      this.resetPagination();
      this.loadOrders();
    }
  }

  resetPagination(): void {
    this.pageIndex = 0;
    this.sortField = 'orderID';
    this.sortDirection = 'asc';
    this.allOrders.set([]);
    this.displayedOrders.set([]);
  }

  loadOrders(): void {
    if (!this.customer) return;
    
    this.isLoading.set(true);
    this.customerService.getCustomerOrders(this.customer.custID).subscribe({
      next: (data) => {
        this.allOrders.set(data);
        this.totalItems = data.length;
        this.applySortingAndPagination();
        this.isLoading.set(false);
      },
      error: (error) => {
        console.error('Error loading orders:', error);
        this.isLoading.set(false);
      }
    });
  }

  applySortingAndPagination(): void {
    let data = [...this.allOrders()];
    
    data.sort((a, b) => {
      const aValue = this.getFieldValue(a, this.sortField);
      const bValue = this.getFieldValue(b, this.sortField);
      
      if (aValue < bValue) return this.sortDirection === 'asc' ? -1 : 1;
      if (aValue > bValue) return this.sortDirection === 'asc' ? 1 : -1;
      return 0;
    });
    
    const startIndex = this.pageIndex * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    const paginatedData = data.slice(startIndex, endIndex);
    this.displayedOrders.set(paginatedData);
  }

  getFieldValue(order: Order, field: string): any {
    switch (field) {
      case 'orderID':
        return order.orderID;
      case 'requiredDate':
        return new Date(order.requiredDate);
      case 'shippedDate':
        return new Date(order.shippedDate);
      case 'shipName':
        return order.shipName.toLowerCase();
      case 'shipAddress':
        return order.shipAddress.toLowerCase();
      case 'shipCity':
        return order.shipCity.toLowerCase();
      default:
        return '';
    }
  }

  formatDate(dateString: string): string {
    const date = new Date(dateString);
    return date.toLocaleDateString('en-US', {
      month: 'numeric',
      day: 'numeric',
      year: 'numeric',
      hour: 'numeric',
      minute: '2-digit',
      second: '2-digit',
      hour12: true
    });
  }

  onSortChange(sort: Sort): void {
    if (sort.active && sort.direction) {
      this.sortField = sort.active;
      this.sortDirection = sort.direction as 'asc' | 'desc';
    } else {
      this.sortField = 'orderID';
      this.sortDirection = 'asc';
    }
    this.applySortingAndPagination();
  }

  onPageChange(event: PageEvent): void {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.applySortingAndPagination();
  }

  closeModal(): void {
    this.modalClose.emit();
  }
}
