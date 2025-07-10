# 🚀 AquaPomodoro Deployment Guide

This guide will help you deploy the AquaPomodoro backend API to Render.

## 📋 Prerequisites

- [Render Account](https://render.com) (free tier available)
- Your PostgreSQL database (Neon is already set up)
- GitHub repository with your code

## 🏗️ Deployment Files

The project now includes:

- `Dockerfile` - Containerizes the .NET application
- `render.yaml` - Render configuration
- `.dockerignore` - Excludes unnecessary files from Docker build
- `appsettings.Production.json` - Production configuration

## 🎨 Render Deployment Steps

### 1. Connect to Render

1. Go to [Render](https://render.com)
2. Sign up/Login with GitHub
3. Click "New +"
4. Select "Web Service"
5. Connect your GitHub account if not already connected
6. Choose your `AquaPomodoro` repository

### 2. Configure Service Settings

**Basic Settings:**
- **Name:** `aquapomodoro-api`
- **Runtime:** `Docker`
- **Branch:** `backend`
- **Root Directory:** Leave empty (uses root)

**Build & Deploy:**
- **Build Command:** (Leave empty - Docker handles this)
- **Start Command:** (Leave empty - Docker handles this)

**Advanced:**
- **Auto Deploy:** `Yes`
- **Health Check Path:** `/`

### 3. Set Environment Variables

In the "Environment" section, add:

```bash
# Required Environment Variables
DATABASE_URL=Host=ep-mute-rice-a944ist1-pooler.gwc.azure.neon.tech;Database=neondb;Username=neondb_owner;Password=npg_yjbQ5VeoG4Zh;SSL Mode=VerifyFull;Channel Binding=Require;

# Optional: Override default settings
ASPNETCORE_ENVIRONMENT=Production
```

### 4. Deploy

1. Click "Create Web Service"
2. Render will automatically:
   - Clone your repository
   - Build the Docker container
   - Deploy your application
   - Provide a public URL

## 🔧 Environment Variables Explained

| Variable | Description | Required |
|----------|-------------|----------|
| `DATABASE_URL` | PostgreSQL connection string | ✅ Yes |
| `ASPNETCORE_ENVIRONMENT` | ASP.NET Core environment | ❌ No (defaults to Production) |

**Note:** Render automatically provides the `PORT` environment variable.

## 🌐 Access Your API

After deployment, Render will provide a URL like:
```
https://aquapomodoro-api.onrender.com
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
   curl https://your-app-name.onrender.com/
   ```

2. **API Health:**
   ```bash
   curl https://your-app-name.onrender.com/api/health
   ```

3. **Database Connection:**
   ```bash
   curl https://your-app-name.onrender.com/api/health/database
   ```

## 📚 Alternative Deployment Options

### Docker Anywhere
Use the included `Dockerfile` to deploy anywhere:
```bash
docker build -t aquapomodoro .
docker run -p 8080:8080 -e DATABASE_URL="your-connection-string" aquapomodoro
```

### Railway
1. Create Railway account
2. Deploy from GitHub
3. Set environment variables

### Azure App Service
1. Create Azure App Service
2. Deploy from GitHub
3. Set connection string in Configuration

## 🔧 Troubleshooting

### Common Issues

1. **Database Connection Failed**
   - Check `DATABASE_URL` environment variable
   - Verify Neon database is accessible
   - Check firewall settings

2. **Build Failures**
   - Check build logs in Render dashboard
   - Ensure all NuGet packages are available
   - Verify .NET 9.0 compatibility

3. **Service Won't Start**
   - Check if PORT is being used correctly
   - Verify Dockerfile syntax
   - Check application logs

4. **CORS Issues**
   - CORS is configured to allow all origins
   - Check frontend URL if needed

### Checking Logs

1. Go to your Render service dashboard
2. Click on "Logs" tab
3. Monitor build and runtime logs
4. Look for startup errors or exceptions

## 🎯 Production Optimizations

### Security
- [ ] Add authentication middleware
- [ ] Configure CORS for specific domains
- [ ] Add rate limiting
- [ ] Use HTTPS everywhere (Render provides this automatically)

### Performance
- [ ] Add caching (Redis)
- [ ] Configure connection pooling
- [ ] Monitor with logging
- [ ] Set up health checks

### Database
- [ ] Connection pooling is already configured
- [ ] Consider read replicas for scaling
- [ ] Set up database backups

## 🆘 Need Help?

- Check Render documentation
- Review service logs in dashboard
- Test database connectivity locally
- Verify environment variables

## 💰 Free Tier Limitations

Render's free tier includes:
- 750 hours per month
- Services sleep after 15 minutes of inactivity
- Cold start delays (takes ~30 seconds to wake up)
- Limited build minutes

For production apps, consider upgrading to a paid plan.

---

🎉 **Congratulations!** Your AquaPomodoro API is now deployed on Render and ready to use! 