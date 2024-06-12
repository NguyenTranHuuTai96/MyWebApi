import { Component } from '@angular/core';
import { DbcontextService } from '../../../Services/dbcontext.service';
import { BaseApiService } from '../../../Services/base-api.service';

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [],
  templateUrl: './product.component.html',
  styleUrl: './product.component.css'
})
export class ProductComponent {
  public url = 'Products/get-list-data';
  public urlsave = 'Products/get-list-data';
constructor(private dbContext: DbcontextService,private baseApiService: BaseApiService ){}
  GetProduct(){
    this.dbContext.GetObject(this.url).then(data => {
      if ( Object.keys(data).length === 0) {
        return { status: false };
      }
        return { status: true };
    });
  }
  SaveListPro(){

  }
}
