FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
WORKDIR /app
EXPOSE 80


COPY Gateways/DefineX.Gateway/*.csproj Gateways/DefineX.Gateway/

RUN dotnet restore Gateways/DefineX.Gateway/*.csproj
COPY . .
RUN dotnet publish Gateways/DefineX.Gateway/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","DefineX.Gateway.dll" ]

