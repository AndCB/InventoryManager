import { InventoryItem } from "./InventoryItem";

export interface PagedResponse {
  items: InventoryItem[];
  totalCount: number;
  pageSize: number;
  currentPage: number;
  totalPages: number;
}
