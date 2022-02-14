import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  constructor(private formBuilder:FormBuilder) { }

  registerForm = this.formBuilder.group({
    fullName:['',Validators.required],
    email:['',[Validators.email,Validators.required]],
    password:['',Validators.required]
  });

  ngOnInit(): void {
  }

  onSubmit(){
    console.log("Register.")
  }

}
