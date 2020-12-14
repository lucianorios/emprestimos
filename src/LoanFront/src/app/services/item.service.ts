import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ItemFilterModel } from "../models/item/item-filter.model";
import { ItemModel } from "../models/item/item.model";
import { ApiModel } from "../models/http/api.model";
import { RequestSearchModel } from "../models/http/request-search.model";
import { ResponseListModel } from "../models/http/response-list.model";
import { BaseService } from "./base.service";

@Injectable({
  providedIn: 'root'
})
export class ItemService extends BaseService {
  protected routService: string = 'item';

  constructor(protected http: HttpClient){
    super();
  }

  getItens(search: RequestSearchModel<ItemFilterModel>){
    return this.http
      .post<ApiModel<ResponseListModel<ItemModel>>>(`${this.routeBase}/${this.routService}/getAll`,
      search);
}

  salvar(item: ItemModel){
    return this.http.post<ApiModel<string>>(`${this.routeBase}/${this.routService}/salvar`, item);
  }

  remover(itemId: number){
    return this.http.delete<ApiModel<string>>(`${this.routeBase}/${this.routService}/${itemId}`);
  }
}
