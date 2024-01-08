import { Component } from '@angular/core';
import { TenantsClient, CreateDemoCommand, OrdersClient, Product, ProductsClient, DemoClient } from '../web-api-client';

@Component({
  selector: 'app-create-demo',
  templateUrl: './create-demo.component.html',
  styleUrls: ['./create-demo.component.css']
})
export class CreateDemoComponent {
  products: Product[] = [];
  createDemoCommand: CreateDemoCommand;

  constructor(
    private productsClient: ProductsClient,
    private ordersClient: OrdersClient,
    private createDemoClient: TenantsClient,
    private demoClient: DemoClient) {

    productsClient.getProducts().subscribe({
      next: result => this.products = result,
      error: error => console.error(error)
    });

    this.createDemoCommand = {} as CreateDemoCommand;
  }

  createDemo() {
    console.log(this.createDemoCommand);
    this.demoClient.createDemo(this.createDemoCommand).subscribe({
      next: result => {
        if (result.isValid)
          alert("Ok");
        else
          alert("Error");
        console.log(result);
      },
      error: error => console.error(error)
    });
  }
}
