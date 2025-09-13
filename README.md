# Metalflux Web App

## Intro

This project involves a web application of video streaming. It's separated into three subproject : the front-end, the ASP.NET back-end API REST and a S3 service to store files.

## How to get started

### Requirements

For this project you'll need to :

- Configure an environment for C# development and choose and IDE, personnaly I use [JetBrains' Rider](https://www.jetbrains.com/rider/).
- Install [Docker](https://www.docker.com/get-started/).
- Install [Node.js](https://nodejs.org/en/download)

### Step-by-step

1. Start the S3 service using docker compose (you can configure ports and credentials inside docker-compose.yaml)
```bash
cd ./ServiceS3
docker compose up -d
# -d option to run compose in the background
```

2. Start the ASP.NET API using your IDE or using this command
```bash
cd ./MetalfluxApi/MetalfluxApi

#These profiles are defined in `launchSettings.json` file

dotnet run --launch-profile "https"
# or if you don't want HTTPS
dotnet run --launch-profile "http"
```

3. Start the front-end
```bash
cd ./Webapp

yarn
yarn dev
# or
npm install
npm run dev
```