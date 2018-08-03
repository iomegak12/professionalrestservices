FROM microsoft/dotnet:2.1-sdk

COPY . /app

WORKDIR /app

RUN dotnet restore

RUN dotnet build

EXPOSE 8080

ENTRYPOINT dotnet run --project ProductServicesHosting --no-launch-profile
