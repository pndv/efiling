FROM ghcr.io/pndv/tex-docker/texlive-dotnet:latest as base

WORKDIR /app
EXPOSE 80
EXPOSE 443

#####################################

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["EFilingWeb/EFilingWeb.csproj", "EFilingWeb/"]
RUN dotnet restore "EFilingWeb/EFilingWeb.csproj"
COPY . .
WORKDIR "/src/EFilingWeb"
RUN dotnet build "EFilingWeb.csproj" -c Release -o /app/build

#####################################

FROM build AS publish
RUN dotnet publish "EFilingWeb.csproj" -c Release -o /app/publish

#####################################

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EFilingWeb.dll"]
