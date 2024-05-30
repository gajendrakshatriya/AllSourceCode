import { Component, Input } from '@angular/core';
import {MatCardModule} from '@angular/material/card';
import { ICardItem } from '../Model/CardItem';
import {MatIconModule} from '@angular/material/icon';

@Component({
  selector: 'app-customcard',
  standalone: true,
  imports: [MatCardModule, MatIconModule],
  templateUrl: './customcard.component.html',
  styleUrl: './customcard.component.css'
})
export class CustomcardComponent {
  /**
   *
   */
  @Input('CardItem')
  public CardDataItem!: ICardItem;
  /**
   *
   */
  constructor() {
   
    
  }
}
