import { styled, useTheme } from "@mui/material/styles";
import Box from "@mui/material/Box";
import ChevronLeftIcon from "@mui/icons-material/ChevronLeft";
import ChevronRightIcon from "@mui/icons-material/ChevronRight";
import CssBaseline from "@mui/material/CssBaseline";
import Divider from "@mui/material/Divider";
import Drawer from "@mui/material/Drawer";
import IconButton from "@mui/material/IconButton";
import InboxIcon from "@mui/icons-material/MoveToInbox";
import List from "@mui/material/List";
import ListItem from "@mui/material/ListItem";
import ListItemIcon from "@mui/material/ListItemIcon";
import ListItemText from "@mui/material/ListItemText";
import MailIcon from "@mui/icons-material/Mail";
import MenuIcon from "@mui/icons-material/Menu";
import MuiAppBar from "@mui/material/AppBar";
import React, { FunctionComponent } from "react";
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";

const DrawerHeader = styled("div")(({ theme }) => ({
    display: "flex",
    alignItems: "center",
    padding: theme.spacing(0, 1),
    ...theme.mixins.toolbar,
    justifyContent: "flex-end",
}));

type Props = {
    width: number;
};

const DrawerComp: FunctionComponent<Props> = ({ width }) => {
    const theme = useTheme();

    return (
        <Drawer
            sx={{
                width: width,
                flexShrink: 0,
                "& .MuiDrawer-paper": {
                    width: width,
                    boxSizing: "border-box",
                },
            }}
            variant="persistent"
            anchor="left"
            open={true}
        >
            <DrawerHeader></DrawerHeader>
            <Divider />
            <List>
                <ListItem button key={"Applications"}>
                    <ListItemIcon>
                        <InboxIcon />
                    </ListItemIcon>
                    <ListItemText primary={"Applications"} />
                </ListItem>
            </List>
            <Divider />
        </Drawer>
    );
};

export default DrawerComp;
