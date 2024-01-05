import { Component, TemplateRef } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { CreateProductCommand, Product, ProductsClient, UpdateProductCommand } from '../web-api-client';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent {
  debug = true;
  products: Product[] = [];
  selectedProduct: Product;
  productDetailsEditor: any = {};
  productDetailsModalRef: BsModalRef;

  constructor(
    private productsClient: ProductsClient,
    private modalService: BsModalService) {
    productsClient.getProducts().subscribe({
      next: result => this.products = result,
      error: error => console.error(error)
    });
  }

  showProductDetailsModal(template: TemplateRef<any>, product: Product): void {
    this.selectedProduct = product;
    this.productDetailsEditor = {
      ...this.selectedProduct
    };

    this.productDetailsModalRef = this.modalService.show(template);
  }

  updateProductDetails(): void {
    const product = this.productDetailsEditor as UpdateProductCommand;

    console.log(product, this.productDetailsEditor);

    this.productsClient.updateProduct(this.selectedProduct.id, product)
      .subscribe(
        () => {
          this.selectedProduct.name = this.productDetailsEditor.name;
          this.selectedProduct.description = this.productDetailsEditor.description;
          this.productDetailsEditor.hide();
          this.productDetailsEditor = {};
        },
        error => console.error(error)
      );
  }

  deleteProduct(product: Product) {
    console.log("Deleting ", product);
  }

  createProduct() {
    this.productsClient.createProduct({
      name: "New name",
      description: "New Description"
    } as CreateProductCommand)
      .subscribe(
        result => {
          this.products.push(result);
        },
        error => console.error(error)
      );
  }
}
