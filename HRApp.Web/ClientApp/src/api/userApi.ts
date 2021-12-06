import { Role } from "models";
import axios from "axios";

export const getRoles = async () => {
    const result = await axios.get<Role[]>("api/user/roles");

    return result.data;
};
