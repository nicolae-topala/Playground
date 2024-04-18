FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BlobStorageDemo.API/BlobStorageDemo.API.csproj", "BlobStorageDemo.API/"]
RUN dotnet restore "BlobStorageDemo.API/BlobStorageDemo.API.csproj"
COPY . .
WORKDIR "/src/BlobStorageDemo.API"
RUN dotnet build "BlobStorageDemo.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlobStorageDemo.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlobStorageDemo.API.dll"]