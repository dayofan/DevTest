import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { CustomerModel } from '../models/customer.model';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private customers: CustomerModel[] = [];
  customersUpdated = new Subject<CustomerModel[]>();

  constructor(private httpClient: HttpClient) { }

  public CreateCustomer(customer: CustomerModel) : void {
    this.httpClient.post('http://localhost:63235/customer', customer).subscribe(() => 
      this.FetchCustomers().subscribe(customers => this.customersUpdated.next(customers)));
  }

  public FetchCustomers() : Observable<CustomerModel[]> {
    return this.httpClient.get<CustomerModel[]>('http://localhost:63235/customer');
  }

  public FetchCustomer(customerId: number) : Observable<CustomerModel> {
    return this.httpClient.get<CustomerModel>(`http://localhost:63235/customer/${customerId}`);
  }
}
