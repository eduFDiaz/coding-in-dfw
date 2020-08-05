#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:3.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:3.1-sdk  AS build
WORKDIR /src
COPY ["coding.API.csproj", "coding.API.csproj"]

RUN dotnet restore "coding.API.csproj"
COPY . .
RUN dotnet build "coding.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "coding.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "coding.API.dll"]