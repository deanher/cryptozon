FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY Cryptozon.Api/*.csproj ./Cryptozon.Api/
COPY Cryptozon.Domain/*.csproj ./Cryptozon.Domain/
COPY Cryptozon.ApplicationService/*.csproj ./Cryptozon.ApplicationService/
COPY Cryptozon.Infrastructure/*.csproj ./Cryptozon.Infrastructure/
COPY Cryptozon.Test/*.csproj ./Cryptozon.Test/

RUN dotnet restore 

# Copy everything else and build
COPY . .
RUN dotnet publish -c Release -o /app/pub

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY --from=build-env /app/pub .
ENV ASPNETCORE_URLS=http://*:5000
EXPOSE 5000
EXPOSE 1433
ENTRYPOINT ["dotnet", "Cryptozon.Api.dll", "--environment", "Production"]
