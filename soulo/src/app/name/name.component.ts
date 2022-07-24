import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-name',
  templateUrl: './name.component.html',
  styleUrls: ['./name.component.css']
})
export class NameComponent implements OnInit {

  name ="";
  constructor(private route:ActivatedRoute) { 
    
  }

  ngOnInit(): void {
    this.name = this.route.snapshot.params['name'];
  }

}
