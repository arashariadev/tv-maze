# TVMaze API
Unofficial implementation of TVMaze API.

[![Nuget Version][nuget-shield]][nuget]
[![Nuget Downloads][nuget-shield-dl]][nuget]

## Installing
You can install this package by entering the following command into your `Package Manager Console`:
```powershell
Install-Package MovieCollection.TVMaze -PreRelease
```

## How to use
First, define an instance of the `HttpClient` class if you haven't already.

```csharp
// HttpClient is intended to be instantiated once per application, rather than per-use.
// See https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient
private static readonly HttpClient httpClient = new HttpClient();
```

Then, for getting today's schedule:

```csharp
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

Please checkout the `Demo` project for more examples.

## Limitations
- Premium capabilities has not been implemented.
- Show Types, Genres, etc. are not strongly typed.

## Notes
- Thanks to [TVMaze][tvmaze] for providing free API services.
- Please read [TVMaze API license][tvmaze-license] before using their API.

## License
This project is licensed under the [MIT License](LICENSE).

[nuget]: https://www.nuget.org/packages/MovieCollection.TVMaze
[nuget-shield]: https://img.shields.io/nuget/v/MovieCollection.TVMaze.svg?label=Release
[nuget-shield-dl]: https://img.shields.io/nuget/dt/MovieCollection.TVMaze?label=Downloads&color=red

[tvmaze]: https://www.tvmaze.com
[tvmaze-license]: https://www.tvmaze.com/api
