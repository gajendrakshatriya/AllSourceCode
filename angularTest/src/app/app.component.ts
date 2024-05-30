import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterModule, RouterOutlet } from '@angular/router';
import { InsertMessageComponent } from "./Message/insert-message/insert-message.component";
import { DocmanComponent } from './docman/docman.component';
import { CustomcardComponent } from './customcard/customcard.component'
import { CustomCardListComponent } from './custom-card-list/custom-card-list.component'
import {MatButtonModule} from '@angular/material/button';


import { FormsModule } from '@angular/forms';

import { MatRadioModule } from '@angular/material/radio';

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
    imports: [RouterLink, RouterLinkActive, RouterOutlet,RouterModule, InsertMessageComponent
      ,DocmanComponent,CustomcardComponent,CustomCardListComponent
    , MatButtonModule
  , FormsModule,MatRadioModule]
})
export class AppComponent {
  title = 'angularTest';
  MsgToChild='Message To Child';
  public MsgFromChild="";
  
}
