import { CircleLoader } from "react-spinners";
import { makeStyles } from "@material-ui/core/styles";
import { signInSuccess } from "slices/auth";
import { signInWithEmailAndPassword } from "authApi";
import { useDispatch } from "hooks";
import { useNavigate } from "react-router";
import Button from "@mui/material/Button";
import Paper from "@mui/material/Paper";
import React, { ChangeEvent, FunctionComponent, useState } from "react";
import Stack from "@mui/material/Stack";
import TextField from "@mui/material/TextField";

const useStyles = makeStyles(() => ({
    login: {
        width: 200,
    },
}));

const Login: FunctionComponent = () => {
    const classes = useStyles();
    const [{ email, password, isSubmitting }, setState] = useState({
        email: "",
        password: "",
        isSubmitting: false,
    });
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const onClick = async () => {
        setState((state) => ({ ...state, isSubmitting: true }));
        const tokenData = await signInWithEmailAndPassword(email, password);

        if (tokenData) {
            navigate("/");
            dispatch(signInSuccess(tokenData));
            return;
        }

        setState((state) => ({ ...state, isSubmitting: false }));
    };

    const onChange = (event: ChangeEvent<HTMLInputElement>) => {
        const { name, value } = event.currentTarget;
        setState((state) => ({ ...state, [name]: value }));
    };

    const isValid = !email.length || !password.length;

    return (
        <Paper
            component={Stack}
            direction="column"
            justifyContent="center"
            alignItems="center"
            sx={{
                width: 300,
                height: 300,
            }}
        >
            {isSubmitting ? (
                <CircleLoader color="white" />
            ) : (
                <>
                    <TextField
                        name="email"
                        onChange={onChange}
                        value={email}
                        label="Email"
                        type="email"
                        margin="normal"
                        required
                    />
                    <TextField
                        name="password"
                        onChange={onChange}
                        value={password}
                        label="Password"
                        type="password"
                        required
                        style={{
                            marginBottom: 10,
                        }}
                    />
                    <Button
                        onClick={onClick}
                        disabled={isValid}
                        className={classes.login}
                        variant="contained"
                    >
                        Log in
                    </Button>
                </>
            )}
        </Paper>
    );
};

export default Login;
