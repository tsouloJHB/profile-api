import { ProfileModule } from './profile/profile.module';
import { ProfileAdminModule } from './../profile-admin/profile-admin.module';
import { AdminModule } from './admin/admin.module';
import { CoursesService } from './courses.service';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CourseComponent } from './course/course.component';
import {HttpClientModule} from '@angular/common/http';
import { HeaderComponent } from './header/header.component';
import { EducationComponent } from './education/education.component';
import { FooterComponent } from './footer/footer.component';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { PlaythemeComponent } from './playtheme/playtheme.component';
import { AuthModule } from './auth/auth.module';

@NgModule({
  declarations: [
    AppComponent,
   // PlaythemeComponent,
    
    // CourseComponent,
    // HeaderComponent,
    // EducationComponent,
    // FooterComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    AdminModule,
    MatAutocompleteModule,
    BrowserAnimationsModule,
    FormsModule,
    AuthModule,
  ],
  providers: [
    CoursesService
  ],

  bootstrap: [AppComponent]
})
export class AppModule { }
