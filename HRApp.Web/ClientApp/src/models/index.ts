export type Selectable<T> = T & { hasSelected: boolean };

export type ApplicationType = "annual-leave";

export type Application = {
    id: number;
    type: ApplicationType;
    title: string;
    createdBy: string;
    createdOn: string;
};
