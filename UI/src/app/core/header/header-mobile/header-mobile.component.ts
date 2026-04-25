import { Component } from '@angular/core';
import { Router } from '@angular/router';
import {
  faBars,
  faSearch,
  faRightFromBracket,
  faHome,
  faFolder,
  faNewspaper,
  faGear,
  faEnvelope,
  faRightToBracket,
  faUser,
} from '@fortawesome/free-solid-svg-icons';
import { User } from 'src/app/models/user/user.module';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-header-mobile',
  templateUrl: './header-mobile.component.html',
  styleUrl: './header-mobile.component.css',
  standalone: false,
})
export class HeaderMobileComponent {
  faBarChart = faBars;
  faSearch = faSearch;
  faRightFromBracket = faRightFromBracket;
  faHome = faHome;
  faFolder = faFolder;
  faNewspaper = faNewspaper;
  faGear = faGear;
  faEnvelope = faEnvelope;
  faSignOut = faRightFromBracket;
  faSignIn = faRightToBracket;
  faUser = faUser;

  user?: User;
  isMenuOpen = false;

  onLogout(): void {
    this.authService.logout();
    this.router.navigateByUrl('/');
  }

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
    console.log('isMenuOpen:', this.isMenuOpen);
  }

  constructor(
    private authService: AuthService,
    private router: Router,
  ) {}

  ngOnInit(): void {
    this.authService.user$.subscribe({
      next: (response) => {
        this.user = response;
      },
    });
  }

  onSearch(search: string) {
    this.router.navigateByUrl(`/articles/search/${search}`);
  }
}
