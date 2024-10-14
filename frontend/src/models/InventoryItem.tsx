export interface InventoryItem {
  id: number;
  name: string;
  quantity: number;
  price: number;
}

export interface InventoryItemCreateDTO {
  name: string;
  quantity: number;
  price: number;
}
