
export class RequestSearchModel <T>{
  public search: T;
  public page: number;
  public pageSize: number;

  constructor(
    page: number,
    pageSize: number,
  ) {
    this.page = page;
    this.pageSize = pageSize;
  }
}
