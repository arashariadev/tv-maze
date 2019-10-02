﻿[![Nuget Version](https://img.shields.io/nuget/v/MovieCollection.TVMaze.svg?style=flat)](https://www.nuget.org/packages/MovieCollection.TVMaze)
[![Nuget Downloads](https://img.shields.io/nuget/dt/MovieCollection.TVMaze?color=red)](https://www.nuget.org/packages/MovieCollection.TVMaze)
[![License](https://img.shields.io/github/license/peymanr34/tv-maze.svg?style=flat)](LICENSE)

# TVMaze API ![UNOFFICIAL](https://img.shields.io/badge/UNOFFICIAL-red)
Minimal implementation of TVMaze API

## Target frameworks
- .Net Standard 2.0
- .Net Framework 4.5.1

## Dependencies
- [Newtonsoft.Json](https://www.newtonsoft.com/json) > 12.0.2

## Limitaions
- Premium capabilities has not been implemented.
- Show Types, Genres, etc. are not strongly typed.

## Installing
Enter following command in your ```NuGet Package Manager```:
```
Install-Package MovieCollection.TVMaze -PreRelease
```

## Get Today's Schedule

```csharp
// Initialize
var service = new MovieCollection.TVMaze.Service();
var results = await service.GetSchedule(country: "GB");

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
**v1.0.0-alpha.2**
- Add `CultureInfo.InvariantCulture` to dates.

**v1.0.0-alpha.1**
- First alpha release.

## Acknowledgments
Special thanks to [TVMaze](https://www.tvmaze.com) for providing free API services.

## TVMaze API License
Please read TVMaze API license [here](https://www.tvmaze.com/api).

## License
This project is licensed under the [MIT License](LICENSE).