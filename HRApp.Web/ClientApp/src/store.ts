import { Application, Selectable } from "models";
import { configureStore } from "@reduxjs/toolkit";
import application from "./slices/newApplication";
import applications from "./slices/applications";
import auth from "./slices/auth";
import notification from "./slices/notification";
import permission from "./slices/permission";
import signalrMiddleware from "signalrMiddleware";
import thunkMiddleware from "redux-thunk";

const store = configureStore({
    reducer: {
        auth,
        applications,
        application,
        permission,
        notification,
    },
    middleware: (getDefaultMiddleware) =>
        getDefaultMiddleware().concat(thunkMiddleware, signalrMiddleware),
    devTools: process.env.NODE_ENV !== "production",
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
export default store;
