import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { Constants } from '../_helpers/constants';
import { ResponseCode } from '../_models/enums';
import { ResponseModel } from '../_models/response-model';
import { Role } from '../_models/role';
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
  register(fullName: string, email: string, password: string, roles: string[]) {
    const body = {
      FullName: fullName,
      Email: email,
      Password: password,
      Roles: roles
    }
    return this.httpClient.post<ResponseModel>(this.baseUrl + "RegisterUser", body);
  }
  getAllUser() {
    let userInfo = JSON.parse(localStorage.getItem(Constants.USER_KEY) || '{}');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${userInfo?.token}`
    });
    return this.httpClient.get<ResponseModel>(this.baseUrl + "GetAllUser", { headers: headers }).pipe(map(res => {
      let userList = new Array<User>();
      if (res.code == ResponseCode.Ok) {
        if (res.dataSet) {
          res.dataSet.map((x: User) => {
            userList.push(new User(x.fullName, x.email, x.userName, x.roles));
          })
        }
      }
      return userList;
    }));
  }

  getUsers() {
    let userInfo = JSON.parse(localStorage.getItem(Constants.USER_KEY) || '{}');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${userInfo?.token}`
    });
    return this.httpClient.get<ResponseModel>(this.baseUrl + "GetUsers", { headers: headers }).pipe(map(res => {
      let userList = new Array<User>();
      if (res.code == ResponseCode.Ok) {
        if (res.dataSet) {
          res.dataSet.map((x: User) => {
            userList.push(new User(x.fullName, x.email, x.userName, x.roles));
          })
        }
      }
      return userList;
    }));
  }

  getAllRole() {
    let userInfo = JSON.parse(localStorage.getItem(Constants.USER_KEY) || '{}');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${userInfo?.token}`
    });
    return this.httpClient.get<ResponseModel>(this.baseUrl + "GetAllRole", { headers: headers }).pipe(map(res => {
      let roleList = new Array<Role>();
      if (res.code == ResponseCode.Ok) {
        if (res.dataSet) {
          res.dataSet.map((x: Role) => {
            roleList.push(new Role(x.name));
          })
        }
      }
      return roleList;
    }));
  }
}
