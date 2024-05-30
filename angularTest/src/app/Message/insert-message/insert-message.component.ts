import { Component, Directive, EventEmitter, Input, Output } from '@angular/core';
import {FormsModule} from '@angular/forms';
import { MessageService } from '../../Service/message.service';
import { NgFor } from '@angular/common';
import { IDocItem } from '../../Model/docItem';

@Component({
  selector: 'app-insert-message',
  standalone: true,
  imports: [FormsModule,NgFor],
  templateUrl: './insert-message.component.html',
  styleUrl: './insert-message.component.css'
})

export class InsertMessageComponent {
  @Input('MsgFromParent') public ParentMsgReceived:any;
  // @Input() public MsgFromParent:any;
  @Output() public childEvent = new EventEmitter();
  message="enter message here.";
  textclass="text";
  public msgArray:any[]=[];
  //docItems:IDocItem[]=[];

constructor(private _messageService:MessageService) {}

ngOnInit(){
  this.msgArray = this._messageService.getMessages();
  // if(this.docItems.length==0)
  //   this._messageService.getDocItems().subscribe(data => this.docItems = data);
}

  getMessage(val:any)
  {
    this.message=val;
    alert("function called: "+this.message);
  }
  getMessageTwoway(){
    this.textclass="textEdit";
    alert("2 function called: "+this.message);
  }
  updateMessage(){
    this.message = "updated.";
  }

  childEventEmitter(){
this.childEvent.emit(this.message);
  }

  getMessageList(){
    return this.msgArray;
  }

}
