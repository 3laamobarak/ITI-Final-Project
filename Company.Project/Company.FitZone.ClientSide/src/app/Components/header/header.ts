import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Category } from '../../Models/category';
import { CategoryServices } from '../../Services/category-services';

@Component({
  selector: 'app-header',
  imports: [ CommonModule],
  templateUrl: './header.html',
  styleUrl: './header.css'
})
export class Header  implements OnInit {


 categories: Category[] = [];

 constructor ( private categoryServices: CategoryServices){}

  ngOnInit(): void {
    this.categoryServices.getCategories().subscribe(data => {
      this.categories = data;
    });
  }
}
