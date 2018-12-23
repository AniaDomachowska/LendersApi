FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["LendersApi/LendersApi.csproj", "LendersApi/"]
RUN dotnet restore "LendersApi/LendersApi.csproj"
COPY . .
WORKDIR "/src/LendersApi"
RUN dotnet build "LendersApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "LendersApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "LendersApi.dll"]