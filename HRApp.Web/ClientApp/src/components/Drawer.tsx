import { Link } from "react-router-dom";
import { styled, useTheme } from "@mui/material/styles";
import AssignmentIcon from "@mui/icons-material/Assignment";
import Divider from "@mui/material/Divider";
import Drawer from "@mui/material/Drawer";
import HomeIcon from "@mui/icons-material/Home";
import List from "@mui/material/List";
import ListItem from "@mui/material/ListItem";
import ListItemIcon from "@mui/material/ListItemIcon";
import ListItemText from "@mui/material/ListItemText";
import React, { FunctionComponent } from "react";

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
                <ListItem button key={"Home"} component={Link} to="/">
                    <ListItemIcon>
                        <HomeIcon />
                    </ListItemIcon>
                    <ListItemText primary={"Home"} />
                </ListItem>
                <ListItem
                    button
                    key={"Applications"}
                    component={Link}
                    to="/list"
                >
                    <ListItemIcon>
                        <AssignmentIcon />
                    </ListItemIcon>
                    <ListItemText primary={"Applications"} />
                </ListItem>
            </List>
            <Divider />
        </Drawer>
    );
};

export default DrawerComp;
