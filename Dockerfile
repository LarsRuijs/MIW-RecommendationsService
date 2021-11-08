FROM mcr.microsoft.com/dotnet/sdk:6.0  AS build-env

WORKDIR /app
COPY ./ ./
RUN dotnet publish \
  -c Release \
  -o ./output

FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app
COPY --from=build-env /app/output .

EXPOSE 5002
ENTRYPOINT ["dotnet", "MIW-CustomerGateway.Api.dll"]