import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthServiceService } from 'src/app/services/auth.service.service';



@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class  ProfileComponent implements OnInit  {
   
  isEditing = false;
  formvalue !: FormGroup;
  selectedfile : File | null  = null;
  UserId! : any;
  Profiledata : any;
  constructor (private fb : FormBuilder, private service: AuthServiceService, private route: Router){}
  ngOnInit(): void {
       this.formvalue=this.fb.group({
         Id:[0],
         name:[''],
         PhoneNo :[''],
         address:[''],
          
       });
       
    this.UserId=this.service.getuserId();
    this.loadUserProfile();
  }
  
  loadUserProfile(){
    this.service.getProfile(this.UserId).subscribe({
      next:(res)=>{
       this.Profiledata=res.result;
        
      }
     })
  
  }
  SelectImage(event : any){
    if(event.target.files && event.target.files.length > 0){
      this.selectedfile=event.target.files[0] }
  }

  UploadData(){
   const  formdata : any = new FormData( );
   formdata.append("UserId",this.UserId);
     formdata.append("Name",this.formvalue.value.name);
     formdata.append("PhoneNo",this.formvalue.value.PhoneNo);
     formdata.append("Address",this.formvalue.value.address);
     formdata.append("ImageFile",this.selectedfile);
   
    this.service.ProfileData(formdata).subscribe({
       next :(res)=>{
      console.log(res)
       }, error:(err)=>{
        console.log(err);
      }

    });
   

  }
  OnEdit(user : any){
    this.formvalue.patchValue({
     name:user.name,
     PhoneNo :user.phoneNo, 
     address : user.address,
     image : this.selectedfile

    });

    
  }
  UpdateProfile(){
   const formdata = new FormData()
   formdata.append("UserId",this.UserId);
   formdata.append("Name",this.formvalue.value.name);
   formdata.append("PhoneNo",this.formvalue.value.phoneNo);
   formdata.append("Address",this.formvalue.value.address);
   if(this.selectedfile){
    formdata.append("ImageFile",this.selectedfile);
   } else formdata.append("ImageFile",this.formvalue.value.image);
   this.service.ProfileData(formdata).subscribe({
    next:(res)=>{
      
      this.route.navigate(['/profile']);
      this.loadUserProfile();
      this.isEditing=false;
      
    
    }
   })

  }
  onCancel(){
    this.isEditing=false;
    this.formvalue.reset();
  }
  
}
