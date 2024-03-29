import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-loading',
  templateUrl: './loading.component.html',
  styleUrls: ['./loading.component.scss']
})
export class LoadingComponent implements OnInit {

  @Input() isLoading: boolean = false;
  @Input() diameter: number = 30;

  constructor() { }

  ngOnInit(): void {
  }

}
