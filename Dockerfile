FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /app

COPY ./tests/Application.FunctionalTests/*.csproj  ./tests/Application.FunctionalTests/
COPY ./tests/Application.UnitTests/*.csproj ./tests/Application.UnitTests/

COPY ./src/Application/*.csproj ./src/Application/
COPY ./src/Domain/*.csproj ./src/Domain/
COPY ./src/Infrastructure/*.csproj ./src/Infrastructure/

COPY ./src/WebAPI/*.csproj ./src/WebAPI/

RUN dotnet restore ./src/WebAPI/WebAPI.csproj

COPY . .

RUN dotnet publish ./src/WebAPI/WebAPI.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime

EXPOSE 80

WORKDIR /app

COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "WebAPI.dll"]
