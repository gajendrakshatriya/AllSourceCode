import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IDocItem } from '../Model/docItem';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  public messages = [
    {"id":1, "text":"msg1"},
    {"id":2, "text":"msg2"},
    {"id":3, "text":"msg3"},
  ];

  constructor(private http : HttpClient) { }

  getDocItems(){
    // var url = 'http://localhost:30351/WeatherForecast/GetProcessedDoc';
    // var blobUrl = 'https://azstoragetest2024.blob.core.windows.net/aidocument/5.pdf';
    // var docItems = this.http.post<IDocItem[]>(url,blobUrl);
     var url = 'https://gkwebapp2024.azurewebsites.net/WeatherForecast/GetProcessedDoc?blobFileUrl=https%3A%2F%2Fazstoragetest2024.blob.core.windows.net%2Faidocument%2F5.pdf';
    var docItems = this.http
                        .post<IDocItem[]>(url,null);
                        //.catch(this.errorHandler);
    return docItems;
  }

  // errorHandler(error:HttpErrorResponse){
  //   return Observable.throw(error.message || "Server Error");
  // }


  getMessages(){
    return this.messages;
  }

  addMessage(text:string){
    var id = this.messages.length;
    this.messages.push({"id":id+1, "text":text});
  }

  removeMessage(id:any){
//     var matchIndex = this.messages.findIndex(item=>item.id=id);
// this.messages.splice(matchIndex,1);
    var filteredItems = this.messages.filter(item => item.id!=id);
    this.messages = filteredItems;
  }
}
