Imports ADTMotionControl
Public Class StopLib

#Region "Stop all axes and relays"

    Public Shared Sub SuddenStopAll()

        If Not My.Settings.UseCard Then Exit Sub
        '
        ' Sudden stop

        adt8940a1.reset_fifo(0)
        For i = 1 To 4
            CtrlCard.adt8940a1_StopRun(i, 0) 'Stop each axis
        Next i

        RoboticControl.IsSUDDEN_STOP = True
        '
        frmMain.LunchMenu()
        '
    End Sub

    Public Shared Sub DecelStopAll()

        If Not My.Settings.UseCard Then Exit Sub
        '
        ' Sudden stop
        RoboticControl.IsSUDDEN_STOP = True

        adt8940a1.reset_fifo(0)
        For i = 1 To 4
            CtrlCard.adt8940a1_StopRun(i, 1) 'DEC Stop each axis
        Next i

        '
        frmMain.LunchMenu()
        '
    End Sub

#End Region

#Region "Stop rotary pulsing and relays"

    'Public Shared Sub StopRotaryRelativeMove()
    '    'EXPort.StopMinusRelativeMoveThread()
    '    'EXPort.StopPlusRelativeMoveThread()
    'End Sub

    'Public Shared Sub StopRotaryAbsoluteMove()
    '    'EXPort.StopMinusAbsoluteMoveThread()
    '    'EXPort.StopPlusAbsoluteMoveThread()
    'End Sub

    'Public Shared Sub StopValve()
    '    'EXPort.Out(890, 0)
    '    'EXPort.RelayState = 0
    'End Sub

#End Region

End Class
