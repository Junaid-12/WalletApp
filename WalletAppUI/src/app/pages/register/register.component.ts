import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { UserSignUp } from 'src/app/Models/UserDto';
import { AuthServiceService } from 'src/app/services/auth.service.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
 

})
export class RegisterComponent implements OnInit {
  usersignup : UserSignUp = new UserSignUp()
  formValue!: FormGroup;
  constructor(private fb : FormBuilder, private authservice : AuthServiceService){}
  ngOnInit(): void {
     this.formValue=this.fb.group({
      id:[0],
      name:['',],
      email:['',],
      password:['',]
     })
  }
  UserSignUp(){
    this.usersignup.Username=this.formValue.value.name;
    this.usersignup.Email=this.formValue.value.email;
    this.usersignup.Password=this.formValue.value.password;
    console.log(this.usersignup); 
    this.authservice.register(this.usersignup).subscribe((res)=>{
      alert("UserRegisterd Succefully");
      console.log();
      this.formValue.reset();
    })

  }
}


