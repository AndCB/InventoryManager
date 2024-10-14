import { AppBar, Box, Container, Typography } from "@mui/material";
import InventoryList from "./Inventory/InventoryList";

export default function Home() {
  return (
    <Container maxWidth="xl" disableGutters className="flex flex-col min-h-screen">
      <AppBar position="static">
        <Typography variant="h6" component="div" sx={{ flexGrow: 1, padding: '0.5em 2em 0.5em 2em' }}>
          Inventory Manager
        </Typography>
      </AppBar>
      <Box className="flex-grow flex items-center justify-center">
        <InventoryList />
      </Box>
    </Container>
  );
}
