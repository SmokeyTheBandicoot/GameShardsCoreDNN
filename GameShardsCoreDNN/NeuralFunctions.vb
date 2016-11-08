
'List of function to use for a Neural Layer(s)
'Makes use of mxparser library for eventual use of derivatives and integrals

Option Strict On

Imports System.Math
Imports GameShardsCore
Imports GameShardsCore.AdvancedMath.SumProd
Imports NCalc

Public Class NeuralFunctions
    'Sigmoid
    Public Shared Function Sigmoid(ByVal x As Double) As Double
        Return (1 / (1 + E ^ (-x)))
    End Function

    'Linear
    Public Shared Function Linear(ByVal x As Double, ByVal m As Double, ByVal q As Double) As Double
        Return (m * x + q)
    End Function

    '#################################### Hyperbolic functions: SinH x + cosH x = E^x ################################################### 
    'Hyperbolic sine
    Public Shared Function SinH(ByVal x As Double) As Double
        Return (0.5 * (E ^ x - E ^ (-x)))
    End Function

    'Hyperbolic cosine
    Public Shared Function CosH(ByVal x As Double) As Double
        Return (0.5 * (E ^ x + E ^ (-x)))
    End Function

    'Hyperbolic Tangent
    Public Shared Function TanH(ByVal x As Double) As Double
        Return (SinH(x) / CosH(x))
    End Function

    'Fast Hyperbolic Tangent
    Public Shared Function FTanH(ByVal x As Double) As Double
        Return ((E ^ x) ^ 2 - 1) / ((E ^ x) ^ 2 + 1)
    End Function

    'Hyperbolic CoTangent
    Public Shared Function CoTanH(ByVal x As Double) As Double
        Return (CosH(x) / SinH(x))
    End Function

    'Fast Hyperbolic CoTangent
    Public Shared Function FCoTanH(ByVal x As Double) As Double
        Return ((E ^ x) ^ 2 + 1) / ((E ^ x) ^ 2 - 1)
    End Function

    'Hyperbolic Secant
    Public Shared Function SecH(ByVal x As Double) As Double
        Return (1 / SinH(x))
    End Function

    'Fast Hyperbolic Secant
    Public Shared Function FSecH(ByVal x As Double) As Double
        Return (2 / (E ^ x + E ^ (-x)))
    End Function

    'Hyperbolic Cosecant
    Public Shared Function CoSecH(ByVal x As Double) As Double
        Return (1 / CosH(x))
    End Function

    'Fast Hyperbolic CoSecant
    Public Shared Function FCoSecH(ByVal x As Double) As Double
        Return (2 / (E ^ x - E ^ (-x)))
    End Function



    '################################################# Softmax: Hard to implement #######################################################
    'Modified to avoid Math OverFlows
    'Softmax Function
    'Computed as following: you have n nodes, with v(n) values. so node 1 has v(1) value, node 2 has v(2) value, etc...
    'Each node n, has the value of E^v(n) / Summation(E^v(n))
    'Node 1 has a value of E^v(1)/(E^v(1) + E^v(2) + E^v(3))
    'Node 2 has a value of E^v(2)/(E^v(1) + E^v(2) + E^v(3))
    'Node 3 has a value of E^v(3)/(E^v(1) + E^v(2) + E^v(3))
    'In Short, Node N has a value of E^v(N) divided by QuickSum
    'Quicksum is the Summation of E^a, where a is the value of each node

    Public Function Softmax(ByVal UnactivatedValues As List(Of Double)) As List(Of Double)
        Dim Results As List(Of Double) = Nothing
        Dim Matrix(1, UnactivatedValues.Count - 1) As Double

        'Creation of ParamVal matrix. N=1, Delta=UnactivatedValues.Count - 1 (Number of Nodes)
        For y As Integer = 0 To UnactivatedValues.Count - 1
            Matrix(1, y) = UnactivatedValues(y)
        Next

        Dim QuickSum As Double = Summation("E^a", 0, UnactivatedValues.Count - 1, {"[a]"}.ToList, Matrix)

        For x As Integer = 0 To UnactivatedValues.Count - 1
            Results.Add(E ^ UnactivatedValues(x) / QuickSum)
        Next

        Return Results
    End Function
End Class