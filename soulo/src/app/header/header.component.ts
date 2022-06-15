import { profile } from './../model/profile';
import { Component, OnInit,HostListener } from '@angular/core';
import { CoursesService } from '../courses.service';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  service;
  heroElement = document.querySelector('#intro');
  constructor(service: CoursesService, private route: ActivatedRoute,private router: Router) {
    this.service = service;
    console.log("im here");
    console.log(service.profiler);
    for (const key in service.profiler) {
      console.log("Hi "+key);
      if (Object.prototype.hasOwnProperty.call(service.profiler, key)) {
        const element = service.profiler[key];
        console.log(element.mySkills);
      }
    }
   }
  once:boolean = false;
  name = "intro"
  over = "intro-overlay"
   @HostListener("document:scroll")
   changeImage(){
    //const {top} = this.heroElement.getBoundingClientRect();
     if(document.body.scrollTop > 100 || document.documentElement.scrollTop > 100 && this.once == false){
        this.once =  true;
        this.name = "outro";
        this.over = "intro-overlay2";
     }
     
   }
  ngOnInit(): void {
    console.log(this.router.url);
    console.log(window.location.pathname)
    this.route.url.subscribe((queryParam) =>{
      console.log(queryParam);
    });
    
  }

}
