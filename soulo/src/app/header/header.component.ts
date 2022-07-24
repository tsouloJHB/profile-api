import { profile } from './../model/profile';
import { Component, OnInit,HostListener } from '@angular/core';
import { CoursesService } from '../courses.service';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { DomSanitizer } from '@angular/platform-browser';
import { themeTemplatesClassic } from '../model/themeTemplatesClassic';
import * as jsonTemplate from '../model/themeTemplate.json';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  service;
  template:themeTemplatesClassic;
  heroElement = document.querySelector('#intro');
  constructor(
    service: CoursesService, 
    private route: ActivatedRoute,
    private router: Router,
    private sanitizer:DomSanitizer
    ) {
    this.service = service;
    console.log(service.profiler.forEach(data=>{
      console.log(data);
    }))
    this.template = service.getThemeTemplate();
    console.log(this.template.headerTextColor1);
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
  transform(html) {
    return this.sanitizer.bypassSecurityTrustResourceUrl(html);
  }

}
