module Courgette

open System
open System.IO
open System.Diagnostics

// todo: do not use external dependencies for serialization.
open Newtonsoft.Json

let [<Literal>] private Subfolder = ".courgette"

let private openFile path =
  use proc = new Process(StartInfo = ProcessStartInfo(path, UseShellExecute = true))

  if proc.Start()
  then proc.WaitForExit()

let private structInit<'model when 'model : struct> (fileName : string) (prompt : bool) =
  let path = Path.Combine(Environment.CurrentDirectory, Subfolder, fileName)
  let dir = Path.GetDirectoryName path

  if not (Directory.Exists dir)
  then ignore (Directory.CreateDirectory dir)

  let isNew = not (File.Exists path)

  if isNew then 
    let empty = Unchecked.defaultof<'model>
    let json = JsonConvert.SerializeObject(empty, Formatting.Indented)  
    File.WriteAllText(path, json)
      
  if isNew || prompt
  then openFile path

  let json = File.ReadAllText(path)

  JsonConvert.DeserializeObject<'model>(json)

type Struct =
  class end with

  /// <summary>Initialize model.</summary>
  /// <param name="name">Model file name.</param>
  static member init (name : obj) = structInit $"%O{name}.json" false

  /// <summary>Initialize model.</summary>
  /// <param name="path">Model file path.</param>
  static member init (path : string) = structInit path false
    
  /// <summary>Initialize model.</summary>
  /// <param name="name">Model file name.</param>
  /// <param name="prompt">Prompt user for input.</param>
  static member init (name : obj, prompt : bool) = structInit $"%O{name}.json" prompt

  /// <summary>Initialize model.</summary>
  /// <param name="path">Model file path.</param>
  /// <param name="prompt">Prompt user for input.</param>
  static member init (path : string, prompt : bool) = structInit path prompt