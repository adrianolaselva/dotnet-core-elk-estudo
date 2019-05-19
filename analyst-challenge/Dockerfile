#FROM microsoft/dotnet:2.2-sdk-alpine
#COPY *.csproj ./
#RUN dotnet build --output=./bin/out
#COPY ./bin/out /app/out
#ENTRYPOINT ["dotnet", "analyst-challenge.dll"]

FROM microsoft/dotnet:2.2-sdk-alpine AS build-env
WORKDIR /app

#Copiar csproj e restaurar dependencias
COPY *.csproj ./
RUN dotnet restore

#Build da aplicacao
COPY . ./
RUN dotnet publish -c Release -o out

#Build da imagem
FROM microsoft/dotnet:2.2-aspnetcore-runtime-alpine
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "analyst-challenge.dll"]