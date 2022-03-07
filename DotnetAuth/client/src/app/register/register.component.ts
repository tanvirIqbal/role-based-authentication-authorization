import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ResponseCode } from '../_models/enums';
import { ResponseModel } from '../_models/response-model';
import { Role } from '../_models/role';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  roles: Role[] = [];
  constructor(private formBuilder: FormBuilder,
    private userService: UserService) { }

  registerForm = this.formBuilder.group({
    fullName: ['', Validators.required],
    email: ['', [Validators.email, Validators.required]],
    password: ['', Validators.required]
  });

  ngOnInit(): void {
    this.getAllRole();
  }

  onSubmit() {
    let fullName = this.registerForm.controls["fullName"].value;
    let email = this.registerForm.controls["email"].value;
    let password = this.registerForm.controls["password"].value;
    let role = this.roles.filter(x => x.isSelected).map(x => x.name);
    this.userService.register(fullName, email, password, role).subscribe({
      next: (v: ResponseModel) => {
        //console.log(v);
        if (v.code == ResponseCode.Ok) {
          this.clearControl();
        }

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

  clearControl() {
    this.registerForm.controls["fullName"].setValue("");
    this.registerForm.controls["email"].setValue("");
    this.registerForm.controls["password"].setValue("");
    this.roles.forEach(x => x.isSelected = false);
  }

  getAllRole() {
    this.userService.getAllRole().subscribe({
      next: (v) => {
        this.roles = v;
      },
      error: (e) => {
        console.error(e);
      },
      complete: () => {
        console.info('complete');
      }
    })
  }

  onRoleChange(role: string) {
    this.roles.forEach(x => {
      if (x.name == role) {
        x.isSelected = !x.isSelected;
      }
    })
  }

  get isRoleSelected() {
    return this.roles.filter(x => x.isSelected == true).length > 0;
  }
}
