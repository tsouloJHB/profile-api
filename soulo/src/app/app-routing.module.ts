import { ProfileComponent } from './profile/profile/profile.component';
import { AdminModule } from './admin/admin.module';
import { EditComponent } from './admin/edit/edit.component';
import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EducationComponent } from './education/education.component';
import { LoginComponent } from './auth/login/login.component';


const routes: Routes = [
  
  //
  {
    path: '',
    component: ProfileComponent,
    children: [
      {
        path: '',
        redirectTo: '/profile',
        pathMatch: 'full'
      },
      {
        path: 'profile',
        loadChildren: () => import('./profile/profile.module').then(e => e.ProfileModule)
      }
    ]
   
  },
  //
  {
    path: '',
    component: LoginComponent,
    children: [
      {
        path: '',
        redirectTo: '/login',
        pathMatch: 'full'
      },
      {
        path: 'login',
        loadChildren: () => import('./auth/auth.module').then(e => e.AuthModule)
      }
    ]
   
  },
  //
  {
    path: '',
    component: EditComponent,
    children: [
      {
        path: '',
        redirectTo: '/admin',
        pathMatch: 'full'
      },
      {
        path: 'admin',
        loadChildren: () => import('./admin/admin.module').then(e => e.AdminModule)
      }
    ]
   
  },
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
