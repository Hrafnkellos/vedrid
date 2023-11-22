'use client'
import * as React from 'react';
import Image from 'next/image';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import { Forecast } from '../src/Interfaces';

export default function MediaCard({ heading, weatherForecast  } : { heading: string; weatherForecast: Forecast; }) {

  return (
    <Card>
      <Image
        alt="Random image"
        src={"/Images/" + heading + '.jpg'}
        // src="https://source.unsplash.com/random"
        width={640}
        height={480}
        style={{
          maxWidth: '100%',
          height: '200px',
          objectFit: 'cover',
        }}
      />
      <CardContent>
        <Typography gutterBottom variant="h5" component="div">
          {heading}
        </Typography>
        <Typography variant="body1" color="text.secondary">
          Veðurlýsing:{weatherForecast?.weatherDescription ?? "missing info"}
        </Typography>
        <Typography variant="body1" color="text.secondary">
          Hitastig: {weatherForecast?.temperature ?? "missing info"}
        </Typography>
        <Typography variant="body1" color="text.secondary">
          Vindhraði: {weatherForecast?.windSpeed ?? "missing info"}
        </Typography>
        <Typography variant="body1" color="text.secondary">
          Vindátt: {weatherForecast?.windDirection ?? "missing info"}
        </Typography>
      </CardContent>
      {/* <CardActions>
        <Button size="small">Share</Button>
        <Button size="small">Learn More</Button>
      </CardActions> */}
    </Card>
  );
}