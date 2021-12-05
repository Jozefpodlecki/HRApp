import { DateTime } from "luxon";

import {
    getApplications,
    selectAll,
    selectItem,
    setPage,
    setRowsPerPage,
} from "slices/applications";
import { styled, useTheme } from "@mui/material/styles";
import { useDispatch, useSelector } from "hooks";
import Box from "@mui/material/Box";
import Checkbox from "@mui/material/Checkbox";
import Paper from "@mui/material/Paper";
import React, { ChangeEvent, FunctionComponent, useEffect } from "react";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TablePagination from "@mui/material/TablePagination";
import TableRow from "@mui/material/TableRow";

const ApplicationList: FunctionComponent = () => {
    const theme = useTheme();
    const { page, rowsPerPage, allSelected, isLoading, pagedItems, items } =
        useSelector((state) => state.applications);
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch(getApplications());
    }, []);

    const onPageChange = (_: unknown, page: number) => dispatch(setPage(page));

    const onRowsPerPageChange = (event: ChangeEvent<HTMLInputElement>) =>
        dispatch(setRowsPerPage(parseInt(event.target.value, 10)));

    const onSelectAllClick = () => dispatch(selectAll());

    const onSelectClick = (event: ChangeEvent<HTMLInputElement>) => {
        const id = parseInt(event.currentTarget.dataset.id);
        dispatch(selectItem(id));
    };

    return (
        <Box sx={{ width: "100%" }}>
            <Paper sx={{ width: "100%", mb: 2 }}>
                <TableContainer
                    component={Paper}
                    sx={{
                        marginTop: 9,
                    }}
                >
                    <Table sx={{ minWidth: 650 }} aria-label="simple table">
                        <TableHead>
                            <TableRow>
                                <TableCell padding="checkbox">
                                    <Checkbox
                                        color="primary"
                                        checked={allSelected}
                                        onChange={onSelectAllClick}
                                    />
                                </TableCell>
                                <TableCell align="right">Title</TableCell>
                                <TableCell align="right">Created By</TableCell>
                                <TableCell align="right">Created On</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {pagedItems.map((item) => (
                                <TableRow
                                    key={item.id}
                                    sx={{
                                        "&:last-child td, &:last-child th": {
                                            border: 0,
                                        },
                                    }}
                                >
                                    <TableCell padding="checkbox">
                                        <Checkbox
                                            inputProps={
                                                {
                                                    "data-id": item.id,
                                                } as unknown
                                            }
                                            data-id={item.id}
                                            color="primary"
                                            checked={item.hasSelected}
                                            onChange={onSelectClick}
                                        />
                                    </TableCell>
                                    <TableCell align="right">
                                        {item.title}
                                    </TableCell>
                                    <TableCell align="right">
                                        {item.createdBy}
                                    </TableCell>
                                    <TableCell align="right">
                                        {DateTime.fromISO(
                                            item.createdOn
                                        ).toLocaleString()}
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
                <TablePagination
                    rowsPerPageOptions={[5, 10, 25]}
                    component="div"
                    count={items.length}
                    rowsPerPage={rowsPerPage}
                    page={page}
                    onPageChange={onPageChange}
                    onRowsPerPageChange={onRowsPerPageChange}
                />
            </Paper>
        </Box>
    );
};

export default ApplicationList;
