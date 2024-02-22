# pv-storagedata-service

The `pv-storagedata-service` is a gRPC service developed for logging storage of data captured through the `pv-pixel-service`. This service provides an efficient and scalable solution for managing and storing data from the pixel capture service.

## Installation

Ensure you have [.NET Core 8](https://dotnet.microsoft.com/download/dotnet/8.0) installed on your machine before proceeding.

1. Clone the repository:

   ```bash
   git clone https://github.com/your-username/pv-storagedata-service.git
   ````
2. Navigate to the project directory:

   ```bash
   cd pv-storagedata-service
   ````
3. Build and run the service:

   ```bash
   dotnet build
   dotnet run
   ````

## Usage

The `pv-storagedata-service` offers a gRPC interface for interaction with the `pv-pixel-service`. Ensure the `pv-pixel-service` is running before using this service.

Example code on how to use the service via gRPC in a .NET application: