import { Component, Input, Output, EventEmitter, OnInit, OnChanges, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { CustomerPrediction, NewOrderRequest, Employee, Shipper, Product, CustomerService } from '../../services/customer.service';

@Component({
  selector: 'app-new-order-modal',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatIconModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  templateUrl: './new-order-modal.component.html',
  styleUrls: ['./new-order-modal.component.css']
})
export class NewOrderModalComponent implements OnInit, OnChanges {
  @Input() customer: CustomerPrediction | null = null;
  @Input() isOpen = false;
  @Output() modalClose = new EventEmitter<void>();
  
  orderForm: FormGroup;
  isLoading = signal(false);
  
  employees = signal<Employee[]>([]);
  shippers = signal<Shipper[]>([]);
  products = signal<Product[]>([]);

  constructor(private fb: FormBuilder, private customerService: CustomerService) {
    this.orderForm = this.fb.group({
      empID: ['', Validators.required],
      shipperID: ['', Validators.required],
      shipName: ['', Validators.required],
      shipAddress: ['', Validators.required],
      shipCity: ['', Validators.required],
      shipCountry: ['', Validators.required],
      orderDate: [new Date(), Validators.required],
      requiredDate: ['', Validators.required],
      shippedDate: ['', Validators.required],
      freight: ['', [Validators.required, Validators.min(0)]],
      
      productID: ['', Validators.required],
      unitPrice: ['', [Validators.required, Validators.min(0.01)]],
      quantity: ['', [Validators.required, Validators.min(1)]],
      discount: ['', [Validators.required, Validators.min(0), Validators.max(100)]]
    });
  }

  ngOnInit(): void {
    this.loadDropdownData();
  }

  ngOnChanges(): void {
    if (this.customer && this.isOpen) {
      this.resetForm();
    }
  }

  loadDropdownData(): void {
    this.customerService.getEmployees().subscribe({
      next: (data) => this.employees.set(data),
      error: (error) => console.error('Error loading employees:', error)
    });

    this.customerService.getShippers().subscribe({
      next: (data) => this.shippers.set(data),
      error: (error) => console.error('Error loading shippers:', error)
    });

    this.customerService.getProducts().subscribe({
      next: (data) => this.products.set(data),
      error: (error) => console.error('Error loading products:', error)
    });
  }

  resetForm(): void {
    this.orderForm.reset({
      empID: '',
      shipperID: '',
      shipName: '',
      shipAddress: '',
      shipCity: '',
      shipCountry: '',
      orderDate: new Date(),
      requiredDate: '',
      shippedDate: '',
      freight: '',
      productID: '',
      unitPrice: '',
      quantity: '',
      discount: ''
    });
  }

  onSubmit(): void {
    if (this.orderForm.valid && this.customer) {
      this.isLoading.set(true);
      
      const formData = this.orderForm.value;
      const orderData: NewOrderRequest = {
        custID: this.customer.custID,
        empID: parseInt(formData.empID),
        requiredDate: formData.requiredDate,
        shippedDate: formData.shippedDate,
        shipperID: parseInt(formData.shipperID),
        freight: parseFloat(formData.freight),
        shipName: formData.shipName,
        shipAddress: formData.shipAddress,
        shipCity: formData.shipCity,
        shipCountry: formData.shipCountry,
        productID: parseInt(formData.productID),
        unitPrice: parseFloat(formData.unitPrice),
        quantity: parseInt(formData.quantity),
        discount: parseFloat(formData.discount)
      };
      
      this.customerService.createOrder(orderData).subscribe({
        next: (response) => {
          console.log('Order created successfully:', response);
          this.isLoading.set(false);
          this.closeModal();
          alert('Order created successfully!');
        },
        error: (error) => {
          console.error('Error creating order:', error);
          this.isLoading.set(false);
          alert('Error creating order. Please try again.');
        }
      });
    } else {
      this.markFormGroupTouched();
    }
  }

  markFormGroupTouched(): void {
    Object.keys(this.orderForm.controls).forEach(key => {
      const control = this.orderForm.get(key);
      control?.markAsTouched();
    });
  }

  getFieldError(fieldName: string): string {
    const field = this.orderForm.get(fieldName);
    if (field?.errors && field.touched) {
      if (field.errors['required']) {
        return `${fieldName} is required`;
      }
      if (field.errors['min']) {
        return `${fieldName} must be greater than 0`;
      }
      if (field.errors['max']) {
        return `${fieldName} must be less than or equal to 100`;
      }
    }
    return '';
  }

  get availableProducts(): Product[] {
    return this.products().filter(product => !product.discontinued);
  }

  onProductChange(): void {
    const productId = this.orderForm.get('productID')?.value;
    if (productId) {
      const product = this.products().find(p => p.productID === parseInt(productId));
      if (product) {
        this.orderForm.patchValue({
          unitPrice: product.unitPrice
        });
      }
    }
  }

  closeModal(): void {
    this.resetForm();
    this.modalClose.emit();
  }
}
