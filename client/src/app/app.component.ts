import { ClassService } from './_services/class.service';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Model, Class } from './_models/model';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';
import { RoleService } from './_services/role.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  classes: any = [];

  model: Model ={
    email: '',
    password: '',
  };
  newclass: Class ={
    classcode:'',
    classname:'',
  };
  roles = {
    email: '',
    roles:[]
  }; 
  

  users: Partial<User[]>;

  constructor(public accountService: AccountService,public classService: ClassService,
    public roleService: RoleService,
    private route: ActivatedRoute,private router: Router,private toastr: ToastrService) { }


  ngOnInit(): void {
    this.setCurrentUser();
    this.accountService.currentUser$.subscribe( x => {
      if(x.roles.some(x => x === "Admin")){
        this.getUsersWithRoles();
      }
      if(x.roles.some(x => x === "Teacher")){
        this.getTeacherClass();
      }
      if(x.roles.some(x => x === "Student") ){ 
        this.getStudentClass();
      }
    });
  }

  getUsersWithRoles() {
    this.roleService.getUsersWithRoles().subscribe(users => {
      this.users = users;
    })
  }
  updateUserRoles(){
    this.roleService.updateUserRoles(this.roles.email,this.roles.roles).subscribe(res => {
        this.toastr.info("User Role Updated");
    })
  }


  login(){
    this.accountService.login(this.model).subscribe(response => {
      if(response){
        this.toastr.info("Login Success");
       }
    }, error => {
      this.toastr.error("Sign Up First");
    })
  }

  createclass(){
    this.classService.classCreate(this.newclass).subscribe(response => {
        console.log(response);
        this.toastr.info("Class Joined");
       
    }, error => {
      this.toastr.error("Class Join Problem");
    })
  }

  joinclass(){
    this.classService.joinClass(this.newclass).subscribe(response => {
        console.log(response);
        this.toastr.info("Class Joined");
       
    }, error => {
      this.toastr.error("Class Join Problem");
    })
  }

  getTeacherClass(){
    this.classService.classTeacher().subscribe(response => {
        this.classes = response;
        console.log(this.classes);

    }, error => {
      this.toastr.error("Class Join Problem");
    })
  }
  getStudentClass(){
    this.classService.classStudent().subscribe(response => {
        this.classes = response;
        console.log(this.classes);

    }, error => {
      this.toastr.error("Class Join Problem");
    })
  }

  logout(){
    this.accountService.logout();
  }

  setCurrentUser() {
    const user: User = JSON.parse(localStorage.getItem('bluebayit'));
    if(user){
      this.accountService.setUser(user);
    }
  }
}
