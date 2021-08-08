FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

copy . ./
WORKDIR /app
RUN dotnet publish -c Release -o out 


FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "AuthenticationService.dll"]


