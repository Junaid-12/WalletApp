import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthServiceService } from 'src/app/services/auth.service.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  Name : any;
  Id!: number | null;
  walleteBalnace! : number;
  constructor (private authservice:AuthServiceService, private router : Router){}

 ngOnInit(): void {
     this.Name =this.authservice.getuserName();
   this.Id =this.authservice.getuserId();
   
    this.authservice.showBalance(this.Id!).subscribe({
      next : (res)=>{
       this.walleteBalnace=res;
      }
    })
 }

 logout(){
  this.authservice.logout();
  
  this.router.navigate(['/login'])
 }


}
