import { DateTime } from "luxon";
import axios from "axios";

type TokenData = {
    expiresOn: string;
    jwt: string;
};

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

    if (signInResult.status === 400) {
        return null;
    }

    const tokenData = signInResult.data;

    localStorage.setItem("jwt", JSON.stringify(tokenData));

    return tokenData;
};
