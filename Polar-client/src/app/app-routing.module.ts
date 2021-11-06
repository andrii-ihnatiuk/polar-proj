import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { TestComponent } from './home/test/test.component';
import { MainComponent } from './home/main/main.component';
import { RegistrationComponent } from './user/registration/registration.component';

const routes: Routes = [
  { path: '', component: MainComponent },
  { path: 'test', component: TestComponent },
  { path: 'register', component: RegistrationComponent },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
