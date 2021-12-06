import { Navigate, Route, Routes } from "react-router-dom";
import AnnualLeaveForm from "./AnnualLeaveForm";
import AppBar from "./AppBar";
import ApplicationList from "./ApplicationList";
import Box from "@mui/material/Box";
import Drawer from "components/Drawer";
import MainPage from "./MainPage";
import React, { FunctionComponent, MouseEvent, useState } from "react";
import styled from "styled-components";
import Employees from "./Employees";

const drawerWidth = 300;

const Main = styled.main`
    display: flex;
`;

const Portal: FunctionComponent = () => {
    return (
        <Box
            sx={{
                display: "flex",
                flexDirection: "column",
                height: "100%",
                width: "100%",
            }}
        >
            <AppBar />
            <Main>
                <Drawer width={drawerWidth} />
                <Routes>
                    <Route path="/" element={<MainPage />} />
                    <Route path="/employees" element={<Employees />} />
                    <Route path="/applications" element={<ApplicationList />} />
                    <Route
                        path="/my-applications"
                        element={<ApplicationList />}
                    />
                    <Route
                        path="/annual-leave/new"
                        element={<AnnualLeaveForm />}
                    />
                </Routes>
            </Main>
        </Box>
    );
};

export default Portal;
