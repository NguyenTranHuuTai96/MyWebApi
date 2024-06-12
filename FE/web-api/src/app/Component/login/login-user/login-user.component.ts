import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import {AccountModel} from '../../../Models/AccountModel';
import { DbcontextService } from '../../../Services/dbcontext.service';
import { Router } from '@angular/router';
import { JwtModel } from '../../../Models/JwtModel';

@Component({
  selector: 'app-login-user',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login-user.component.html',
  styleUrl: './login-user.component.css'
})
export class LoginUserComponent {
  constructor(private dbContext: DbcontextService, private router: Router,){}
  public accountModel: AccountModel = new AccountModel();
  public url: string = "Authentication/login";
  onSubmit(){
    this.dbContext.PostObject(this.url, this.accountModel).then(data => {
      if ( Object.keys(data as JwtModel).length === 0) {
        return { status: false };
      }
        sessionStorage.setItem('access-token',data.AccessToken );
        sessionStorage.setItem('refresh-token',data.RefreshToken );
        return { status: true };
    }).then(rs => {
      if (rs.status) {
       this.router.navigate(['main-page']);
      }
    });
    
  }
}
