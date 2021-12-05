import { Application, Selectable } from "models";
import { DateTime } from "luxon";
import { createAction, createAsyncThunk, createSlice } from "@reduxjs/toolkit";

type State = {
    isAdmin: boolean;
    isManager: boolean;
    isEmployee: boolean;
};

export const permissionSlice = createSlice({
    name: "permission",
    initialState: {
        isAdmin: false,
        isManager: false,
        isEmployee: false,
    } as State,
    reducers: {},
    extraReducers: {},
});

export const { } = permissionSlice.actions;

export default permissionSlice.reducer;
