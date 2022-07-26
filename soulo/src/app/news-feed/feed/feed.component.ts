import { CommentModel } from './../../model/comment';
import { CommentService } from './../../service/comment.service';
import { profile } from './../../model/profile';
import { PostService } from './../../service/post.service';
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Post } from 'src/app/model/Post';
import * as $ from 'jquery';

@Component({
  selector: 'app-feed',
  templateUrl: './feed.component.html',
  styleUrls: ['./feed.component.css']
})
export class FeedComponent implements OnInit {

  form:FormGroup; 
  allPost:Post[] = [];
  user:profile[] = [];
  comments:CommentModel[] = [];
  constructor(
    private PostService:PostService,
    private http:HttpClient,
    private formBuilder:FormBuilder,
    private CommentService:CommentService,
    ){
    
  }
  

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      Post: ''
   });
   this.getPosts();
   this.getUser();

  }

  onPostCreate(){
    console.log(this.form.getRawValue());
    this.PostService.createPost(this.form.getRawValue());
    window.location.reload();
  }

  getPosts(){
    this.PostService.getPost().subscribe((data)=>{
      this.allPost = data
      console.log(this.allPost);
      
    });
    
  }
  getUser(){
    this.PostService.getUser().subscribe((data)=>{
      this.user = data;;
      console.log(this.user);
    });
  }

  getComments(postId:number){
    this.CommentService.getComments(postId).subscribe((data)=>{
      this.comments = data;
      console.log(this.comments);
    });
  }

}
