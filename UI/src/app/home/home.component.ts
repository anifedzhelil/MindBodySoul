import { Component } from '@angular/core';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  standalone: false
})
export class HomeComponent {
  //Оriginal image on Cloudinaryş the pipe adds width, format and quality 
  heroImageUrl = 'https://res.cloudinary.com/drhqukmht/image/upload/v1794155902/MineBodySoul/home6_zd1lcn.jpg';
}
