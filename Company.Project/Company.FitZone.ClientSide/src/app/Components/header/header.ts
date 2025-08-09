import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ICategory } from '../../Models/icategory';
import { CategoryServices } from '../../Services/category-services';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-header',
  imports: [CommonModule, RouterLink],
  templateUrl: './header.html',
  styleUrl: './header.css',
})
export class Header implements OnInit {
  categories: ICategory[] = [];

  constructor(
    private categoryServices: CategoryServices,
    private cd: ChangeDetectorRef
  ) {}

  ngOnInit(): void {

    this.categoryServices.getAllCategories().subscribe((data) => {

      this.categories = data;
      
      this.cd.detectChanges();
    });
  }
}
