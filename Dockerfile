FROM microsoft/dotnet:2.0.7-sdk-2.1.105-nanoserver-1709

COPY . /app

WORKDIR /app

RUN dotnet restore

RUN dotnet build

EXPOSE 8080

ENTRYPOINT dotnet run --project ProductServicesHosting --no-launch-profile
