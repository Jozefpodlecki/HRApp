import { configureStore } from "@reduxjs/toolkit";
import applications from "./slices/applications";
import auth from "./slices/auth";
import thunkMiddleware from "redux-thunk";

const preloadedState = {
    auth: {
        isAuthenticating: true,
        isAuthenticated: false,
    },
};

const store = configureStore({
    reducer: {
        auth,
        applications,
    },
    preloadedState,
    middleware: (getDefaultMiddleware) =>
        getDefaultMiddleware().concat(thunkMiddleware),
    devTools: process.env.NODE_ENV !== "production",
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
export default store;
