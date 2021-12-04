import { CircleLoader } from "react-spinners";
import { Navigate, Route, Routes } from "react-router-dom";
import { useDispatch, useSelector } from "hooks";
import { verifyToken } from "slices/auth";
import Background from "./Background";
import Dashboard from "components/Dashboard";
import Login from "components/LoginForm";
import LoginPage from "./LoginPage";
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
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch(verifyToken());
    }, []);

    return (
        <Main>
            <GlobalStyle />
            {isAuthenticating ? (
                <>
                    <Background />
                    <CircleLoader color="white" />
                </>
            ) : isAuthenticated ? (
                <Routes>
                    <Route path="/" element={<Dashboard />} />
                </Routes>
            ) : (
                <>
                    <Background />
                    <Routes>
                        <Route path="/login" element={<LoginPage />} />
                        <Route path="/" element={<Navigate to="/login" />} />
                    </Routes>
                </>
            )}
        </Main>
    );
};

export default App;
