import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { ResponseCode } from '../_models/enums';
import { ResponseModel } from '../_models/response-model';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private readonly baseUrl: string = "https://localhost:5001/api/user/"
  constructor(private httpClient: HttpClient) { }

  login(email: string, password: string) {
    const body = {
      Email: email,
      Password: password
    }
    return this.httpClient.post<ResponseModel>(this.baseUrl + "Login", body);
  }
  register(fullName: string, email: string, password: string) {
    const body = {
      FullName: fullName,
      Email: email,
      Password: password
    }
    return this.httpClient.post<ResponseModel>(this.baseUrl + "RegisterUser", body);
  }
  getAllUser() {
    let userInfo = JSON.parse(localStorage.getItem("userInfo") || '{}');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${userInfo?.token}`
    });
    return this.httpClient.get<ResponseModel>(this.baseUrl + "GetAllUser", { headers: headers }).pipe(map(res => {
      let userList = new Array<User>();
      if (res.code == ResponseCode.Ok) {
        if (res.dataSet) {
          res.dataSet.map((x: User) => {
            userList.push(new User(x.fullName, x.email, x.userName));
          })
        }
      }
      return userList;
    }));
  }
}
