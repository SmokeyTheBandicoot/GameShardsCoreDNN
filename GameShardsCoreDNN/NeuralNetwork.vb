Option Strict On

Imports GameShardsCore
Imports GameShardsCoreDNN.NeuralFunctions

Module Core
    Public Class NeuralNetwork
        Dim _layers As New List(Of Layer)
        Dim _learningRate As Double

        Public Property Layers As List(Of Layer)
            Get
                Return _layers
            End Get
            Set(value As List(Of Layer))
                _layers = value
            End Set
        End Property

        Public Property LearningRate As Double
            Get
                Return _learningRate
            End Get
            Set(value As Double)
                _learningRate = value
            End Set
        End Property

        Public ReadOnly Property LayerCount As Integer
            Get
                Return _layers.Count
            End Get
        End Property

        Sub New(LearningRate As Double, nLayers As List(Of Integer))
            If nLayers.Count < 2 Then Exit Sub

            Me.LearningRate = LearningRate

            For ii As Integer = 0 To nLayers.Count - 1

                Dim l As Layer = New Layer(nLayers(ii) - 1)
                Me.Layers.Add(l)

                For jj As Integer = 0 To nLayers(ii) - 1
                    l.Neurons.Add(New Neuron())
                Next

                For Each n As Neuron In l.Neurons
                    If ii = 0 Then n.Bias = 0

                    If ii > 0 Then
                        For k As Integer = 0 To nLayers(ii - 1) - 1
                            n.Dendrites.Add(New Dendrite)
                        Next
                    End If

                Next

            Next
        End Sub

        Function Execute(inputs As List(Of Double)) As List(Of Double)
            If inputs.Count <> Me.Layers(0).NeuronCount Then
                Return Nothing
            End If

            For ii As Integer = 0 To Me.LayerCount - 1
                Dim curLayer As Layer = Me.Layers(ii)

                For jj As Integer = 0 To curLayer.NeuronCount - 1
                    Dim curNeuron As Neuron = curLayer.Neurons(jj)

                    If ii = 0 Then
                        curNeuron.Value = inputs(jj)
                    Else
                        curNeuron.Value = 0
                        For k = 0 To Me.Layers(ii - 1).NeuronCount - 1
                            curNeuron.Value = curNeuron.Value + Me.Layers(ii - 1).Neurons(k).Value * curNeuron.Dendrites(k).Weight
                        Next k

                        curNeuron.Value = Sigmoid(curNeuron.Value + curNeuron.Bias)
                    End If

                Next
            Next

            Dim outputs As New List(Of Double)
            Dim la As Layer = Me.Layers(Me.LayerCount - 1)
            For ii As Integer = 0 To la.NeuronCount - 1
                outputs.Add(la.Neurons(ii).Value)
            Next

            Return outputs
        End Function

        Public Function Train(inputs As List(Of Double), outputs As List(Of Double)) As Boolean
            If inputs.Count <> Me.Layers(0).NeuronCount Or outputs.Count <> Me.Layers(Me.LayerCount - 1).NeuronCount Then
                Return False
            End If

            Execute(inputs)

            For ii = 0 To Me.Layers(Me.LayerCount - 1).NeuronCount - 1
                Dim curNeuron As Neuron = Me.Layers(Me.LayerCount - 1).Neurons(ii)

                curNeuron.Delta = curNeuron.Value * (1 - curNeuron.Value) * (outputs(ii) - curNeuron.Value)

                For jj = Me.LayerCount - 2 To 1 Step -1
                    For kk = 0 To Me.Layers(jj).NeuronCount - 1
                        Dim iNeuron As Neuron = Me.Layers(jj).Neurons(kk)

                        iNeuron.Delta = iNeuron.Value *
                                    (1 - iNeuron.Value) * Me.Layers(jj + 1).Neurons(ii).Dendrites(kk).Weight *
                                    Me.Layers(jj + 1).Neurons(ii).Delta
                    Next kk
                Next jj
            Next ii


            For ii = Me.LayerCount - 1 To 0 Step -1
                For jj = 0 To Me.Layers(ii).NeuronCount - 1
                    Dim iNeuron As Neuron = Me.Layers(ii).Neurons(jj)
                    iNeuron.Bias = iNeuron.Bias + (Me.LearningRate * iNeuron.Delta)

                    For kk = 0 To iNeuron.DendriteCount - 1
                        iNeuron.Dendrites(kk).Weight = iNeuron.Dendrites(kk).Weight + (Me.LearningRate * Me.Layers(ii - 1).Neurons(kk).Value * iNeuron.Delta)
                    Next kk
                Next jj
            Next ii

            Return True
        End Function

    End Class
End Module
