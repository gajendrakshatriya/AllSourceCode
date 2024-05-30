import { Injectable } from '@angular/core';
import {ICardItem} from '../Model/CardItem'

@Injectable({
  providedIn: 'root'
})
export class CustomCardService {

  private Cards:ICardItem[]=[
  {Name: "Dog", ImageUrl:"assets/dog.jpg", Detail:"Dog is a domestic animal."},
  {Name: "Cat", ImageUrl:"assets/images/cat.jpg", Detail:"Cat is a domestic animal."},
  {Name: "Cow", ImageUrl:"assets/images/cow.jpg", Detail:"Cow is a domestic animal."},
  {Name: "Lion", ImageUrl:"assets/images/lion.jpg", Detail:"Lion is a domestic animal."},
  {Name: "Elephant", ImageUrl:"assets/images/elephant.jpg", Detail:"Elephant is a domestic animal."},

];
  constructor() { }

  getAllCards(){
    return this.Cards;
  }


}
