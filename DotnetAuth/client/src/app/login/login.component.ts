import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Constants } from '../_helpers/constants';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private formBuilder:FormBuilder,
    private userService: UserService,
    private router: Router) { }

  loginForm = this.formBuilder.group({
    email:['',[Validators.email,Validators.required]],
    password:['',Validators.required]
  });

  ngOnInit(): void {
  }

  onSubmit(){
    let email = this.loginForm.controls["email"].value;
    let password = this.loginForm.controls["password"].value;
    this.userService.login(email, password).subscribe({
      next: (data:any) => {
        //console.log(v);
        localStorage.setItem(Constants.USER_KEY,JSON.stringify(data.dataSet));
        this.router.navigate(["/user-management"]);
      },
      error: (e) => {
        console.error(e);
      },
      complete: () => {
        console.info('complete');
      }
    })
    console.log("Login.")
  }
}
