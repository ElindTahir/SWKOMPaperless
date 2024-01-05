FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["NPaperless.Services.OCR/NPaperless.Services.OCR.csproj", "NPaperless.Services.OCR/"]
RUN dotnet restore "NPaperless.Services.OCR/NPaperless.Services.OCR.csproj"
COPY . .
WORKDIR "/src/NPaperless.Services.OCR"
RUN dotnet build "NPaperless.Services.OCR.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NPaperless.Services.OCR.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NPaperless.Services.OCR.dll"]
