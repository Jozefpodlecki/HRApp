import { DateTime } from "luxon";
import { getApplications } from "slices/applications";
import { styled, useTheme } from "@mui/material/styles";
import { useSelector } from "hooks";
import Paper from "@mui/material/Paper";
import React, { FunctionComponent, useEffect } from "react";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";

const ApplicationList: FunctionComponent = () => {
    const theme = useTheme();
    const { isLoading, items } = useSelector((state) => state.applications);

    useEffect(() => {
        getApplications();
    }, []);

    return (
        <TableContainer component={Paper}>
            <Table sx={{ minWidth: 650 }} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell align="right">Title</TableCell>
                        <TableCell align="right">Created On</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {items.map((item) => (
                        <TableRow
                            key={item.id}
                            sx={{
                                "&:last-child td, &:last-child th": {
                                    border: 0,
                                },
                            }}
                        >
                            <TableCell component="th" scope="row">
                                {item.title}
                            </TableCell>
                            <TableCell align="right">
                                {DateTime.fromISO(item.createdOn).toLocal()}
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
};

export default ApplicationList;
