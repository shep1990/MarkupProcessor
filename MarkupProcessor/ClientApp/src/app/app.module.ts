import { BrowserModule } from '@angular/platform-browser';
import { Inject, Injectable, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MatIconModule } from '@angular/material/icon';
import { ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { FileUploadComponent } from './file-upload/file-upload.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { HomeComponent } from './home/home.component';

import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { ApiService } from './services/api.service';
import { AppRoutingModule } from './app-routing.module';


@Injectable()
export class BaseUrlInterceptor implements HttpInterceptor {

  constructor(
    @Inject('BASE_API_URL') private baseUrl: string) {
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    const apiReq = request.clone({ url: `${this.baseUrl}/${request.url}` });
    return next.handle(apiReq);
  }
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    FileUploadComponent,
    CounterComponent,
    FetchDataComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    MatIconModule,
    ReactiveFormsModule,
    AppRoutingModule
  ],
  bootstrap: [AppComponent],
  providers: [
    ApiService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: BaseUrlInterceptor,
      multi: true
    },
    {
      provide: "BASE_API_URL", useValue: environment.apiUrl
    }
  ]
})
export class AppModule { }
