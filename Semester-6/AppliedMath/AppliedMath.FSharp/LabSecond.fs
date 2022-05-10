module LabSecond
open MathNet.Numerics.LinearAlgebra

let LengthToVector (a: Vector<double> ,b: Vector<double>) =
    a.Subtract(b)
    |> Seq.map (fun x -> pown x 2)
    |> Seq.average
    |> sqrt

let RunBrutForce (matrix: Matrix<double>, vect: Vector<double>) =
    let rec inside (a: Vector<double>, b: Vector<double>) =
        if LengthToVector(a, b) < 1e-7
        then b
        else inside(b, matrix.Multiply(b))

    inside(vect, matrix.Multiply(vect))

let SolverWithSystemOfLinearEquations (matrix: Matrix<float>) =
    let mutable m = matrix
    for i in 0..m.RowCount - 1 do
        m.[i, i] <- m.[i, i] - 1.0
    
    m <- m.InsertRow(m.RowCount, Vector.Build.Dense(m.ColumnCount, 1.0))
    let result = Vector.Build.Dense(m.RowCount, fun i -> if i = m.RowCount - 1 then 1.0 else 0.0)
    m.Solve(result)

let Init =
    let A = matrix [[ 0.4; 0.0; 0.6; 0.0; 0.0; 0.0; 0.0; 0.0]
                    [0.7; 0.1; 0.2; 0.0; 0.0; 0.0; 0.0; 0.0]
                    [0.0; 0.0; 0.6; 0.3; 0.1; 0.0; 0.0; 0.0]
                    [0.0; 0.0; 0.8; 0.1; 0.1; 0.0; 0.0; 0.0]
                    [0.0; 0.0; 0.1; 0.4; 0.5; 0.0; 0.0; 0.0]
                    [0.0; 0.0; 0.0; 0.2; 0.3; 0.1; 0.2; 0.2]
                    [0.0; 0.0; 0.0; 0.0; 0.0; 0.1; 0.8; 0.1]
                    [0.0; 0.0; 0.0; 0.0; 0.0; 0.2; 0.6; 0.2]]
    
    let initPos : Vector<double> = vector [1.0; 0.0; 0.0; 0.0; 0.0; 0.0; 0.0; 0.0]
    let firstRes = RunBrutForce(A.Transpose(), initPos)
    
    let secondRes = SolverWithSystemOfLinearEquations(A.Transpose())
    
    printfn "%A" firstRes
    printfn "%A" secondRes

