[![Nuget Version][nuget-ver-badge]][nuget]
[![Nuget Downloads][nuget-dl-badge]][nuget]
[![License][license-badge]](LICENSE)

# TVMaze API ![UNOFFICIAL][unofficial-badge]
Unofficial implementation of TVMaze API.

## Target frameworks
- .NET Standard 2.0
- .NET Framework 4.5.1

## Dependencies
- [Newtonsoft.Json](https://www.newtonsoft.com/json) > 12.0.2

## Limitaions
- Premium capabilities has not been implemented.
- Show Types, Genres, etc. are not strongly typed.

## Installing
Enter following command in your ```NuGet Package Manager```:
```powershell
Install-Package MovieCollection.TVMaze -PreRelease
```

## Get Today's Schedule
1. Define an application wide `HttpClient` if you haven't already.

```csharp
// HttpClient is intended to be instantiated once per application, rather than per-use.
// See https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient
private static readonly HttpClient httpClient = new HttpClient();
```

2. Initialize `TVMazeService`:
```csharp
// Initialize
var service = new MovieCollection.TVMaze.TVMazeService(httpClient);
var results = await service.GetScheduleAsync(country: "GB");

foreach (var item in results)
{
    Console.WriteLine("Id: {0}", item.Id);
    Console.WriteLine("Show Name: {0}", item.Show?.Name);
    Console.WriteLine("Episode Name: {0}", item.Name);
    Console.WriteLine("Network Name: {0}", item.Show?.Network?.Name);
    Console.WriteLine("Summary: {0}", item.Summary);
    Console.WriteLine("******************************");
}
```
### Result:
```
Id: 1694627
Show Name: BBC Proms
Episode Name: Pappano and the National Youth Orchestra USA
Network Name: BBC Four
Summary: <p>Suzy Klein introduces America's most talented young musicians in an ambitious concert from the National Youth Orchestra of the USA. Antonio Pappano conducts the UK premiere of Benjamin Beckman's new work, Occidentalis and mezzo-soprano Joyce DiDonato sings Berlioz's beautifully crafted miniatures Les nuits d'?t?. The impressive programme culminates with Richard Strauss's epic work, An Alpine Symphony.</p>
...
```

You can checkout `Demo` project for more samples.

## Change log
Please visit releases page.

## Acknowledgments
Special thanks to [TVMaze](https://www.tvmaze.com) for providing free API services.

## TVMaze API License
Please read TVMaze API license [here](https://www.tvmaze.com/api).

## License
This project is licensed under the [MIT License](LICENSE).

[nuget]: https://www.nuget.org/packages/MovieCollection.TVMaze
[nuget-ver-badge]: https://img.shields.io/nuget/v/MovieCollection.TVMaze.svg?style=flat
[nuget-dl-badge]: https://img.shields.io/nuget/dt/MovieCollection.TVMaze?color=red
[unofficial-badge]: https://img.shields.io/badge/UNOFFICIAL-red
[license-badge]: https://img.shields.io/github/license/peymanr34/tv-maze.svg?style=flat