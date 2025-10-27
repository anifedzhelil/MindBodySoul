import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { UserModule } from './user/user.module';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { CloudinaryModule } from '@cloudinary/ng';
import { AuthInterceptor } from './core/interceptors/auth.interceptor';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import { ContactsComponent } from './contacts/contacts.component';

@NgModule({ declarations: [
        AppComponent,
        HomeComponent,
        PageNotFoundComponent,
        ContactsComponent,
    ],
    bootstrap: [AppComponent], imports: [BrowserModule,
        FontAwesomeModule,
        FormsModule,
        ReactiveFormsModule,
        UserModule,
        CoreModule,
        CloudinaryModule,
        SharedModule,
        AppRoutingModule], providers: [
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthInterceptor,
            multi: true,
        },
        provideHttpClient(withInterceptorsFromDi()),
    ] })
export class AppModule {}
