import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-all-user-managment',
  templateUrl: './all-user-managment.component.html',
  styleUrls: ['./all-user-managment.component.scss']
})
export class AllUserManagmentComponent implements OnInit {

  userList: User[] = [];
  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.getUsers();
  }
  getUsers() {
    this.userService.getAllUser().subscribe({
      next: (data: User[]) => {
        //console.log(v);
        this.userList = data;
      },
      error: (e) => {
        console.error(e);
      },
      complete: () => {
        //console.info('complete');
      }
    });
  }

}
