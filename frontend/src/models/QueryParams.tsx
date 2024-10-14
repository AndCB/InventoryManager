export type PaginationAttributes = {
  page: number;
  pageSize: number;
};

export interface QueryParams {
  Filter: string;
  SortBy: string;
  IsDescending: boolean;
  Page: number;
  PageSize: number;
}
