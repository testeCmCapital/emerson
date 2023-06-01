# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

# copiar csproj e restaurar os packages e camadas distintas
COPY *.sln .
COPY ./src/services/Services/Services.csproj ./src/services/Services/Services.csproj
COPY ./src/infrastructure/ ./src/infrastructure/
COPY ./src/domain/ ./src/domain/
COPY ./src/test/ ./src/test/
RUN dotnet restore

# copiar tudo e entao construir o app
COPY . .
WORKDIR /source/src/services/Services
RUN dotnet build "Services.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c release -o /app/publish --self-contained false --no-restore

# passo final
FROM base AS final
RUN apk add --no-cache bash icu-libs krb5-libs libgcc libintl libssl1.1 libstdc++ zlib
# RUN apk add libgdiplus-dev --update-cache --repository https://dl-3.alpinelinux.org/alpine/edge/testing/ --allow-untrusted
RUN apk add libgdiplus --repository http://dl-cdn.alpinelinux.org/alpine/edge/testing/
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Services.dll"]