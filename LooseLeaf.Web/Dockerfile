#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["LooseLeaf.Web/LooseLeaf.Web.csproj", "LooseLeaf.Web/"]
COPY ["LooseLeaf.Business/LooseLeaf.Business.csproj", "LooseLeaf.Business/"]
COPY ["LooseLeaf.DataAccess/LooseLeaf.DataAccess.csproj", "LooseLeaf.DataAccess/"]
RUN dotnet restore "LooseLeaf.Web/LooseLeaf.Web.csproj"
COPY . .
WORKDIR "/src/LooseLeaf.Web"
RUN dotnet build "LooseLeaf.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LooseLeaf.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LooseLeaf.Web.dll"]