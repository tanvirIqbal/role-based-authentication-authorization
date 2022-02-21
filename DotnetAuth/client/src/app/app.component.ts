import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'WebAuth';
  onLogout() {
    localStorage.removeItem("userInfo");
  }

  get isUserLoggedIn() {
    const userInfo = localStorage.getItem("userInfo");
    return userInfo && userInfo.length > 0;
  }
}
