import { Application } from "models";
import axios from "axios";

export const getApplications = async () => {
    const result = await axios.get<Application[]>("api/applications/assigned");
    return result.data;
};
