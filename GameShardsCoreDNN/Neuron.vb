Option Strict On

Public Class Neuron
    Dim _dendrites As New List(Of Dendrite)
    Dim _dendriteCount As Integer
    Dim _bias As Double
    Dim _value As Double
    Dim _delta As Double

    Public Property Dendrites As List(Of Dendrite)
        Get
            Return _dendrites
        End Get
        Set(value As List(Of Dendrite))
            _dendrites = value
        End Set
    End Property

    Public Property Bias As Double
        Get
            Return _bias
        End Get
        Set(value As Double)
            _bias = value
        End Set
    End Property

    Public Property Value As Double
        Get
            Return _value
        End Get
        Set(value As Double)
            _value = value
        End Set
    End Property

    Public Property Delta As Double
        Get
            Return _delta
        End Get
        Set(value As Double)
            _delta = value
        End Set
    End Property

    Public ReadOnly Property DendriteCount As Integer
        Get
            Return _dendrites.Count
        End Get
    End Property

    Public Sub New()
        Dim r As New Random
        Me.Bias = r.NextDouble()
    End Sub
End Class
