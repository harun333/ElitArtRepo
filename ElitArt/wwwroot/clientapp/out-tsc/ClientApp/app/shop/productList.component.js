import * as tslib_1 from "tslib";
import { Component } from "@angular/core";
import { DataService } from '../shared/dataService';
var ProductList = /** @class */ (function () {
    function ProductList(data) {
        this.data = data;
        this.products = data.products;
    }
    ProductList.prototype.ngOnInit = function () {
        var that = this;
        this.data.loadProducts()
            .subscribe(function (success) {
            if (success) {
                that.products = that.data.products;
            }
        });
    };
    //ngOnInit() {
    //    this.data.loadProducts()
    //        .subscribe(() => this.products = this.data.products);
    //}
    ProductList.prototype.addProduct = function (product) {
        //console.log(product);
        this.data.addProduct(product);
    };
    ProductList = tslib_1.__decorate([
        Component({
            selector: "product-list",
            templateUrl: "productList.component.html",
            styleUrls: ["productList.component.css"]
        }),
        tslib_1.__metadata("design:paramtypes", [DataService])
    ], ProductList);
    return ProductList;
}());
export { ProductList };
//# sourceMappingURL=productList.component.js.map