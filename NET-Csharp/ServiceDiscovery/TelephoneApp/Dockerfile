FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App
ENV ASPNETCORE_ENVIRONMENT=Development
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
ENV ASPNETCORE_ENVIRONMENT=Development
ENV CONSUL_URI=http://consul:8500
COPY --from=build-env /App/out/ .
EXPOSE 8080
ENTRYPOINT ["dotnet", "TelephoneApp.dll", "--launch-profile=https"]