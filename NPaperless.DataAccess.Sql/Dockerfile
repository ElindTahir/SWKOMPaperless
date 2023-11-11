# Use the official image as a parent image for the runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

# Use SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy the csproj file from the specific project directory
# Adjust this path according to your solution structure
COPY ["NPaperless.DataAccess.Sql/NPaperless.DataAccess.Sql.csproj", "./NPaperless.DataAccess.Sql/"]

# Run dotnet restore
RUN dotnet restore "NPaperless.DataAccess.Sql/NPaperless.DataAccess.Sql.csproj"

# Copy the rest of the source code into the image
# Again, adjust these paths as per your solution structure
COPY ["NPaperless.DataAccess.Sql/", "NPaperless.DataAccess.Sql/"]

# Set the work directory to the project directory
WORKDIR "NPaperless.DataAccess.Sql"

# Build the project
RUN dotnet build "NPaperless.DataAccess.Sql.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "NPaperless.DataAccess.Sql.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Build the runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NPaperless.DataAccess.Sql.dll"]
