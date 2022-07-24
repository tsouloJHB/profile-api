import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { map } from 'rxjs';
import { CoursesService } from '../courses.service';
import { profile } from '../model/profile';
import { themeTemplatesCreative } from '../model/themeTemplatesCreative';

@Component({
  selector: 'app-playtheme',
  templateUrl: './playtheme.component.html',
  styleUrls: ['./playtheme.component.css']
})
export class PlaythemeComponent implements OnInit {

  service;
  theme:string;
  template:themeTemplatesCreative;
  constructor(
    private http:HttpClient,
    service: CoursesService,
    private sanitizer:DomSanitizer 
    ) {
    this.service = service;
    
   
    this.template = service.getThemeTemplate();
    console.log(this.template.background1);
   }


   //Side scroll effect
    onWheel(event: WheelEvent): void {
      if (event.deltaY > 0) (<Element>event.target).parentElement!.scrollLeft +=  event.deltaY;
      else (<Element>event.target).parentElement!.scrollLeft -= 20;
    }

  ngOnInit(): void {
  }



  transform(html) {
    return this.sanitizer.bypassSecurityTrustResourceUrl(html);
  }

}
