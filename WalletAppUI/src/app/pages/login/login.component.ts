import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {  Router } from '@angular/router';
import { LoginUser } from 'src/app/Models/Auth';
import { Userlogin } from 'src/app/Models/UserDto';
import { AuthServiceService } from 'src/app/services/auth.service.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']  
})
export class  LoginComponent implements OnInit  {
  
  formvalue !:FormGroup;
  userlogin : Userlogin = new Userlogin();
   constructor( private fb : FormBuilder , private Authservice : AuthServiceService , private router : Router)  {}
    
   ngOnInit(): void {
    this.formvalue=this.fb.group
    ({
     username:['',Validators.required],
       email:['',Validators.required]
    });
 
  }

  Login(){
   
    debugger
     if(this.formvalue.valid){
       const credentials: LoginUser = this.formvalue.value as LoginUser;
       this.Authservice.login(credentials).subscribe({
           next :(res)=>{
          
            localStorage.setItem('jwtToken',res.token)
            this.Authservice.setUser(res);
            this.router.navigate(['/dashboard'])
            alert('Login Sucessful');
           console.log(res);
             
           },
           error :(err)=>{
            console.log(err);
            alert('InValid Credentials');
           }
       });

     }
  
      
  }
   
   

}
  



