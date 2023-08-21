FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

WORKDIR /app
EXPOSE 8080
EXPOSE 443
 
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PaymentGateway.API/PaymentGateway.API.csproj", "PaymentGateway.API/"]
RUN dotnet restore "PaymentGateway.API/PaymentGateway.API.csproj"
COPY . .
WORKDIR "/src/PaymentGateway.API"
RUN dotnet build "PaymentGateway.API.csproj" -c Release -o /app/build
 
FROM build AS publish
RUN dotnet publish "PaymentGateway.API.csproj" -c Release -o /app/publish /p:UseAppHost=false
 
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentGateway.API.dll"]