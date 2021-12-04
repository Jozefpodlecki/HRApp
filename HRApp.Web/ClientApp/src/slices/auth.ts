import { DateTime } from "luxon";
import { createSlice } from "@reduxjs/toolkit";
import { getTokenData } from "authApi";

export const authSlice = createSlice({
    name: "auth",
    initialState: {
        isAuthenticating: true,
        isAuthenticated: false,
    },
    reducers: {
        verifyToken: (state) => {
            const tokenData = getTokenData();

            if (tokenData) {
                const now = DateTime.now();
                const expiresOn = DateTime.fromISO(tokenData.expiresOn);

                if (now < expiresOn) {
                    state.isAuthenticating = false;
                }

                state.isAuthenticating = false;
                state.isAuthenticated = true;
                return;
            }

            state.isAuthenticating = false;
        },
        signInSuccess: (state) => {
            state.isAuthenticating = false;
            state.isAuthenticated = true;
        },
        signIn: (state) => {
            state.isAuthenticating = true;
        },
        signOut: (state) => {
            state.isAuthenticated = false;
        },
    },
});

export const { signIn, signOut, verifyToken, signInSuccess } =
    authSlice.actions;

export default authSlice.reducer;
