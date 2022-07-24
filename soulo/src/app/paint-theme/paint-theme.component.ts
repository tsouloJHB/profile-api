
import { DomSanitizer } from '@angular/platform-browser';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { CoursesService } from '../courses.service';
import { themeTemplatesClassic } from '../model/themeTemplatesClassic';
import { ThemeEdits } from '../model/themeEdits';
import { profile } from '../model/profile';
import { map } from 'rxjs';
import { themeTemplatesCreative } from '../model/themeTemplatesCreative';

@Component({
  selector: 'app-paint-theme',
  templateUrl: './paint-theme.component.html',
  styleUrls: ['./paint-theme.component.css']
})
export class PaintThemeComponent implements OnInit {

  service;
  theme:string;
  template:themeTemplatesClassic;
  
  public  editTheme:ThemeEdits
  public jsonTheme;
  public jsonTemplate
  public profiler: profile[];
  public ThemeTemplate;
  user;
  color:string;
  constructor(
    private http:HttpClient,
    service: CoursesService,
    private sanitizer:DomSanitizer
  ) { 
    this.service = service;
    if(localStorage.getItem("theme") == "creative"){
      this.ThemeTemplate = new themeTemplatesCreative();
    }else{
      this.ThemeTemplate = new themeTemplatesClassic();
    }
    
    //this.template = service.getThemeTemplate();
    //console.log(this.template);
  
  }

  ngOnInit(): void {
    //this.test();
   
    this.getThemes1();
    //this.service = service;
    
   // this.template = this.service.getThemeTemplate();
    //console.log(this.template.background);
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
    
    // datas.forEach(element => {
    //   console.log(element.mySkills);
    //   this.splitSkills(element.mySkills);
    // });
      
    });
    
  }


  public  getThemes1(){
    this.getLoggedInProfile();
    console.log(localStorage.getItem("name"));
    var name  = localStorage.getItem("name");
    this.http.get<ThemeEdits>(`https://localhost:7096/api/Profile/GetThemeByName?name=`+name
     ).pipe(
       map((data)=>{
         return data
       })
     ).subscribe(data=>{
      this.editTheme = data;
      this.jsonTheme =  this.editTheme.jsonCode != ""? JSON.parse(this.editTheme.jsonCode):Object;
      console.log(this.editTheme.theme)
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
    console.log(this.ThemeTemplate.background);
   
    this.color = this.ThemeTemplate.background;
   }

}
