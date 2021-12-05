import { Application, Selectable } from "models";
import { DateTime } from "luxon";
import { createAction, createAsyncThunk, createSlice } from "@reduxjs/toolkit";

const now = DateTime.now();

export const getApplications = createAsyncThunk("getApplications", () =>
    Promise.resolve(
        Array(100)
            .fill(0)
            .map((_, index) => ({
                id: index,
                type: "annual-leave",
                title: "Annual Leave",
                createdBy: `Employee ${index}`,
                createdOn: now.minus({ hours: index }).toISO(),
            }))
    )
);

export const setPage = createAction<number>("setPage");
export const setRowsPerPage = createAction<number>("setRowsPerPage");
export const selectItem = createAction<number>("selectItem");

export const applicationsSlice = createSlice({
    name: "applications",
    initialState: {
        isLoading: true,
        allSelected: false,
        selectedItems: new Array<number>(),
        items: new Array<Selectable<Application>>(),
        pagedItems: new Array<Selectable<Application>>(),
        rowsPerPage: 5,
        page: 0,
    },
    reducers: {
        selectAll: (state) => {
            if (state.allSelected) {
                state.selectedItems = state.selectedItems.concat(
                    state.items.map((pr) => pr.id)
                );
                state.pagedItems = state.pagedItems.map((pr) => ({
                    ...pr,
                    hasSelected: false,
                }));
            } else {
                state.selectedItems = state.selectedItems.concat(
                    state.items.map((pr) => pr.id)
                );
                state.pagedItems = state.pagedItems.map((pr) => ({
                    ...pr,
                    hasSelected: true,
                }));
            }
            state.allSelected = !state.allSelected;
        },
    },
    extraReducers: {
        [selectItem.type]: (state, action) => {
            const id = action.payload;
            const itemIndex = state.pagedItems.findIndex((pr) => pr.id === id);
            const index = state.selectedItems.indexOf(id);

            if (index > -1) {
                state.selectedItems.splice(index, 1);
                state.pagedItems[itemIndex] = {
                    ...state.pagedItems[itemIndex],
                    hasSelected: false,
                };
            } else {
                state.pagedItems[itemIndex] = {
                    ...state.pagedItems[itemIndex],
                    hasSelected: true,
                };
                state.selectedItems.push(id);
            }
        },
        [setPage.type]: (state, action) => {
            state.page = action.payload;
            const offset = state.page * state.rowsPerPage;
            state.pagedItems = state.items.slice(
                offset,
                offset + state.rowsPerPage
            );
            state.allSelected = false;
        },
        [setRowsPerPage.type]: (state, action) => {
            state.rowsPerPage = action.payload;
            state.page = 0;
            state.pagedItems = state.items.slice(0, state.rowsPerPage);
            state.allSelected = false;
        },
        [getApplications.fulfilled.type]: (state, action) => {
            state.items = action.payload.map((pr: Record<string, unknown>) => ({
                ...pr,
                hasSelected: false,
            }));
            const offset = state.page * state.rowsPerPage;
            state.pagedItems = state.items.slice(
                offset,
                offset + state.rowsPerPage
            );
            state.isLoading = false;
        },
    },
});

export const { selectAll } = applicationsSlice.actions;

export default applicationsSlice.reducer;
