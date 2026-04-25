import { Component } from '@angular/core';
import { Router } from '@angular/router';
import {
  faBars,
  faSearch,
  faRightFromBracket,
} from '@fortawesome/free-solid-svg-icons';
import { User } from 'src/app/models/user/user.module';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-header-desktop',
  templateUrl: './header-desktop.component.html',
  styleUrl: './header-desktop.component.css',
  standalone: false,
})
export class HeaderDesktopComponent {
  user?: User;
  faBarChart = faBars;
  faSearch = faSearch;
  faRightFromBracket = faRightFromBracket;
  isMenuOpen = false;

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
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

  onLogout(): void {
    this.authService.logout();
    this.router.navigateByUrl('/');
  }

  onSearch(search: string) {
    this.router.navigateByUrl(`/articles/search/${search}`);
  }
}
