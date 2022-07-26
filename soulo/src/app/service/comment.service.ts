import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { MatSnackBar } from '@angular/material/snack-bar';
import { map } from 'rxjs';

@Injectable({providedIn:"root"})
export class CommentService{

    constructor(
        private http:HttpClient,
        private _snackBar: MatSnackBar,
    ){

    }

      //get comments
      getComments( postId:number){
        return this.http.get<{[key: string]: Comment}>(`https://localhost:7096/api/Comment?postId=`+postId).pipe(
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

    //post comment
    createComment(data:any){
        const urlValue = "https://localhost:7096/api/Comment";
        this.http.post(urlValue,data,{withCredentials:true,
          headers: new HttpHeaders({
          'Authorization': 'bearer '+ localStorage.getItem("token")
        }),
      }).subscribe(
          (datas) =>{
            this._snackBar.open("Submitted")  
            });
    }
}