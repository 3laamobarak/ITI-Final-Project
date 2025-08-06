import { Component } from '@angular/core';
import { Header } from "./Components/header/header";
import { Footer } from "./Components/footer/footer";
import { Products } from "./Components/products/products";

@Component({
  selector: 'app-root',
  imports: [Header, Footer, Products],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'IHerb';
}
