import { themeTemplatesClassic } from './model/themeTemplatesClassic';
import { CourseComponent } from './course/course.component';
import { profile } from './model/profile';
import { Products } from './model/products';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, map, retry } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import { ThemeEdits } from './model/themeEdits';
import * as jsonTemplate from './model/themeTemplate.json';
import { themeTemplatesCreative } from './model/themeTemplatesCreative';


@Injectable({
  providedIn: 'root',
})
export class CoursesService implements OnInit {
  public courses = ["english","math","science"];
  public profiles: profile[] = [];
  public profiler: profile[];
  public pro : profile;
  public image : string;
  public profileAccount : string;
  public skillList : string[];
  public  editTheme:ThemeEdits
  public jsonTheme;
  public jsonTemplate
  public ThemeTemplate;
  name = "";

  constructor(private http:HttpClient,private route : ActivatedRoute) { 
  
    this.name = window.location.pathname.split("/")[1];
    //this.name = route.snapshot.params['name'];
  
    
    //define empty theme
   // this.ThemeTemplate = new themeTemplatesCreative();
    this.jsonTemplate = jsonTemplate
    
    this.getThemes1();
    
    this.getProfiles();
    
  
      
     
  }

  ngOnInit(): void {
    //this.getProfiles(); 
  }


    public getProfiles(){
      const cat = this.http.get<{[key: string]: profile}>(`https://localhost:7096/api/Profile/name?name=`+this.name).pipe(
      map((data)=>{
        const products = [];
        for(const key in data){
          if(data.hasOwnProperty(key)){
            console.log(data[key]);
            products.push({...data[key],id:key})
          }
          
        }
        return products;
      })  
    ).subscribe(
    (datas) =>{
      this.profiler = datas;
      datas.forEach(element => {
        console.log(element.mySkills);
        this.splitSkills(element.mySkills);
        console.log("current: "+ this.profiler);
        console.log(this.profiler.forEach(data=>{
          console.log(data);
        }))  
      });
        
      });
      
    }

    private splitSkills(skills:string){
      this.skillList = skills.split(',')
    }


    getBanners(): Observable<any[]> {
      let name : Observable<any[]>;
      return name;
    }

 
    public  getThemes(){
      return this.http.get<ThemeEdits>(`https://localhost:7096/api/Profile/GetTheme`,{withCredentials:true,
      headers: new HttpHeaders({
        'Authorization': 'bearer '+ localStorage.getItem("token")
      }),
    }
      
      );
     
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
      this.image = this.profiler[0].image;
      localStorage.setItem("name",this.profiler[0].name);
      
      // datas.forEach(element => {
      //   console.log(element.mySkills);
      //   this.splitSkills(element.mySkills);
      // });
        
      });
      
    }


   public  getThemes1(){
    if(this.name == "paint"){
      //this.getProfiles();
      this.getLoggedInProfile();
      console.log(localStorage.getItem("name"));
      this.name = localStorage.getItem("name");
    }

    this.http.get<ThemeEdits>(`https://localhost:7096/api/Profile/GetThemeByName?name=`+this.name
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

   public getCreativeTheme(){

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
    console.log(this.ThemeTemplate);
   }

   
  public getThemeTemplate(){
    console.log("right here");
   this.getThemes1();

    console.log(this.profiler);
    return this.ThemeTemplate;    
  }

  
}
