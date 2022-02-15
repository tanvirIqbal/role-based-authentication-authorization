import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private readonly baseUrl:string = "https://localhost:5001/api/"
  constructor(private httpClient:HttpClient) { }

  login(email:string, password: string){
    const body = {
      Email:email,
      Password:password
    }
    return this.httpClient.post(this.baseUrl+"Login", body);
  }
  register(fullName:string, email:string, password: string){
    const body = {
      FullName: fullName,
      Email:email,
      Password:password
    }
    return this.httpClient.post(this.baseUrl+"RegisterUser", body);
  }
}
