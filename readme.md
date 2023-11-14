# Vedrid - Portfolio Project

The app browses Icelandic weather data from https://www.vedur.is/um-vi/vefurinn/xml/
This app is only designed for the all green path and is not handling errors that might come up.

## Project Milestones/Features

### A priority tasks

* [X] Setup Documentation
* * [ ] Configuration
* * [ ] Project Structure
* * [ ] External Resources
* [X] Setup Project boilerplate + repository
* * [x] editorconfig
* * [x] gitignore
* * [x] swagger
* * [ ] DockerFile
* [X] Research integration endpoint and create a design
* [ ] Implement Resource
* [ ] Implement Service layer
* [ ] Implement Tests
* [ ] Implement Console Interface

### B priority tasks
* [ ] Implement API Layer
* [ ] Finish the react client so that is fetches from the api
* [ ] Add sentry logging
* [ ] AWS Deployment
* [ ] Github Actions build
* [ ] Test Mini profiler. https://miniprofiler.com/

## Tools

* The boilerplate for the front end we get from next.js it's a fairly popular react scaffolding system. 
https://nextjs.org/

* We will use dotnet core as a backend to fetch data from vedur.is to pass data to the frontend.
https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-7.0&tabs=visual-studio-code

# vedur.is resource

To start i would like to say the [documentation from vedur.is](https://www.vedur.is/media/vedurstofan/XMLthjonusta.pdf) could be better. We can't get a list of ids from a web service call, we have to scrape or manually get ids from the website. But we will write some documentation here on what we discover about the resource and how we are going to use it.

# The Request
How are we going to get the data we want? We have a few options. We start with a basic request and then we want to add maybe 1 or 2 optional parameters to get different results from the service.

Check out the http [vscode rest client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) file to test the endpoint yourself: [veduris.http](Documentation/veduris.http)

BasePath with required parameters: 

https://xmlweather.vedur.is?op_w=xml&type=forec&lang=is&view=xml&ids=1;422

## Parameters

###  Ids - weather station ids

Parameter to add to path  `&ids=1;422;2642`

Source data: https://www.vedur.is/vedur/stodvar

Since we can cant get a list of ids we will just list a few popular places.

| ID | Description |
| -- | -- |
| 1 | Reykjavík |
| 2642 | Ísafjörður |
| 422 | Akureyri |
| 571 | Egilsstaðir |
| 5544 | Höfn í Hornafirði |

### Lang

Parameter to add to path  `&lang=is`

Language is ``is`` for icelandic or ``en`` english.

### Type - forcast type

Parameter to add to path  `&type=forec`

What type of forecast does the user want? we will only use forc. changing type we change the return object completely.

| Type | Description |
| ---  | -- |
| obs | Veðurathuganir |
| forec | Sjálfvirkar veðurspár  |
| txt | Textaspár og lýsingar |
| forec-info | Upplýsingar um ve| ðurspálíkön |

### Time (optional) - how many hours to predict

Parameter to added to path `&time=1h`

Time between measurements is always in hours. For automatic measurements 1h is common. Fer manned posts its more likely to jump every 3h.

Default value = **"1h"**

### Params - what data to show   

Here we can filter all kinds of parameters we want returned to us. Not every parameter is returned for every type.

| Tákn | Mælistærð og eining |
|-|-|
| F | Vindhraði (m/s) |
| FX | Mesti vindhraði (m/s) |
| FG | Mesta vindhviða (m/s) |
| D | Vindstefna (sjá lista af skammstöfunum aftar í skjalinu) |
| T | Hiti (°C) |
| W | Veðurlýsing (sjá lista af lýsingum aftar í skjalinu) |
| V | Skyggni (km) |
| N | Skýjahula (%) |
| P | Loftþrýstingur (hPa) |
| RH | Rakastig (%) |
| SNC | Lýsing á snjó |
| SND | Snjódýpt (cm) |
| SED | Sjólag |
| RTE | Vegahiti (°C) |
| TD | Daggarmark (°C) |
| R | Uppsöfnuð úrkoma (mm / klst) úr sjálfvirkum mælum. |


# The Response 
What data are we going to be displaying?

``` XML
<?xml version="1.0" encoding="UTF-8"?>
<forecasts>
  <station id="1"  valid="1">
    <name>Reykjavík</name>
    <atime>2023-11-14 06:00:00</atime>
    <err></err>
    <link>
      <![CDATA[http://www.vedur.is/vedur/spar/stadaspar/hofudborgarsvaedid/#group=100&station=1]]>
    </link>
    <forecast>
      <ftime>2023-11-14 07:00:00</ftime>
      <F>9</F>
      <D>A</D>
      <T>6</T>
      <W>Alskýjað</W>
    </forecast>
    <forecast>
      <ftime>2023-11-14 08:00:00</ftime>
      <F>9</F>
      <D>A</D>
      <T>5</T>
      <W>Alskýjað</W>
    </forecast>
    ...
<forecasts>
```

## Api Design

Vedrid API documentation

## Get list of forecasts

### Request

`GET /forecasts?ids={ids:all}&time={time:1}&lang={lang:is}`

    curl -i -H 'Accept: application/json' http://localhost:7000/forecasts

### Response 

    HTTP/1.1 200 OK
    Date: Thu, 24 Feb 2011 12:36:30 GMT
    Status: 200 OK
    Connection: close
    Content-Type: application/json
    Content-Length: 2

    {
        "locations": 
        [
            {
                "id": 1,
                "name": "Reykjavík",
                "fromTime","2023-11-14 06:00:00"
                "forecasts": 
                [
                    {
                        "time": "2023-11-14 08:00:00"
                        "temperature": 5,
                        "windSpeed": 9,
                        "windDirection": "East"
                    },
                    ...
                ]
            },
            ...
        ]
    }


## Generate class from XML

As described here https://stackoverflow.com/a/4203551 creating a class from an xml document is easy. 

Visual studio installation is required. It's easiest to install the community version. OR you might have it and need to add the correct folder to **environmental path**

```
C:\Program Files (x86)\Microsoft SDKs\Windows\{version}\bin\NETFX {version} Tools\
```

We first create an XSD definition: `c:\path> xsd ExampleResponse.xml`

Then we create the class: `c:\path> xsd ExampleResponse.xsd /classes`