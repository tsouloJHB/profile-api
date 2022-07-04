import { profile } from './model/profile';
import { Products } from './model/products';
import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, map, retry } from 'rxjs/operators';

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

  constructor(private http:HttpClient) { 
    this.getProfiles();
    this.profileAccount = window.location.pathname.split("/")[1];
    const cat = this.http.get<{[key: string]: profile}>(`https://localhost:7096/api/Profile`).pipe(
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

      this.profiles = datas;
  
      });
     
  }

  ngOnInit(): void {
    this.getProfiles();
    console.log(this.profiler);
  }


    public getProfiles(){
      const cat = this.http.get<{[key: string]: profile}>(`https://localhost:7096/api/Profile/name?name=Thabang`).pipe(
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
      console.log("Thabang");
      console.log(datas); 

      this.profiler = datas;
      datas.forEach(element => {
        console.log(element.mySkills);
        this.splitSkills(element.mySkills);
      });
        
      });
      
    }

    private splitSkills(skills:string){
      this.skillList = skills.split(',')
      console.log(this.skillList);

    }

    async  getUserData(){
    console.log("func");
     return 1;
      
    }
    getBanners(): Observable<any[]> {
      let name : Observable<any[]>;
      return name;
    }
  
  
}
