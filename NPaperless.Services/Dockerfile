# Use the official ASP.NET Core runtime image from Microsoft
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8081

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ["NPaperless.Services/NPaperless.Services.csproj", "NPaperless.Services/"]
RUN dotnet restore "NPaperless.Services/NPaperless.Services.csproj"

# Copy everything else and build the application
COPY . .
WORKDIR "/src/NPaperless.Services"
RUN dotnet build "NPaperless.Services.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NPaperless.Services.csproj" -c Release -o /app/publish

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NPaperless.Services.dll"]
