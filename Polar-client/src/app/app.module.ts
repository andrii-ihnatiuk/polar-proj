import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TestComponent } from './home/test/test.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { MainComponent } from './home/main/main.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { ReactiveFormsModule } from '@angular/forms';
import { UserService } from './shared/services/user.service';

@NgModule({
  declarations: [
    AppComponent,
    TestComponent,
    PageNotFoundComponent,
    MainComponent,
    RegistrationComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [UserService],
  bootstrap: [AppComponent]
})
export class AppModule { }
