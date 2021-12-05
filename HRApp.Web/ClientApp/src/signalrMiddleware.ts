import { Middleware } from "redux";
import signalR from "@microsoft/signalr";

const signalRMiddleware: Middleware<{}, any> =
    (storeAPI) => (next) => (action) => {
        if (action.type === "signInSuccess") {
            debugger;
            const token = storeAPI.getState().auth.token;

            const connection = new signalR.HubConnectionBuilder()
                .withUrl("/signalr", { accessTokenFactory: () => token })
                .configureLogging(signalR.LogLevel.Information)
                .build();

            connection.on("new-annual-leave", (message) => {
                storeAPI.dispatch({
                    type: "new-annual-leave",
                    payload: message,
                });
            });

            connection.start();
            connection.onclose(() =>
                setTimeout(() => connection.start(), 5000)
            );
        }

        return next(action);
    };

export default signalRMiddleware;
