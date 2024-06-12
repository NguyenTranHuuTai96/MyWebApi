import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NavigationComponent } from './navigation.component';

const routes: Routes = [
  {
    path:'',
    redirectTo:'product',
    pathMatch:'full',
  },
  {
    path: 'product',
    loadChildren:()=> import('./product/product.module').then(x => x.ProductModule),
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class NavigationRoutingModule { }
