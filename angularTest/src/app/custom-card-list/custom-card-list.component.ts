import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomCardService} from '../Service/custom-card.service'
import { CustomcardComponent } from '../customcard/customcard.component';
import { ICardItem } from '../Model/CardItem';
import {MatGridListModule} from '@angular/material/grid-list'
// import {MatGridTile} from '@angular/material/grid-tile'

@Component({
  selector: 'app-custom-card-list',
  standalone: true,
  imports: [CustomcardComponent,CommonModule,MatGridListModule],
  templateUrl: './custom-card-list.component.html',
  styleUrl: './custom-card-list.component.css'
})
export class CustomCardListComponent {

  /**
   *
   */
  public Cards:ICardItem[]=[];
  constructor(private _cardService: CustomCardService) { }

  ngOnInit(){
  this.Cards = this._cardService.getAllCards();
  }
}
