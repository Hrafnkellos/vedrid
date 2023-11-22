'use client'
import { Box, Grid, Slider, Typography } from '@mui/material'
import React from 'react'

function valuetext(value: number) {
    return `${value}`;
}

export const TimeSlider = ({ maxValue, onChange } : { maxValue: number, onChange: (_:Event, v: number) => void }) => {
    return (
        <Box sx={{}}>
            <Typography variant="h3" color="text.primary">
                Time
            </Typography>
            <div>
                <Grid container sx={{ minHeight: 50, minWidth: 300, maxWidth: '100%' }}>
                    <Slider
                        aria-label="Temperature"
                        defaultValue={0}
                        onChange={onChange}
                        // onChange={handleChange}
                        // getAriaValueText={valuetext}
                        // onChange={(_,v:number) => {setValue(v)}}
                        valueLabelFormat={valuetext}
                        // value={sliderValue}
                        valueLabelDisplay="auto"
                        step={1}
                        marks
                        min={0}
                        max={maxValue}
                    />
                </Grid>
            </div>
        </Box>
    )
}
