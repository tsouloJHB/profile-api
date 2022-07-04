import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CoursesService } from 'src/app/courses.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  constructor(service: CoursesService, private route: ActivatedRoute,private router: Router) {
    console.log("the bag");
    console.log(service.image);
    for (const key in service.profiler) {
      console.log("Hi "+key);
      if (Object.prototype.hasOwnProperty.call(service.profiler, key)) {
        const element = service.profiler[key];
        console.log(element.mySkills);
      }
      service.getUserData();
    }
   }

  ngOnInit(): void {
  }

}
