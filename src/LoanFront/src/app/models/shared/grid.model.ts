export class GridModel<T>{
  columns: string[];
  source: T[];
  pageSize: number;
  pageSizeOptions: number[];
  totalRows: number;

  constructor(){
    this.pageSizeOptions = [10, 20, 50, 100];
    this.pageSize = 10;
  }

  setColumns(columns: string[]): void {
    this.columns = columns;
  }

  setPageSize(size: number): void {
    this.pageSize = size;
  }

  setPageSizeOptions(option: number[]): void {
    this.pageSizeOptions = option;
  }

  setSource(data: T[], total: number){
    this.source = data;
    this.totalRows = total;
  }
}
