import { create } from "zustand";
import { WeatherForecastResponse } from "./Interfaces";

type State = {
    weatherForecasts: WeatherForecastResponse;
    selectedTimeIndex: number,
    maxLength: number,
    isLoading: boolean,
  }
  
  type Action = {
    updateTimeIndex: (selectedTimeIndex: State['selectedTimeIndex']) => void;
    fetch: (url: string) => Promise<void>;
  }

export const useStore = create<State & Action>((set) => ({
    weatherForecasts: <WeatherForecastResponse>{ stations: [] },
    selectedTimeIndex: 0,
    maxLength: 0,
    isLoading: true,
    fetch: async (url: string) => {
        const response = await fetch(url);
        const data = await response.json();
        set(() => ({ weatherForecasts: data, maxLength: data.stations[0].forecasts.length, isLoading: false }));       
           
    },
    updateTimeIndex: (selectedTimeIndex:number) => set(() => ({ selectedTimeIndex: selectedTimeIndex})) 
}));
