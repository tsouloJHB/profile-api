import { PlaythemeComponent } from './../playtheme/playtheme.component';
import { profile } from './../model/profile';
import { AdminModule } from './../admin/admin.module';
import { CourseComponent } from './../course/course.component';
import { EducationComponent } from './../education/education.component';
import { FooterComponent } from './../footer/footer.component';
import { HeaderComponent } from './../header/header.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProfileRoutingModule } from './profile-routing.module';
import { ProfileComponent } from './profile/profile.component';
import { CoursesService } from '../courses.service';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import { EditComponent } from './edit/edit.component';
import { AuthModule } from '../auth/auth.module';
import { NameComponent } from '../name/name.component';


@NgModule({
  declarations: [
    ProfileComponent,
    CourseComponent,
    HeaderComponent,
    EducationComponent,
    FooterComponent,
    EditComponent,
    PlaythemeComponent,
    NameComponent,
  ],
  imports: [
    CommonModule,
    ProfileRoutingModule,
    AdminModule,
    MatAutocompleteModule,
    AuthModule
  ],
  providers: [
    CoursesService
  ],
})
export class ProfileModule { }
