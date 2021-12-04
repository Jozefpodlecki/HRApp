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
    const fakeTokenData = {
        jwt: "fake token",
        expiresOn: DateTime.now().plus({ hours: 1 }).toISO(),
    };

    localStorage.setItem("jwt", JSON.stringify(fakeTokenData));
    return Promise.resolve(fakeTokenData);

    const signInResult = await axios.post<TokenData>("api/auth/token", {
        email,
        password,
    });

    if (signInResult.status === 400) {
        return false;
    }

    const tokenData = signInResult.data;

    localStorage.setItem("jwt", JSON.stringify(tokenData));

    return true;
};