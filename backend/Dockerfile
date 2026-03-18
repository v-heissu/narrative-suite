FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY NarrativeSuite.Api/NarrativeSuite.Api.csproj NarrativeSuite.Api/
RUN dotnet restore NarrativeSuite.Api/NarrativeSuite.Api.csproj
COPY . .
WORKDIR /src/NarrativeSuite.Api
RUN dotnet publish -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:5080
EXPOSE 5080
ENTRYPOINT ["dotnet", "NarrativeSuite.Api.dll"]
