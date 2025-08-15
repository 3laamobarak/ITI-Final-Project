import { Routes } from '@angular/router';
import { Home } from './Components/home/home';
import { NotFound } from './Components/not-found/not-found';
import { ProductList } from './Components/products/product-list/product-list';

export const routes: Routes = [

    { path: '', redirectTo: '/home', pathMatch: 'full' },

    {path: 'home' , component: Home},

    {path: 'productsList' , component: ProductList},

    {path: 'NotFound' , component: NotFound},

    {path: '**', redirectTo: '/NotFound'}
   
];
