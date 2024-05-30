# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env
WORKDIR /ASPNETCORETEST
COPY ASPNETCORETEST/*.csproj .
RUN dotnet restore
COPY ASPNETCORETEST .
RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /publish
COPY --from=build-env /publish .
EXPOSE 30351
ENTRYPOINT ["dotnet", "ASPNETCORETEST.dll"]