import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable()
export class BaseService {
  protected routeBase: string = environment.apiUrl;

  constructor() { }
}
