import { DateTime } from "luxon";

import { useDispatch, useSelector } from "hooks";
import AdapterDateFns from "@mui/lab/AdapterDateFns";
import Box from "@mui/material/Box";
import DateTimePicker from "@mui/lab/DateTimePicker";
import DesktopDatePicker from "@mui/lab/DesktopDatePicker";
import FormControl from "@mui/material/FormControl";
import FormControlLabel from "@mui/material/FormControlLabel";
import FormGroup from "@mui/material/FormGroup";
import LocalizationProvider from "@mui/lab/LocalizationProvider";
import MobileDatePicker from "@mui/lab/MobileDatePicker";
import React, { ChangeEvent, FunctionComponent, useEffect } from "react";
import Switch from "@mui/material/Switch";
import TextField from "@mui/material/TextField";
import TimePicker from "@mui/lab/TimePicker";

const AnnualLeaveForm: FunctionComponent = () => {
    const { allDay, multipleDays, time, date, dateFrom, dateTo } = useSelector(
        (state) => state.application
    );

    const onChange = (event: ChangeEvent<HTMLInputElement>) => {
        console.log(event);
    };

    return (
        <Box sx={{ margin: 1, width: "100%" }}>
            <FormGroup>
                <FormControlLabel
                    control={<Switch defaultChecked />}
                    label="date range"
                />
                {multipleDays ? (
                    <>
                        <DesktopDatePicker
                            label="dateFrom"
                            inputFormat="MM/dd/yyyy"
                            value={dateFrom}
                            onChange={onChange}
                            renderInput={(params) => <TextField {...params} />}
                        />
                        <DesktopDatePicker
                            label="dateTo"
                            inputFormat="MM/dd/yyyy"
                            value={dateTo}
                            onChange={onChange}
                            renderInput={(params) => <TextField {...params} />}
                        />
                    </>
                ) : (
                    <FormControlLabel
                        disabled
                        control={<Switch />}
                        label="All day"
                    />
                )}
                {/* <DesktopDatePicker
                    label="Date"
                    inputFormat="MM/dd/yyyy"
                    value={value}
                    onChange={handleChange}
                    renderInput={(params) => <TextField {...params} />}
                /> */}
            </FormGroup>
        </Box>
    );
};

export default AnnualLeaveForm;
