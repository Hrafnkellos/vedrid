'use client' 
import { Box, Slider, Typography } from '@mui/material';
import Grid from '@mui/material/Unstable_Grid2';
import MediaCard from './components/MediaCard';
import React, { useEffect } from 'react';
import { WeatherForecastResponse } from './src/Interfaces';
import { useStore } from './src/store';
import { TimeSlider } from './components/TimeSlider';
import { WeatherCards } from './components/WeatherCards';

// async function getForecasts(): Promise<WeatherForecastResponse> {
// 	return await (await fetch('http://localhost:3000/weather', {
// 		cache: "no-store",
// 	})).json();
// }

// function valuetext(value: number) {
// 	return `${value}°C`;
// }


export default function HomePage() {
	// const [data, setData] = useState(null)
	// const [isLoading, setLoading] = useState(true)
	
	const state = useStore();

	useEffect(() => {
		state.fetch('http://localhost:3000/weather');
	}, []);

	// await 
	// const weatherForecast: WeatherForecastResponse = await getForecasts();
	// const sliderMaxValue = weatherForecast?.stations[0]?.forecasts?.length ?? 0;
	// state.setState({ weatherForecasts: weatherForecast });
	// const maxVal = useStore((state) => state.selectedTimeIndex)


	// const forecasts = useStore((state)=> state.weatherForecasts);
	// TODO Move slide to client component
	// TODO Move card repeater to client component

	if (state.isLoading) return <p>Loading...</p>
	if (state.maxLength <= 0) return <p>No profile data</p>

	return (
		<div>
			
			<TimeSlider
				maxValue={state.maxLength-1}
				onChange={(_: Event, v: number): void => state.updateTimeIndex( v )}
			/>
			<Typography id="discrete-slider" gutterBottom variant="h5" component="div">
				Date: {state.weatherForecasts.stations[0].forecasts[state.selectedTimeIndex].time.split('T')[0]} 
			</Typography>
			<Typography id="discrete-slider" gutterBottom variant="h5" component="div">
				Time: {state.weatherForecasts.stations[0].forecasts[state.selectedTimeIndex].time.split('T')[1]} 
			</Typography>

			<WeatherCards
				weatherForecast={state.weatherForecasts}
				selectedTimeIndex={state.selectedTimeIndex}
			/>

		</div>
	);
}
