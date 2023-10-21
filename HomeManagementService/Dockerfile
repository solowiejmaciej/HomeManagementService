FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ReportingServiceWorker.csproj", "ReportingServiceWorker/"]
RUN dotnet restore "ReportingServiceWorker/ReportingServiceWorker.csproj"
WORKDIR "/src/ReportingServiceWorker"
COPY . .
RUN dotnet build "ReportingServiceWorker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReportingServiceWorker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReportingServiceWorker.dll"]

