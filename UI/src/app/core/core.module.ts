import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { GlobalLoaderComponent } from './global-loader/global-loader.component';
import { RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  declarations: [HeaderComponent, FooterComponent, GlobalLoaderComponent],
  imports: [CommonModule, RouterModule, SharedModule],
  exports: [HeaderComponent, FooterComponent, GlobalLoaderComponent],
})
export class CoreModule {}
