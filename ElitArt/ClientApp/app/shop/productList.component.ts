import { Component, OnInit } from "@angular/core";
import { DataService } from '../shared/dataService';
import { Product } from "../shared/product";

@Component({
    selector: "product-list",
    templateUrl: "productList.component.html",
    styleUrls: ["productList.component.css"]
})
export class ProductList implements OnInit {
    public products: Product[];
    constructor(private data: DataService) {
        this.products = data.products;
    }

    ngOnInit(): void {
        let that = this;
        this.data.loadProducts()
            .subscribe(success => {
                if (success) {
                    that.products = that.data.products;
                }
            });
    }

    //ngOnInit() {
    //    this.data.loadProducts()
    //        .subscribe(() => this.products = this.data.products);
    //}

    addProduct(product: Product) {
        //console.log(product);
        this.data.addProduct(product);
    }
}