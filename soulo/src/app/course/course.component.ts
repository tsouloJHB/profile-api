import { CoursesService } from './../courses.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css']
})
export class CourseComponent implements OnInit {
  title = "The  title is here";
  
  service;
  constructor(service: CoursesService) 
  {
    this.service = service;
   }

  ngOnInit(): void {
  }

}
