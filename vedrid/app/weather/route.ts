export async function GET(request: Request) {   
	const res = await fetch('http://localhost:5400/weather-forecasts', )
  	const data = await res.json()
 
  	return Response.json( data )
}