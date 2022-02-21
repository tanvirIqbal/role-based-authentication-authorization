import { Component } from '@angular/core';
import { Constants } from './_helpers/constants';

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
}
