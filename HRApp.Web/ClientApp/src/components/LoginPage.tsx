import { Typography } from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";
import Login from "components/LoginForm";
import React, { FunctionComponent } from "react";

const useStyles = makeStyles(() => ({
    header: {
        color: "#FFF",
        margin: 20,
    },
}));

const LoginPage: FunctionComponent = () => {
    const classes = useStyles();

    return (
        <>
            <Typography className={classes.header} variant="h3" component="h3">
                HR App
            </Typography>
            <Login />
        </>
    );
};

export default LoginPage;
