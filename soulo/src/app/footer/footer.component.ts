import { Component, OnInit } from '@angular/core';
import { CoursesService } from '../courses.service';
import { themeTemplatesClassic } from '../model/themeTemplatesClassic';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent implements OnInit {

  service;
  template:themeTemplatesClassic;
  constructor(service: CoursesService) {
    this.service = service;
    this.template = service.getThemeTemplate();
   }

  ngOnInit(): void {
  }

}
