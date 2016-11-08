Option Strict On
Public Class Layer
    Dim _neurons As New List(Of Neuron)
    Dim _neuronCount As Integer

    Public Property Neurons As List(Of Neuron)
        Get
            Return _neurons
        End Get
        Set(value As List(Of Neuron))
            _neurons = value
        End Set
    End Property

    Public ReadOnly Property NeuronCount As Integer
        Get
            Return _neurons.Count
        End Get
    End Property

    Public Sub New(neuronNum As Integer)
        _neuronCount = neuronNum
    End Sub
End Class