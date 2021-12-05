import { Application, Selectable } from "models";
import { DateTime } from "luxon";
import { createAction, createAsyncThunk, createSlice } from "@reduxjs/toolkit";

const now = DateTime.now();

type State = {
    multipleDays: true;
    allDay: boolean;
    date: string | null;
    dateFrom: string | null;
    dateTo: string | null;
    time: string | null;
};

export const applicationSlice = createSlice({
    name: "application",
    initialState: {
        multipleDays: true,
        allDay: true,
        date: null,
        dateFrom: null,
        dateTo: null,
        time: null,
    } as State,
    reducers: {},
    extraReducers: {},
});

export const { } = applicationSlice.actions;

export default applicationSlice.reducer;
