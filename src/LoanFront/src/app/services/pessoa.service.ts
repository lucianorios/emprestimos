import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { PessoaFilterModel } from "../models/pessoa/pessoa-filter.model";
import { PessoaModel } from "../models/pessoa/pessoa.model";
import { ApiModel } from "../models/http/api.model";
import { RequestSearchModel } from "../models/http/request-search.model";
import { ResponseListModel } from "../models/http/response-list.model";
import { BaseService } from "./base.service";

@Injectable({
  providedIn: 'root'
})
export class PessoaService extends BaseService {
  protected routService: string = 'pessoa';

  constructor(protected http: HttpClient){
    super();
  }

  getPessoas(search: RequestSearchModel<PessoaFilterModel>){
      return this.http
        .post<ApiModel<ResponseListModel<PessoaModel>>>(`${this.routeBase}/${this.routService}/getAll`,
        search);
  }

  salvar(pessoa: PessoaModel){
    return this.http.post<ApiModel<string>>(`${this.routeBase}/${this.routService}/salvar`, pessoa);
  }

  remover(pessoaId: number){
    return this.http.delete<ApiModel<string>>(`${this.routeBase}/${this.routService}/${pessoaId}`);
  }
}
