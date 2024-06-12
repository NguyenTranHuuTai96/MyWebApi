import { Routes } from '@angular/router';
import { LayoutLoginComponent } from './Layout/layout-login/layout-login.component';

export const routes: Routes = [
    {
        path:'',
        redirectTo:'login',
        pathMatch: 'full',
      }, 
      {
        path:'login',
        component: LayoutLoginComponent,
        loadChildren: () => import('./Component/login/login.module').then(x => x.LoginModule),
      },
      {
        path:'main-page',
        loadChildren: () => import('./Component/main-page/main-page.module').then(x => x.MainPageModule),
      },
      {
        path: 'navigation',
        loadChildren:()=> import('./Component/navigation/navigation.module').then(x => x.NavigationModule),
      },
];
