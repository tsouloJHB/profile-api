import { PostService } from './../../service/post.service';
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Post } from 'src/app/model/Post';

@Component({
  selector: 'app-feed',
  templateUrl: './feed.component.html',
  styleUrls: ['./feed.component.css']
})
export class FeedComponent implements OnInit {

  form:FormGroup; 
  allPost:Post[] = [];
  constructor(
    private PostService:PostService,
    private http:HttpClient,
    private formBuilder:FormBuilder,
    ){
    
  }
  

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      Post: ''
   });
   this.getPosts();
  }

  onPostCreate(){
    console.log(this.form.getRawValue());
    this.PostService.createPost(this.form.getRawValue());
  }

  getPosts(){
    this.PostService.getPost().subscribe((data)=>{
      this.allPost = data
      console.log(this.allPost);
    });
    
  }

}
