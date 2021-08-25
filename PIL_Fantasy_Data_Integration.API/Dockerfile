#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PIL_Fantasy_Data_Integration.API/PIL_Fantasy_Data_Integration.API.csproj", "PIL_Fantasy_Data_Integration.API/"]
RUN dotnet restore "PIL_Fantasy_Data_Integration.API/PIL_Fantasy_Data_Integration.API.csproj"
COPY . .
WORKDIR "/src/PIL_Fantasy_Data_Integration.API"
RUN dotnet build "PIL_Fantasy_Data_Integration.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PIL_Fantasy_Data_Integration.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PIL_Fantasy_Data_Integration.API.dll"]
