import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user/user.module';
import { AuthService } from 'src/app/services/auth/auth.service';
import {  faBars, faSearch} from '@fortawesome/free-solid-svg-icons'

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.css'],
    standalone: false
})
export class HeaderComponent implements OnInit {
  user?: User;
  faBarChart = faBars;
  faSearch = faSearch;
  isMenuOpen = false;

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }
  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.authService.user$.subscribe({
      next: (response) => {
        this.user = response;
      },
    });
  }

  onLogout(): void {
    this.authService.logout();
    this.router.navigateByUrl('/');
  }

  onSearch(search: string){
    this.router.navigateByUrl(`/articles/search/${search}`);
  }
}
