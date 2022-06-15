import { Component, OnInit } from '@angular/core';
import { CoursesService } from '../courses.service';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent implements OnInit {

  service;
  
  constructor(service: CoursesService) {
    this.service = service;
   }

  ngOnInit(): void {
  }

}
