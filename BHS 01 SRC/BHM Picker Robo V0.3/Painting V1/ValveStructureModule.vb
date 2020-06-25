Imports ADTMotionControl
Class ValveStructureModule

    Public Structure ValveCommand

        Public Valve1 As Boolean
        Public Valve2 As Boolean
        Public Valve3 As Boolean
        Public Pressure1 As Double
        Public Pressure2 As Double
        Public Pressure3 As Double

        Public Sub New(ByVal v1 As Boolean, ByVal v2 As Boolean, ByVal v3 As Boolean, ByVal P1 As Double, ByVal P2 As Double, ByVal P3 As Double)
            Valve1 = v1
            Valve2 = v2
            Valve3 = v3
            Pressure1 = P1
            Pressure2 = P2
            Pressure3 = P3

        End Sub

        Public Overrides Function ToString() As String
            Return IIf(Valve1, "ON", "OFF") & "," & IIf(Valve2, "ON", "OFF") & "," & IIf(Valve3, "ON", "OFF") & "," & Pressure1.ToString & "," & Pressure2.ToString & "," & Pressure3.ToString
        End Function

        ' Analog control
        Public Sub SetPressure()

        End Sub

        Public Sub TurnValve()
            CtrlCard.adt8940a1_Write_Output(8, Valve1)
            CtrlCard.adt8940a1_Write_Output(9, Valve2)
            CtrlCard.adt8940a1_Write_Output(10, Valve3)
            'EXPort.TurnRelay(1, Valve1)
            'EXPort.TurnRelay(2, Valve2)
            'EXPort.TurnRelay(3, Valve3)
        End Sub

        Public Sub CloseAllValve()
            CtrlCard.adt8940a1_Write_Output(8, 0)
            CtrlCard.adt8940a1_Write_Output(9, 0)
            CtrlCard.adt8940a1_Write_Output(10, 0)
            'EXPort.TurnRelay(1, False)
            'EXPort.TurnRelay(2, False)
            'EXPort.TurnRelay(3, False)
        End Sub

    End Structure

End Class
