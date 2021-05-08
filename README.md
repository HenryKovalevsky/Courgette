# Courgette

Courgette is a small library that helps to manage configuration models in the F# scripts.

-----------------------------------------------------------------

Stuffed courgette — [фаршированные](https://fsharp.org/) кабачки.

## Basic Usage Example

```fsharp
#r @"nuget: Courgette"

[<Struct>]
type Config =
  { ConnectionString: string
    ProjectionServiceBaseUri: string }

type Environment =
  | Development
  | Production

let config = Courgette.Struct.init<Config> Environment.Development
```