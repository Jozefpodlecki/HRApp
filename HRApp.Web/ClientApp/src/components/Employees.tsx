import Avatar from "@mui/material/Avatar";
import Box from "@mui/material/Box";
import Divider from "@mui/material/Divider";
import List from "@mui/material/List";
import ListItem from "@mui/material/ListItem";
import ListItemAvatar from "@mui/material/ListItemAvatar";
import ListItemButton from "@mui/material/ListItemButton";
import ListItemText from "@mui/material/ListItemText";
import React, { FunctionComponent } from "react";
import Typography from "@mui/material/Typography";

const list = new Array(10).fill(0).map((_, id) => ({
    id,
    name: "Employee {1}",
}));

const avatarUrl = "https://i.pravatar.cc/300";

const Employees: FunctionComponent = () => {
    return (
        <div>
            <Typography variant="h4" component="div">
                Employees
            </Typography>
            <div>
                <List>
                    {list.map((pr) => (
                        <ListItem alignItems="flex-start">
                            <ListItemButton key={pr.id}>
                                <ListItemAvatar>
                                    <Avatar alt={pr.name} src={avatarUrl} />
                                </ListItemAvatar>

                                <ListItemText primary={pr.name} />
                            </ListItemButton>
                        </ListItem>
                    ))}
                </List>
            </div>
        </div>
    );
};

export default Employees;
