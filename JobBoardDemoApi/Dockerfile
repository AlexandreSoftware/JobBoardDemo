FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["JobBoardDemo/JobBoardDemo.csproj", "JobBoardDemo/"]
RUN dotnet restore "JobBoardDemo/JobBoardDemo.csproj"
COPY . .
WORKDIR "/src/JobBoardDemo"
RUN dotnet build "JobBoardDemo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JobBoardDemo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JobBoardDemo.dll"]
