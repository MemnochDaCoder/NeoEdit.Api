# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
# For more information, please see https://aka.ms/containercompat

# Use the ASP.NET Core runtime image from Microsoft Container Registry (MCR).
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the .NET SDK image to build the application code.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NeoEdit.Api.csproj", "./"]
RUN dotnet restore "NeoEdit.Api.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "NeoEdit.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application.
FROM build AS publish
RUN dotnet publish "NeoEdit.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage/image.
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NeoEdit.Api.dll"]
