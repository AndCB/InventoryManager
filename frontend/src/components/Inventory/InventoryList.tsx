// src/components/InventoryPage.tsx
import React, { useState, useEffect } from "react";
import {
  Button,
  TextField,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  TablePagination,
  IconButton,
  Snackbar,
} from "@mui/material";
import { InventoryItem } from "../../models/InventoryItem";
import {
  addInventoryItem,
  deleteInventoryItem,
  fetchInventoryItems,
  updateInventoryItem,
} from "../../services/apiService";
import { ArrowDownward, ArrowUpward, Delete, Edit } from "@mui/icons-material";
import JumpToFirstOrLast from "../pagination/JumpToFirstOrLastActions";
import { useSnackbar } from "../../Contexts/SnackBarContext";

const InventoryList: React.FC = () => {
  const [items, setItems] = useState<InventoryItem[]>([]);
  const [name, setName] = useState("");
  const [quantity, setQuantity] = useState<number>(0);
  const [price, setPrice] = useState<number>(0);
  const [editId, setEditId] = useState<number | null>(null);

  // Pagination state
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(25);
  const [totalItems, setTotalItems] = useState(0);

  // Sorting state
  const [sortBy, setSortBy] = useState<keyof InventoryItem>("id");
  const [isDescending, setIsDescending] = useState(false);

  // Filtering state
  const [filter, setFilter] = useState("");

  //Styling state
  const [onHover, setOnHover] = useState("");

  //User feedback
  const snackbar = useSnackbar();

  useEffect(() => {
    fetchItems();
  }, [page, rowsPerPage, sortBy, isDescending, filter]);

  const fetchItems = async () => {
    const response = await fetchInventoryItems({
      Filter: filter,
      SortBy: sortBy,
      IsDescending: isDescending,
      Page: page + 1,
      PageSize: rowsPerPage,
    });
    setItems(response.items);
    setTotalItems(response.totalCount);
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    if (editId) {
      const updatedItem = { name, quantity, price };
      await updateInventoryItem(editId, updatedItem);
      snackbar.openSnackbarWithMessage("An Item has been updated.");
    } else {
      await addInventoryItem({ name, quantity, price });
      snackbar.openSnackbarWithMessage("An Item has been created.");
    }
    clearFields();
    fetchItems();
  };

  const handleEdit = (item: InventoryItem) => {
    setName(item.name);
    setQuantity(item.quantity);
    setPrice(item.price);
    setEditId(item.id);
  };

  const handleDelete = async (id: number) => {
    await deleteInventoryItem(id);
    snackbar.openSnackbarWithMessage("An Item has been deleted.");
    fetchItems();
  };

  const clearFields = () => {
    setName("");
    setQuantity(0);
    setPrice(0);
    setEditId(null);
  };

  return (
    <div>
      <form onSubmit={handleSubmit} className="flex gap-2 justify-evenly">
        <TextField
          label="Name"
          value={name}
          onChange={(e) => setName(e.target.value)}
          required
        />
        <TextField
          label="Quantity"
          type="number"
          value={quantity}
          onChange={(e) => setQuantity(Number(e.target.value))}
          required
        />
        <TextField
          label="Price"
          type="number"
          value={price}
          onChange={(e) => setPrice(Number(e.target.value))}
          required
        />
        <Button type="submit" variant="contained" color="primary">
          {editId ? "Update Item" : "Add Item"}
        </Button>
        <Button onClick={clearFields} variant="outlined" color="secondary">
          Clear
        </Button>
      </form>

      {/* Filter input */}
      <TextField
        label="Filter by Name"
        value={filter}
        onChange={(e) => setFilter(e.target.value)}
        style={{ marginTop: "16px", marginBottom: "16px" }}
      />

      <TableContainer component={Paper} className="max-h-[30rem] overflow-y-auto">
        <Table>
          <TableHead>
            <TableRow>
              {["id", "name", "quantity", "price"].map((column) => (
                <TableCell
                  key={column}
                  onClick={() => {
                    setSortBy(column as keyof InventoryItem);
                    setIsDescending(!isDescending);
                  }}
                  onMouseEnter={() => setOnHover(column)}
                  onMouseLeave={() => setOnHover("")}
                  className="sticky top-0 z-10 bg-white"
                >
                  {column.charAt(0).toUpperCase() + column.slice(1)}

                  <IconButton
                    sx={{
                      visibility:
                        onHover === column || sortBy === column
                          ? "visible"
                          : "hidden",
                    }}
                  >
                    {isDescending ? (
                      <ArrowDownward fontSize="small" />
                    ) : (
                      <ArrowUpward fontSize="small" />
                    )}
                  </IconButton>

                  <div
                    className={`absolute right-2 top-1/2 transform -translate-y-1/2 ${
                      sortBy !== column ? "invisible" : "visible"
                    }`}
                  >
                    {isDescending ? (
                      <ArrowDownward
                        fontSize="small"
                        className="invisible group-hover:visible"
                      />
                    ) : (
                      <ArrowUpward
                        fontSize="small"
                        className="invisible group-hover:visible"
                      />
                    )}
                  </div>
                </TableCell>
              ))}
              <TableCell className="sticky top-0 z-10 bg-white">Actions</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {items.map((item) => (
              <TableRow key={item.id}>
                <TableCell>{item.id}</TableCell>
                <TableCell>{item.name}</TableCell>
                <TableCell>{item.quantity}</TableCell>
                <TableCell>{item.price.toFixed(2)}</TableCell>
                <TableCell>
                  <IconButton onClick={() => handleEdit(item)} color="primary">
                    <Edit fontSize="small" />
                  </IconButton>
                  <Button
                    onClick={() => handleDelete(item.id)}
                    color="secondary"
                  >
                    <Delete fontSize="small" />
                  </Button>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>

      {/* Pagination */}
      <TablePagination
        rowsPerPageOptions={[10, 25, 50]}
        component="div"
        count={totalItems}
        rowsPerPage={rowsPerPage}
        page={page}
        onPageChange={(_event, newPage) => setPage(newPage)}
        onRowsPerPageChange={(event) => {
          setRowsPerPage(parseInt(event.target.value, 10));
          setPage(0); // Reset to first page when changing rows per page
        }}
        ActionsComponent={JumpToFirstOrLast}
      />
      <Snackbar
        open={snackbar.openSnackbar}
        autoHideDuration={3000}
        onClose={snackbar.closeSnackbar}
        anchorOrigin={{ vertical: "bottom", horizontal: "left" }}
        message={snackbar.snackbarMessage}
      />
    </div>
  );
};

export default InventoryList;
