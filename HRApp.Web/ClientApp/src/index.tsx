import * as React from "react";
import * as ReactDOM from "react-dom";
import { BrowserRouter } from "react-router-dom";
import { ThemeProvider } from "@emotion/react";
import { createTheme } from "@mui/material";
import App from "components/App";
import DateAdapter from "@mui/lab/AdapterLuxon";
import LocalizationProvider from "@mui/lab/LocalizationProvider";
import store from "./store";

import { Provider } from "react-redux";

const root = document.getElementById("root");

const theme = createTheme({});

ReactDOM.render(
    <React.StrictMode>
        <Provider store={store}>
            <ThemeProvider theme={theme}>
                <LocalizationProvider dateAdapter={DateAdapter}>
                    <BrowserRouter>
                        <App />
                    </BrowserRouter>
                </LocalizationProvider>
            </ThemeProvider>
        </Provider>
    </React.StrictMode>,
    root
);
