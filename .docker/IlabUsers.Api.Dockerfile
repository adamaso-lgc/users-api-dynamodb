FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

COPY "./src/LetsGetChecked.IlabUsers.Api/bin/Release/net6.0/publish" .

EXPOSE 8100
ENTRYPOINT ["dotnet", "LetsGetChecked.IlabUsers.Api.dll"]