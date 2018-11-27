# jtocsvoc
console application in C# to retrieve the data from web site and save it as a CSV file

# start
You have to install .net core 2.1 (https://www.microsoft.com/net/download/dotnet-core/2.1). Then download source code from this repository via git clone or via http. Open console from solution folder. Move to ```Console``` forlder. Run ```dotnet run```. Like:

```
git clone https://github.com/vladimir-shmidt/jtocsvoc.git
cd gmtinterview
cd Console
gotnet run
```
This command will run console application. So as no parametrs was given to console it will show help screen like:
```
Console 1.0.0
Copyright (C) 2018 Console
ERROR(S):
Required option 'h, host' is missing.
Required option 'o, output' is missing.

  -h, --host      Required. Host URL where to download data from

  -o, --output    Required. Result csv file path

  --help          Display this help screen.

  --version       Display version information.
```
You can specify parameters with a help of -- flags like:
```
dotnet run -- -h http://example.com/data.json -o output.csv
```
