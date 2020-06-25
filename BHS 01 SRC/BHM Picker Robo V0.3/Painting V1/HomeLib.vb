Imports ADTMotionControl
Public Class HomeLib

    Public Shared Sub MoveToHomeTraverse()
        Dim nAx As Integer = 1
        CtrlCard.adt8940a1_Sym_AbsoluteMove(nAx, 0, 0, 100000, 0.5)
        Dim nStatus As Single
        Do
            Application.DoEvents()
            CtrlCard.adt8940a1_Get_MoveStatus(nAx, nStatus, 0)
        Loop Until nStatus = 0 Or RoboticControl.IsSUDDEN_STOP
    End Sub

    Public Shared Sub MoveToHomeKick()
        Dim nAx As Integer = 2
        CtrlCard.adt8940a1_Sym_AbsoluteMove(nAx, 0, 0, 50000, 0.5)
        Dim nStatus As Single
        Do
            Application.DoEvents()
            CtrlCard.adt8940a1_Get_MoveStatus(nAx, nStatus, 0)
        Loop Until nStatus = 0 Or RoboticControl.IsSUDDEN_STOP
    End Sub

    Public Shared Sub MoveToHomeVertical()
        Dim nAx As Integer = 3
        CtrlCard.adt8940a1_Sym_AbsoluteMove(nAx, 0, 0, 100000, 0.5)
        Dim nStatus As Single
        Do
            Application.DoEvents()
            CtrlCard.adt8940a1_Get_MoveStatus(nAx, nStatus, 0)
        Loop Until nStatus = 0 Or RoboticControl.IsSUDDEN_STOP
    End Sub

    Public Shared Sub MoveToHomeTwist()
        Dim nAx As Integer = 4
        CtrlCard.adt8940a1_Sym_AbsoluteMove(nAx, 0, 0, 800000, 0.5)
        Dim nStatus As Single
        Do
            Application.DoEvents()
            CtrlCard.adt8940a1_Get_MoveStatus(nAx, nStatus, 0)
        Loop Until nStatus = 0 Or RoboticControl.IsSUDDEN_STOP
    End Sub

    Public Shared Sub MoveToHomeAll_Arm_BlowGun()

        RoboticControl.IsSUDDEN_STOP = False
        CtrlCard.SensorAllineffective()
        CtrlCard.adt8940a1_Sym_AbsoluteLine4(0, 0, 0, 0, 0, 8000, 0.5)
        Dim nStatus(0 To 8) As Single
        Do
            Application.DoEvents()
            CtrlCard.adt8940a1_Get_MoveStatus(1, nStatus(0), 0)
            CtrlCard.adt8940a1_Get_MoveStatus(2, nStatus(1), 0)
            CtrlCard.adt8940a1_Get_MoveStatus(3, nStatus(2), 0)
            CtrlCard.adt8940a1_Get_MoveStatus(4, nStatus(3), 0)
        Loop Until (nStatus(0) = 0 And nStatus(1) = 0 And nStatus(2) = 0 And nStatus(3) = 0) Or RoboticControl.IsSUDDEN_STOP
        'Loop Until nStatus(0) = 0
        CtrlCard.SensorAlleffective()
        'CtrlCard.adt8940a1_Sym_AbsoluteLine2(1, 2, 5000, 5000, 5000, 5000, 0.1)
        'Do
        '    Application.DoEvents()
        '    CtrlCard.adt8940a1_Get_MoveStatus(1, nStatus(5), 0)
        '    CtrlCard.adt8940a1_Get_MoveStatus(2, nStatus(6), 0)
        '    'CtrlCard.adt8940a1_Get_MoveStatus(3, nStatus(7), 0)
        'Loop Until nStatus(5) = 0 And nStatus(6) = 0 Or RoboticControl.IsSUDDEN_STOP

    End Sub

End Class
