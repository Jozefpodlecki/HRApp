import { Link } from "react-router-dom";
import { styled, useTheme } from "@mui/material/styles";
import { useSelector } from "hooks";
import AssignmentIcon from "@mui/icons-material/Assignment";
import BarChartIcon from "@mui/icons-material/BarChart";
import Divider from "@mui/material/Divider";
import Drawer from "@mui/material/Drawer";
import HomeIcon from "@mui/icons-material/Home";
import List from "@mui/material/List";
import ListItem from "@mui/material/ListItem";
import ListItemIcon from "@mui/material/ListItemIcon";
import ListItemText from "@mui/material/ListItemText";
import PeopleIcon from "@mui/icons-material/People";
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
    const { isAdmin, isManager, isEmployee } = useSelector(
        (state) => state.permission
    );

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
                {isAdmin ? (
                    <ListItem
                        button
                        key={"Statistics"}
                        component={Link}
                        to="/statistics"
                    >
                        <ListItemIcon>
                            <BarChartIcon />
                        </ListItemIcon>
                        <ListItemText primary={"Statistics"} />
                    </ListItem>
                ) : null}
                <ListItem button key={"Home"} component={Link} to="/">
                    <ListItemIcon>
                        <HomeIcon />
                    </ListItemIcon>
                    <ListItemText primary={"Home"} />
                </ListItem>
                {isManager ? (
                    <ListItem
                        button
                        key={"Employees"}
                        component={Link}
                        to="/employees"
                    >
                        <ListItemIcon>
                            <PeopleIcon />
                        </ListItemIcon>
                        <ListItemText primary={"Employees"} />
                    </ListItem>
                ) : null}
                {isManager ? (
                    <ListItem
                        button
                        key={"Applications"}
                        component={Link}
                        to="/applications"
                    >
                        <ListItemIcon>
                            <AssignmentIcon />
                        </ListItemIcon>
                        <ListItemText primary={"Applications"} />
                    </ListItem>
                ) : null}
                {isEmployee ? (
                    <ListItem
                        button
                        key={"My Applications"}
                        component={Link}
                        to="/my-applications"
                    >
                        <ListItemIcon>
                            <AssignmentIcon />
                        </ListItemIcon>
                        <ListItemText primary={"My Applications"} />
                    </ListItem>
                ) : null}
            </List>
            <Divider />
        </Drawer>
    );
};

export default DrawerComp;
