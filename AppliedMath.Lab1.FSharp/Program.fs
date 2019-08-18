open System
open System.IO

let ReadInput fileName =
    File
        .ReadAllText(fileName)
        .Replace('\n', ' ')
        .Replace("  ", " ")
        .Split(' ')
    |> Seq.map (fun s -> s.Replace("\n", ""))
    |> Seq.map float
    |> Seq.sortBy (fun v -> v)

let AverageValue (dataSet) =
    ((dataSet |> Seq.sum) / (float (Seq.length dataSet)))

let CentralMoment step dataSet=
    dataSet
    |> Seq.map (fun x -> pown (x - (AverageValue dataSet)) step)
    |> AverageValue

let Dispersion = CentralMoment 2

let Mode dataSet =
    let maxCount =
        dataSet
        |> Seq.countBy (fun i -> i)
        |> Seq.max
        |> (fun (_, count) -> count)
        
    dataSet
    |> Seq.countBy (fun i -> i)
    |> Seq.filter (fun (_, count) -> count = maxCount)
    |> Seq.map (fun (element, _) -> element)
    |> AverageValue

let Median dataSet =
    if (dataSet |> Seq.length) % 2 = 2
    then dataSet |> Seq.item ((dataSet |> Seq.length) / 2)
    else
        dataSet
        |> Seq.skip ((dataSet |> Seq.length) / 2)
        |> Seq.take 2
        |> AverageValue

let StandardDeviation dataSet =
    dataSet |> Dispersion |> sqrt
    
let Skewness dataSet =
    (dataSet |> CentralMoment 3) / (pown (dataSet |> StandardDeviation) 3)

let Kurtosis dataSet =
    (dataSet |> CentralMoment 4) / (pown (dataSet |> StandardDeviation) 4)

[<EntryPoint>]
let main argv =
    let dataSet = ReadInput "Input.txt"
    
    printfn "Avg: %A"  (dataSet |> AverageValue)
    printfn "Dispersion: %A" (dataSet |> Dispersion)
    printfn "StandardError: %A" ((Dispersion dataSet) / (dataSet |> Seq.length |> float |> sqrt))
    printfn "Mode: %A" (dataSet |> Mode)
    printfn "Q1: %A" (dataSet |> Seq.take ((dataSet |> Seq.length) / 2) |> Median )
    printfn "Q2: %A" (dataSet |> Median)
    printfn "Q3: %A" (dataSet |> Seq.skip ((dataSet |> Seq.length) / 2) |> Median )
    printfn "StandardDeviation: %A" (dataSet |> StandardDeviation)
    printfn "Skewness: %A" (dataSet |> Skewness)
    printfn "Kurtosis: %A" (dataSet |> Kurtosis)
    printfn "Min: %A" (dataSet |> Seq.min)
    printfn "Max: %A" (dataSet |> Seq.max)
    0 // return an integer exit code
