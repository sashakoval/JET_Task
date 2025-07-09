# PowerShell script to run the Movie Search Application with Docker

Write-Host "🎬 Movie Search Application - Docker Setup" -ForegroundColor Green
Write-Host "=============================================" -ForegroundColor Green

# Check if Docker is running
Write-Host "Checking Docker status..." -ForegroundColor Yellow
try {
    docker version | Out-Null
    Write-Host "✅ Docker is running" -ForegroundColor Green
} catch {
    Write-Host "❌ Docker is not running. Please start Docker Desktop first." -ForegroundColor Red
    exit 1
}

# Check if .env file exists
if (-not (Test-Path ".env")) {
    Write-Host "📝 Creating .env file..." -ForegroundColor Yellow
    @"
# OMDb API Configuration
OMDB_API_KEY=6a310aa8

# Redis Configuration
REDIS_HOST=redis
REDIS_PORT=6379

# API Configuration
API_PORT=7001
BLAZOR_PORT=7000
"@ | Out-File -FilePath ".env" -Encoding UTF8
    Write-Host "✅ .env file created with default API key" -ForegroundColor Green
    Write-Host "⚠️  Please update the OMDB_API_KEY in .env file with your actual API key" -ForegroundColor Yellow
} else {
    Write-Host "✅ .env file already exists" -ForegroundColor Green
}

# Build and start services
Write-Host "🐳 Building and starting Docker services..." -ForegroundColor Yellow
docker-compose up --build -d

# Wait for services to start
Write-Host "⏳ Waiting for services to start..." -ForegroundColor Yellow
Start-Sleep -Seconds 10

# Check service status
Write-Host "🔍 Checking service status..." -ForegroundColor Yellow

$services = @("redis", "api", "blazor")
foreach ($service in $services) {
    $status = docker-compose ps $service --format "table {{.Status}}"
    if ($status -like "*Up*") {
        Write-Host "✅ $service is running" -ForegroundColor Green
    } else {
        Write-Host "❌ $service is not running" -ForegroundColor Red
    }
}

Write-Host ""
Write-Host "🎉 Application is ready!" -ForegroundColor Green
Write-Host "=============================================" -ForegroundColor Green
Write-Host "🌐 Blazor Frontend: http://localhost:7000" -ForegroundColor Cyan
Write-Host "📚 API Documentation: http://localhost:7001/swagger" -ForegroundColor Cyan
Write-Host "🔍 API Health Check: http://localhost:7001/health" -ForegroundColor Cyan
Write-Host ""
Write-Host "📋 Useful Commands:" -ForegroundColor Yellow
Write-Host "  View logs: docker-compose logs -f" -ForegroundColor White
Write-Host "  Stop services: docker-compose down" -ForegroundColor White
Write-Host "  Restart: docker-compose restart" -ForegroundColor White
Write-Host ""
Write-Host "Press any key to open the application in your browser..."
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")

# Open browser
Start-Process "http://localhost:7000" 