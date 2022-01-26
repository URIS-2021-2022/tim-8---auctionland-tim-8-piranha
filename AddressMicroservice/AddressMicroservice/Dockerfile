#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["AddressMicroservice/AddressMicroservice.csproj", "AddressMicroservice/"]
RUN dotnet restore "AddressMicroservice/AddressMicroservice.csproj"
COPY . .
WORKDIR "/src/AddressMicroservice"
RUN dotnet build "AddressMicroservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AddressMicroservice.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AddressMicroservice.dll"]