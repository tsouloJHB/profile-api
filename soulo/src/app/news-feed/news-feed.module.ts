import { PostService } from './../service/post.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NewsFeedRoutingModule } from './news-feed-routing.module';
import { FeedComponent } from './feed/feed.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { CoursesService } from '../courses.service';


@NgModule({
  declarations: [
    FeedComponent
  ],
  imports: [
    CommonModule,
    NewsFeedRoutingModule,
    FormsModule,
    MatSnackBarModule,
    ReactiveFormsModule,
  ],
  providers: [
    CoursesService,
    PostService
  ]
})
export class NewsFeedModule { 
}
