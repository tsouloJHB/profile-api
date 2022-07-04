import { CoursesService } from 'src/app/courses.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { profile } from 'src/app/model/profile';
import { map } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  service;
  public profiler: profile[];
  image:string;
  constructor(service: CoursesService, private route: ActivatedRoute,private router: Router,private http:HttpClient,private _snackBar: MatSnackBar) {
    this.getProfiles();
    console.log("the bag");
    console.log(service.image);
    // for (const key in service.profiler) {
    //   console.log("Hi "+key);
    //   if (Object.prototype.hasOwnProperty.call(service.profiler, key)) {
    //     const element = service.profiler[key];
    //     console.log(element.mySkills);
    //   }
     // service.getUserData();
    
   }

   getUserFromData(data:profile){
      this.postProfile(data).subscribe(da=>{
        this._snackBar.open("Data edited")
      },err=>{
        this._snackBar.open("Submitted")
      });
   }

   ngOnInit(): void {
  }

  public postProfile(data:profile){
 
    data.userId = 1;
    data.myProjects = "toy";
    data.myProjectsDescription = "stuff";
    data.image = this.image;
    data.currentOccupation = "intern";
    console.log(data.theme);
    const urlValue = "https://localhost:7096/api/Profile/1";
    this.http.put(urlValue,data).subscribe(
      (datas) =>{
        
        console.log(datas); 
 
          
        });
    return this.http.put(urlValue,data);
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
    this.image = this.profiler[0].image;
    // datas.forEach(element => {
    //   console.log(element.mySkills);
    //   this.splitSkills(element.mySkills);
    // });
      
    });
    
  }

}
