export type Selectable<T> = T & { hasSelected: boolean };

export type ApplicationType = "annual-leave";

export type Application = {
    id: number;
    type: ApplicationType;
    title: string;
    createdBy: string;
    createdOn: string;
};

export type TokenData = {
    expiresOn: string;
    token: string;
};

export type Role = {
    id: number;
    name: string;
};

export type ErrorResponse = {
    type: string;
    title: string;
    status: number;
    traceId: string;
    errors: Record<string, string[]>;
};

export const systemErrorKey = "";
