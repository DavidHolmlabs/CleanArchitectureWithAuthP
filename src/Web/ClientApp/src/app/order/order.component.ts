import { Component, TemplateRef } from '@angular/core';
import { CreateOrderCommand, OrderDto, OrdersClient, Product, ProductsClient } from '../web-api-client';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent {
  debug = true;
  public orders: OrderDto[] = [];
  public availableOrders: OrderDto[] = [];

  products: Product[] = [];
  activeOrder: OrderDto;
  orderDetailsEditor: any = {};
  orderDetailsModalRef: BsModalRef;
  constructor(
    private ordersClient: OrdersClient,
    private productsClient: ProductsClient,
    private modalService: BsModalService) {
    ordersClient.getOrders().subscribe({
      next: result => this.orders = result,
      error: error => console.error(error)
    });

    ordersClient.availableOrders().subscribe({
      next: result => this.availableOrders = result,
      error: error => console.error(error)
    });

    productsClient.getProducts().subscribe({
      next: result => this.products = result,
      error: error => console.error(error)
    });
  }

  showOrderDetailsModal(template: TemplateRef<any>) {
    this.orderDetailsModalRef = this.modalService.show(template);
  }

  purchase() {
    this.ordersClient.createOrder({
      productId: 1,
      quantity: this.orderDetailsEditor.quantity
    } as CreateOrderCommand)
      .subscribe(
        result => {
          this.orders.push(result);
        },
        error => console.error(error)
      );
  }

  deleteOrder(order: OrderDto) {
    console.log("Deleting ", order);
  }
}
