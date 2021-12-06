import { DateTime } from "luxon";
import { TokenData } from "models";
import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { getTokenData, signInWithEmailAndPassword } from "api/authApi";
import axios, { AxiosResponse } from "axios";

type State = {
    isAuthenticating: boolean;
    isAuthenticated: boolean;
    token: string;
};

// export const signIn = createAsyncThunk<
//     AxiosResponse<TokenData, unknown>,
//     {
//         email: string;
//         password: string;
//     },
//     unknown
// >("signIn", ({ email, password }, _) =>
//     signInWithEmailAndPassword(email, password)
// );

export const authSlice = createSlice({
    name: "auth",
    initialState: {
        isAuthenticating: true,
        isAuthenticated: false,
        token: "",
    } as State,
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
                state.token = tokenData.token;
                axios.defaults.headers.common[
                    "Authorization"
                ] = `Bearer ${state.token}`;

                return;
            }

            state.isAuthenticating = false;
        },
        signInSuccess: (state, action) => {
            state.isAuthenticating = false;
            state.isAuthenticated = true;

            const tokenData = action.payload;
            state.token = tokenData.token;
            localStorage.setItem("jwt", JSON.stringify(tokenData));

            axios.defaults.headers.common[
                "Authorization"
            ] = `Bearer ${state.token}`;
        },
        signIn: (state) => {
            state.isAuthenticating = true;
        },
        signOut: (state) => {
            state.isAuthenticated = false;
            localStorage.removeItem("jwt");

            axios.defaults.headers.common["Authorization"] = undefined;
        },
    },
    extraReducers: {},
});

export const { signOut, verifyToken, signInSuccess } = authSlice.actions;

export default authSlice.reducer;
