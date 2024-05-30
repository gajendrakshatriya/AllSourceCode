import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { InsertMessageComponent } from './Message/insert-message/insert-message.component';
import { DocmanComponent } from './docman/docman.component';
import { NoPageFoundComponent } from './no-page-found/no-page-found.component';
import { LoginComponent } from './login/login.component';
import { CustomCardListComponent } from './custom-card-list/custom-card-list.component';
import { NgModule } from '@angular/core';
import { CustomcardComponent } from './customcard/customcard.component';

export const routes: Routes = [
    
    { path: 'login', component: LoginComponent },
    { path: 'insert', component: InsertMessageComponent },
    { path: 'docman', component: DocmanComponent },
    { path: 'cardlist', component: CustomCardListComponent },
    { path: '',   redirectTo: 'login', pathMatch: 'full' }, //patch match for default entry
    { path: '**', component: NoPageFoundComponent }
];

// @NgModule({
//     imports: [RouterModule.forChild(routes)],
//     exports: [RouterModule]
// })
