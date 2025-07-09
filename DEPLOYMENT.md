# 🚀 AquaPomodoro Deployment Guide

This guide will help you deploy the AquaPomodoro backend API to Railway.

## 📋 Prerequisites

- [Railway Account](https://railway.app) (free tier available)
- Your PostgreSQL database (Neon is already set up)
- GitHub repository with your code

## 🏗️ Deployment Files

The project now includes:

- `Dockerfile` - Containerizes the .NET application
- `railway.json` - Railway configuration
- `.dockerignore` - Excludes unnecessary files from Docker build
- `appsettings.Production.json` - Production configuration

## 🚂 Railway Deployment Steps

### 1. Connect to Railway

1. Go to [Railway](https://railway.app)
2. Sign up/Login with GitHub
3. Click "New Project"
4. Select "Deploy from GitHub repo"
5. Choose your `AquaPomodoro` repository
6. Select the `backend` branch

### 2. Set Environment Variables

In your Railway project dashboard, go to **Variables** and add:

```bash
# Required Environment Variables
DATABASE_URL=Host=ep-mute-rice-a944ist1-pooler.gwc.azure.neon.tech;Database=neondb;Username=neondb_owner;Password=npg_yjbQ5VeoG4Zh;SSL Mode=VerifyFull;Channel Binding=Require;

# Optional: Override default settings
ASPNETCORE_ENVIRONMENT=Production
PORT=8080
```

### 3. Deploy

Railway will automatically:
1. Detect the Dockerfile
2. Build the container
3. Deploy your application
4. Provide a public URL

## 🔧 Environment Variables Explained

| Variable | Description | Required |
|----------|-------------|----------|
| `DATABASE_URL` | PostgreSQL connection string | ✅ Yes |
| `ASPNETCORE_ENVIRONMENT` | ASP.NET Core environment | ❌ No (defaults to Production) |
| `PORT` | Application port | ❌ No (Railway sets this) |

## 🌐 Access Your API

After deployment, Railway will provide a URL like:
```
https://your-app-name.railway.app
```

### API Endpoints

- `GET /` - Health check
- `GET /api/health` - Detailed health check
- `POST /api/auth/login` - User login
- `GET /api/users` - Get all users
- `POST /api/users` - Register new user
- And more... (see AquaPomodoro.http)

## 🧪 Testing Deployment

1. **Health Check:**
   ```bash
   curl https://your-app-name.railway.app/
   ```

2. **API Health:**
   ```bash
   curl https://your-app-name.railway.app/api/health
   ```

3. **Database Connection:**
   ```bash
   curl https://your-app-name.railway.app/api/health/database
   ```

## 📚 Alternative Deployment Options

### Docker Anywhere
Use the included `Dockerfile` to deploy anywhere:
```bash
docker build -t aquapomodoro .
docker run -p 8080:8080 -e DATABASE_URL="your-connection-string" aquapomodoro
```

### Azure App Service
1. Create Azure App Service
2. Deploy from GitHub
3. Set connection string in Configuration

### DigitalOcean App Platform
1. Create new app
2. Connect GitHub repository
3. Set environment variables

## 🔧 Troubleshooting

### Common Issues

1. **Database Connection Failed**
   - Check `DATABASE_URL` environment variable
   - Verify Neon database is accessible
   - Check firewall settings

2. **Build Failures**
   - Ensure all NuGet packages are restored
   - Check Dockerfile syntax
   - Verify .NET 9.0 compatibility

3. **CORS Issues**
   - CORS is configured to allow all origins
   - Check frontend URL if needed

### Logs

Check Railway logs:
1. Go to your Railway project
2. Click on your service
3. Go to "Logs" tab
4. Look for startup errors

## 🎯 Production Optimizations

### Security
- [ ] Add authentication middleware
- [ ] Configure CORS for specific domains
- [ ] Add rate limiting
- [ ] Use HTTPS everywhere

### Performance
- [ ] Add caching (Redis)
- [ ] Configure connection pooling
- [ ] Add health checks
- [ ] Monitor with logging

### Database
- [ ] Connection pooling is already configured
- [ ] Consider read replicas for scaling
- [ ] Set up database backups

## 📝 Environment Variables Template

Create a `.env` file for local development:

```env
DATABASE_URL=Host=ep-mute-rice-a944ist1-pooler.gwc.azure.neon.tech;Database=neondb;Username=neondb_owner;Password=npg_yjbQ5VeoG4Zh;SSL Mode=VerifyFull;Channel Binding=Require;
ASPNETCORE_ENVIRONMENT=Development
```

## 🆘 Need Help?

- Check Railway documentation
- Review application logs
- Test database connectivity
- Verify environment variables

---

🎉 **Congratulations!** Your AquaPomodoro API is now deployed and ready to use! 