
import { NameComponent } from './name/name.component';
import { ProfileComponent } from './profile/profile/profile.component';
import { AdminModule } from './admin/admin.module';
import { EditComponent } from './admin/edit/edit.component';
import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EducationComponent } from './education/education.component';
import { LoginComponent } from './auth/login/login.component';
import { EditThemeComponent } from './admin/edit-theme/edit-theme.component';
import { PaintThemeComponent } from './paint-theme/paint-theme.component';
import { FeedComponent } from './news-feed/feed/feed.component';



const routes: Routes = [
  {
    path: '',
    component: FeedComponent,
    children: [
      {
        path: '',
        redirectTo: '/feed',
        pathMatch: 'full'
      },
      {
        path: 'feed',
        loadChildren: () => import('./news-feed/news-feed.module').then(e => e.NewsFeedModule)
      }
    ]
   
  },

   //
  
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
        path: 'profiles',
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
    component: FeedComponent,
    children: [
      {
        path: '',
        redirectTo: '/feed',
        pathMatch: 'full'
      },
      {
        path: 'feed',
        loadChildren: () => import('./news-feed/news-feed.module').then(e => e.NewsFeedModule)
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
  //
  // {
  //   path: '',
  //   component: EditComponent,
  //   children: [
  //     {
  //       path: '',
  //       redirectTo: '/EditThem',
  //       pathMatch: 'full'
  //     },
  //     {
  //       path: 'theme',
  //       loadChildren: () => import('./admin/edit-theme/edit-theme.component').then(e => e.EditThemeComponent)
  //     }
  //   ]
   
  // },
  {path:'theme',component: EditThemeComponent},
  //
  {path:'paint',component: PaintThemeComponent},
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
        path: ':name',
        loadChildren: () => import('./profile/profile.module').then(e => e.ProfileModule)
      }
    ]
   
  },

  //
  {
    path: '',
    component: FeedComponent,
    children: [
      {
        path: '',
        redirectTo: '/feed',
        pathMatch: 'full'
      },
      {
        path: 'feed',
        loadChildren: () => import('./news-feed/news-feed.module').then(e => e.NewsFeedModule)
      }
    ]
   
  },

   //
   
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
