FROM mcr.microsoft.com/dotnet/sdk:5.0  AS build-env

WORKDIR /app
COPY ./ ./
RUN dotnet publish \
  -c Release \
  -o ./output

FROM mcr.microsoft.com/dotnet/aspnet:5.0

WORKDIR /app
COPY --from=build-env /app/output .

EXPOSE 5012
ENTRYPOINT ["dotnet", "MIW-RecommendationsService.Api.dll"]