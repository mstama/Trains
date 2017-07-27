# Create and configura a docker image to run the program
FROM microsoft/dotnet:latest AS build-env
WORKDIR /app

# Add all files but those included in docker ignore
COPY *.csproj ./
RUN dotnet restore

# Copy other files and build
ADD . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/dotnet:runtime
WORKDIR /app
COPY --from=build-env /app/out ./
ENTRYPOINT ["dotnet", "Trains.dll", "input.txt"]