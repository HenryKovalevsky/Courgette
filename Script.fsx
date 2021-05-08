#r @"bin\Debug\netstandard2.1\Courgette.dll"

[<Struct>]
type Config =
  { ConnectionString: string
    ProjectionServiceBaseUri: string }

type Environment =
  | Development
  | Production

let config = Courgette.Struct.init<Config> Environment.Development