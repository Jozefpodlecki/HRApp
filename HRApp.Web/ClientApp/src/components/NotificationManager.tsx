import { makeStyles } from "@material-ui/core/styles";
import { useDispatch, useSelector } from "hooks";
import MuiAlert from "@mui/material/Alert";
import React, {
    ChangeEvent,
    FunctionComponent,
    memo,
    useEffect,
    useState,
} from "react";
import Snackbar from "@mui/material/Snackbar";

const Alert: any = React.forwardRef(function Alert(props, ref) {
    return (
        <MuiAlert elevation={6} ref={ref as any} variant="filled" {...props} />
    );
});

const NotificationManager: FunctionComponent = () => {
    const dispatch = useDispatch();
    const { items, message } = useSelector((state) => state.notification);

    useEffect(() => {
        if (items.length) {
        }
    }, [items]);

    const onClose = () => {
        debugger;
    };

    if (!items.length) {
        return null;
    }

    return (
        <Snackbar
            anchorOrigin={{ vertical: "bottom", horizontal: "right" }}
            open={true}
            onClose={onClose}
        >
            <Alert onClose={onClose} severity="success" sx={{ width: "100%" }}>
                {message}
            </Alert>
        </Snackbar>
    );
};

export default memo(NotificationManager);
