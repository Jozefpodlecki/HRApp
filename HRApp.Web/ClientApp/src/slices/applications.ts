import { DateTime } from "luxon";
import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";

type ApplicationType = "annual-leave";

type Application = {
    id: number;
    type: ApplicationType;
    title: string;
    createdOn: string;
};

const now = DateTime.now();

export const getApplications = createAsyncThunk("getApplications", () =>
    Promise.resolve(
        Array(100)
            .fill(0)
            .map((_, index) => ({
                id: index,
                type: "annual-leave",
                title: `Employee ${index} - Annual Leave`,
                createdOn: now.minus({ hours: index }).toISO(),
            }))
    )
);

export const applicationsSlice = createSlice({
    name: "applications",
    initialState: {
        isLoading: true,
        items: new Array<Application>(),
    },
    reducers: {},
    extraReducers: {
        [getApplications.fulfilled.type]: (state, action) => {
            state.items = action.payload;
            state.isLoading = false;
        },
    },
});

export const { } = applicationsSlice.actions;

export default applicationsSlice.reducer;
