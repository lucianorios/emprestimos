import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { EmprestimoFilterModel } from "../models/emprestimo/emprestimo-filter.model";
import { EmprestimoModel } from "../models/emprestimo/emprestimo.model";
import { ApiModel } from "../models/http/api.model";
import { RequestSearchModel } from "../models/http/request-search.model";
import { ResponseListModel } from "../models/http/response-list.model";
import { BaseService } from "./base.service";

@Injectable({
  providedIn: 'root'
})
export class EmprestimoService extends BaseService {
  protected routService: string = 'emprestimo';

  constructor(protected http: HttpClient){
    super();
  }

  getEmprestimos(search: RequestSearchModel<EmprestimoFilterModel>){
      return this.http
        .post<ApiModel<ResponseListModel<EmprestimoModel>>>(`${this.routeBase}/${this.routService}/getAll`, search);
  }

  salvar(emprestimo: EmprestimoModel){
    return this.http.post<ApiModel<string>>(`${this.routeBase}/${this.routService}/salvar`, emprestimo);
  }

  finalizar(id: number){
    return this.http.put<ApiModel<string>>(`${this.routeBase}/${this.routService}/${id}`, null);
  }
}
