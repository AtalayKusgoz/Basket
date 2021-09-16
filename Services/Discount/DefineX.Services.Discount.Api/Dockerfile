FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
WORKDIR /app
EXPOSE 80

COPY Shared/Shared/*.csproj Shared/Shared/
COPY Services/Discount/DefineX.Services.Discount.Business/*.csproj Services/Discount/DefineX.Services.Discount.Business/
COPY Services/Discount/DefineX.Services.Discount.DataAccess/*.csproj Services/Discount/DefineX.Services.Discount.DataAccess/
COPY Services/Discount/DefineX.Services.Discount.Entities/*.csproj Services/Discount/DefineX.Services.Discount.Entities/

COPY Services/Discount/DefineX.Services.Discount.Api/*.csproj Services/Discount/DefineX.Services.Discount.Api/

RUN dotnet restore Services/Discount/DefineX.Services.Discount.Api/*.csproj
COPY . .
RUN dotnet publish Services/Discount/DefineX.Services.Discount.Api/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","DefineX.Services.Discount.Api.dll" ]

