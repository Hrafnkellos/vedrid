'use client'
import { Box } from '@mui/material'
import Grid from '@mui/material/Unstable_Grid2';
import React from 'react'
import MediaCard from './MediaCard';
import { WeatherForecastResponse } from '../src/Interfaces';

export const WeatherCards = ({ weatherForecast, selectedTimeIndex } : { weatherForecast:WeatherForecastResponse, selectedTimeIndex:number }) => {
  return (
    <Box sx={{ display: 'flex' }}>
    <div>
      <Grid container rowSpacing={3} columnSpacing={3}>
        {weatherForecast.stations.map((station) => (
          <Grid key={station.name} xs={4}>
            <MediaCard
              heading={station.name}
              weatherForecast={station.forecasts[selectedTimeIndex]}
            />
          </Grid>
        ))}
      </Grid>
    </div>
  </Box>
  )
}
