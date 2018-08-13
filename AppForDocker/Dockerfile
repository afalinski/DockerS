FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /AppForDocker
COPY *.csproj AppForDocker/
RUN dotnet restore AppForDocker/AppForDocker.csproj
COPY . AppForDocker/
WORKDIR AppForDocker
RUN dotnet build AppForDocker.csproj -c Release -o /app


FROM build AS publish
RUN dotnet publish AppForDocker.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AppForDocker.dll"]
