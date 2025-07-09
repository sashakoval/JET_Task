FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY ["JET_Task.Blazor/JET_Task.Blazor.csproj", "JET_Task.Blazor/"]

# Restore dependencies
RUN dotnet restore "JET_Task.Blazor/JET_Task.Blazor.csproj"

# Copy source code
COPY . .
WORKDIR "/src/JET_Task.Blazor"

# Build the application
RUN dotnet build "JET_Task.Blazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JET_Task.Blazor.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use a simple HTTP server
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Install a simple HTTP server
RUN apt-get update && apt-get install -y python3 && rm -rf /var/lib/apt/lists/*

# Copy the published app
COPY --from=publish /app/publish/wwwroot .

# Create a simple startup script
RUN echo '#!/bin/bash\ncd /app && python3 -m http.server 80' > /app/start.sh && chmod +x /app/start.sh

EXPOSE 80

ENTRYPOINT ["/app/start.sh"] 