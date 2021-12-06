import { CircleLoader } from "react-spinners";
import { ErrorResponse, systemErrorKey } from "models";
import { makeStyles } from "@material-ui/core/styles";
import { signInSuccess } from "slices/auth";
import { signInWithEmailAndPassword } from "api/authApi";
import { useDispatch } from "hooks";
import { useNavigate } from "react-router";
import Button from "@mui/material/Button";
import FormHelperText from "@mui/material/FormHelperText";
import Paper from "@mui/material/Paper";
import React, { ChangeEvent, FunctionComponent, useState } from "react";
import Stack from "@mui/material/Stack";
import TextField from "@mui/material/TextField";
import axios from "axios";

type Errors = Record<string, string[]>;

type State = {
    email: string;
    password: string;
    isSubmitting: boolean;
    errors: Errors;
};

const useStyles = makeStyles(() => ({
    login: {
        width: 200,
    },
    input: {
        maxWidth: 300,
    },
}));

const Login: FunctionComponent = () => {
    const classes = useStyles();
    const [{ email, password, isSubmitting, errors }, setState] =
        useState<State>({
            email: "",
            password: "",
            isSubmitting: false,
            errors: {},
        });
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const onClick = async () => {
        try {
            setState((state) => ({ ...state, isSubmitting: true }));
            const tokenData = await signInWithEmailAndPassword(email, password);

            if (tokenData) {
                navigate("/");
                dispatch(signInSuccess(tokenData.data));
                return;
            }
        } catch (error) {
            let errors: Errors = {
                [systemErrorKey]: ["Internal error occurred please try later."],
            };

            if (axios.isAxiosError(error)) {
                const errorResponse = error.response.data as ErrorResponse;
                errors = errorResponse.errors;
                errors = Object.entries(errors).reduce(
                    (acc, entry: [string, string[]]) => {
                        const [key, value] = entry;
                        acc[key.toLowerCase()] = value;
                        return acc;
                    },
                    {} as Errors
                );
            }

            setState((state) => ({
                ...state,
                isSubmitting: false,
                errors,
            }));
        }
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
                width: 400,
                height: 300,
            }}
        >
            {isSubmitting ? (
                <CircleLoader color="black" />
            ) : (
                <>
                    <TextField
                        name="email"
                        onChange={onChange}
                        value={email}
                        label="Email"
                        type="email"
                        margin="normal"
                        error={!!errors.email}
                        helperText={errors.email}
                        className={classes.input}
                        fullWidth
                        required
                    />
                    <TextField
                        name="password"
                        onChange={onChange}
                        value={password}
                        label="Password"
                        type="password"
                        error={!!errors.password}
                        helperText={errors.password}
                        className={classes.input}
                        fullWidth
                        required
                        style={{
                            marginBottom: 10,
                        }}
                    />
                    <FormHelperText error={!!errors[systemErrorKey]}>
                        {errors[systemErrorKey]}
                    </FormHelperText>
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
