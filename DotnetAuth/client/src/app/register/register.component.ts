import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  constructor(private formBuilder:FormBuilder,
    private userService: UserService) { }

  registerForm = this.formBuilder.group({
    fullName:['',Validators.required],
    email:['',[Validators.email,Validators.required]],
    password:['',Validators.required]
  });

  ngOnInit(): void {
  }

  onSubmit(){
    let fullName = this.registerForm.controls["fullName"].value;
    let email = this.registerForm.controls["email"].value;
    let password = this.registerForm.controls["password"].value;
    this.userService.register(fullName, email, password).subscribe({
      next: (v) => {
        console.log(v);
      },
      error: (e) => {
        console.error(e);
      },
      complete: () => {
        console.info('complete');
      } 
  })
    console.log("Register.");
  }

}
