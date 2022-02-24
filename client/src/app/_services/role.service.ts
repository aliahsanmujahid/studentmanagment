import { User } from './../_models/user';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }
  
  getUsersWithRoles() {
    return this.http.get<Partial<User[]>>(this.baseUrl + 'role/users-with-roles');
  }

  updateUserRoles(email: string, roles: string[]) {
    return this.http.post(this.baseUrl + 'role/edit-roles/' + email + '?roles=' + roles, {});
  }
  
}
