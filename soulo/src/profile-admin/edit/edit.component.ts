import { Component, OnInit,HostListener } from '@angular/core';
import { CoursesService } from '../../app/courses.service';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';


@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {
  service;
  public themes: string[];  
  constructor(service: CoursesService) { 
   // this.themes.push("james");
  }

  ngOnInit(): void {
  }

}
