import { Application, Role, Selectable } from "models";
import { DateTime } from "luxon";
import { createAction, createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { getRoles } from "api/userApi";

type State = {
    isLoading: boolean;
    isAdmin: boolean;
    isManager: boolean;
    isEmployee: boolean;
    roles: Role[];
};

const roleMap = {
    Admin: "isAdmin",
    Manager: "isManager",
    Employee: "isEmployee",
} as Record<string, keyof State>;

export const getRolesAction = createAsyncThunk("signIn", () => getRoles());

export const permissionSlice = createSlice({
    name: "permission",
    initialState: {
        isLoading: true,
        isAdmin: false,
        isManager: false,
        isEmployee: false,
        roles: [],
    } as State,
    reducers: {},
    extraReducers: {
        [getRolesAction.rejected.type]: (state, action) => {
            state.isLoading = false;
        },
        [getRolesAction.fulfilled.type]: (state, action) => {
            const roles = action.payload as Role[];
            for (const role of roles) {
                const key = roleMap[role.name];
                state[key] = true as any;
            }
            state.roles = roles;
            state.isLoading = false;
        },
    },
});

export const { } = permissionSlice.actions;

export default permissionSlice.reducer;
