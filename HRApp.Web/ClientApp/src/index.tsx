import * as React from "react";
import * as ReactDOM from "react-dom";
import { HashRouter } from "react-router-dom";
import { ThemeProvider } from "@emotion/react";
import { createTheme } from "@mui/material";
import App from "components/App";
import store from "./store";

import { Provider } from "react-redux";

const root = document.getElementById("root");

const theme = createTheme({});

ReactDOM.render(
    <Provider store={store}>
        <ThemeProvider theme={theme}>
            <React.StrictMode>
                <HashRouter>
                    <App />
                </HashRouter>
            </React.StrictMode>
        </ThemeProvider>
    </Provider>,
    root
);
