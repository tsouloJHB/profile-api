import { CoursesService } from 'src/app/courses.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { map } from 'rxjs';
import { profile } from 'src/app/model/profile';
import { ThemeEdits } from 'src/app/model/themeEdits';
import { Observable, throwError } from 'rxjs';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  
  theme:string;
  name = "";
  editTheme:ThemeEdits
  constructor(
    private http:HttpClient,
    private service: CoursesService, 
    private route: ActivatedRoute,
    private router: Router,
    
    ) {
     
  
    this.name = window.location.pathname.split("/")[1];
    this.getThemes();
  
    this.getProfiles();
     
  
   
    for (const key in service.profiler) {
      console.log("Hi "+key);
      if (Object.prototype.hasOwnProperty.call(service.profiler, key)) {
        const element = service.profiler[key];
        console.log(element.mySkills);
        console.log(element.currentOccupation);
      }
  
    }
     
   }

  ngOnInit(): void {

    // this.service.getThemes().subscribe((data:ThemeEdits)=> this.stuff ={
    //   id : data.id,
    //   userId : data.userId,
    //   jsonCode : data.jsonCode,
    //   theme : data.theme
    // });  
    
 
   
  }
  ngAfterViewInit() {
    
  
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
      console.log(this.editTheme.jsonCode)
    });
 
  }

}
