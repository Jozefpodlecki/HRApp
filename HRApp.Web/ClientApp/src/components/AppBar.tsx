import { Navigate, Route, Routes } from "react-router-dom";
import { signOut } from "slices/auth";
import { styled, useTheme } from "@mui/material/styles";
import { useDispatch } from "hooks";
import AccountCircle from "@mui/icons-material/AccountCircle";
import IconButton from "@mui/material/IconButton";
import MainPage from "./MainPage";
import Menu from "@mui/material/Menu";
import MenuItem from "@mui/material/MenuItem";
import MuiAppBar from "@mui/material/AppBar";
import React, { FunctionComponent, MouseEvent, useState } from "react";
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";

const drawerWidth = 300;

const AppBar = styled(MuiAppBar, {
    shouldForwardProp: (prop) => prop !== "open",
})(({ theme }) => ({
    transition: theme.transitions.create(["margin", "width"], {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.leavingScreen,
    }),
    ...{
        width: `calc(100% - ${drawerWidth}px)`,
        marginLeft: `${drawerWidth}px`,
        transition: theme.transitions.create(["margin", "width"], {
            easing: theme.transitions.easing.easeOut,
            duration: theme.transitions.duration.enteringScreen,
        }),
    },
}));

const AppBarComp: FunctionComponent = () => {
    const [anchorEl, setAnchorEl] = useState<HTMLElement>(null);
    const dispatch = useDispatch();

    const onSignOut = () => {
        dispatch(signOut());
    };

    const onMenuOpen = (event: MouseEvent<HTMLElement>) => {
        setAnchorEl(event.currentTarget);
    };

    const onMenuClose = () => {
        setAnchorEl(null);
    };

    return (
        <AppBar position="sticky">
            <Toolbar>
                <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                    HR App
                </Typography>
                <div>
                    <IconButton
                        size="large"
                        onClick={onMenuOpen}
                        color="inherit"
                    >
                        <AccountCircle />
                    </IconButton>
                    <Menu
                        id="menu-appbar"
                        anchorEl={anchorEl}
                        anchorOrigin={{
                            vertical: "top",
                            horizontal: "right",
                        }}
                        keepMounted
                        transformOrigin={{
                            vertical: "top",
                            horizontal: "right",
                        }}
                        open={Boolean(anchorEl)}
                        onClose={onMenuClose}
                    >
                        <MenuItem onClick={onSignOut}>Sign Out</MenuItem>
                    </Menu>
                </div>
            </Toolbar>
        </AppBar>
    );
};

export default AppBarComp;
