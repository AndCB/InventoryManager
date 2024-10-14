import React, { createContext, useContext, useState, ReactNode } from "react";

type SnackbarContextType = {
  openSnackbar: boolean;
  snackbarMessage: string;
  openSnackbarWithMessage: (message: string) => void;
  closeSnackbar: () => void;
};

const SnackbarContext = createContext<SnackbarContextType | undefined>(
  undefined
);

export const SnackbarProvider: React.FC<{ children: ReactNode }> = ({
  children,
}) => {
  const [openSnackbar, setOpenSnackbar] = useState(false);
  const [snackbarMessage, setSnackbarMessage] = useState("");

  const openSnackbarWithMessage = (message: string) => {
    console.log(message);
    setSnackbarMessage(message);
    setOpenSnackbar(true);
  };

  const closeSnackbar = () => {
    setOpenSnackbar(false);
  };

  const contextValue: SnackbarContextType = {
    openSnackbar,
    snackbarMessage,
    openSnackbarWithMessage,
    closeSnackbar,
  };

  return (
    <SnackbarContext.Provider value={contextValue}>
      {children}
    </SnackbarContext.Provider>
  );
};

export const useSnackbar = (): SnackbarContextType => {
  const context = useContext(SnackbarContext);

  if (!context) {
    throw new Error("useSnackbar must be used within a SnackbarProvider");
  }

  return context;
};
