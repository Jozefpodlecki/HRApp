import { DateTime } from "luxon";
import { TokenData } from "models";
import axios from "axios";

export const getTokenData = (): TokenData | undefined =>
    JSON.parse(localStorage.getItem("jwt"));

export const signInWithEmailAndPassword = async (
    email: string,
    password: string
) => {
    const signInResult = await axios.post<TokenData>("api/auth", {
        email,
        password,
    });
    return signInResult;
};
