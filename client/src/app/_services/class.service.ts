import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ClassService {
  
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient,private router: Router) { }

  
  classCreate(data){
    console.log(data);
    return this.http.post(this.baseUrl + 'class/create', data).pipe(map(response => {
      return response;
    }))
  }
  joinClass(data){
    return this.http.post(this.baseUrl + 'class/join/', data);
  }
  
  classStudent() {
    return this.http.get(this.baseUrl + 'class/student');
  }
  classTeacher() {
    return this.http.get(this.baseUrl + 'class/teacher');
  }


}
