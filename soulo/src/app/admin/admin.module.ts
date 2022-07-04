import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { EditComponent } from './edit/edit.component';
import { CoursesService } from '../courses.service';
import { FormsModule } from '@angular/forms';
import {MatSnackBarModule} from '@angular/material/snack-bar';


@NgModule({
  declarations: [
    EditComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    FormsModule,
    MatSnackBarModule,
  ]
  ,
  providers: [
    CoursesService
  ],
})
export class AdminModule { }
