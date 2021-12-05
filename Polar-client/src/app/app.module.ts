import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClient, HttpClientModule } from '@angular/common/http'
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TestComponent } from './home/test/test.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { MainComponent } from './home/main/main.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { ReactiveFormsModule } from '@angular/forms';
import { UserService } from './shared/services/user.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { LoginComponent } from './user/login/login.component'; 
import { FormsModule } from '@angular/forms';
import { ProfileComponent } from './home/profile/profile.component';
import { RatingComponent } from './home/rating/rating.component';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { NavbarComponent } from './navbar/navbar.component';
import { NewsComponent } from './home/news/news.component';
import { AchievementsComponent } from './home/achievements/achievements.component';
import { StoriesComponent } from './home/stories/stories.component';

export function createTranslateLoader (http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

@NgModule({
  declarations: [
    AppComponent,
    TestComponent,
    PageNotFoundComponent,
    MainComponent,
    RegistrationComponent,
    LoginComponent,
    ProfileComponent,
    RatingComponent,
    NavbarComponent,
    NewsComponent,
    AchievementsComponent,
    StoriesComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    FormsModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: (createTranslateLoader),
        deps: [HttpClient]
      }
    })

  ],
  providers: [UserService],
  bootstrap: [AppComponent]
})
export class AppModule { }
