  import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
  import { ProductServices } from '../../Services/product-services';
  import { IProduct } from '../../Models/iproduct';
  import { CommonModule } from '@angular/common';

  @Component({
    selector: 'app-products',
    imports: [ CommonModule],
    templateUrl: './products.html',
    styleUrl: './products.css'
  })
  export class Products implements OnInit {

    products: IProduct[] = [];
    
    constructor (private productServices: ProductServices , private cd : ChangeDetectorRef) {}
    ngOnInit(): void { 

      this.productServices.getAllProducts().subscribe({

        next: (data) => {
  console.log('Products received:', data);
  this.products = data; 
  this.cd.detectChanges();
},
        
        error: (err) => console.error('Error fetching products:', err)
      });
    }
    
  }
