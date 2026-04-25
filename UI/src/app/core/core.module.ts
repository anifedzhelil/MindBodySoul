import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { HeaderDesktopComponent } from './header/header-desktop/header-desktop.component';
import { HeaderMobileComponent } from './header/header-mobile/header-mobile.component';

@NgModule({
  declarations: [HeaderComponent, FooterComponent, HeaderMobileComponent, HeaderDesktopComponent],
  imports: [CommonModule, RouterModule, SharedModule, FontAwesomeModule],
  exports: [HeaderComponent, FooterComponent],
})
export class CoreModule {}
