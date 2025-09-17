import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginUser } from '../Models/Auth';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AddBalance } from '../Models/Wallet';
import { Transactions } from '../Models/Transaction';

@Injectable({
  providedIn: 'root'
})
export class AuthServiceService {

  private baseUrl="https://localhost:7233/";
  private userData : any =null;
  constructor( private http : HttpClient, private jwthelper : JwtHelperService) { }

  register(data : any) : Observable<any> {
    return this.http.post<any>(`${this.baseUrl}api/User/RegisterUser`,data);
  }
  login(data : LoginUser) : Observable <any>{
    return this.http.post<any>(`${this.baseUrl}api/User/Login`,data);
  }
  saveToken(token: string) {
    localStorage.setItem('jwtToken', token);
  }
  // Get token
  getToken(): string | null {
    return localStorage.getItem('jwtToken');
  }

  // Check if logged in
  isLoggedIn(): boolean {
    const token = this.getToken();
    return !!token && !this.jwthelper.isTokenExpired(token);
  }

  logout() {
    localStorage.removeItem('jwtToken');
    localStorage.removeItem('UserData');
    localStorage.clear();
  }
  // fetch User Data Afetr Login
  setUser(res: any) {
    if (res) {
      localStorage.setItem('UserData', JSON.stringify(res));
    }
  }
  
  getUser(): any {
    const stored = localStorage.getItem('UserData');
    return stored ? JSON.parse(stored) : null;
  }
  
  getuserId(): number | null {
    const user = this.getUser();
    // 🔎 Debug
    return user && user.id ? user.id : null;
  }
  
  getuserName(): string | null {
    const user = this.getUser();
    return user && user.username ? user.username : null;
  }
  
  //AddMoney In wallet Backend Api
  addbalance(data : AddBalance) : Observable<any>{
    return this.http.post<any>(`${this.baseUrl}api/Wallet/Addbalance`,data);
  }
  
  //Show Balance Of Indviduals 
  showBalance(id:Number) : Observable<any>
  {
     return this.http.get<any>(`${this.baseUrl}api/Wallet/balance/${id}`)
  }
  
  //Send Money From User to Anoher
  sendMoney(data :Transactions) : Observable <any>  {
    return this.http.post<any>(`${this.baseUrl}api/Transecation/SendMoney`,data);
  }
  //Transactions history Indivisuals
  transationHistory(id : number) : Observable <any>{
    return this.http.get<any>(`${this.baseUrl}api/Transecation/Usertransactions/${id}`)
  }
  //Add Profile Details
  ProfileData (data : FormData) : Observable <any>{
    return this.http.post<any>(`${this.baseUrl}api/Profile/ProfileData`,data)
  }
  //Get all Profile Deatils
  getProfile (Id :number) : Observable<any>
  {
    return this.http.get<any> (`${this.baseUrl}api/Profile/GetProfile/${Id}`) 
  } 
}
