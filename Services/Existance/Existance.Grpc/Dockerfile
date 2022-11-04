#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Existance/Existance.Grpc/Existance.Grpc.csproj", "Services/Existance/Existance.Grpc/"]
RUN dotnet restore "Services/Existance/Existance.Grpc/Existance.Grpc.csproj"
COPY . .
WORKDIR "/src/Services/Existance/Existance.Grpc"
RUN dotnet build "Existance.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Existance.Grpc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Existance.Grpc.dll"]