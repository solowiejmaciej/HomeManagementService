FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["HomeManagementService.csproj", "HomeManagementService/"]
RUN dotnet restore "HomeManagementService/HomeManagementService.csproj"
WORKDIR "/src/HomeManagementService"
COPY . .
RUN dotnet build "HomeManagementService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HomeManagementService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HomeManagementService.dll"]

