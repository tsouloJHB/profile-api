
import {  Emitters } from '../../admin/emitters/emitters';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { EventEmitter ,Output} from "@angular/core";
import { DataService } from 'src/app/Data/data.service';
import { map } from 'rxjs';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  form:FormGroup; 
  even = false;
  message = "";
  @Output() messageEvent = new EventEmitter<string>();
  constructor(
    private http:HttpClient,
    private formBuilder:FormBuilder,
    private router:Router,
     private data:DataService
    ) { 
    
  }



  ngOnInit(): void {
 
   this.form = this.formBuilder.group({
      email: '',
      password: ''
   });
   console.log(this.message);
  }

  submit():void{
    console.log(this.form.getRawValue());
    var login = this.http.post('https://localhost:7096/api/Auth/login',this.form.getRawValue(),{withCredentials: true})
    .pipe(
      map((data) =>{
        console.log(data);
        localStorage.setItem("login","true");
        for(const key in data ){ localStorage.setItem("token",data[key])}
      })
      
    ).subscribe();
      
    //this.router.navigate(['/admin']) 
    setTimeout(() => 
      {
        this.router.navigate(['/admin']) 
      },
      3000);
    
  }


  // public postLogin(){
  //   //delete this below after the database has data
   
  //   const urlValue = "https://localhost:7096/api/auth/login/";
  //   this.http.put(urlValue,data).subscribe(
  //     (datas) =>{
        
  //       console.log(datas); 
 
          
  //       });
  //   return this.http.put(urlValue,data);
  // }

}
