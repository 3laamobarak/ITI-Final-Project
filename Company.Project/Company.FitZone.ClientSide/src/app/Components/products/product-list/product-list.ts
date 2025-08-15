import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ProductServices } from '../../../Services/product-services';
import { IProduct } from '../../../Models/iproduct';
import { CommonModule } from '@angular/common';
import { ProductCard } from "../product-card/product-card";


@Component({
  selector: 'app-product-list',
  imports: [CommonModule , ProductCard],
  templateUrl: './product-list.html',
  styleUrl: './product-list.css'
})
export class ProductList implements OnInit {

  products: IProduct[] = []

  constructor(private cd: ChangeDetectorRef , private productServices: ProductServices) { }
  ngOnInit(): void {
  
    this.productServices.getAllProducts().subscribe({
      next: (data) => this.products = data,
      complete: () => this.cd.detectChanges(),
      error: (err) => console.error('Error:', err)
    });
  }

}
