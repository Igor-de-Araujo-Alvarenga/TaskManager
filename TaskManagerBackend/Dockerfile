FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ./TaskManager.API/TaskManager.API.csproj                         ./TaskManager.API/
COPY ./TaskManager.Application/TaskManager.Application.csproj         ./TaskManager.Application/
COPY ./TaskManager.Domain/TaskManager.Domain.csproj                   ./TaskManager.Domain/
COPY ./TaskManager.Infrastructure/TaskManager.Infrastructure.csproj   ./TaskManager.Infrastructure/
COPY ./TaskManager.Tests/TaskManager.Tests.csproj                     ./TaskManager.Tests/
COPY ./TaskManager.sln                                                ./

RUN dotnet restore ./TaskManager.sln

COPY ./ ./

WORKDIR /src/TaskManager.API
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "TaskManager.API.dll"]