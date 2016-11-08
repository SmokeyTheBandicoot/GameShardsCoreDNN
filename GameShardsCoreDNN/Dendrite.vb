Option Strict On

Public Class Dendrite
    Dim _weight As Double

    Property Weight As Double
        Get
            Return _weight
        End Get
        Set(value As Double)
            _weight = value
        End Set
    End Property

    Public Sub New()
        Dim r As New Random
        Me.Weight = r.NextDouble()
    End Sub
End Class
