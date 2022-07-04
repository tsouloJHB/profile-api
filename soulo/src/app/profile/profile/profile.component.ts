import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { map } from 'rxjs';
import { CoursesService } from 'src/app/courses.service';
import { profile } from 'src/app/model/profile';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  service;
  theme:string;
  constructor(private http:HttpClient,service: CoursesService, private route: ActivatedRoute,private router: Router) {
    this.getProfiles();

    console.log(service.image);
    for (const key in service.profiler) {
      console.log("Hi "+key);
      if (Object.prototype.hasOwnProperty.call(service.profiler, key)) {
        const element = service.profiler[key];
        console.log(element.mySkills);
        console.log(element.currentOccupation);
      }
      service.getUserData();
    }
   }

  ngOnInit(): void {
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
    console.log(datas[0].image); 
    this.theme = datas[0].theme;
    console.log(this.theme);
    // this.profiler = datas;
    // this.image = this.profiler[0].image;
    // datas.forEach(element => {
    //   console.log(element.mySkills);
    //   this.splitSkills(element.mySkills);
    // });
      
    });
    
  }

}
