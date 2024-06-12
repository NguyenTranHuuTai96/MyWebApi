import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';



const routes: Routes = [
  {
    path:'',
    redirectTo:'login-user',
    pathMatch:'full',
  },
  {
    path: 'login-user',
    loadChildren:()=> import('./login-user/login-user.module').then(x => x.LoginUserModule),
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LoginRoutingModule { }
