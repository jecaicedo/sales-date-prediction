import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface CustomerPrediction {
  custID: number;
  customerName: string;
  lastOrderDate: string;
  nextPredictedOrder: string;
}

export interface Order {
  orderID: number;
  orderDate: string;
  requiredDate: string;
  shippedDate: string;
  freight: number | null;
  shipName: string;
  shipAddress: string;
  shipCity: string;
  shipCountry: string | null;
  custID: number;
  customer: any | null;
  empID: number;
  employee: any | null;
  shipperID: number;
  shipper: any | null;
}

export interface NewOrderRequest {
  custID: number;
  empID: number;
  requiredDate: string;
  shippedDate: string;
  shipperID: number;
  freight: number;
  shipName: string;
  shipAddress: string;
  shipCity: string;
  shipCountry: string;
  productID: number;
  unitPrice: number;
  quantity: number;
  discount: number;
}

export interface Employee {
  empID: number;
  firstName: string;
  lastName: string;
}

export interface Shipper {
  shipperID: number;
  companyName: string;
}

export interface Product {
  productID: number;
  productName: string;
  unitPrice: number;
  discontinued: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private baseUrl = 'http://localhost:8080/api';

  constructor(private http: HttpClient) { }

  getCustomerPredictions(searchTerm?: string): Observable<CustomerPrediction[]> {
    let params = new HttpParams();
    if (searchTerm && searchTerm.trim()) {
      params = params.set('customerName', searchTerm.trim());
    }
    
    const url = `${this.baseUrl}/customers/predictions`;
    console.log('API URL:', url);
    console.log('Search params:', params.toString());
    
    return this.http.get<CustomerPrediction[]>(url, { params });
  }

  getCustomerOrders(custId: number): Observable<Order[]> {
    return this.http.get<Order[]>(`${this.baseUrl}/orders/customer/${custId}`);
  }

  getEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(`${this.baseUrl}/employees`);
  }

  getShippers(): Observable<Shipper[]> {
    return this.http.get<Shipper[]>(`${this.baseUrl}/shippers`);
  }

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.baseUrl}/products`);
  }

  createOrder(orderData: NewOrderRequest): Observable<any> {
    return this.http.post(`${this.baseUrl}/orders/`, orderData);
  }
}
