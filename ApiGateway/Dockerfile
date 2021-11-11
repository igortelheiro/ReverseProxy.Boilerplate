#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80 9010

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MGR.ApiGateway/MGR.ApiGateway.csproj", "MGR.ApiGateway/"]
RUN dotnet restore "MGR.ApiGateway/MGR.ApiGateway.csproj"
COPY . .
WORKDIR "/src/MGR.ApiGateway"
RUN dotnet build "MGR.ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MGR.ApiGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MGR.ApiGateway.dll"]
