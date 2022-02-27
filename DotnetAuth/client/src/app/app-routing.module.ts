import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AllUserManagmentComponent } from './all-user-managment/all-user-managment.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { UserManagementComponent } from './user-management/user-management.component';
import { AuthGuardService } from './_guard/auth-guard.service';

const routes: Routes = [
  {path:"login",component:LoginComponent},
  {path:"register",component:RegisterComponent, canActivate:[AuthGuardService]},
  {path:"user-management",component:UserManagementComponent, canActivate:[AuthGuardService]},
  {path:"all-user-management",component:AllUserManagmentComponent, canActivate:[AuthGuardService]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
