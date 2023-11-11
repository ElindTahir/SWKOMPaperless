# Use the official image as a parent image for the runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

# Use SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["NPaperless.WebUI.csproj", "./"]
RUN dotnet restore "NPaperless.WebUI.csproj"

# Copy the rest of the source code into the image
COPY . .
RUN dotnet build "NPaperless.WebUI.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "NPaperless.WebUI.csproj" -c Release -o /app/publish

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NPaperless.WebUI.dll"]
