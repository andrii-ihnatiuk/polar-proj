import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TestComponent } from './home/test/test.component';
import { MainComponent } from './home/main/main.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { LoginComponent } from './user/login/login.component';
import { ProfileComponent } from './home/profile/profile.component';
import { AuthGuard } from './auth/auth.guard';
import { RatingComponent } from './home/rating/rating.component';
import { NewsComponent } from './home/news/news.component';
import { AchievementsComponent } from './home/achievements/achievements.component';

const routes: Routes = [
  { path: '', component: MainComponent },
  { path: 'test', component: TestComponent },
  { path: 'register', component: RegistrationComponent },
  // { path: '**', component: PageNotFoundComponent },
  { path: 'login', component: LoginComponent },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
  { path: 'rating', component: RatingComponent },
  { path: 'news', component: NewsComponent },
  { path: 'achievments', component: AchievementsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
