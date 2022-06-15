import { Component, OnInit } from '@angular/core';
import { CoursesService } from '../courses.service';

@Component({
  selector: 'app-education',
  templateUrl: './education.component.html',
  styleUrls: ['./education.component.css']
})
export class EducationComponent implements OnInit {

  service;
  heroElement = document.querySelector('#intro');
  constructor(service: CoursesService) {
    this.service = service;
   }

  ngOnInit(): void {
  }

}
