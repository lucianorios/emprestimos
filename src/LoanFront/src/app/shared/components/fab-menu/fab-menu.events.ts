import { Injectable } from "@angular/core";
import { Subject } from "rxjs/internal/Subject";

@Injectable()
export class FabMenuComponentEvents {
  public event$ = new Subject<any>();

  public atualizar(index: number) {
    this.event$.next(index);
  }
}
