# Movie Search Application

A modern web application for searching movies using the OMDb API, built with .NET Core and Blazor WebAssembly following Clean Architecture and Domain-Driven Design principles.

## 🏗️ Architecture

This project follows **Clean Architecture** and **DDD** principles with the following structure:

```
JET_Task/
├── src/
│   ├── JET_Task.Domain/          # Domain Layer (Entities, Interfaces)
│   ├── JET_Task.Application/      # Application Layer (Use Cases, DTOs)
│   ├── JET_Task.Infrastructure/  # Infrastructure Layer (External APIs, Redis)
│   ├── JET_Task.Api/             # API Layer (Minimal APIs)
│   └── JET_Task.Blazor/          # Frontend (Blazor WebAssembly)
└── tests/
    ├── JET_Task.UnitTests/       # Backend Unit Tests
    └── JET_Task.BlazorTests/     # Frontend Unit Tests
```

## 🚀 Features

- **Movie Search**: Search movies by title using OMDb API
- **Search History**: Automatically saves and displays the 5 latest search queries
- **Movie Details**: View detailed movie information (poster, plot, IMDb rating, etc.)
- **Modern UI**: Responsive Blazor WebAssembly frontend
- **Redis Integration**: Fast search query storage and retrieval
- **Minimal APIs**: High-performance, low-overhead API endpoints
- **Docker Support**: Complete containerization with docker-compose

## 🛠️ Technology Stack

### Backend
- **.NET 8** with Minimal APIs
- **Clean Architecture** + **DDD**
- **Redis** for search query storage
- **OMDb API** for movie data
- **Swagger/OpenAPI** for API documentation

### Frontend
- **Blazor WebAssembly**
- **Bootstrap** for responsive design
- **HttpClient** for API communication

### DevOps
- **Docker** for containerization
- **docker-compose** for orchestration
- **nginx** for serving Blazor app

## 📋 Prerequisites

- .NET 8 SDK (for local development)
- Docker and Docker Compose (for containerized deployment)
- OMDb API Key (free at http://www.omdbapi.com/)

## 🔧 Setup

### Option 1: Docker (Recommended)

#### 1. Clone and Navigate
```bash
git clone <repository-url>
cd JET_Task
```

#### 2. Set Environment Variables
Create a `.env` file in the root directory:
```bash
# OMDb API Configuration
OMDB_API_KEY=your_omdb_api_key_here

# Redis Configuration
REDIS_HOST=redis
REDIS_PORT=6379

# API Configuration
API_PORT=7001
BLAZOR_PORT=7000
```

#### 3. Run with Docker Compose
```bash
# Build and start all services
docker-compose up --build

# Or run in detached mode
docker-compose up -d --build
```

#### 4. Access the Application
- **API Documentation**: http://localhost:7001/swagger
- **Blazor Frontend**: http://localhost:7000
- **Redis**: localhost:6379

#### 5. Stop Services
```bash
docker-compose down
```

### Option 2: Local Development

#### 1. Clone and Build
```bash
git clone <repository-url>
cd JET_Task
dotnet restore
dotnet build
```

#### 2. Configure API Key
Update `src/JET_Task.Api/appsettings.json`:
```json
{
  "OmdbApi": {
    "ApiKey": "your-omdb-api-key-here"
  }
}
```

#### 3. Configure Redis
Update the Redis connection string in `src/JET_Task.Api/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "Redis": "localhost:6379"
  }
}
```

#### 4. Run the Application

**Terminal 1 - API:**
```bash
cd src/JET_Task.Api
dotnet run
```

**Terminal 2 - Blazor Frontend:**
```bash
cd src/JET_Task.Blazor
dotnet run
```

## 🌐 API Endpoints

The application uses **Minimal APIs** for high performance:

### Search Movies
```
GET /api/movies/search?title={title}
```

### Get Movie Details
```
GET /api/movies/{imdbId}
```

### Get Latest Queries
```
GET /api/movies/latest-queries
```

### Health Check
```
GET /health
```

## 🐳 Docker Commands

### Build Individual Services
```bash
# Build API
docker build -f src/JET_Task.Api/Dockerfile -t jet-task-api ./src

# Build Blazor
docker build -f src/JET_Task.Blazor/Dockerfile -t jet-task-blazor ./src
```

### Run Individual Containers
```bash
# Run Redis
docker run -d -p 6379:6379 --name redis redis:7-alpine

# Run API
docker run -d -p 7001:80 --name api --env-file .env jet-task-api

# Run Blazor
docker run -d -p 7000:80 --name blazor jet-task-blazor
```

### Docker Compose Commands
```bash
# Start all services
docker-compose up

# Start in detached mode
docker-compose up -d

# View logs
docker-compose logs -f

# Stop all services
docker-compose down

# Rebuild and start
docker-compose up --build

# Remove volumes (clears Redis data)
docker-compose down -v
```

## 🧪 Testing

### Run Unit Tests
```bash
dotnet test
```

### Run Tests in Docker
```bash
# Build and run tests in container
docker build -f src/JET_Task.Api/Dockerfile -t test-api ./src
docker run test-api dotnet test
```

## 📁 Project Structure Details

### Domain Layer (`JET_Task.Domain`)
- **Entities**: `Movie`, `SearchQuery`
- **Interfaces**: `IMovieRepository`, `ISearchQueryRepository`
- Pure business logic with no external dependencies

### Application Layer (`JET_Task.Application`)
- **DTOs**: Data transfer objects for API responses
- **Services**: `MovieService` orchestrates domain operations
- **Interfaces**: Application service contracts

### Infrastructure Layer (`JET_Task.Infrastructure`)
- **Services**: `OmdbApiService` for external API calls
- **Repositories**: `MovieRepository`, `RedisSearchQueryRepository`
- **Models**: OMDb API response models

### API Layer (`JET_Task.Api`)
- **Minimal APIs**: High-performance endpoint definitions
- **Endpoints**: `MovieEndpoints.cs` contains all route definitions
- **Configuration**: Dependency injection, CORS, Redis setup

### Blazor Frontend (`JET_Task.Blazor`)
- **Pages**: Main search interface
- **Services**: `MovieService` for API communication
- **Models**: Frontend data models

## 🔄 Key Improvements in This Refactor

### 1. **Minimal APIs**
- **Performance**: Lower overhead, faster startup
- **Simplicity**: Less boilerplate code
- **Modern**: Microsoft's recommended approach

### 2. **Docker Support**
- **Containerization**: Complete Docker setup
- **Orchestration**: docker-compose for easy deployment
- **Production Ready**: Optimized for containerized environments

### 3. **Better Error Handling**
- Structured error responses
- Comprehensive logging
- Graceful fallbacks

### 4. **Enhanced Documentation**
- OpenAPI/Swagger integration
- Detailed endpoint descriptions
- Response type specifications

### 5. **Clean Architecture**
- Clear separation of concerns
- Dependency inversion
- Testable design

## 🚀 Performance Benefits

- **Minimal APIs**: ~20% faster startup time
- **Redis**: Sub-millisecond query storage
- **Blazor WebAssembly**: Client-side rendering
- **HttpClientFactory**: Optimized HTTP connections
- **Docker**: Consistent deployment environments

## 🔧 Development

### Adding New Features
1. Add domain entities/interfaces
2. Implement application services
3. Create infrastructure implementations
4. Add Minimal API endpoints
5. Update Blazor components
6. Write unit tests
7. Update Docker configuration if needed

### Code Quality
- Follow Clean Architecture principles
- Use dependency injection
- Write comprehensive tests
- Document public APIs

## 📝 License

This project is for educational purposes and demonstrates modern .NET development practices.

## 🤝 Contributing

1. Follow the existing architecture patterns
2. Add tests for new features
3. Update documentation
4. Ensure all tests pass
5. Test with Docker deployment

---
## Screenshots

![Movie Search Interface](docs/image.png)

**Built with ❤️ using .NET 8, Blazor, Clean Architecture, and Docker**