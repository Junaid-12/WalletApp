import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Route, Router } from '@angular/router';
import { Transactions } from 'src/app/Models/Transaction';
import { AuthServiceService } from 'src/app/services/auth.service.service';

@Component({
  selector: 'app-send-money',
  templateUrl: './send-money.component.html',
  styleUrls: ['./send-money.component.css']
})
export class SendMoneyComponent implements OnInit {
  formvalue ! : FormGroup;
  SendId! : number | null;
   constructor ( private fb  : FormBuilder , private authservice : AuthServiceService, private router  : Router){}
  ngOnInit(): void {
    this.formvalue=this.fb.group({
      Id:[0],
      ReceiverId:[''],
      Amount : ['']
      
    });
    this.SendId=this.authservice.getuserId();

  }
 

  sendmoney(){

    const data : Transactions  = {
      senderid : this.SendId!,
      receiverId:this.formvalue.value.ReceiverId,
      amount : this.formvalue.value.Amount
      
    }
   
    debugger
    if( this.formvalue.valid){
      this.authservice.sendMoney(data).subscribe({
        next : (res)=> {
         console.log(res); 
          alert ('Sucessfully');
          this.formvalue.reset();
          this.router.navigate(['/dashboard'])
        }, error :(err)=>{
          alert("Insufficient balance")
          this.router.navigate(["/dashboard"]);
          console.log(err)
        }
      })
    }
    else{
      console.log('Unsuccessfuly');
    }
  }
   
 

 
 
}
