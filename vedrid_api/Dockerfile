# Use the official .NET Core 7 SDK as a base image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy the solution file and restore as distinct layers
COPY *.sln .
COPY Vedrid/*.csproj ./Vedrid/
COPY Vedrid.Business/*.csproj ./Vedrid.Business/
COPY Vedrid.Resource/*.csproj ./Vedrid.Resource/
COPY Tests/Vedrid.ResourceTests/*.csproj Tests/Vedrid.ResourceTests/
RUN dotnet restore

# Copy the remaining source code
COPY . .

# Build the application
RUN dotnet build -c Release -o /app
# ENV ASPNETCORE_URLS=http://localhost:5000

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Vedrid.dll"]