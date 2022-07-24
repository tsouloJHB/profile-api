import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { map } from 'rxjs';
import { CoursesService } from '../courses.service';
import { ThemeEdits } from '../model/themeEdits';
import { themeTemplatesClassic } from '../model/themeTemplatesClassic';
import * as jsonTemplate from '../model/themeTemplate.json';

@Component({
  selector: 'app-education',
  templateUrl: './education.component.html',
  styleUrls: ['./education.component.css']
})
export class EducationComponent implements OnInit {

  service;
  heroElement = document.querySelector('#intro');
  value = "red";
  editTheme:ThemeEdits
  jsonTheme;
  template:themeTemplatesClassic;
  constructor(
    service: CoursesService,
    private http:HttpClient,
    ) {
    this.service = service;
    this.jsonTheme = service
    this.template = service.getThemeTemplate();
    
   }

  ngOnInit(): void {
    //this.getThemes()
   
  
  }

  public  getThemes(){
    var stuffs
    this.http.get<ThemeEdits>(`https://localhost:7096/api/Profile/GetTheme`,{withCredentials:true,
     headers: new HttpHeaders({
       'Authorization': 'bearer '+ localStorage.getItem("token")
     }),
   }
     
     ).pipe(
       map((data)=>{
         var products;
         this.editTheme = data;
         return data
       })
     ).subscribe(data=>{
       this.editTheme = data;
       this.jsonTheme = JSON.parse(this.editTheme.jsonCode);
       let stringObject = JSON.parse(this.editTheme.jsonCode);
       console.log(this.jsonTheme.background)
     });
  
   }

}
