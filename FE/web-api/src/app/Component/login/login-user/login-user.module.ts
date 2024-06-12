import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoginUserRoutingModule } from './login-user-routing.module';
import { DbcontextService } from '../../../Services/dbcontext.service';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    LoginUserRoutingModule,
  ],
  //providers: [DbcontextService]
})
export class LoginUserModule { }
