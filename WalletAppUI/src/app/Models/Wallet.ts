export interface AddBalance{
    UserId : number;
    Balance : number;   
}
export interface SendRequest {
    toEmail : string ;
    amount : string;
    note?: number;
}
