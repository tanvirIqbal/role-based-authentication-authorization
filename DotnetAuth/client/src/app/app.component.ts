import { Component } from '@angular/core';
import { Constants } from './_helpers/constants';
import { User } from './_models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'WebAuth';
  onLogout() {
    localStorage.removeItem(Constants.USER_KEY);
  }

  get isUserLoggedIn() {
    const userInfo = localStorage.getItem(Constants.USER_KEY);
    return userInfo && userInfo.length > 0;
  }

  get user(): User {
    const user = localStorage.getItem(Constants.USER_KEY) ? JSON.parse(localStorage.getItem(Constants.USER_KEY) || "") as User
    : new User("","","","");
    return user;
  }

  get isAdmin() {
    return this.user.role == 'Admin';
  }

  get isUser() {
    return this.user.role == 'User';
  }
}
