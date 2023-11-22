interface WeatherForecastResponse {
	stations: WeatherStations[];
}

interface WeatherStations {
	id: number;
	name: string;
	fromTime: Date,
	forecasts: Forecast[]
}

interface Forecast {
	time: string,
	temperature: number,
	windSpeed: number,
	windDirection: string,
	weatherDescription: string
}


export type { WeatherForecastResponse, WeatherStations, Forecast }