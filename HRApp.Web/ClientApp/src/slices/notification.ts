import { createSlice } from "@reduxjs/toolkit";
import { useDispatch, useSelector } from "hooks";

type Notification = {
    id: number;
    message: string;
};

type State = {
    items: Notification[];
    readIndex: number;
    message: string;
    open: boolean;
};

export const notificationSlice = createSlice({
    name: "notification",
    initialState: {
        items: [],
        readIndex: 0,
        message: "",
        open: false,
    } as State,
    reducers: {
        close(state) {
            state.open = false;
        },
    },
    extraReducers: {
        "new-annual-leave": (state) => {
            state.message = "New application - annual leave";
            state.open = true;
        },
    },
});

export const { close } = notificationSlice.actions;

export default notificationSlice.reducer;
