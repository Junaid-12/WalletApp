export interface RegisterUser{
    name : string;
    email : string;
    password : string;
}

export interface LoginUser{
   
    Username : string;
    Email : string;
}
export interface LoginResponce {
    token:string;
}