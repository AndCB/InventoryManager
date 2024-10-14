import "./App.css";
import { SnackbarProvider } from "./Contexts/SnackBarContext";
import Home from "./components/home";

function App() {
  return (
    <SnackbarProvider>
      <Home />
    </SnackbarProvider>
  );
}

export default App;
