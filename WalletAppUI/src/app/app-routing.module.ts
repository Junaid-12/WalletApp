import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterComponent } from './pages/register/register.component';
import { LoginComponent } from './pages/login/login.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { AddBalanceComponent } from './pages/add-balance/add-balance.component';
import { SendMoneyComponent } from './pages/send-money/send-money.component';
import { TransactionsComponent } from './pages/transactions/transactions.component';
import { AuthGuard } from './auth.guard';

const routes: Routes = [

  {path: '', redirectTo: '/register', pathMatch: 'full' }, 

  { path: 'register',component: RegisterComponent},
  {path:'login' , component: LoginComponent},
  {path: 'dashboard',component:DashboardComponent , canActivate : [AuthGuard]},
  {path:'profile',component:ProfileComponent, canActivate : [AuthGuard]},
  {path:'add-balance', component:AddBalanceComponent, canActivate:[AuthGuard]},
  {path:'send-money',component:SendMoneyComponent, canActivate : [AuthGuard]},
  {path : 'transactions',component:TransactionsComponent, canActivate : [AuthGuard]},
  {path :'**' , redirectTo:'login'}
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

