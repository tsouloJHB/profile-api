import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EditprofileComponent } from './editprofile/editprofile.component';
import { EditComponent } from './edit/edit.component';
import {MatAutocompleteModule} from '@angular/material/autocomplete';



@NgModule({
  declarations: [
    EditprofileComponent,
    EditComponent,
   
  ],
  imports: [
    CommonModule,
    MatAutocompleteModule,
  ]
})
export class ProfileAdminModule { }
