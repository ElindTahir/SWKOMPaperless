# Use the official image as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app


# Use SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Since we are already in the context of NPaperless.Services, just copy the csproj file directly
COPY ["NPaperless.Services.csproj", "./"]
RUN dotnet restore "NPaperless.Services.csproj"

# Copy the rest of the source code into the image
COPY . .
WORKDIR "/src"
RUN dotnet build "NPaperless.Services.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "NPaperless.Services.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Build runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NPaperless.Services.dll"]
