import { CircleLoader } from "react-spinners";
import { Navigate, Route, Routes } from "react-router-dom";
import { getRolesAction } from "slices/permission";
import { useDispatch, useSelector } from "hooks";
import { verifyToken } from "slices/auth";
import Background from "./Background";
import Dashboard from "components/Portal";
import LoginPage from "./LoginPage";
import NotificationManager from "./NotificationManager";
import React, { FunctionComponent, useEffect } from "react";
import styled, { createGlobalStyle } from "styled-components";

const GlobalStyle = createGlobalStyle`
    html, body {
        height: 100%;
    }

    body {
        margin: 0;
        padding: 0;
        font-family: Roboto;
    }

    #root {
        width: 100%;
        height: 100%;
    }
`;

const Main = styled.main`
    width: 100%;
    height: 100%;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
`;

const App: FunctionComponent = () => {
    const { isAuthenticating, isAuthenticated } = useSelector(
        (state) => state.auth
    );
    const { isLoading: isLoadingRoles } = useSelector(
        (state) => state.permission
    );
    const dispatch = useDispatch();

    useEffect(() => {
        if (!isAuthenticated) {
            return;
        }

        if (isLoadingRoles) {
            dispatch(getRolesAction());
        }
    }, [isAuthenticated, isLoadingRoles]);

    useEffect(() => {
        dispatch(verifyToken());
    }, []);

    return (
        <Main>
            <GlobalStyle />
            <NotificationManager />
            {isAuthenticating ? (
                <>
                    <Background />
                    <CircleLoader color="white" />
                </>
            ) : isAuthenticated ? (
                isLoadingRoles ? (
                    <>
                        <Background />
                        <CircleLoader color="white" />
                    </>
                ) : (
                    <Dashboard />
                )
            ) : (
                <>
                    <Background />
                    <Routes>
                        <Route path="/login" element={<LoginPage />} />
                        <Route path="*" element={<Navigate to="/login" />} />
                    </Routes>
                </>
            )}
        </Main>
    );
};

export default App;
