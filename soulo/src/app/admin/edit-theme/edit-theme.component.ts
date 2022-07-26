import { themeTemplatesClassic } from './../../model/themeTemplatesClassic';
import {  HttpClient, HttpErrorResponse, HttpEventType, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { map } from 'rxjs';
import { CoursesService } from 'src/app/courses.service';
import { profile } from 'src/app/model/profile';
import { ThemeEdits } from 'src/app/model/themeEdits';
import { themeTemplatesCreative } from 'src/app/model/themeTemplatesCreative';
import { ActivatedRoute, Router } from '@angular/router';
import { KeyValuePipe } from '@angular/common';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-edit-theme',
  templateUrl: './edit-theme.component.html',
  styleUrls: ['./edit-theme.component.css']
})
export class EditThemeComponent implements OnInit {
  name
  location
  template:themeTemplatesClassic;
  service
  theme:string;
  themeData;
  public profiler: profile[];  
  public  editTheme:ThemeEdits
  public jsonTheme;
  public jsonTemplate
  public ThemeTemplate;

  constructor(
    private route: ActivatedRoute,
     private router: Router,
    private sanitizer:DomSanitizer,
  
    private _snackBar: MatSnackBar,
    private http:HttpClient,
  ) { 
  
   
   

    //console.log(this.template.background1);
    this.name = localStorage.getItem("name");
    this.location = "http://localhost:4200/"+this.name
    
  }

  ngOnInit(): void {
    this.getThemes1();
  }
  keepOrder = (a, b) => {
    return a;
  }

  postThemeEdit(themeData:any){
    
    if(localStorage.getItem("theme") == "classic"){
        this.themeData = new themeTemplatesClassic();
    }else if(localStorage.getItem("theme") == "creative"){
      this.themeData = new themeTemplatesCreative();
    }
    this.themeData = themeData;
    var data = JSON.stringify(themeData);
    // post data
    const themeEdits = {
      id : 0,
      userId: 0,
      jsonCode: data,
      theme:localStorage.getItem("theme")
    } as ThemeEdits
    this.postProfile(themeEdits);
     
    
  }


  transform(html) {
    return this.sanitizer.bypassSecurityTrustResourceUrl(html);
  }

  createJson(key,value){
    console.log(key, value)
    return 45 
  }

  public getLoggedInProfile(){
    console.log(localStorage.getItem("token"))
    const cat = this.http.get<{[key: string]: profile}>(`https://localhost:7096/api/Profile`,{withCredentials:true,
    headers: new HttpHeaders({
    'Authorization': 'bearer '+ localStorage.getItem("token")
  }),
}).pipe(
    map((data)=>{
      const products = [];
      for(const key in data){
        if(data.hasOwnProperty(key)){
          products.push({...data[key],id:key})
        }
        
      }
      return products;
    })
  ).subscribe(
  (datas) =>{
    console.log(datas);
    this.profiler = datas;
    //this.image = this.profiler[0].image;
    localStorage.setItem("name",this.profiler[0].name);
    });
    
  }


  public  getThemes1(){
    this.getLoggedInProfile();
    var name  = localStorage.getItem("name");
    this.http.get<ThemeEdits>(`https://localhost:7096/api/Profile/GetThemeByName?name=`+name
     ).pipe(
       map((data)=>{
      
         return data
       })
     ).subscribe(data=>{
      this.editTheme = data;
      this.jsonTheme =  this.editTheme.jsonCode != ""? JSON.parse(this.editTheme.jsonCode):Object;
      
      this.assignTemplate(this.editTheme.theme);
      
     });
    
   }

  

   public assignTemplate(theme){
    
    if(theme == "classic"){
      this.ThemeTemplate = new themeTemplatesClassic();
    }else{
      this.ThemeTemplate = new themeTemplatesCreative();
    }  
    for(let key in this.ThemeTemplate){
      for(let objectKey in Object.keys(this.jsonTheme)){
        if(key === Object.keys(this.jsonTheme)[objectKey] ){
          this.ThemeTemplate[key] = this.jsonTheme[key];
        }   
      }
    }
   }

   //Update themes data
   public postProfile(data:ThemeEdits){
 
    const urlValue = "https://localhost:7096/api/Profile/EditTheme";
    this.http.put(urlValue,data,{withCredentials:true,
      headers: new HttpHeaders({
      'Authorization': 'bearer '+ localStorage.getItem("token")
    }),
  }).subscribe(
      (datas) =>{   
        this._snackBar.open("Submitted")  
        });  
  }

}
