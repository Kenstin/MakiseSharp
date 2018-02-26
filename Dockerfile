FROM microsoft/dotnet:2.0-sdk AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY MakiseSharp/*.csproj ./MakiseSharp/
COPY MakiseSharp.Tests/*.csproj ./MakiseSharp.Tests/
RUN dotnet restore

# copy and build everything else
COPY MakiseSharp/. ./MakiseSharp/
COPY MakiseSharp.Tests/. ./MakiseSharp.Tests/

RUN dotnet build

FROM build AS testrunner
WORKDIR /app/MakiseSharp.Tests
ENTRYPOINT ["dotnet", "test","--logger:trx"]

FROM build AS test
WORKDIR /app/MakiseSharp.Tests
RUN dotnet test

FROM test AS publish
WORKDIR /app/MakiseSharp
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.0-runtime AS runtime
WORKDIR /app
COPY --from=publish /app/MakiseSharp/out ./
ENTRYPOINT ["dotnet", "MakiseSharp.dll"]
