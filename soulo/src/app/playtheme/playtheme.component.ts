import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { map } from 'rxjs';
import { profile } from '../model/profile';

@Component({
  selector: 'app-playtheme',
  templateUrl: './playtheme.component.html',
  styleUrls: ['./playtheme.component.css']
})
export class PlaythemeComponent implements OnInit {


  theme:string;
  constructor(private http:HttpClient) {
  
    this.getProfiles();
   }

//    onWheel(event: WheelEvent): void {
//     (<Element>event.target).parentElement.scrollLeft += event.deltaY;
//     event.preventDefault();
//  } 
    onWheel(event: WheelEvent): void {
      if (event.deltaY > 0) (<Element>event.target).parentElement!.scrollLeft +=  event.deltaY;
      else (<Element>event.target).parentElement!.scrollLeft -= 20;
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
    // this.profiler = datas;
    // this.image = this.profiler[0].image;
    // datas.forEach(element => {
    //   console.log(element.mySkills);
    //   this.splitSkills(element.mySkills);
    // });
      
    });
    
  }

}
