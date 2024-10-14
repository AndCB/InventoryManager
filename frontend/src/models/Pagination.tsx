export interface Pagination {
  sortOrder: string;
  sortBy?: string;
  totalPages: number;
  totalCount: number;
  model: {
    currentPage: number;
    pageSize: number;
  };
}
