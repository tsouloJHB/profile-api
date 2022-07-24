import { Post } from './../model/Post';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { MatSnackBar } from '@angular/material/snack-bar';
import { map } from 'rxjs';

@Injectable({providedIn:"root"})
export class PostService{
    constructor(
        private http: HttpClient,
        private _snackBar: MatSnackBar,
        ){

    }
    //create Post
    createPost(data:any){
        const urlValue = "https://localhost:7096/api/Post";
        this.http.post(urlValue,data,{withCredentials:true,
          headers: new HttpHeaders({
          'Authorization': 'bearer '+ localStorage.getItem("token")
        }),
      }).subscribe(
          (datas) =>{
            this._snackBar.open("Submitted")  
            });
    }
    
    
    //get the post
    getPost(){
        return this.http.get<{[key: string]: Post}>(`https://localhost:7096/api/Post`,{withCredentials:true,
        headers: new HttpHeaders({
        'Authorization': 'bearer '+ localStorage.getItem("token")
      }),
    }).pipe(
        map((data)=>{
          const products = [];
          for(const key in data){
            if(data.hasOwnProperty(key)){
              products.push({...data[key],id:key})
            }
            
          }
          return products;
        })
      );
    }
}