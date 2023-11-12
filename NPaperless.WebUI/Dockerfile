# Use the official ASP.NET Core runtime image from Microsoft
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ["NPaperless.WebUI/NPaperless.WebUI.csproj", "NPaperless.WebUI/"]
RUN dotnet restore "NPaperless.WebUI/NPaperless.WebUI.csproj"

# Copy everything else and build the application
COPY . .
WORKDIR "/src/NPaperless.WebUI"
RUN dotnet build "NPaperless.WebUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NPaperless.WebUI.csproj" -c Release -o /app/publish

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NPaperless.WebUI.dll"]
