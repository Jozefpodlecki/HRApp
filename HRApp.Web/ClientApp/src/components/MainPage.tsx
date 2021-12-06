import { DateTime } from "luxon";

import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Card from "@mui/material/Card";
import CardActions from "@mui/material/CardActions";
import CardContent from "@mui/material/CardContent";
import CardMedia from "@mui/material/CardMedia";
import Grid from "@mui/material/Grid";
import React, { ChangeEvent, FunctionComponent, useEffect } from "react";
import Typography from "@mui/material/Typography";
import annualLeaveImage from "./../assets/annual-leave.jfif";
import appraisalImage from "./../assets/appraisal.jpg";
import statutorySickPayImage from "./../assets/statutorySickPay.png";

const MainPage: FunctionComponent = () => {
    return (
        <Box sx={{ margin: 1, width: "100%" }}>
            <Typography variant="h4" component="div">
                Create new application
            </Typography>
            <Grid container spacing={2} margin={1}>
                <Grid item xs={3} sx={{ minWidth: 345 }}>
                    <Card>
                        <CardMedia
                            component="img"
                            height="140"
                            image={annualLeaveImage}
                            alt="annual leave"
                        />
                        <CardContent>
                            <Typography variant="h5">Annual Leave</Typography>
                        </CardContent>
                        <CardActions>
                            <Button href="/annual-leave/new" size="small">
                                Start
                            </Button>
                        </CardActions>
                    </Card>
                </Grid>
                <Grid item xs={3} sx={{ minWidth: 345 }}>
                    <Card>
                        <CardMedia
                            component="img"
                            height="140"
                            image={statutorySickPayImage}
                            alt="statutory form"
                        />
                        <CardContent>
                            <Typography variant="h5">
                                Statutory Form SC2
                            </Typography>
                        </CardContent>
                        <CardActions>
                            <Button href="/annual-leave/new" size="small">
                                Start
                            </Button>
                        </CardActions>
                    </Card>
                </Grid>
                <Grid item xs={3} sx={{ minWidth: 345 }}>
                    <Card>
                        <CardMedia
                            component="img"
                            height="140"
                            image={appraisalImage}
                            alt="appraisal"
                        />
                        <CardContent>
                            <Typography variant="h5">
                                Basic Appraisal Form
                            </Typography>
                        </CardContent>
                        <CardActions>
                            <Button href="/annual-leave/new" size="small">
                                Start
                            </Button>
                        </CardActions>
                    </Card>
                </Grid>
            </Grid>
        </Box>
    );
};

export default MainPage;
