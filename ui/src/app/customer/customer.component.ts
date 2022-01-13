import { Component, OnDestroy, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Subscription } from 'rxjs';
import { CustomerModel } from '../models/customer.model';
import { CustomerService } from '../services/customer.service';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss']
})
export class CustomerComponent implements OnInit, OnDestroy {
  private customersUpdateSubscription: Subscription;
  private customersSubscription: Subscription;
  public customers: CustomerModel[] = [];
  public newCustomer: CustomerModel = {
    id: null,
    name: null,
    type: null
  };

  constructor(private customerService: CustomerService) { }
  
  ngOnInit() {
    this.customersUpdateSubscription = this.customerService.customersUpdated.subscribe(customers => this.customers = customers);
    this.customersSubscription = this.customerService.FetchCustomers().subscribe(customers => this.customers = customers);
  }
  
  createCustomer(customerForm: NgForm) {
    if (customerForm.invalid) {
      alert('form is not valid');
    }
    else {
      this.customerService.CreateCustomer(this.newCustomer);
    }
  }
  
  ngOnDestroy(): void {
    this.customersUpdateSubscription.unsubscribe();
    this.customersSubscription.unsubscribe();
  }
}
