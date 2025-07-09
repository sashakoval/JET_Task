FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY ["JET_Task.Api/JET_Task.Api.csproj", "JET_Task.Api/"]
COPY ["JET_Task.Application/JET_Task.Application.csproj", "JET_Task.Application/"]
COPY ["JET_Task.Domain/JET_Task.Domain.csproj", "JET_Task.Domain/"]
COPY ["JET_Task.Infrastructure/JET_Task.Infrastructure.csproj", "JET_Task.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "JET_Task.Api/JET_Task.Api.csproj"

# Copy source code
COPY . .
WORKDIR "/src/JET_Task.Api"

# Build the application
RUN dotnet build "JET_Task.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JET_Task.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:80;https://+:443
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "JET_Task.Api.dll"] 