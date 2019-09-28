# diff-tool
A solution for Descartes labs tasks

## Assumptions
* As task did not specify what kind of storage should be used, I am using in memory storage and I simulate async I/O operations
* All validation logic was simplified to the specs, I am not handling all edge cases i.e. inputs that are too large etc.
* There are just a few tests, to showcase how I write tests, but they are by no means complete as code coverage is quite low

## How to run
* Download and install .net Core 3 from https://dotnet.microsoft.com/download/dotnet-core/3.0
* Clone this repository
* In root of this repository, check SDK version by typing `dotnet --version`. It should be "3.0.100"
* If you have installed .net Core 3 SDK and result is different, add global.json to the root with this content:
```json
{
  "sdk": {
    "version": "3.0.100"
  }
}
```
* Run the application by running `dotnet run --project src/Descartes.Demo/Descartes.Demo.csproj` or navigating to `src/Descartes.Demo/` and running `dotnet run`.
* Opening a browser and navigate to `http://localhost:5000`, where a swagger UI should open

## How to test
From the root of the repository folder run `dotnet test`.