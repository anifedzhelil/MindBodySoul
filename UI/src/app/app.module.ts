import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './core/header/header.component';
import { FooterComponent } from './core/footer/footer.component';
import { UserModule } from './user/user.module';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { AdminModule } from './admin/admin.module';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { CloudinaryModule } from '@cloudinary/ng';
import { AuthInterceptor } from './core/interceptors/auth.interceptor';
import { GlobalLoaderComponent } from './core/global-loader/global-loader.component';
import { CoreModule } from './core/core/core.module';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';

@NgModule({
  declarations: [AppComponent, HomeComponent, PageNotFoundComponent],
  imports: [
    BrowserModule,
    FontAwesomeModule,
    FormsModule,
    HttpClientModule,
    AdminModule,
    UserModule,
    CoreModule,
    CloudinaryModule,
    AppRoutingModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
