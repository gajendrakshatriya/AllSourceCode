import { Component } from '@angular/core';
import { IDocItem } from '../Model/docItem';
import { MessageService } from '../Service/message.service';
import { NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { routes } from '../app.routes';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-docman',
  standalone: true,
  imports: [FormsModule,NgFor,RouterModule],
  templateUrl: './docman.component.html',
  styleUrl: './docman.component.css'
})
export class DocmanComponent {
  docItems: IDocItem[] = [];

  constructor(private _messageService: MessageService) { }

  ngOnInit() {
    if (this.docItems.length == 0)
      this._messageService.getDocItems().subscribe(data => this.docItems = data);
  }

  getDocmanData(){
    this._messageService.getDocItems().subscribe(data => this.docItems = data);
    console.warn("docItems length: " + this.docItems.length);
  }

}
