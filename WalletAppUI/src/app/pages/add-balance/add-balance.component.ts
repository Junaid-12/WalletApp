import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AddBalance } from 'src/app/Models/Wallet';
import { AuthServiceService } from 'src/app/services/auth.service.service';

@Component({
  selector: 'app-add-balance',
  templateUrl: './add-balance.component.html',
  styleUrls: ['./add-balance.component.css']
})
export class  AddBalanceComponent implements OnInit {
  formvalue! : FormGroup;
  UserId !: number | null;
 constructor ( private fb : FormBuilder, private authsevices : AuthServiceService , private router : Router){}
  ngOnInit(): void {
     this.formvalue=this.fb.group({
      Id :[0],
      Balance : ['',Validators.required]
     });

     this.UserId=this.authsevices.getuserId();

  }
 

  
  AddMoney() {
    const data : AddBalance ={
         UserId:this.UserId!,
         Balance:this.formvalue.value.Balance,
    };
    if( this.formvalue.valid){
  
      this.authsevices.addbalance(data).subscribe({
        next : (res)=>{
          alert ('Add Balance Sucessfully');
          this.router.navigate(['/dashboard'])

        }
      })
    }
   
  }
}
