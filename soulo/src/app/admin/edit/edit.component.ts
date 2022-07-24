import { Emitters } from '../emitters/emitters';
import { CoursesService } from 'src/app/courses.service';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient, HttpErrorResponse, HttpEventType, HttpHeaders } from '@angular/common/http';
import { profile } from 'src/app/model/profile';
import { map } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DataService } from 'src/app/Data/data.service';
import { DomSanitizer } from '@angular/platform-browser';



@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  service;
  public profiler: profile[];
  image:string;
  authenticated = false;
  message = "";
  progress: number;
  uploadMessage: string;
  @Output() public onUploadFinished = new EventEmitter();

  constructor(
     private route: ActivatedRoute,
     private router: Router,
     private http:HttpClient,
     private _snackBar: MatSnackBar,
     private data:DataService,
     private sanitizer:DomSanitizer
     ) {
      //location.reload();
      
   }
   //GET user data
  //  getUserFromData(data:profile){
  //     this.postProfile(data).subscribe(da=>{
  //       this._snackBar.open("Data edited")
  //     },err=>{
  //       this._snackBar.open("Submitted")
  //     });
   //}

   ngOnInit(): void {
  
    this.getProfiles();
    this.authenticated =  ( localStorage.getItem("login") == "true");
    console.log(this.authenticated);
    console.log(localStorage.getItem("token"));
  }

  public postProfile(data:profile){
    //delete this below after the database has data
    data.userId = 0;
    // data.myProjects = "toy";
    // data.myProjectsDescription = "stuff";
    data.image = this.image;
    // data.currentOccupation = "intern";
    console.log(data);
    const urlValue = "https://localhost:7096/api/Profile/UpdateProfile";
    this.http.put(urlValue,data,{withCredentials:true,
      headers: new HttpHeaders({
      'Authorization': 'bearer '+ localStorage.getItem("token")
    }),
  }).subscribe(
      (datas) =>{
        
        console.log(datas); 
       
        this._snackBar.open("Submitted")  
        });
        this.getProfiles();
    
  }
  public getProfiles(){
    console.log(localStorage.getItem("token"))
    const cat = this.http.get<{[key: string]: profile}>(`https://localhost:7096/api/Profile`,{withCredentials:true,
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
  ).subscribe(
  (datas) =>{
    console.log(datas);
    this.profiler = datas;
    this.image = this.profiler[0].image;
    localStorage.setItem("name",this.profiler[0].name);
    localStorage.setItem("theme",this.profiler[0].theme);
   
      
    });
    
  }

  logout(){
    localStorage.setItem("login","false");
    this.http.delete("https://localhost:7096/api/Auth/logout",{withCredentials:true}).subscribe(data=>{
      localStorage.setItem("login","false");
    })
  }

  uploadFile = (files) => {
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    
    this.http.post('https://localhost:7096/api/Profile/imageupload', formData, {reportProgress: true, observe: 'events',withCredentials:true,
        headers: new HttpHeaders({
        'Authorization': 'bearer '+ localStorage.getItem("token")
      }),
  })
      .subscribe({
        next: (event) => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
          this.uploadMessage = 'Upload success.';
          this.onUploadFinished.emit(event.body);
        }
      },
      error: (err: HttpErrorResponse) => console.log(err)
    });
  }

  transform(html) {
    return this.sanitizer.bypassSecurityTrustHtml(html);
  }

}
