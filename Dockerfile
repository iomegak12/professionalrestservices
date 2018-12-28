FROM microsoft/dotnet:2.2-sdk AS build

COPY . /app

WORKDIR /app

RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.2-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=build /app/ProductServicesHosting/out ./

ENTRYPOINT ["dotnet", "ProductServicesHosting.dll"]
