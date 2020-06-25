Imports ADTMotionControl
Imports System.Text

Public Class RoboticControl

#Region "ค่าคงที่ และ ตัวแปร"

    Public Enum SensorNo

        LimitMinus1 = 0
        LimitPlus1 = 1
        Stop0_1 = 2
        Stop1_1 = 3

        LimitMinus2 = 6
        LimitPlus2 = 7
        Stop0_2 = 8
        Stop1_2 = 9

        LimitMinus3 = 12
        LimitPlus3 = 13
        Stop0_3 = 14
        Stop1_3 = 15

        LimitMinus4 = 18
        LimitPlus4 = 19
        Stop0_4 = 20
        Stop1_4 = 21

    End Enum

    Public Enum ContactNo
        SON = 5
        CLR = 7
        ARST = 6

        V_Break = 12

        MC_ON = 0
        MC_OFF = 4

    End Enum

#End Region

#Region "Homing"

    Public Shared IsSUDDEN_STOP As Boolean = False

    Shared Sub Home1()
        IsSUDDEN_STOP = False

        Dim status As Short = 1
        Dim axis As Short = 1, nlim As Short = 0, plim As Short = 1, stop0 As Short = 2
        Dim pulseUp As Integer = -1600 * My.Settings.Xppmm_Numerator / My.Settings.Xppmm_Denominator, speed1 As Integer = My.Settings.XJogSpeed * 10
        Dim pulseDown As Integer = 1600 * My.Settings.Xppmm_Numerator / My.Settings.Xppmm_Denominator, speed2 As Integer = My.Settings.XJogSpeed * 10
        Dim tacc As Double = My.Settings.XJogDt

        CtrlCard.adt8940a1_Setup_Stop0Mode(axis, 0, 0)
        CtrlCard.adt8940a1_Setup_Stop1Mode(axis, 1, 0)
        CtrlCard.adt8940a1_Setup_LimitMode(axis, 0, 0, 0)

        ' ขึ้น ---------------------------------------------------------------------
        'CtrlCard.adt8940a1_Sym_RelativeMove(axis, pulseUp, 0, speed1, tacc)
        'Threading.Thread.Sleep(100)
        CtrlCard.adt8940a1_Setup_Speed(axis, 0, speed1, 5000)
        CtrlCard.Axis_Pmove(axis, pulseUp)

        status = 1
        Do
            Application.DoEvents() ' Caution about sudden stop
            CtrlCard.adt8940a1_Get_MoveStatus(axis, status, 0)
        Loop Until CtrlCard.adt8940a1_Read_Input(nlim) = 0 Or IsSUDDEN_STOP Or status = 0

        ' Sudden stop
        adt8940a1.reset_fifo(0)
        CtrlCard.adt8940a1_StopRun(1, 0) 'Stop each axis
        Threading.Thread.Sleep(200)
        If IsSUDDEN_STOP Then Exit Sub

        ' ลง ---------------------------------------------------------------------
        CtrlCard.adt8940a1_Setup_Stop0Mode(axis, 1, 0)
        CtrlCard.adt8940a1_Setup_Stop1Mode(axis, 1, 0)
        CtrlCard.adt8940a1_Setup_LimitMode(axis, 0, 0, 0)
        status = 1
        CtrlCard.adt8940a1_Sym_RelativeMove(axis, pulseDown, 0, speed2, tacc)
        Do
            Application.DoEvents() ' Caution about sudden stop
            CtrlCard.adt8940a1_Get_MoveStatus(axis, status, 0)
        Loop Until CtrlCard.adt8940a1_Read_Input(stop0) = 0 Or IsSUDDEN_STOP Or status = 0

        ' Sudden stop
        adt8940a1.reset_fifo(0)
        CtrlCard.adt8940a1_StopRun(axis, 0) 'Stop each axis
        Threading.Thread.Sleep(200)

        If IsSUDDEN_STOP Then Exit Sub

        'ตั้งค่า
        CtrlCard.adt8940a1_Setup_Stop0Mode(axis, 0, 0)
        CtrlCard.adt8940a1_Setup_Stop1Mode(axis, 1, 0)
        CtrlCard.adt8940a1_Setup_LimitMode(axis, 0, 0, 0)

        'ล้างต่า
        CtrlCard.adt8940a1_Setup_Pos(axis, 0, 0)     'Logical position counter clear
        CtrlCard.adt8940a1_Setup_Pos(axis, 0, 1)     'Clear the actual position of the counter

    End Sub
    Shared Sub Home2()
        IsSUDDEN_STOP = False

        Dim status As Short = 1
        Dim axis As Short = 2, nlim As Short = 6, plim As Short = 7, stop0 As Short = 8
        Dim pulse1 As Integer = -20 * My.Settings.Yppmm_Numerator / My.Settings.Yppmm_Denominator, speed1 As Integer = My.Settings.YJogSpeed
        Dim pulse2 As Integer = 190 * My.Settings.Yppmm_Numerator / My.Settings.Yppmm_Denominator, speed2 As Integer = My.Settings.YJogSpeed
        Dim tacc As Double = My.Settings.YJogDt

        CtrlCard.adt8940a1_Setup_Stop0Mode(axis, 0, 0)
        CtrlCard.adt8940a1_Setup_Stop1Mode(axis, 1, 0)
        CtrlCard.adt8940a1_Setup_LimitMode(axis, 0, 0, 0)

        ' ---------------------------------------------------------------------
        CtrlCard.adt8940a1_Setup_Speed(axis, 0, 20000, 1000)
        CtrlCard.Axis_Pmove(axis, pulse2)
        status = 1
        Do
            Application.DoEvents()
            CtrlCard.adt8940a1_Get_MoveStatus(axis, status, 0)
        Loop Until CtrlCard.adt8940a1_Read_Input(nlim) = 0 Or status = 0 Or IsSUDDEN_STOP

        ' Sudden stop
        adt8940a1.reset_fifo(0)
        CtrlCard.adt8940a1_StopRun(axis, 0) 'Stop each axis
        Threading.Thread.Sleep(200)

        If IsSUDDEN_STOP Then Exit Sub

        ' ---------------------------------------------------------------------
        CtrlCard.adt8940a1_Setup_Speed(axis, 0, 50000, 1000)
        CtrlCard.Axis_Pmove(axis, pulse1)
        status = 1
        Do
            Application.DoEvents()
            CtrlCard.adt8940a1_Get_MoveStatus(axis, status, 0)
        Loop Until status = 0 Or IsSUDDEN_STOP

        ' Sudden stop
        adt8940a1.reset_fifo(0)
        CtrlCard.adt8940a1_StopRun(axis, 0) 'Stop each axis
        Threading.Thread.Sleep(200)

        If IsSUDDEN_STOP Then Exit Sub


        '' ---------------------------------------------------------------------
        CtrlCard.adt8940a1_Setup_Stop0Mode(axis, 1, 0)
        CtrlCard.adt8940a1_Setup_Stop1Mode(axis, 1, 0)
        CtrlCard.adt8940a1_Setup_LimitMode(axis, 0, 0, 0)

        CtrlCard.adt8940a1_Setup_Speed(axis, 0, 9000, 1000)
        CtrlCard.Axis_Pmove(axis, pulse2)
        status = 1
        Do
            Application.DoEvents() ' Caution about sudden stop
            CtrlCard.adt8940a1_Get_MoveStatus(axis, status, 0)
        Loop Until CtrlCard.adt8940a1_Read_Input(stop0) = 0 Or IsSUDDEN_STOP Or status = 0

        ' Sudden stop
        adt8940a1.reset_fifo(0)
        CtrlCard.adt8940a1_StopRun(axis, 0) 'Stop each axis
        Threading.Thread.Sleep(200)

        If IsSUDDEN_STOP Then Exit Sub

        '' ---------------------------------------------------------------------
        'ล้างต่า
        CtrlCard.adt8940a1_Setup_Stop0Mode(axis, 0, 0)
        CtrlCard.adt8940a1_Setup_Stop1Mode(axis, 1, 0)
        CtrlCard.adt8940a1_Setup_LimitMode(axis, 0, 0, 0)

        CtrlCard.adt8940a1_Setup_Pos(axis, 0, 0)     'Logical position counter clear
        CtrlCard.adt8940a1_Setup_Pos(axis, 0, 1)     'Clear the actual position of the counter

    End Sub
    Shared Sub Home3()
        IsSUDDEN_STOP = False

        Dim axis As Short = 3, nlim As Short = 12, plim As Short = 13, stop0 As Short = 14
        Dim pulse1 As Integer = 10 * My.Settings.Zppmm_Numerator / My.Settings.Zppmm_Denominator, speed1 As Integer = 10000
        Dim pulse2 As Integer = -270 * My.Settings.Zppmm_Numerator / My.Settings.Zppmm_Denominator, speed2 As Integer = 20000
        Dim tacc As Double = 0.5
        Dim status As Short = 1

        CtrlCard.adt8940a1_Setup_Stop0Mode(axis, 0, 0)
        CtrlCard.adt8940a1_Setup_Stop1Mode(axis, 1, 0)
        CtrlCard.adt8940a1_Setup_LimitMode(axis, 0, 0, 0)

        ' ---------------------------------------------------------------------
        CtrlCard.adt8940a1_Setup_Speed(axis, 0, speed2, 1000)
        CtrlCard.Axis_Pmove(axis, pulse2)
        status = 1
        Do
            Application.DoEvents() ' Caution about sudden stop
            CtrlCard.adt8940a1_Get_MoveStatus(axis, status, 0)
        Loop Until CtrlCard.adt8940a1_Read_Input(nlim) = 0 Or IsSUDDEN_STOP Or status = 0

        ' Sudden stop
        adt8940a1.reset_fifo(0)
        CtrlCard.adt8940a1_StopRun(axis, 0) 'Stop each axis
        Threading.Thread.Sleep(200)

        If IsSUDDEN_STOP Then Exit Sub

        ' ---------------------------------------------------------------------
        CtrlCard.adt8940a1_Setup_Speed(axis, 0, speed1, 1000)
        CtrlCard.Axis_Pmove(axis, pulse1 * 3)
        status = 1
        Do
            Application.DoEvents() ' Caution about sudden stop
            CtrlCard.adt8940a1_Get_MoveStatus(axis, status, 0)
        Loop Until CtrlCard.adt8940a1_Read_Input(plim) = 0 Or IsSUDDEN_STOP Or status = 0

        ' Sudden stop
        adt8940a1.reset_fifo(0)
        CtrlCard.adt8940a1_StopRun(axis, 0) 'Stop each axis
        Threading.Thread.Sleep(200)

        If IsSUDDEN_STOP Then Exit Sub

        ' ---------------------------------------------------------------------
        CtrlCard.adt8940a1_Setup_Stop0Mode(axis, 1, 0)
        CtrlCard.adt8940a1_Setup_Stop1Mode(axis, 1, 0)
        CtrlCard.adt8940a1_Setup_LimitMode(axis, 0, 0, 0)

        ' ---------------------------------------------------------------------
        CtrlCard.adt8940a1_Setup_Speed(axis, 0, speed1, 1000)
        CtrlCard.Axis_Pmove(axis, pulse2 / 4)
        status = 1
        Do
            Application.DoEvents() ' Caution about sudden stop
            CtrlCard.adt8940a1_Get_MoveStatus(axis, status, 0)
        Loop Until CtrlCard.adt8940a1_Read_Input(stop0) = 0 Or IsSUDDEN_STOP Or status = 0

        ' Sudden stop
        adt8940a1.reset_fifo(0)
        CtrlCard.adt8940a1_StopRun(axis, 0) 'Stop each axis
        Threading.Thread.Sleep(200)

        If IsSUDDEN_STOP Then Exit Sub

        'ล้างต่า
        CtrlCard.adt8940a1_Setup_Stop0Mode(axis, 0, 0)
        CtrlCard.adt8940a1_Setup_Stop1Mode(axis, 1, 0)
        CtrlCard.adt8940a1_Setup_LimitMode(axis, 0, 0, 0)
        CtrlCard.adt8940a1_Setup_Pos(axis, 0, 0)     'Logical position counter clear
        CtrlCard.adt8940a1_Setup_Pos(axis, 0, 1)     'Clear the actual position of the counter

    End Sub

    Shared Sub Home4()
        IsSUDDEN_STOP = False

        Dim axis As Short = 4, nlim As Short = 18, plim As Short = 19, stop0 As Short = 20
        Dim pulse1 As Integer = -380 * My.Settings.Appmm_Numerator / My.Settings.Appmm_Denominator, speed1 As Integer = 200000
        Dim tacc As Double = 0.5


        ' ---------------------------------------------------------------------
        CtrlCard.adt8940a1_Setup_Stop0Mode(axis, 0, 0)
        CtrlCard.adt8940a1_Setup_Stop1Mode(axis, 1, 0)
        CtrlCard.adt8940a1_Setup_LimitMode(axis, 1, 1, 0)
        Dim status As Short = 1
        CtrlCard.adt8940a1_Setup_Speed(axis, 0, speed1, 7000)
        CtrlCard.Axis_Pmove(axis, pulse1 / 8)
        status = 1
        Do
            Application.DoEvents() ' Caution about sudden stop
            CtrlCard.adt8940a1_Get_MoveStatus(axis, status, 0)
        Loop Until IsSUDDEN_STOP Or status = 0

        ' ---------------------------------------------------------------------
        CtrlCard.adt8940a1_Setup_Stop0Mode(axis, 0, 0)
        CtrlCard.adt8940a1_Setup_Stop1Mode(axis, 1, 0)
        CtrlCard.adt8940a1_Setup_LimitMode(axis, 0, 0, 0)
        CtrlCard.Axis_Pmove(axis, pulse1)
        status = 1
        Do
            Application.DoEvents() ' Caution about sudden stop
            CtrlCard.adt8940a1_Get_MoveStatus(axis, status, 0)
        Loop Until CtrlCard.adt8940a1_Read_Input(nlim) = 0 Or IsSUDDEN_STOP Or status = 0

        ' Sudden stop
        adt8940a1.reset_fifo(0)
        CtrlCard.adt8940a1_StopRun(axis, 0) 'Stop each axis
        Threading.Thread.Sleep(700)


        ' ---------------------------------------------------------------------
        CtrlCard.adt8940a1_Setup_Stop0Mode(axis, 0, 0)
        CtrlCard.adt8940a1_Setup_Stop1Mode(axis, 1, 0)
        CtrlCard.adt8940a1_Setup_LimitMode(axis, 1, 1, 0)

        If IsSUDDEN_STOP Then Exit Sub

        ' ---------------------------------------------------------------------
        'ล้างต่า
        CtrlCard.adt8940a1_Setup_Pos(axis, 0, 0)     'Logical position counter clear
        CtrlCard.adt8940a1_Setup_Pos(axis, 0, 1)     'Clear the actual position of the counter


    End Sub

#End Region

#Region "Alert dialog"

    Public Shared Sub ShowAlert(ByVal text As String, ByVal ms As Integer)
        Dim dlg As dlgMessage
        dlg = New dlgMessage(ms)
        dlg.rtbText.Text = text
        dlg.ShowDialog()
        dlg.Dispose()
        dlg = Nothing
    End Sub

#End Region


#Region "Get system information"

    Shared FFormat As String = My.Settings.FFormat

    Public Shared Sub CheckAxis(ByVal nAxis As Integer, ByRef status As Integer, _
                   ByRef lblLogicPos As Label, ByRef lblRealPos As Label, _
                   ByRef lblRunningSpeed As Label, _
                   ByRef lblLimitCW As Label, ByRef lblLimitCCW As Label, _
                   ByRef lblStop0 As Label, ByRef lblStop1 As Label)

        '----------------------------------------------------------------------------------
        'อ่านตำแหน่ง และ ความเร็ว
        '----------------------------------------------------------------------------------
        If nAxis <= 4 Then
            Dim nLogPos As Long                   'Logical location
            Dim nActPos As Long                   'Actual location
            Dim nSpeed As Long                    'Running Speeed
            CtrlCard.adt8940a1_Get_CurrentInfo(nAxis, nLogPos, nActPos, nSpeed)

            lblLogicPos.Text = (nLogPos).ToString(FFormat)
            lblRealPos.Text = nActPos.ToString
            lblRunningSpeed.Text = (nSpeed).ToString(FFormat)
        End If
        '----------------------------------------------------------------------------------
        'คืนค่าสถานะ
        '----------------------------------------------------------------------------------
        CtrlCard.adt8940a1_Get_MoveStatus(nAxis, status, 0)

        '----------------------------------------------------------------------------------
        'เรียกรหัสเซนเซอร์
        '----------------------------------------------------------------------------------
        Dim LimitCWSensorNbr As Integer
        Dim LimitCCWSensorNbr As Integer
        Dim Stop0SensorNbr As Integer
        Dim Stop1SensorNbr As Integer
        Select Case nAxis
            Case 1
                LimitCWSensorNbr = SensorNo.LimitPlus1
                LimitCCWSensorNbr = SensorNo.LimitMinus1
                Stop0SensorNbr = SensorNo.Stop0_1
                Stop1SensorNbr = SensorNo.Stop1_1
            Case 2
                LimitCWSensorNbr = SensorNo.LimitPlus2
                LimitCCWSensorNbr = SensorNo.LimitMinus2
                Stop0SensorNbr = SensorNo.Stop0_2
                Stop1SensorNbr = SensorNo.Stop1_2
            Case 3
                LimitCWSensorNbr = SensorNo.LimitPlus3
                LimitCCWSensorNbr = SensorNo.LimitMinus3
                Stop0SensorNbr = SensorNo.Stop0_3
                Stop1SensorNbr = SensorNo.Stop1_3
            Case 4
                LimitCWSensorNbr = SensorNo.LimitPlus4
                LimitCCWSensorNbr = SensorNo.LimitMinus4
                Stop0SensorNbr = SensorNo.Stop0_4
                Stop1SensorNbr = SensorNo.Stop1_4

        End Select

        If nAxis <= 4 Then
            If CtrlCard.adt8940a1_Read_Input(LimitCWSensorNbr) = 0 Then
                lblLimitCW.Text = "X"
            Else
                lblLimitCW.Text = "."
            End If
            If CtrlCard.adt8940a1_Read_Input(LimitCCWSensorNbr) = 0 Then
                lblLimitCCW.Text = "X"
            Else
                lblLimitCCW.Text = "."
            End If
            If CtrlCard.adt8940a1_Read_Input(Stop0SensorNbr) = 0 Then
                lblStop0.Text = "X"
            Else
                lblStop0.Text = "."
            End If
            If CtrlCard.adt8940a1_Read_Input(Stop1SensorNbr) = 0 Then
                lblStop1.Text = "X"
            Else
                lblStop1.Text = "."
            End If
        End If

        ' SOMETHING FOR SENSOR
        If nAxis = 5 Then
            If CtrlCard.adt8940a1_Read_Input(4) = 0 Then 'Color sensor
                lblLimitCW.Text = "X"
            Else
                lblLimitCW.Text = "."
            End If
            If CtrlCard.adt8940a1_Read_Input(27) = 0 Then 'BBOT
                lblLimitCCW.Text = "X"
            Else
                lblLimitCCW.Text = "."
            End If
            If CtrlCard.adt8940a1_Read_Input(17) = 0 Then 'BTOP
                lblStop0.Text = "X"
            Else
                lblStop0.Text = "."
            End If
            If CtrlCard.adt8940a1_Read_Input(23) = 0 Then 'DISK/ROTARY
                lblStop1.Text = "X"
            Else
                lblStop1.Text = "."
            End If
        End If

    End Sub



#End Region

    Public Shared Sub ClearAllPosition()

        For i = 1 To 4
            CtrlCard.adt8940a1_Setup_Pos(i, 0, 0)     'Logical position counter clear
            CtrlCard.adt8940a1_Setup_Pos(i, 0, 1)     'Clear the actual position of the counter
        Next i

    End Sub

    Enum AxisNbr
        LIFT = 1
        ARM = 2
        HAND = 3
        TURN = 4
    End Enum

    'Enum OutputNbr
    '    HAND = My.Settings.Output_Hand ' เดิมเป็น 1 แล้วเปลี่ยนเป็น 13
    'End Enum
    Shared OUTPUT_HAND As Integer = My.Settings.Output_Hand

    Enum InputNbr
        HAND_L = 24
        HAND_R = 25

        BTOP = 17
    End Enum

    Public Shared tLift As New System.Threading.Thread(AddressOf Lift)
    Public Shared Sub Lift()
        Debug.WriteLine("Lift....")
        Dim nLogPos As Long                   'Logical location
        Dim nActPos As Long                   'Actual location
        Dim nSpeed As Long                    'Running Speeed
        CtrlCard.adt8940a1_Get_CurrentInfo(1, nLogPos, nActPos, nSpeed)
        Debug.WriteLine(nLogPos.ToString() & ", " & nActPos.ToString & ", " & (nLogPos - nActPos).ToString)

        'Dim LIFT_UP As Long = -236214
        CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.LIFT, 0, 0)
        CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.LIFT, 1, 0)
        CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.LIFT, 0, 0, 0)

        ' ---------------------------------------------------------------------
        CtrlCard.adt8940a1_Setup_Speed(AxisNbr.LIFT, 0, LiftSpeed, LiftAcc2)
        CtrlCard.Axis_Pmove(AxisNbr.LIFT, -Math.Abs(LIFT_UP)) ' -HEIGH * 796
        Dim status As Short = 1
        Do
            Application.DoEvents()
            CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.LIFT, status, 0)
            'Or CtrlCard.adt8940a1_Read_Input(InputNbr.BTOP) = 0
        Loop Until status = 0 Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
        Threading.Thread.Sleep(200)
        ' ---------------------------------------------------------------------
        ' Sudden stop
        If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
            adt8940a1.reset_fifo(0)
            For a As Short = 1 To 4
                CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
            Next
        End If

        CtrlCard.adt8940a1_Get_CurrentInfo(1, nLogPos, nActPos, nSpeed)
        Debug.WriteLine(nLogPos.ToString() & ", " & nActPos.ToString & ", " & (nLogPos - nActPos).ToString)
        Debug.WriteLine("Lift....DONE")
    End Sub

    Public Shared tTurnPostive As New System.Threading.Thread(AddressOf procTurnPositive)
    Public Shared Sub procTurnPositive()
        Debug.WriteLine("Trun positive....")

        Dim turnSpeed As Long = 650000
        Threading.Thread.Sleep(500) 'รอให้แขนพ้น
        CtrlCard.adt8940a1_Setup_Stop0Mode(4, 0, 0)
        CtrlCard.adt8940a1_Setup_Stop1Mode(4, 1, 0)
        CtrlCard.adt8940a1_Setup_LimitMode(4, 1, 1, 0)
        Dim status As Short
        status = 1
        CtrlCard.adt8940a1_Setup_Speed(AxisNbr.TURN, 0, turnSpeed, 50000)
        CtrlCard.Axis_Pmove(AxisNbr.TURN, 2 * 250000)
        Do
            Application.DoEvents()
            CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.TURN, status, 0)
        Loop Until status = 0 Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
        ' Sudden stop
        If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
            adt8940a1.reset_fifo(0)
            For a As Short = 1 To 4
                CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
            Next
        End If

        'แจ้งตัว CVY ให้รับตะกร้าทำงานต่อไป เช่น แตะ
        procKick()

        Debug.WriteLine("Trun positive....DONE")
    End Sub
    Public Shared tTurnNegative As New System.Threading.Thread(AddressOf procTurnNegative)
    Public Shared Sub procTurnNegative()
        Debug.WriteLine("Trun negative....")

        Dim turnSpeed As Long = 650000
        Threading.Thread.Sleep(500) 'รอให้แขนพ้น
        CtrlCard.adt8940a1_Setup_Stop0Mode(4, 0, 0)
        CtrlCard.adt8940a1_Setup_Stop1Mode(4, 1, 0)
        CtrlCard.adt8940a1_Setup_LimitMode(4, 1, 1, 0)
        Dim status As Short
        status = 1
        CtrlCard.adt8940a1_Setup_Speed(AxisNbr.TURN, 0, turnSpeed, 50000)
        CtrlCard.Axis_Pmove(AxisNbr.TURN, -2 * 250000)
        Do
            Application.DoEvents()
            CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.TURN, status, 0)
        Loop Until status = 0 Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
        ' Sudden stop
        If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
            adt8940a1.reset_fifo(0)
            For a As Short = 1 To 4
                CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
            Next
        End If

        'แจ้งตัว CVY ให้รับตะกร้าทำงานต่อไป เช่น แตะ
        procKick()

        Debug.WriteLine("Trun negative....DONE")
    End Sub


    Public Shared EndLift_InPosition As Boolean
    Public Shared tEndLift As New System.Threading.Thread(AddressOf procEndLift)
    Public Shared Sub procEndLift()
        Debug.WriteLine("END Lift....")
        Dim nLogPos As Long                   'Logical location
        Dim nActPos As Long                   'Actual location
        Dim nSpeed As Long                    'Running Speeed
        CtrlCard.adt8940a1_Get_CurrentInfo(1, nLogPos, nActPos, nSpeed)
        Debug.WriteLine(nLogPos.ToString() & ", " & nActPos.ToString & ", " & (nLogPos - nActPos).ToString)

        ' ลง _--------------------------------------------------------
        Dim theLock As New Object
        Dim status As Short
        EndLift_InPosition = False
        SyncLock theLock
            Dim inPosition As Boolean = False
            status = 1
            '
            CtrlCard.adt8940a1_Setup_Speed(AxisNbr.LIFT, 0, LiftDownSpeed, LiftAcc2)
            CtrlCard.Axis_Pmove(AxisNbr.LIFT, -nLogPos)
            Do
                Application.DoEvents()
                CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.LIFT, status, 0)
                CtrlCard.adt8940a1_Get_CurrentInfo(1, nLogPos, nActPos, nSpeed)

                If status = 0 Then
                    If nLogPos > -100 Then
                        inPosition = True
                        Exit Do
                    End If
                End If

            Loop Until CtrlCard.adt8940a1_Read_Input(1) = 0 Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
            Threading.Thread.Sleep(200)

            CtrlCard.adt8940a1_Get_CurrentInfo(1, nLogPos, nActPos, nSpeed)
            Debug.WriteLine(nLogPos.ToString() & ", " & nActPos.ToString & ", " & (nLogPos - nActPos).ToString)

            EndLift_InPosition = inPosition
            '
        End SyncLock

        ' Sudden stop
        If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
            adt8940a1.reset_fifo(0)
            For a As Short = 1 To 4
                CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
            Next
            Exit Sub
        End If

        CtrlCard.adt8940a1_Get_CurrentInfo(1, nLogPos, nActPos, nSpeed)
        Debug.WriteLine(nLogPos.ToString() & ", " & nActPos.ToString & ", " & (nLogPos - nActPos).ToString)
        '
        status = 1
        CtrlCard.adt8940a1_Sym_AbsoluteMove(1, 0, 0, LiftSpeed, 0.5)
        Do
            Application.DoEvents()
            CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.LIFT, status, 0)
        Loop Until status = 0 Or CtrlCard.adt8940a1_Read_Input(1) = 0 Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
        Threading.Thread.Sleep(200)

        CtrlCard.adt8940a1_Get_CurrentInfo(1, nLogPos, nActPos, nSpeed)
        Debug.WriteLine(nLogPos.ToString() & ", " & nActPos.ToString & ", " & (nLogPos - nActPos).ToString)

        ' Sudden stop
        If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
            adt8940a1.reset_fifo(0)
            For a As Short = 1 To 4
                CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
            Next
            Exit Sub
        End If

        '' -------------------------------------------------------------------------------
        '' Reload
        '' -------------------------------------------------------------------------------
        'If CtrlCard.adt8940a1_Read_Input(5) = 0 Then

        '    ' -------------------------------------------------------------------------------
        '    ' Check color sensor
        '    ' -------------------------------------------------------------------------------
        '    Debug.WriteLine("Color Sensor")
        '    If CtrlCard.adt8940a1_Read_Input(4) = 1 Then 'in4(xexp+)
        '        LGRN = False
        '        Debug.WriteLine("LGRN = FALSE")
        '    Else
        '        LGRN = True
        '        Debug.WriteLine("LGRN = TRUE")
        '    End If
        '    ' Sudden stop
        '    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
        '        adt8940a1.reset_fifo(0)
        '        For a As Short = 1 To 4
        '            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
        '        Next
        '        Exit Sub
        '    End If

        '    procReload()

        'End If


        'CtrlCard.adt8940a1_Setup_Pos(1, 0, 0)
        'CtrlCard.adt8940a1_Setup_Pos(1, 0, 1)

        Debug.WriteLine("END Lift....DONE")
    End Sub
    Public Shared Sub procSTEPMovePOS()
        Dim status As Short
        Dim lACC, lSpeed, lDistance As Long
        lACC = CLng(Val(frmHome.txtIncrementACC.Text))
        lSpeed = CLng(Val(frmHome.txtIncrementSpeed.Text))
        lDistance = CLng(Val(frmHome.txtIncrmentDistance.Text))
        Dim sAxis As Short = CShort(frmHome.numAxis.Value)

        RoboticControl.IsSUDDEN_STOP = False
        status = 1

        '
        CtrlCard.adt8940a1_Setup_Speed(sAxis, 0, lSpeed, lACC)
        CtrlCard.Axis_Pmove(sAxis, -lDistance)
        Do
            Application.DoEvents()
            CtrlCard.adt8940a1_Get_MoveStatus(sAxis, status, 0)
        Loop Until status = 0 Or RoboticControl.IsSUDDEN_STOP

        ' Sudden stop
        If IsSUDDEN_STOP Then
            adt8940a1.reset_fifo(0)
            For a As Short = 1 To 4
                CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
            Next
            Exit Sub
        End If
    End Sub
    Public Shared Sub procSTEPMoveNEG()
        Dim status As Short
        Dim lACC, lSpeed, lDistance As Long
        lACC = CLng(Val(frmHome.txtIncrementACC.Text))
        lSpeed = CLng(Val(frmHome.txtIncrementSpeed.Text))
        lDistance = CLng(Val(frmHome.txtIncrmentDistance.Text))
        Dim sAxis As Short = CShort(frmHome.numAxis.Value)

        RoboticControl.IsSUDDEN_STOP = False
        status = 1

        '
        CtrlCard.adt8940a1_Setup_Speed(sAxis, 0, lSpeed, lACC)
        CtrlCard.Axis_Pmove(sAxis, lDistance)
        Do
            Application.DoEvents()
            CtrlCard.adt8940a1_Get_MoveStatus(sAxis, status, 0)
        Loop Until status = 0 Or RoboticControl.IsSUDDEN_STOP

        ' Sudden stop
        If IsSUDDEN_STOP Then
            adt8940a1.reset_fifo(0)
            For a As Short = 1 To 4
                CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
            Next
            Exit Sub
        End If
    End Sub

    Shared tKick As New System.Threading.Thread(AddressOf procKick)
    Public Shared Sub procKick()
        Debug.WriteLine("Kick....")
        ' KICK ---------------------------------------------------------------------
        Dim pin As Short = 2
        Dim delay1 As Short = 100
        Dim delay2 As Short = 150

        ' ส่งสัญญาณ
        If isNG Then
            CtrlCard.adt8940a1_Write_Output(3, 1) 'NG SIGNAL
            System.Threading.Thread.Sleep(100)
        End If

        CtrlCard.adt8940a1_Write_Output(3, 0)
        System.Threading.Thread.Sleep(100)

        Dim inPosition As Boolean = False

        Do
            Application.DoEvents()

            If CtrlCard.adt8940a1_Read_Input(28) = 0 Then 'in28 = ready to kick 
                inPosition = True
                Exit Do
            End If
            If CtrlCard.adt8940a1_Read_Input(20) = 1 Or RoboticControl.IsSUDDEN_STOP Then
                Exit Do
            End If
        Loop While True

        If inPosition Then
            Threading.Thread.Sleep(300)
            CtrlCard.adt8940a1_Write_Output(pin, 1)
            Threading.Thread.Sleep(1000)
            CtrlCard.adt8940a1_Write_Output(pin, 0)
        End If

        CtrlCard.adt8940a1_Write_Output(pin, 0)
        CtrlCard.adt8940a1_Write_Output(3, 0)

        Debug.WriteLine("Kick....DONE")
    End Sub

    Public Shared tReload As New System.Threading.Thread(AddressOf procReload)
    Public Shared Sub procReload()
        Debug.WriteLine("Reloading....")
        IsSUDDEN_STOP = False
        Dim inPosition As Boolean = False
        Dim reloaded As Boolean = False
        Do
            Application.DoEvents()

            If CtrlCard.adt8940a1_Read_Input(27) = 0 Then
                'in27 = reloaded sensor-มีตะกร้าค้าง - photo sensor - BBOT-IN27
                reloaded = True
                inPosition = False
                Exit Do
            End If
            If CtrlCard.adt8940a1_Read_Input(30) = 1 And CtrlCard.adt8940a1_Read_Input(5) = 0 Then
                'in30 = cvy stop, ready to reload - P3-IN30-PLC2-Y2
                'in5 = cvy sensor, in-position - photo sensor - J5-IN5-CVY-P3-PHOTO-2
                inPosition = True
                Exit Do
            End If
            If CtrlCard.adt8940a1_Read_Input(20) = 1 Or RoboticControl.IsSUDDEN_STOP Then
                Exit Do
            End If
        Loop While True

        If inPosition Then
            Const outpin As Short = 10
            Const pulse_ms As Integer = 1000
            CtrlCard.adt8940a1_Write_Output(outpin, 1)
            Threading.Thread.Sleep(pulse_ms)
            CtrlCard.adt8940a1_Write_Output(outpin, 0)
            Threading.Thread.Sleep(pulse_ms)

            Debug.WriteLine("Reload....pulse")
        End If

        If CtrlCard.adt8940a1_Read_Input(20) = 1 Or RoboticControl.IsSUDDEN_STOP Or reloaded Then
            Debug.WriteLine("Reload....Cancel")
        End If

    End Sub

    Public Shared tTestReload As New System.Threading.Thread(AddressOf procTestReload)
    Public Shared Sub procTestReload()
        Const outpin As Short = 10
        CtrlCard.adt8940a1_Write_Output(outpin, 1)
    End Sub

    Public Shared tClearReload As New System.Threading.Thread(AddressOf procClearReload)
    Public Shared Sub procClearReload()
        Const outpin As Short = 10
        CtrlCard.adt8940a1_Write_Output(outpin, 0)
    End Sub

    Shared LIFT_10 As Long = -103170 ' -59466
    Shared LIFT_DOWN As Long = 120000
    Shared LIFT_UP As Long = 115675 + LIFT_DOWN
    Shared LIFT_UP_STD As Long = 9000 '7560
    Shared DY = -LIFT_UP + LIFT_DOWN
    Public Shared Function FirstAdjust() As Short
        Try
            IsSUDDEN_STOP = False

            Dim status(0 To 3) As Short

            ' --------------------------------------------------------------------------------
            ' LEVELING
            ' --------------------------------------------------------------------------------
            Dim LPOS, APOS, Speed As Long
            Dim Y_SENSOR As Long
            Dim YC(0 To 9) As Long
            Dim Nbr, BNbr As Integer

            ' --------------------------------------------------------------------------------
            ' SENSOR LEVELING
            ' --------------------------------------------------------------------------------
            ' GO UP
            CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.LIFT, 0, 0)
            CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.LIFT, 1, 0)
            CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.LIFT, 0, 0, 0)
            CtrlCard.adt8940a1_Disable_Manu(AxisNbr.LIFT)
            CtrlCard.adt8940a1_Disable_Manu(AxisNbr.ARM)
            CtrlCard.adt8940a1_Disable_Manu(AxisNbr.HAND)
            CtrlCard.adt8940a1_Disable_Manu(AxisNbr.TURN)
            CtrlCard.adt8940a1_Setup_Speed(AxisNbr.LIFT, 0, 80000, 5000)
            CtrlCard.Axis_Pmove(AxisNbr.LIFT, -3000000) ' -HEIGH * 796
            status(AxisNbr.LIFT - 1) = 1
            Do
                Application.DoEvents()
                CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.LIFT, status(AxisNbr.LIFT - 1), 0)
            Loop Until status(AxisNbr.LIFT - 1) = 0 Or CtrlCard.adt8940a1_Read_Input(InputNbr.BTOP) = 0 Or CtrlCard.adt8940a1_Read_Input(0) = 0 Or RoboticControl.IsSUDDEN_STOP


            ' ---------------------------------------------------------------------
            ' Sudden stop
            adt8940a1.reset_fifo(0)
            CtrlCard.adt8940a1_StopRun(AxisNbr.LIFT, 0) 'Stop each axis
            If IsSUDDEN_STOP Then Return 0
            Threading.Thread.Sleep(200)
            CtrlCard.adt8940a1_Get_CurrentInfo(AxisNbr.LIFT, LPOS, APOS, Speed)
            Y_SENSOR = LPOS

            ' --------------------------------------------------------------------------------
            ' FIND NUMBER OF BASKET
            ' --------------------------------------------------------------------------------
            Nbr = -1
            For I As Integer = 0 To 9
                YC(I) = LIFT_10 + DY * (I)
                If YC(I) - LIFT_DOWN / 4 <= Y_SENSOR And Y_SENSOR <= YC(I) + LIFT_DOWN / 4 Then
                    Nbr = I
                    Exit For
                End If
            Next
            If Nbr < 0 Then
                MsgBox("Wrong basket orientation! [" & Y_SENSOR.ToString() & "]", MsgBoxStyle.Exclamation, "ERROR")
                Return 0
            Else
                BNbr = 10 - Nbr
            End If

            ' CAUTION!! , FOR TEST
            'BNbr = 10

            ' --------------------------------------------------------------------------------
            ' FIRST ADJUSTMENT
            ' --------------------------------------------------------------------------------
            Dim DY_SENSOR As Long = YC(Nbr) - Y_SENSOR
            CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.LIFT, 0, 0)
            CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.LIFT, 1, 0)
            CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.LIFT, 0, 0, 0)
            CtrlCard.adt8940a1_Setup_Speed(AxisNbr.LIFT, 0, LiftSpeed, LiftAcc2)
            CtrlCard.Axis_Pmove(AxisNbr.LIFT, DY_SENSOR) ' -HEIGH * 796

            status(AxisNbr.LIFT - 1) = 1
            Do
                Application.DoEvents()
                CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.LIFT, status(AxisNbr.LIFT - 1), 0)
            Loop Until status(AxisNbr.LIFT - 1) = 0 Or RoboticControl.IsSUDDEN_STOP
            Threading.Thread.Sleep(200)

            ' Sudden stop
            If IsSUDDEN_STOP Then
                adt8940a1.reset_fifo(0)
                For a As Short = 1 To 4
                    CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                Next
                Return 0
            End If

            Return BNbr

        Catch ex As Exception
            Return 0
        End Try

    End Function

    Public Shared LiftSpeed As Long = 350000
    Public Shared LiftDownSpeed As Long = 450000
    Public Shared LiftAcc1 As Long = 4000
    Public Shared LiftAcc2 As Long = 6000

    Public Shared ARM_ACC As Long = 7000
    Public Shared ARM_SPEED As Long = 590000

    Public Shared ARM_Range As Double = 185

    Public Shared IsEnable As Boolean = True
    Private Shared IsEmptyPicking As Boolean = False

    Private Shared Time_HandOFFDelay As Integer = 200

    Private Shared ARM_TO_CAM_RANGE As Double = 108
    Private Shared HAND_TO_CAM_RANGE As Double = 30

    Public Shared Sub ResetControlBox()
        'ไฟเตือนที่ห้องเครื่อง
        CtrlCard.adt8940a1_Write_Output(15, 1)

        ' Break
        CtrlCard.adt8940a1_Write_Output(RoboticControl.ContactNo.V_Break, 0)
        Times.Delay_ms2(1000)

        ' Hand off
        CtrlCard.adt8940a1_Write_Output(13, 0)
        Times.Delay_ms2(1000)

        ' SV OFF
        CtrlCard.adt8940a1_Write_Output(RoboticControl.ContactNo.SON, 0)
        Times.Delay_ms2(2000)

        ' MC OFF
        CtrlCard.adt8940a1_Write_Output(RoboticControl.ContactNo.MC_OFF, 1)
        Times.Delay_ms2(200)
        CtrlCard.adt8940a1_Write_Output(RoboticControl.ContactNo.MC_OFF, 0)

        ' SEND Reset
        CtrlCard.adt8940a1_Write_Output(RoboticControl.ContactNo.ARST, 1)
        Times.Delay_ms2(2000)
        CtrlCard.adt8940a1_Write_Output(RoboticControl.ContactNo.ARST, 0)

        ' MC ON
        CtrlCard.adt8940a1_Write_Output(RoboticControl.ContactNo.MC_ON, 1)
        Times.Delay_ms2(1000)
        CtrlCard.adt8940a1_Write_Output(RoboticControl.ContactNo.MC_ON, 0)

        ' SV ON
        CtrlCard.adt8940a1_Write_Output(RoboticControl.ContactNo.SON, 1)
        Times.Delay_ms2(1000)

        ' Unbreake
        CtrlCard.adt8940a1_Write_Output(RoboticControl.ContactNo.V_Break, 1)
        Times.Delay_ms2(1000)
    End Sub

    Public Shared Sub GetUI()

        Try
            Long.TryParse(frmHome.txtUI_LiftDown.Text, LIFT_DOWN)
            My.Settings.LIFT_Down = frmHome.txtUI_LiftDown.Text
            My.Settings.Save()
        Catch ex As Exception
            LIFT_DOWN = 120000
        End Try

        Try
            LIFT_10 = CLng(Val(frmHome.txtFirstAdjust.Text))
            If LIFT_10 > 0 Then LIFT_10 *= -1
            My.Settings.Lift_10 = frmHome.txtFirstAdjust.Text
            My.Settings.Save()
        Catch ex As Exception

        End Try

        Try
            LIFT_UP = CLng(Val(frmHome.txtLiftUP.Text)) + LIFT_DOWN
            My.Settings.Lift_up = frmHome.txtLiftUP.Text
            My.Settings.Save()
        Catch ex As Exception

        End Try

        LIFT_UP_STD = 9000
        DY = -LIFT_UP + LIFT_DOWN

        Try
            ARM_ACC = CLng(Val(frmHome.txtArmACC.Text))
            My.Settings.Arm_ACC = frmHome.txtArmACC.Text
            My.Settings.Save()
        Catch ex As Exception
            ARM_ACC = 6000
        End Try
        Try
            ARM_SPEED = CLng(Val(frmHome.txtArmSpeed.Text))
            My.Settings.Arm_Speed = frmHome.txtArmSpeed.Text
            My.Settings.Save()
        Catch ex As Exception
            ARM_SPEED = 560000
        End Try

        Try
            Long.TryParse(frmHome.txtLiftSpeed.Text, LiftSpeed)
            My.Settings.LIFT_SPEED = frmHome.txtLiftSpeed.Text
            My.Settings.Save()
        Catch ex As Exception
            LiftSpeed = 300000
        End Try

        Try
            Long.TryParse(frmHome.txtUILiftDownSpeed.Text, LiftDownSpeed)
            My.Settings.LIFT_DOWN_SPEED = frmHome.txtUILiftDownSpeed.Text
            My.Settings.Save()
        Catch ex As Exception
            LiftDownSpeed = 300000
        End Try

        Try
            Long.TryParse(frmHome.txtLiftAcc1.Text, LiftAcc1)
            My.Settings.LIFT_ACC1 = frmHome.txtLiftAcc1.Text
            My.Settings.Save()
        Catch ex As Exception
            LiftAcc1 = 6000
        End Try

        Try
            Long.TryParse(frmHome.txtLiftAcc2.Text, LiftAcc2)
            My.Settings.LIFT_ACC2 = frmHome.txtLiftAcc2.Text
            My.Settings.Save()
        Catch ex As Exception
            LiftAcc2 = 6000
        End Try

        Try
            Double.TryParse(frmHome.txtUIArmRange.Text, ARM_Range)
            My.Settings.ARM_RANGE = frmHome.txtUIArmRange.Text
            My.Settings.Save()
        Catch ex As Exception
            ARM_Range = 179
        End Try

        Try
            Integer.TryParse(frmHome.txtUIHandOFFDelay.Text, Time_HandOFFDelay)
            My.Settings.Time_HandOFF = frmHome.txtUIHandOFFDelay.Text
            My.Settings.Save()
        Catch ex As Exception
            Time_HandOFFDelay = 500
        End Try

        Try
            Double.TryParse(frmHome.txtUIArmToCam.Text, ARM_TO_CAM_RANGE)
            My.Settings.ARM_TO_CAM_RANGE = frmHome.txtUIArmToCam.Text
            My.Settings.Save()
        Catch ex As Exception
            ARM_TO_CAM_RANGE = 108
        End Try

        Try
            Double.TryParse(frmHome.txtHandToCam.Text, HAND_TO_CAM_RANGE)
            My.Settings.HAND_TO_CAM_RANGE = frmHome.txtHandToCam.Text
            My.Settings.Save()
        Catch ex As Exception
            HAND_TO_CAM_RANGE = 200
        End Try

        IsEmptyPicking = frmHome.chkEmptyPicking.Checked

    End Sub


    Public Shared Function FirstAdjust10() As Short
        Try
            IsSUDDEN_STOP = False

            Dim status(0 To 3) As Short

            '' --------------------------------------------------------------------------------
            '' LEVELING
            '' --------------------------------------------------------------------------------
            Dim LPOS, APOS, Speed As Long
            Dim Y_SENSOR As Long
            Dim YC(0 To 9) As Long
            Dim Nbr, BNbr As Integer

            Dim nLogPos As Long                   'Logical location
            Dim nActPos As Long                   'Actual location
            Dim nSpeed As Long                    'Running Speeed
            CtrlCard.adt8940a1_Get_CurrentInfo(1, nLogPos, nActPos, nSpeed)
            Debug.WriteLine(nLogPos.ToString() & ", " & nActPos.ToString & ", " & (nLogPos - nActPos).ToString)


            ' CAUTION!! , FOR TEST
            BNbr = 10

            ' --------------------------------------------------------------------------------
            ' FIRST ADJUSTMENT
            ' --------------------------------------------------------------------------------
            Dim DY_SENSOR As Long = YC(Nbr) - Y_SENSOR
            CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.LIFT, 0, 0)
            CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.LIFT, 1, 0)
            CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.LIFT, 0, 0, 0)
            CtrlCard.adt8940a1_Setup_Speed(AxisNbr.LIFT, 0, LiftSpeed, LiftAcc1)
            CtrlCard.Axis_Pmove(AxisNbr.LIFT, LIFT_10) ' -HEIGH * 796 Need soft move up

            status(AxisNbr.LIFT - 1) = 1
            Do
                Application.DoEvents()
                CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.LIFT, status(AxisNbr.LIFT - 1), 0)
            Loop Until status(AxisNbr.LIFT - 1) = 0 Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
            Threading.Thread.Sleep(200)

            ' Sudden stop
            If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                adt8940a1.reset_fifo(0)
                For a As Short = 1 To 4
                    CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                Next
                Return 0
            End If

            CtrlCard.adt8940a1_Get_CurrentInfo(1, nLogPos, nActPos, nSpeed)
            Debug.WriteLine(nLogPos.ToString() & ", " & nActPos.ToString & ", " & (nLogPos - nActPos).ToString)

            Debug.WriteLine("First adjust")

            Return BNbr

        Catch ex As Exception
            Return 0
        End Try

    End Function

    'ใส่ TURN 11:47 16/05/17
    Shared lArmLPos, lTurnLPos As Long
    Shared postID As Integer = 0
    Public Shared isNG As Boolean = False
    Shared sb1 As New StringBuilder, sb2 As New StringBuilder
    Shared firstTimeText As String
    Public Shared Sub PICK_SLOW()
        Try
            frmHome.FreezUI()
            frmMain.PostText("SLOW Start")
            postID = 0
            IsSUDDEN_STOP = False

            Dim lArmSpeed As Long = ARM_SPEED
            Dim lArmACC As Long = ARM_ACC

            'If MsgBox("Top = " & LIFT_10 & ", Dz = " & DY & " Arm speed/ACC = " & ARM_SPEED & ", " & ARM_ACC & Environment.NewLine & "ต้องการทำต่อใช่หรือไม่", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
            '    IsEnable = True
            '    Exit Sub
            'End If

            CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.LIFT, 0, 0)
            CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.LIFT, 1, 0)
            CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.LIFT, 0, 0, 0)

            CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.ARM, 0, 0)
            CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.ARM, 1, 0)
            CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.ARM, 1, 1, 0)

            CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.HAND, 0, 0)
            CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.HAND, 1, 0)
            CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.HAND, 1, 1, 0)

            CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.TURN, 0, 0)
            CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.TURN, 1, 0)
            CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.TURN, 1, 1, 0)

            Do
                '---------------------------------------------------------------------------
                If CtrlCard.adt8940a1_Read_Input(27) = 1 Then
                    frmMain.PostText("Reload")
                    If CtrlCard.adt8940a1_Read_Input(5) = 1 Then
                        Do
                            Application.DoEvents()
                        Loop Until CtrlCard.adt8940a1_Read_Input(5) = 0 Or IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
                    End If ' INPUT 5
                    Threading.Thread.Sleep(1000)
                    If CtrlCard.adt8940a1_Read_Input(4) = 0 Then
                        LGRN = True
                    Else
                        LGRN = False
                    End If
                    Do
                        tReload = New System.Threading.Thread(AddressOf procReload)
                        tReload.Start()
                        Application.DoEvents()
                    Loop Until CtrlCard.adt8940a1_Read_Input(27) = 0 Or IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
                    Threading.Thread.Sleep(600)
                End If
                If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then Exit Do
                '---------------------------------------------------------------------------
                Application.DoEvents()

                frmMain.PostText("SLOW Cycle")

                Threading.Thread.Sleep(200)
                'Continue Do

                Dim status(0 To 3) As Short
                adt8940a1.reset_fifo(0)

                Home2()
                CtrlCard.adt8940a1_Setup_Pos(2, 0, 0)     'Logical position counter clear
                CtrlCard.adt8940a1_Setup_Pos(2, 0, 1)     'Clear the actual position of the counter

                ' Hand off
                CtrlCard.adt8940a1_Write_Output(OUTPUT_HAND, 0)
                Times.Delay_ms(200)
                ' ZERO
                CtrlCard.adt8940a1_Sym_AbsoluteMove(1, 0, 0, 200000, 2)
                Do
                    Application.DoEvents()
                    CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.LIFT, status(AxisNbr.LIFT - 1), 0)
                Loop Until status(AxisNbr.LIFT - 1) = 0 Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
                ' Sudden stop
                If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                    adt8940a1.reset_fifo(0)
                    For a As Short = 1 To 4
                        CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                    Next
                    Exit Try
                End If

                Dim BNbr As Integer = FirstAdjust()

                ' --------------------------------------------------------------------------------
                ' LIFT LOOP
                ' --------------------------------------------------------------------------------
                For ib As Integer = 1 To BNbr

                    ' จับ---------------------------------------------------------------------
                    'Times.Delay_ms(500)
                    CtrlCard.adt8940a1_Write_Output(OUTPUT_HAND, 1)
                    Times.Delay_ms(250)

                    ' ตรวจการจับ---------------------------------------------------------------------
                    If CtrlCard.adt8940a1_Read_Input(InputNbr.HAND_R) = 1 Then
                        'do nothing
                    Else
                        ' ปล่อย
                        CtrlCard.adt8940a1_Write_Output(OUTPUT_HAND, 0)
                        Times.Delay_ms(700)

                        ' ขึ้น
                        status(AxisNbr.LIFT - 1) = 0
                        CtrlCard.adt8940a1_Setup_Speed(AxisNbr.LIFT, 0, 400000, 5000)
                        CtrlCard.Axis_Pmove(AxisNbr.LIFT, -5 * 796)
                        Do
                            Application.DoEvents()
                            CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.LIFT, status(AxisNbr.LIFT - 1), 0)
                        Loop Until status(AxisNbr.LIFT - 1) = 0 Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

                        ' Sudden stop
                        If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                            adt8940a1.reset_fifo(0)
                            For a As Short = 1 To 4
                                CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                            Next
                            Exit Try
                        End If

                        ' จับ
                        CtrlCard.adt8940a1_Write_Output(OUTPUT_HAND, 1)
                        Times.Delay_ms(700)

                    End If

                    ' ลง ---------------------------------------------------------------------
                    If 1 <= ib And ib <= BNbr - 1 Then
                        status(AxisNbr.LIFT - 1) = 0
                        CtrlCard.adt8940a1_Setup_Speed(AxisNbr.LIFT, 0, 650000, 50000)
                        CtrlCard.Axis_Pmove(AxisNbr.LIFT, LIFT_DOWN)
                        Do
                            Application.DoEvents()
                            CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.LIFT, status(AxisNbr.LIFT - 1), 0)
                        Loop Until status(AxisNbr.LIFT - 1) = 0 Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

                        ' Sudden stop
                        If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                            adt8940a1.reset_fifo(0)
                            For a As Short = 1 To 4
                                CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                            Next
                            Exit Try
                        End If
                    End If

                    ' สำหรับใบสุดท้าย ลดตัวยกได้ ---------------------------------------------------
                    If ib = BNbr Then
                        tEndLift = New System.Threading.Thread(AddressOf procEndLift)
                        tEndLift.Start()
                    End If

                    '******************************************************************************************************
                    Dim armStroke As Double = 185
                    Dim handStroke As Double = 185
                    ' บิดมือเข้า พร้อม ยกขึ้นเล็กน้อย ---------------------------------------------------------------------
                    status(AxisNbr.ARM - 1) = 0 'แขน
                    status(AxisNbr.HAND - 1) = 0 'มือ
                    'CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.ARM, 0, 0)
                    'CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.ARM, 1, 0)
                    'CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.ARM, 1, 1, 0)
                    CtrlCard.adt8940a1_Setup_Speed(AxisNbr.HAND, 0, lArmSpeed, lArmACC)
                    CtrlCard.adt8940a1_Setup_Speed(AxisNbr.ARM, 0, lArmSpeed, lArmACC)
                    CtrlCard.Axis_Pmove(AxisNbr.HAND, handStroke * 1421)
                    CtrlCard.Axis_Pmove(AxisNbr.ARM, -armStroke * 2741)
                    Threading.Thread.Sleep(200)
                    ' ยกขึ้นตามระยะ ---------------------------------------------------------------------
                    If 1 <= ib And ib <= BNbr - 1 Then
                        tLift = New Threading.Thread(AddressOf Lift)
                        tLift.Start()
                    End If
                    Do
                        Application.DoEvents()
                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.ARM, status(AxisNbr.ARM - 1), 0)
                    Loop Until status(AxisNbr.ARM - 1) = 0 Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

                    ' Sudden stop
                    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If

                    '' ยกขึ้นตามระยะ ---------------------------------------------------------------------
                    'If 1 <= ib And ib <= BNbr - 1 Then
                    '    tLift = New Threading.Thread(AddressOf Lift)
                    '    tLift.Start()
                    'End If

                    ' ปล่อยมือ ---------------------------------------------------------------------
                    CtrlCard.adt8940a1_Write_Output(OUTPUT_HAND, 0)
                    System.Threading.Thread.Sleep(200)



                    '******************************************************************************************************
                    ' แขนกลับ 0 พร้อมกับ ---------------------------------------------------------------------
                    ' หมุนมือกลับ ---------------------------------------------------------------------
                    status(AxisNbr.ARM - 1) = 1
                    status(AxisNbr.HAND - 1) = 1
                    CtrlCard.adt8940a1_Setup_Speed(AxisNbr.ARM, 0, lArmSpeed, lArmACC)
                    CtrlCard.adt8940a1_Setup_Speed(AxisNbr.HAND, 0, lArmSpeed, lArmACC)
                    CtrlCard.Axis_Pmove(AxisNbr.ARM, armStroke * 2741)
                    CtrlCard.Axis_Pmove(AxisNbr.HAND, -handStroke * 1421)

                    ' หมุนตะกร้า
                    If Not ib Mod 2 = 0 Then
                        'Check ตำแหน่ง และเลือกทิศ
                        adt8940a1.get_command_pos(0, 4, lTurnLPos)
                        If lTurnLPos > 2147483648 Then
                            lTurnLPos = lTurnLPos - 4294967296
                        End If
                        ' รอองศาแขนพ้นตะกร้า
                        'Threading.Thread.Sleep(500)
                        'lArmLPos = 0
                        'adt8940a1.get_command_pos(0, 2, lArmLPos)
                        'If lArmLPos > 2147483648 Then
                        '    lArmLPos = lArmLPos - 4294967296
                        'End If
                        'frmMain.Text = lArmLPos
                        'สั่งหมุน
                        If lTurnLPos > 0 Then
                            tTurnNegative = New System.Threading.Thread(AddressOf procTurnNegative)
                            tTurnNegative.Start()
                            'procTurnNegative()
                        ElseIf lTurnLPos = 0 Then
                            tTurnPostive = New System.Threading.Thread(AddressOf procTurnPositive)
                            tTurnPostive.Start()
                            procTurnPositive()
                        Else ' Less than zero
                            tTurnPostive = New System.Threading.Thread(AddressOf procTurnPositive)
                            tTurnPostive.Start()
                            'procTurnPositive()
                        End If
                    Else
                        'แจ้งตัว CVY ให้รับตะกร้าทำงานต่อไป เช่น แตะ
                        tKick = New System.Threading.Thread(AddressOf procKick)
                        tKick.Start()
                    End If

                    postID += 1
                    If postID = 1 Then
                        sb1.Remove(0, sb1.Length)
                        sb1.Append("1. ")
                        firstTimeText = Now.ToString("HH_mm_ss")
                        sb1.Append(firstTimeText)
                        System.IO.File.AppendAllText("C:\log\picker\pick_" & firstTimeText & ".txt", sb1.ToString())
                    Else
                        sb2.Remove(0, sb2.Length)
                        sb2.Append(Environment.NewLine)
                        sb2.Append(postID.ToString)
                        sb2.Append(". ")
                        sb2.Append(Now.ToString("HH:mm:ss"))
                        System.IO.File.AppendAllText("C:\log\picker\pick_" & firstTimeText & ".txt", sb2.ToString())
                    End If

                    ' รอการกลับ 0 มือและแขน---------------------------------------------------------------------
                    'Times.Delay_ms(1000)
                    status(AxisNbr.ARM - 1) = 1
                    status(AxisNbr.HAND - 1) = 1
                    Do
                        Application.DoEvents()
                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.ARM, status(AxisNbr.ARM - 1), 0)
                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.HAND, status(AxisNbr.HAND - 1), 0)
                    Loop Until (status(AxisNbr.ARM - 1) = 0 And status(AxisNbr.HAND - 1) = 0) Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

                    ' Sudden stop
                    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If

                    ' Goto home
                    'CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.HAND, 0, 0, 200000, 0.5)
                    CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.ARM, 0, 0, 50000, 2)

                    ' รอให้นิ่ง---------------------------------------------------------------------
                    status(AxisNbr.LIFT - 1) = 1
                    status(AxisNbr.HAND - 1) = 1
                    status(AxisNbr.ARM - 1) = 1
                    Do
                        Application.DoEvents()
                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.ARM, status(AxisNbr.ARM - 1), 0)
                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.HAND, status(AxisNbr.HAND - 1), 0)
                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.LIFT, status(AxisNbr.LIFT - 1), 0)
                    Loop Until (status(AxisNbr.ARM - 1) = 0 And status(AxisNbr.HAND - 1) = 0 And status(AxisNbr.LIFT - 1) = 0) Or IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

                    ' Sudden stop
                    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If

                Next ib

                ' Goto home
                status(AxisNbr.LIFT - 1) = 1
                status(AxisNbr.HAND - 1) = 1
                status(AxisNbr.ARM - 1) = 1
                CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.LIFT, 0, 0, 200000, 2)
                CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.ARM, 0, 0, 200000, 2)
                CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.HAND, 0, 0, 200000, 0.5)
                Do
                    Application.DoEvents()
                    CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.HAND, status(AxisNbr.HAND - 1), 0)
                    CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.ARM, status(AxisNbr.ARM - 1), 0)
                    CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.LIFT, status(AxisNbr.LIFT - 1), 0)
                Loop Until (status(AxisNbr.HAND - 1) = 0 And status(AxisNbr.ARM - 1) = 0 And status(AxisNbr.LIFT - 1) = 0) Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

                ' Sudden stop
                adt8940a1.reset_fifo(0)
                For a As Short = 1 To 4
                    CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                Next

                ' ---------------------------------------------------------------------

            Loop Until IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

            IsSUDDEN_STOP = True

            'MsgBox("Cycle ended!", MsgBoxStyle.Information, "Fast Cycle")
            IsEnable = True

            frmMain.PostText("END Cycle")
            frmHome.LunchUI()

        Catch ex As Exception
            frmHome.LunchUI()
            ' Sudden stop
            adt8940a1.reset_fifo(0)
            For i = 1 To 4
                CtrlCard.adt8940a1_StopRun(i, 0) 'Stop each axis
            Next i
        End Try

    End Sub


    Shared LGRN As Boolean = False
    'Public Shared Sub PICK_FAST()
    '    Try
    '        frmHome.FreezUI()
    '        frmMain.PostText("Fast Start")
    '        postID = 0
    '        IsSUDDEN_STOP = False

    '        Dim lArmSpeed As Long = ARM_SPEED
    '        Dim lArmACC As Long = ARM_ACC

    '        If MsgBox("Top = " & LIFT_10 & ", Dz = " & DY & " Arm speed/ACC = " & ARM_SPEED & ", " & ARM_ACC & Environment.NewLine & "ต้องการทำต่อใช่หรือไม่", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
    '            IsEnable = True
    '            Exit Sub
    '        End If

    '        CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.LIFT, 0, 0)
    '        CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.LIFT, 1, 0)
    '        CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.LIFT, 0, 0, 0)

    '        CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.ARM, 0, 0)
    '        CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.ARM, 1, 0)
    '        CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.ARM, 0, 0, 0)

    '        CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.HAND, 0, 0)
    '        CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.HAND, 1, 0)
    '        CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.HAND, 1, 1, 0)

    '        CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.TURN, 0, 0)
    '        CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.TURN, 1, 0)
    '        CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.TURN, 1, 1, 0)

    '        Do
    '            '---------------------------------------------------------------------------
    '            If CtrlCard.adt8940a1_Read_Input(27) = 1 Then 'Bottom sensor = 27, 1 = OFF
    '                frmMain.PostText("Reload")
    '                If CtrlCard.adt8940a1_Read_Input(5) = 1 Then 'CVY Sensor = 5
    '                    Do
    '                        Application.DoEvents()
    '                    Loop Until CtrlCard.adt8940a1_Read_Input(5) = 0 Or IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
    '                End If ' INPUT 5
    '                Threading.Thread.Sleep(1000)
    '                If CtrlCard.adt8940a1_Read_Input(4) = 1 Then
    '                    LGRN = True
    '                Else
    '                    LGRN = False
    '                End If
    '                Do
    '                    tReload = New System.Threading.Thread(AddressOf procReload)
    '                    tReload.Start()
    '                    Application.DoEvents()
    '                Loop Until CtrlCard.adt8940a1_Read_Input(27) = 0 Or IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
    '                Threading.Thread.Sleep(600)
    '            End If
    '            If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then Exit Do
    '            '---------------------------------------------------------------------------
    '            Application.DoEvents()

    '            frmMain.PostText("Cycle")

    '            Threading.Thread.Sleep(200)
    '            'Continue Do

    '            Dim status(0 To 3) As Short
    '            adt8940a1.reset_fifo(0)

    '            'Home2()
    '            'CtrlCard.adt8940a1_Setup_Pos(2, 0, 0)     'Logical position counter clear
    '            'CtrlCard.adt8940a1_Setup_Pos(2, 0, 1)     'Clear the actual position of the counter

    '            ' Hand off
    '            CtrlCard.adt8940a1_Write_Output(OUTPUT_HAND, 0)
    '            Times.Delay_ms(200)
    '            ' ZERO
    '            CtrlCard.adt8940a1_Sym_AbsoluteMove(1, 0, 0, 200000, 2)
    '            Do
    '                Application.DoEvents()
    '                CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.LIFT, status(AxisNbr.LIFT - 1), 0)
    '            Loop Until status(AxisNbr.LIFT - 1) = 0 Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
    '            ' Sudden stop
    '            If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
    '                adt8940a1.reset_fifo(0)
    '                For a As Short = 1 To 4
    '                    CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
    '                Next
    '                Exit Try
    '            End If

    '            Dim BNbr As Integer = FirstAdjust10()

    '            ' --------------------------------------------------------------------------------
    '            ' LIFT LOOP
    '            ' --------------------------------------------------------------------------------
    '            For ib As Integer = 1 To BNbr

    '                ' ตรวจความว่างที่จานหมุน ------------------------------------------------------------
    '                Do
    '                    'tKick = New System.Threading.Thread(AddressOf procKick)
    '                    'tKick.Start()
    '                    Application.DoEvents()
    '                    'in22 = aexp+ = Ready to Pick = acitve Low
    '                    'in23 = inPosition sensor (empty)
    '                    'in28 = Ready to kick = Active low
    '                Loop Until (CtrlCard.adt8940a1_Read_Input(22) = 0 And
    '                            CtrlCard.adt8940a1_Read_Input(28) = 0 And CtrlCard.adt8940a1_Read_Input(23) = 1) _
    '                    Or IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

    '                ' จับ---------------------------------------------------------------------
    '                'Times.Delay_ms(500)
    '                CtrlCard.adt8940a1_Write_Output(OUTPUT_HAND, 1)
    '                Times.Delay_ms(250)

    '                ' ตรวจการจับ---------------------------------------------------------------------
    '                'If CtrlCard.adt8940a1_Read_Input(InputNbr.HAND_R) = 0 Then
    '                '    'do nothing
    '                'Else
    '                '    ' ปล่อย
    '                '    CtrlCard.adt8940a1_Write_Output(OUTPUT_HAND, 0)
    '                '    Times.Delay_ms(700)

    '                '    ' ขึ้น
    '                '    status(AxisNbr.LIFT - 1) = 0
    '                '    CtrlCard.adt8940a1_Setup_Speed(AxisNbr.LIFT, 0, 400000, 5000)
    '                '    CtrlCard.Axis_Pmove(AxisNbr.LIFT, -5 * 796)
    '                '    Do
    '                '        Application.DoEvents()
    '                '        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.LIFT, status(AxisNbr.LIFT - 1), 0)
    '                '    Loop Until status(AxisNbr.LIFT - 1) = 0 Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

    '                '    ' Sudden stop
    '                '    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
    '                '        adt8940a1.reset_fifo(0)
    '                '        For a As Short = 1 To 4
    '                '            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
    '                '        Next
    '                '        Exit Try
    '                '    End If

    '                '    ' จับ
    '                '    CtrlCard.adt8940a1_Write_Output(OUTPUT_HAND, 1)
    '                '    Times.Delay_ms(700)

    '                'End If

    '                ' ลง ---------------------------------------------------------------------
    '                If 1 <= ib And ib <= BNbr - 1 Then
    '                    status(AxisNbr.LIFT - 1) = 0
    '                    CtrlCard.adt8940a1_Setup_Speed(AxisNbr.LIFT, 0, 650000, 50000)
    '                    CtrlCard.Axis_Pmove(AxisNbr.LIFT, LIFT_DOWN)
    '                    Do
    '                        Application.DoEvents()
    '                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.LIFT, status(AxisNbr.LIFT - 1), 0)
    '                    Loop Until status(AxisNbr.LIFT - 1) = 0 Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

    '                    ' Sudden stop
    '                    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
    '                        adt8940a1.reset_fifo(0)
    '                        For a As Short = 1 To 4
    '                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
    '                        Next
    '                        Exit Try
    '                    End If
    '                End If

    '                ' สำหรับใบสุดท้าย ลดตัวยกได้ ---------------------------------------------------
    '                If ib = BNbr Then
    '                    tEndLift = New System.Threading.Thread(AddressOf procEndLift)
    '                    tEndLift.Start()
    '                End If

    '                '******************************************************************************************************
    '                Dim armStroke As Double = 185
    '                Dim handStroke As Double = 185
    '                ' บิดมือเข้า พร้อม ยกขึ้น ---------------------------------------------------------------------
    '                status(AxisNbr.ARM - 1) = 0 'แขน
    '                status(AxisNbr.HAND - 1) = 0 'มือ
    '                'CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.ARM, 0, 0)
    '                'CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.ARM, 1, 0)
    '                'CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.ARM, 1, 1, 0)
    '                CtrlCard.adt8940a1_Setup_Speed(AxisNbr.HAND, 0, lArmSpeed, lArmACC)
    '                CtrlCard.adt8940a1_Setup_Speed(AxisNbr.ARM, 0, lArmSpeed, lArmACC)
    '                CtrlCard.Axis_Pmove(AxisNbr.HAND, handStroke * 1421)
    '                CtrlCard.Axis_Pmove(AxisNbr.ARM, -armStroke * 2741)
    '                Threading.Thread.Sleep(200)
    '                ' ยกขึ้นตามระยะ ---------------------------------------------------------------------
    '                If 1 <= ib And ib <= BNbr - 1 Then
    '                    tLift = New Threading.Thread(AddressOf Lift)
    '                    tLift.Start()
    '                End If
    '                Do
    '                    Application.DoEvents()
    '                    CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.ARM, status(AxisNbr.ARM - 1), 0)
    '                Loop Until status(AxisNbr.ARM - 1) = 0 Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

    '                ' Sudden stop
    '                If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
    '                    adt8940a1.reset_fifo(0)
    '                    For a As Short = 1 To 4
    '                        CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
    '                    Next
    '                    Exit Try
    '                End If

    '                '' ยกขึ้นตามระยะ ---------------------------------------------------------------------
    '                'If 1 <= ib And ib <= BNbr - 1 Then
    '                '    tLift = New Threading.Thread(AddressOf Lift)
    '                '    tLift.Start()
    '                'End If

    '                ' ปล่อยมือ ---------------------------------------------------------------------
    '                CtrlCard.adt8940a1_Write_Output(OUTPUT_HAND, 0)
    '                System.Threading.Thread.Sleep(200)

    '                '******************************************************************************************************
    '                ' แขนกลับ 0 พร้อมกับ ---------------------------------------------------------------------
    '                ' หมุนมือกลับ ---------------------------------------------------------------------
    '                status(AxisNbr.ARM - 1) = 1
    '                status(AxisNbr.HAND - 1) = 1
    '                CtrlCard.adt8940a1_Setup_Speed(AxisNbr.ARM, 0, lArmSpeed, lArmACC)
    '                CtrlCard.adt8940a1_Setup_Speed(AxisNbr.HAND, 0, lArmSpeed, lArmACC)
    '                CtrlCard.Axis_Pmove(AxisNbr.ARM, armStroke * 2741)
    '                CtrlCard.Axis_Pmove(AxisNbr.HAND, -handStroke * 1421)

    '                ' หมุนตะกร้า-----------------------------------
    '                Dim ibt As Integer = ib
    '                If LGRN Then
    '                    ibt += 1
    '                End If
    '                If Not ibt Mod 2 = 0 Then
    '                    'Check ตำแหน่ง และเลือกทิศ
    '                    adt8940a1.get_command_pos(0, 4, lTurnLPos)
    '                    If lTurnLPos > 2147483648 Then
    '                        lTurnLPos = lTurnLPos - 4294967296
    '                    End If
    '                    ' รอองศาแขนพ้นตะกร้า
    '                    'Threading.Thread.Sleep(500)
    '                    'lArmLPos = 0
    '                    'adt8940a1.get_command_pos(0, 2, lArmLPos)
    '                    'If lArmLPos > 2147483648 Then
    '                    '    lArmLPos = lArmLPos - 4294967296
    '                    'End If
    '                    'frmMain.Text = lArmLPos
    '                    'สั่งหมุน
    '                    If lTurnLPos > 0 Then
    '                        tTurnNegative = New System.Threading.Thread(AddressOf procTurnNegative)
    '                        tTurnNegative.Start()
    '                        'procTurnNegative()
    '                    ElseIf lTurnLPos = 0 Then
    '                        tTurnPostive = New System.Threading.Thread(AddressOf procTurnPositive)
    '                        tTurnPostive.Start()
    '                        procTurnPositive()
    '                    Else ' Less than zero
    '                        tTurnPostive = New System.Threading.Thread(AddressOf procTurnPositive)
    '                        tTurnPostive.Start()
    '                        'procTurnPositive()
    '                    End If
    '                Else
    '                    'แจ้งตัว CVY ให้รับตะกร้าทำงานต่อไป เช่น แตะ
    '                    tKick = New System.Threading.Thread(AddressOf procKick)
    '                    tKick.Start()
    '                End If

    '                postID += 1
    '                'If postID = 1 Then
    '                '    isNG = True
    '                'Else
    '                '    isNG = False
    '                'End If
    '                'If postID = 1 Then
    '                '    sb1.Remove(0, sb1.Length)
    '                '    sb1.Append("1, ")
    '                '    firstTimeText = Now.ToString("HH_mm_ss")
    '                '    sb1.Append(firstTimeText)
    '                '    System.IO.File.AppendAllText("D:\log\picker\pick_" & firstTimeText & ".txt", sb1.ToString())
    '                'Else
    '                '    sb2.Remove(0, sb2.Length)
    '                '    sb2.Append(Environment.NewLine)
    '                '    sb2.Append(postID.ToString)
    '                '    sb2.Append(", ")
    '                '    sb2.Append(Now.ToString("HH:mm:ss"))
    '                '    System.IO.File.AppendAllText("D:\log\picker\pick_" & firstTimeText & ".txt", sb2.ToString())
    '                'End If

    '                ' รอการกลับ 0 มือและแขน---------------------------------------------------------------------
    '                'Times.Delay_ms(1000)
    '                status(AxisNbr.ARM - 1) = 1
    '                status(AxisNbr.HAND - 1) = 1
    '                Do
    '                    Application.DoEvents()
    '                    CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.ARM, status(AxisNbr.ARM - 1), 0)
    '                    CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.HAND, status(AxisNbr.HAND - 1), 0)
    '                Loop Until (status(AxisNbr.ARM - 1) = 0 And status(AxisNbr.HAND - 1) = 0) Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

    '                ' Sudden stop
    '                If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
    '                    adt8940a1.reset_fifo(0)
    '                    For a As Short = 1 To 4
    '                        CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
    '                    Next
    '                    Exit Try
    '                End If

    '                ' Goto home
    '                'CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.HAND, 0, 0, 200000, 0.5)
    '                CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.ARM, 0, 0, 50000, 2)

    '                ' รอให้นิ่ง---------------------------------------------------------------------
    '                status(AxisNbr.LIFT - 1) = 1
    '                status(AxisNbr.HAND - 1) = 1
    '                status(AxisNbr.ARM - 1) = 1
    '                Do
    '                    Application.DoEvents()
    '                    CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.ARM, status(AxisNbr.ARM - 1), 0)
    '                    CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.HAND, status(AxisNbr.HAND - 1), 0)
    '                    CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.LIFT, status(AxisNbr.LIFT - 1), 0)
    '                Loop Until (status(AxisNbr.ARM - 1) = 0 And status(AxisNbr.HAND - 1) = 0 And status(AxisNbr.LIFT - 1) = 0) Or IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

    '                ' Sudden stop
    '                If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
    '                    adt8940a1.reset_fifo(0)
    '                    For a As Short = 1 To 4
    '                        CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
    '                    Next
    '                    Exit Try
    '                End If

    '            Next ib

    '            ' Goto home
    '            status(AxisNbr.LIFT - 1) = 1
    '            status(AxisNbr.HAND - 1) = 1
    '            status(AxisNbr.ARM - 1) = 1
    '            CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.LIFT, 0, 0, 200000, 2)
    '            CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.ARM, 0, 0, 200000, 2)
    '            CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.HAND, 0, 0, 200000, 0.5)
    '            Do
    '                Application.DoEvents()
    '                CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.HAND, status(AxisNbr.HAND - 1), 0)
    '                CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.ARM, status(AxisNbr.ARM - 1), 0)
    '                CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.LIFT, status(AxisNbr.LIFT - 1), 0)
    '            Loop Until (status(AxisNbr.HAND - 1) = 0 And status(AxisNbr.ARM - 1) = 0 And status(AxisNbr.LIFT - 1) = 0) Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

    '            ' Sudden stop
    '            adt8940a1.reset_fifo(0)
    '            For a As Short = 1 To 4
    '                CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
    '            Next

    '            ' ---------------------------------------------------------------------

    '        Loop Until IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

    '        IsSUDDEN_STOP = True

    '        'MsgBox("Cycle ended!", MsgBoxStyle.Information, "Fast Cycle")
    '        IsEnable = True

    '        frmMain.PostText("END Cycle")
    '        frmHome.LunchUI()
    '    Catch ex As Exception
    '        frmHome.LunchUI()
    '        ' Sudden stop
    '        adt8940a1.reset_fifo(0)
    '        For i = 1 To 4
    '            CtrlCard.adt8940a1_StopRun(i, 0) 'Stop each axis
    '        Next i
    '    End Try

    'End Sub


    'Public Shared Sub PICK_FAST_NewReload()
    '    Debug.WriteLine("PICK FAST 2....")
    '    Try
    '        postID = 0
    '        IsSUDDEN_STOP = False

    '        Dim lArmSpeed As Long = ARM_SPEED
    '        Dim lArmACC As Long = ARM_ACC

    '        If MsgBox("Top = " & LIFT_10 & ", Dz = " & DY & " Arm speed/ACC = " & ARM_SPEED & ", " & ARM_ACC & Environment.NewLine & "ต้องการทำต่อใช่หรือไม่", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
    '            IsEnable = True
    '            Exit Sub
    '        End If

    '        CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.LIFT, 0, 0)
    '        CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.LIFT, 1, 0)
    '        CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.LIFT, 0, 0, 0)

    '        CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.ARM, 0, 0)
    '        CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.ARM, 1, 0)
    '        CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.ARM, 1, 1, 0)

    '        CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.HAND, 0, 0)
    '        CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.HAND, 1, 0)
    '        CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.HAND, 1, 1, 0)

    '        CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.TURN, 0, 0)
    '        CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.TURN, 1, 0)
    '        CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.TURN, 1, 1, 0)

    '        Do
    '            '---------------------------------------------------------------------------
    '            If CtrlCard.adt8940a1_Read_Input(27) = 1 Then 'Bottom sensor = 27, 1 = OFF
    '                frmMain.PostText("Reload")
    '                If CtrlCard.adt8940a1_Read_Input(5) = 1 Then 'CVY Sensor = 5
    '                    Do
    '                        Application.DoEvents()
    '                    Loop Until CtrlCard.adt8940a1_Read_Input(5) = 0 Or IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
    '                End If ' INPUT 5
    '                Threading.Thread.Sleep(1000)
    '                If CtrlCard.adt8940a1_Read_Input(4) = 1 Then
    '                    LGRN = True
    '                Else
    '                    LGRN = False
    '                End If
    '                Do
    '                    tReload = New System.Threading.Thread(AddressOf procReload)
    '                    tReload.Start()
    '                    Application.DoEvents()
    '                Loop Until CtrlCard.adt8940a1_Read_Input(27) = 0 Or IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
    '                Threading.Thread.Sleep(600)
    '            End If
    '            If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then Exit Do
    '            '---------------------------------------------------------------------------
    '            Application.DoEvents()

    '            frmMain.PostText("Cycle")

    '            Threading.Thread.Sleep(200)
    '            'Continue Do

    '            Dim status(0 To 3) As Short
    '            adt8940a1.reset_fifo(0)

    '            'Home2()
    '            'CtrlCard.adt8940a1_Setup_Pos(2, 0, 0)     'Logical position counter clear
    '            'CtrlCard.adt8940a1_Setup_Pos(2, 0, 1)     'Clear the actual position of the counter

    '            ' Hand off
    '            CtrlCard.adt8940a1_Write_Output(OUTPUT_HAND, 0)
    '            Times.Delay_ms(200)
    '            ' ZERO
    '            CtrlCard.adt8940a1_Sym_AbsoluteMove(1, 0, 0, 200000, 2)
    '            Do
    '                Application.DoEvents()
    '                CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.LIFT, status(AxisNbr.LIFT - 1), 0)
    '            Loop Until status(AxisNbr.LIFT - 1) = 0 Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
    '            ' Sudden stop
    '            If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
    '                adt8940a1.reset_fifo(0)
    '                For a As Short = 1 To 4
    '                    CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
    '                Next
    '                Exit Try
    '            End If

    '            Dim BNbr As Integer = FirstAdjust10()

    '            ' --------------------------------------------------------------------------------
    '            ' LIFT LOOP
    '            ' --------------------------------------------------------------------------------
    '            For ib As Integer = 1 To BNbr

    '                ' ตรวจความว่างที่จานหมุน ------------------------------------------------------------
    '                Do
    '                    'tKick = New System.Threading.Thread(AddressOf procKick)
    '                    'tKick.Start()
    '                    Application.DoEvents()
    '                    'in22 = aexp+ = Ready to Pick = acitve Low
    '                    'in28 = Ready to kick = Active low
    '                Loop Until (CtrlCard.adt8940a1_Read_Input(22) = 0 And CtrlCard.adt8940a1_Read_Input(28) = 0 And CtrlCard.adt8940a1_Read_Input(23) = 1) Or IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

    '                ' จับ---------------------------------------------------------------------
    '                'Times.Delay_ms(500)
    '                CtrlCard.adt8940a1_Write_Output(OUTPUT_HAND, 1)
    '                Times.Delay_ms(250)

    '                ' ตรวจการจับ---------------------------------------------------------------------
    '                'If CtrlCard.adt8940a1_Read_Input(InputNbr.HAND_R) = 1 Then
    '                '    'do nothing
    '                'Else
    '                '    ' ปล่อย
    '                '    CtrlCard.adt8940a1_Write_Output(OUTPUT_HAND, 0)
    '                '    Times.Delay_ms(700)

    '                '    ' ขึ้น
    '                '    status(AxisNbr.LIFT - 1) = 0
    '                '    CtrlCard.adt8940a1_Setup_Speed(AxisNbr.LIFT, 0, 400000, 5000)
    '                '    CtrlCard.Axis_Pmove(AxisNbr.LIFT, -5 * 796)
    '                '    Do
    '                '        Application.DoEvents()
    '                '        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.LIFT, status(AxisNbr.LIFT - 1), 0)
    '                '    Loop Until status(AxisNbr.LIFT - 1) = 0 Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

    '                '    ' Sudden stop
    '                '    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
    '                '        adt8940a1.reset_fifo(0)
    '                '        For a As Short = 1 To 4
    '                '            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
    '                '        Next
    '                '        Exit Try
    '                '    End If

    '                '    ' จับ
    '                '    CtrlCard.adt8940a1_Write_Output(OUTPUT_HAND, 1)
    '                '    Times.Delay_ms(700)

    '                'End If

    '                ' ลง ---------------------------------------------------------------------
    '                If 1 <= ib And ib <= BNbr - 1 Then
    '                    status(AxisNbr.LIFT - 1) = 0
    '                    If ib = 1 Then
    '                        CtrlCard.adt8940a1_Setup_Speed(AxisNbr.LIFT, 0, 650000, LiftAcc1)
    '                        CtrlCard.Axis_Pmove(AxisNbr.LIFT, LIFT_DOWN)
    '                    Else
    '                        CtrlCard.adt8940a1_Setup_Speed(AxisNbr.LIFT, 0, 650000, LiftAcc2)
    '                        CtrlCard.Axis_Pmove(AxisNbr.LIFT, LIFT_DOWN)
    '                    End If
    '                    Do
    '                        Application.DoEvents()
    '                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.LIFT, status(AxisNbr.LIFT - 1), 0)
    '                    Loop Until status(AxisNbr.LIFT - 1) = 0 Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

    '                    ' Sudden stop
    '                    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
    '                        adt8940a1.reset_fifo(0)
    '                        For a As Short = 1 To 4
    '                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
    '                        Next
    '                        Exit Try
    '                    End If
    '                End If

    '                ' สำหรับใบสุดท้าย ลดตัวยกได้ ---------------------------------------------------
    '                If ib = BNbr Then
    '                    tEndLift = New System.Threading.Thread(AddressOf procEndLift)
    '                    tEndLift.Start()
    '                End If

    '                '******************************************************************************************************
    '                Dim armStroke As Double = 185
    '                Dim handStroke As Double = 186.5
    '                ' บิดมือเข้า พร้อม ยกขึ้น ---------------------------------------------------------------------
    '                status(AxisNbr.ARM - 1) = 0 'แขน
    '                status(AxisNbr.HAND - 1) = 0 'มือ
    '                'CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.ARM, 0, 0)
    '                'CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.ARM, 1, 0)
    '                'CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.ARM, 1, 1, 0)
    '                CtrlCard.adt8940a1_Setup_Speed(AxisNbr.HAND, 0, lArmSpeed, lArmACC)
    '                CtrlCard.adt8940a1_Setup_Speed(AxisNbr.ARM, 0, lArmSpeed, lArmACC)
    '                CtrlCard.Axis_Pmove(AxisNbr.HAND, handStroke * 1421)
    '                CtrlCard.Axis_Pmove(AxisNbr.ARM, -armStroke * 2741)
    '                Threading.Thread.Sleep(200)
    '                ' ยกขึ้นตามระยะ ---------------------------------------------------------------------
    '                If 1 <= ib And ib <= BNbr - 1 Then
    '                    tLift = New Threading.Thread(AddressOf Lift)
    '                    tLift.Start()
    '                End If
    '                Do
    '                    Application.DoEvents()
    '                    CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.ARM, status(AxisNbr.ARM - 1), 0)
    '                Loop Until status(AxisNbr.ARM - 1) = 0 Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

    '                ' Sudden stop
    '                If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
    '                    adt8940a1.reset_fifo(0)
    '                    For a As Short = 1 To 4
    '                        CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
    '                    Next
    '                    Exit Try
    '                End If

    '                '' ยกขึ้นตามระยะ ---------------------------------------------------------------------
    '                'If 1 <= ib And ib <= BNbr - 1 Then
    '                '    tLift = New Threading.Thread(AddressOf Lift)
    '                '    tLift.Start()
    '                'End If

    '                ' ปล่อยมือ ---------------------------------------------------------------------
    '                CtrlCard.adt8940a1_Write_Output(OUTPUT_HAND, 0)
    '                System.Threading.Thread.Sleep(200)

    '                '******************************************************************************************************
    '                ' แขนกลับ 0 พร้อมกับ ---------------------------------------------------------------------
    '                ' หมุนมือกลับ ---------------------------------------------------------------------
    '                status(AxisNbr.ARM - 1) = 1
    '                status(AxisNbr.HAND - 1) = 1
    '                CtrlCard.adt8940a1_Setup_Speed(AxisNbr.ARM, 0, lArmSpeed, lArmACC)
    '                CtrlCard.adt8940a1_Setup_Speed(AxisNbr.HAND, 0, lArmSpeed, lArmACC)
    '                CtrlCard.Axis_Pmove(AxisNbr.ARM, armStroke * 2741)
    '                CtrlCard.Axis_Pmove(AxisNbr.HAND, -handStroke * 1421)

    '                ' หมุนตะกร้า-----------------------------------
    '                Dim ibt As Integer = ib
    '                If LGRN Then
    '                    ibt += 1
    '                End If
    '                If Not ibt Mod 2 = 0 Then
    '                    'Check ตำแหน่ง และเลือกทิศ
    '                    adt8940a1.get_command_pos(0, 4, lTurnLPos)
    '                    If lTurnLPos > 2147483648 Then
    '                        lTurnLPos = lTurnLPos - 4294967296
    '                    End If
    '                    ' รอองศาแขนพ้นตะกร้า
    '                    'Threading.Thread.Sleep(500)
    '                    'lArmLPos = 0
    '                    'adt8940a1.get_command_pos(0, 2, lArmLPos)
    '                    'If lArmLPos > 2147483648 Then
    '                    '    lArmLPos = lArmLPos - 4294967296
    '                    'End If
    '                    'frmMain.Text = lArmLPos
    '                    'สั่งหมุน
    '                    If lTurnLPos > 0 Then
    '                        tTurnNegative = New System.Threading.Thread(AddressOf procTurnNegative)
    '                        tTurnNegative.Start()
    '                        'procTurnNegative()
    '                    ElseIf lTurnLPos = 0 Then
    '                        tTurnPostive = New System.Threading.Thread(AddressOf procTurnPositive)
    '                        tTurnPostive.Start()
    '                        'procTurnPositive()
    '                    Else ' Less than zero
    '                        tTurnPostive = New System.Threading.Thread(AddressOf procTurnPositive)
    '                        tTurnPostive.Start()
    '                        'procTurnPositive()
    '                    End If
    '                Else
    '                    'แจ้งตัว CVY ให้รับตะกร้าทำงานต่อไป เช่น แตะ
    '                    tKick = New System.Threading.Thread(AddressOf procKick)
    '                    tKick.Start()
    '                End If

    '                postID += 1
    '                'If postID = 1 Then
    '                '    isNG = True
    '                'Else
    '                '    isNG = False
    '                'End If
    '                'If postID = 1 Then
    '                '    sb1.Remove(0, sb1.Length)
    '                '    sb1.Append("1, ")
    '                '    firstTimeText = Now.ToString("HH_mm_ss")
    '                '    sb1.Append(firstTimeText)
    '                '    System.IO.File.AppendAllText("D:\log\picker\pick_" & firstTimeText & ".txt", sb1.ToString())
    '                'Else
    '                '    sb2.Remove(0, sb2.Length)
    '                '    sb2.Append(Environment.NewLine)
    '                '    sb2.Append(postID.ToString)
    '                '    sb2.Append(", ")
    '                '    sb2.Append(Now.ToString("HH:mm:ss"))
    '                '    System.IO.File.AppendAllText("D:\log\picker\pick_" & firstTimeText & ".txt", sb2.ToString())
    '                'End If

    '                ' รอการกลับ 0 มือและแขน---------------------------------------------------------------------
    '                'Times.Delay_ms(1000)
    '                status(AxisNbr.ARM - 1) = 1
    '                status(AxisNbr.HAND - 1) = 1
    '                Do
    '                    Application.DoEvents()
    '                    CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.ARM, status(AxisNbr.ARM - 1), 0)
    '                    CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.HAND, status(AxisNbr.HAND - 1), 0)
    '                Loop Until (status(AxisNbr.ARM - 1) = 0 And status(AxisNbr.HAND - 1) = 0) Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

    '                ' Sudden stop
    '                If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
    '                    adt8940a1.reset_fifo(0)
    '                    For a As Short = 1 To 4
    '                        CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
    '                    Next
    '                    Exit Try
    '                End If

    '                ' Goto home
    '                CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.HAND, 0, 0, 200000, 0.5)
    '                CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.ARM, 0, 0, 50000, 2)

    '                ' รอให้นิ่ง---------------------------------------------------------------------
    '                status(AxisNbr.LIFT - 1) = 1
    '                status(AxisNbr.HAND - 1) = 1
    '                status(AxisNbr.ARM - 1) = 1
    '                Do
    '                    Application.DoEvents()
    '                    CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.ARM, status(AxisNbr.ARM - 1), 0)
    '                    CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.HAND, status(AxisNbr.HAND - 1), 0)
    '                    CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.LIFT, status(AxisNbr.LIFT - 1), 0)
    '                Loop Until (status(AxisNbr.ARM - 1) = 0 And status(AxisNbr.HAND - 1) = 0 And status(AxisNbr.LIFT - 1) = 0) Or IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

    '                ' Sudden stop
    '                If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
    '                    adt8940a1.reset_fifo(0)
    '                    For a As Short = 1 To 4
    '                        CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
    '                    Next
    '                    Exit Try
    '                End If

    '            Next ib

    '            ' Goto home
    '            status(AxisNbr.LIFT - 1) = 1
    '            status(AxisNbr.HAND - 1) = 1
    '            status(AxisNbr.ARM - 1) = 1
    '            CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.LIFT, 0, 0, 200000, 2)
    '            CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.ARM, 0, 0, 200000, 2)
    '            CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.HAND, 0, 0, 200000, 0.5)
    '            Do
    '                Application.DoEvents()
    '                CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.HAND, status(AxisNbr.HAND - 1), 0)
    '                CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.ARM, status(AxisNbr.ARM - 1), 0)
    '                CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.LIFT, status(AxisNbr.LIFT - 1), 0)
    '            Loop Until (status(AxisNbr.HAND - 1) = 0 And status(AxisNbr.ARM - 1) = 0 And status(AxisNbr.LIFT - 1) = 0) Or _
    '                RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

    '            ' Sudden stop
    '            adt8940a1.reset_fifo(0)
    '            For a As Short = 1 To 4
    '                CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
    '            Next

    '            Exit Do ' TEST**********************************
    '            ' ---------------------------------------------------------------------

    '        Loop Until IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

    '        'IsSUDDEN_STOP = True

    '        IsEnable = True

    '    Catch ex As Exception
    '        frmHome.LunchUI()
    '        ' Sudden stop
    '        adt8940a1.reset_fifo(0)
    '        For i = 1 To 4
    '            CtrlCard.adt8940a1_StopRun(i, 0) 'Stop each axis
    '        Next i
    '    End Try

    '    Debug.WriteLine("PICK FAST 2....DONE")

    'End Sub

    Public Shared Sub PICK_FAST_NewLiftMove()
        Dim nLogPos As Long                   'Logical location
        Dim nActPos As Long                   'Actual location
        Dim nSpeed As Long                    'Running Speeed
        Debug.WriteLine("PICK FAST 3....")
        Try
            postID = 0
            IsSUDDEN_STOP = False

            Dim lArmSpeed As Long = ARM_SPEED
            Dim lArmACC As Long = ARM_ACC

            ' -------------------------------------------------------------------------------
            ' สรุปค่าที่อ่านได้จากหน้าจอ
            ' -------------------------------------------------------------------------------
            'If MsgBox("Top = " & LIFT_10 & ", Dz = " & DY & ", UP = " & LIFT_UP & ", Angle = " & ARM_Range & " Arm speed/ACC = " & ARM_SPEED & ", " & ARM_ACC & Environment.NewLine & "ต้องการทำต่อใช่หรือไม่", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
            '    IsEnable = True
            '    Exit Sub
            'End If

            ' -------------------------------------------------------------------------------
            ' ตั้งการทำงานของ เซนเซอร์ คุมมอเตอร์
            ' -------------------------------------------------------------------------------
            CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.LIFT, 0, 0)
            CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.LIFT, 1, 0)
            CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.LIFT, 0, 0, 0)

            CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.ARM, 0, 0)
            CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.ARM, 1, 0)
            CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.ARM, 0, 0, 0)

            CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.HAND, 0, 0)
            CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.HAND, 1, 0)
            CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.HAND, 1, 1, 0)

            CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.TURN, 0, 0)
            CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.TURN, 1, 0)
            CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.TURN, 1, 1, 0)


            ' -------------------------------------------------------------------------------
            ' ส่วนวนซ้ำ ตั้งใหม่
            ' -------------------------------------------------------------------------------
            Do
                ' -------------------------------------------------------------------------------
                ' Reload
                ' -------------------------------------------------------------------------------
                Debug.WriteLine("Reloading...")
                If CtrlCard.adt8940a1_Read_Input(27) = 1 Then 'Bottom sensor = 27, 1 = OFF

                    Dim inPosition As Boolean = False
                    Dim reloaded As Boolean = False
                    Do
                        Application.DoEvents()

                        ' Reloaded
                        If CtrlCard.adt8940a1_Read_Input(27) = 0 Then 'in27 = reloaded sensor
                            reloaded = True
                            inPosition = False
                            Exit Do
                        End If

                        ' จอดแล้ว
                        If CtrlCard.adt8940a1_Read_Input(30) = 1 And CtrlCard.adt8940a1_Read_Input(5) = 0 Then
                            'in30 = cvy stop, ready to reload - 
                            'A-1IN30-P1CVY-PLC2-Y0-ADTA-U24
                            'B-2IN30-P5CVY-PLC2-Y2-ADTB-U24
                            '
                            ' in5 = cvy sensor, in-position, in5(xexp-) - 
                            'A-1IN5-J2-PHOTO-P1CVY-PLC2-X2-ADTA-T25
                            'B-2IN5-J5-Photo-P3CVY-PLC2-X5-ADTB-T25
                            inPosition = True
                            Exit Do
                        End If

                    Loop Until IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

                    ' Sudden stop
                    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If

                    ' Reloaeding...
                    If inPosition Then
                        ' รอนิ่ง
                        Threading.Thread.Sleep(1000)

                        ' -------------------------------------------------------------------------------
                        ' Check color sensor
                        ' -------------------------------------------------------------------------------
                        Debug.WriteLine("Color Sensor")
                        If CtrlCard.adt8940a1_Read_Input(4) = 0 Then 'in4(xexp+)
                            LGRN = True
                            Debug.WriteLine("LGRN = TRUE")
                        Else
                            LGRN = False
                            Debug.WriteLine("LGRN = FALSE")
                        End If
                        ' Sudden stop
                        If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                            adt8940a1.reset_fifo(0)
                            For a As Short = 1 To 4
                                CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                            Next
                            Exit Try
                        End If

                        ' -------------------------------------------------------------------------------
                        ' Reload loop
                        ' -------------------------------------------------------------------------------
                        Do
                            'tReload = New System.Threading.Thread(AddressOf procReload)
                            'tReload.Start()
                            procReload()
                            Application.DoEvents()
                            Threading.Thread.Sleep(2000)
                        Loop Until CtrlCard.adt8940a1_Read_Input(27) = 0 Or IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
                        Threading.Thread.Sleep(600)

                    End If

                End If
                Debug.WriteLine("Reloaded")
                If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then Exit Do


                Application.DoEvents()

                ' -------------------------------------------------------------------------------
                ' Fast pick cycle
                ' -------------------------------------------------------------------------------
                Debug.WriteLine("Fast pick")

                Threading.Thread.Sleep(200)
                'Continue Do

                Dim status(0 To 3) As Short
                adt8940a1.reset_fifo(0)

                'Home2()
                'CtrlCard.adt8940a1_Setup_Pos(2, 0, 0)     'Logical position counter clear
                'CtrlCard.adt8940a1_Setup_Pos(2, 0, 1)     'Clear the actual position of the counter

                ' -------------------------------------------------------------------------------
                ' Hand off
                ' -------------------------------------------------------------------------------
                CtrlCard.adt8940a1_Write_Output(OUTPUT_HAND, 0)
                Times.Delay_ms(200)


                ' -------------------------------------------------------------------------------
                ' Lift up with first position by soft movement *******************************************
                ' -------------------------------------------------------------------------------
                Dim BNbr As Integer = FirstAdjust10()
                Debug.WriteLine("First up")

                ' --------------------------------------------------------------------------------
                ' LIFT LOOP
                ' --------------------------------------------------------------------------------
                For ib As Integer = 1 To BNbr

                    ' --------------------------------------------------------------------------------
                    ' ตรวจความว่างที่จานหมุน และ Ready to pick + Ready to Kick จาก PLC 
                    ' --------------------------------------------------------------------------------
                    Do
                        Application.DoEvents()
                        'in22 = aexp+ = Ready to Pick = acitve Low 
                        'A-1RDP-1IN22/1AEXP+(T44)-PLC2-Y45
                        'B-1RDP-2IN22/2AEXP+(T44)-PLC2-Y46
                        '
                        'in28 = Ready to kick = Active low
                        'A-1RDK-1IN28(U22)-PLC2-Y15
                        'B-2RDK-2IN28(U22)-PLC2-Y16
                        '
                        'in23 = Disk empty sensor
                        'A-K1/1ROT-1IN23/1AEXP-(T45)-PHOTO-ON-DISK-PLC2-X15
                        'B-K2/2ROT-2IN23/2AEXP-(T45)-PHOTO-ON-DISK-PLC2-X13
                        '
                    Loop Until (CtrlCard.adt8940a1_Read_Input(22) = 0 And CtrlCard.adt8940a1_Read_Input(28) = 0 And CtrlCard.adt8940a1_Read_Input(23) = 1) Or IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

                    ' --------------------------------------------------------------------------------
                    ' จับ
                    ' --------------------------------------------------------------------------------
                    'Times.Delay_ms(500)
                    CtrlCard.adt8940a1_Write_Output(OUTPUT_HAND, 1)
                    Times.Delay_ms(250)


                    ' --------------------------------------------------------------------------------
                    ' ลง 
                    ' --------------------------------------------------------------------------------
                    If 1 <= ib And ib <= BNbr - 1 Then

                        CtrlCard.adt8940a1_Get_CurrentInfo(1, nLogPos, nActPos, nSpeed)
                        Debug.WriteLine(nLogPos.ToString() & ", " & nActPos.ToString & ", " & (nLogPos - nActPos).ToString)

                        status(AxisNbr.LIFT - 1) = 1
                        If ib = 1 Then
                            CtrlCard.adt8940a1_Setup_Speed(AxisNbr.LIFT, 0, LiftSpeed, LiftAcc1)
                            CtrlCard.Axis_Pmove(AxisNbr.LIFT, LIFT_DOWN)
                        Else
                            CtrlCard.adt8940a1_Setup_Speed(AxisNbr.LIFT, 0, LiftDownSpeed, LiftAcc2)
                            CtrlCard.Axis_Pmove(AxisNbr.LIFT, LIFT_DOWN)
                        End If
                        Do
                            Application.DoEvents()
                            CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.LIFT, status(AxisNbr.LIFT - 1), 0)
                        Loop Until status(AxisNbr.LIFT - 1) = 0 Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
                        Threading.Thread.Sleep(200)

                        ' Sudden stop
                        If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                            adt8940a1.reset_fifo(0)
                            For a As Short = 1 To 4
                                CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                            Next
                            Exit Try
                        End If
                    End If

                    CtrlCard.adt8940a1_Get_CurrentInfo(1, nLogPos, nActPos, nSpeed)
                    Debug.WriteLine(nLogPos.ToString() & ", " & nActPos.ToString & ", " & (nLogPos - nActPos).ToString)

                    ' สำหรับใบสุดท้าย ลดตัวยกได้ ---------------------------------------------------*****************
                    If ib = BNbr Then
                        tEndLift = New System.Threading.Thread(AddressOf procEndLift)
                        tEndLift.Start()
                    End If

                    '******************************************************************************************************
                    Dim armStroke As Double = ARM_Range
                    Dim handStroke As Double = ARM_Range
                    ' บิดมือเข้า พร้อม ยกขึ้น ---------------------------------------------------------------------
                    status(AxisNbr.ARM - 1) = 1 'แขน
                    status(AxisNbr.HAND - 1) = 1 'มือ
                    'CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.ARM, 0, 0)
                    'CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.ARM, 1, 0)
                    'CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.ARM, 1, 1, 0)
                    CtrlCard.adt8940a1_Setup_Speed(AxisNbr.HAND, 0, lArmSpeed, lArmACC)
                    CtrlCard.adt8940a1_Setup_Speed(AxisNbr.ARM, 0, lArmSpeed, lArmACC)
                    CtrlCard.Axis_Pmove(AxisNbr.HAND, handStroke * 1421)
                    CtrlCard.Axis_Pmove(AxisNbr.ARM, -armStroke * 2741)
                    Threading.Thread.Sleep(200)


                    ' ยกขึ้นตามระยะ ---------------------------------------------------------------------
                    If 1 <= ib And ib <= BNbr - 1 Then
                        tLift = New Threading.Thread(AddressOf Lift)
                        tLift.Start()
                    End If


                    Do
                        Application.DoEvents()
                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.ARM, status(AxisNbr.ARM - 1), 0)
                    Loop Until status(AxisNbr.ARM - 1) = 0 Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
                    'Threading.Thread.Sleep(20)'==========

                    ' Sudden stop
                    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If


                    ' ปล่อยมือ ---------------------------------------------------------------------
                    If ib = BNbr Then
                        Do
                            Application.DoEvents()
                            If RoboticControl.EndLift_InPosition Then
                                Exit Do
                            End If
                        Loop Until RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
                    End If

                    ' Sudden stop
                    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If

                    CtrlCard.adt8940a1_Write_Output(OUTPUT_HAND, 0)
                    System.Threading.Thread.Sleep(Time_HandOFFDelay)

                    If CtrlCard.adt8940a1_Read_Input(23) = 1 And Not IsEmptyPicking Then 'ไม่มีตะกร้า ****** empty picking
                        'in23 = disk sensor
                        IsSUDDEN_STOP = True
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If


                    '******************************************************************************************************
                    ' แขนกลับ 0 พร้อมกับ ---------------------------------------------------------------------
                    ' หมุนมือกลับ ---------------------------------------------------------------------
                    status(AxisNbr.ARM - 1) = 1
                    status(AxisNbr.HAND - 1) = 1
                    CtrlCard.adt8940a1_Setup_Speed(AxisNbr.ARM, 0, lArmSpeed, lArmACC)
                    CtrlCard.adt8940a1_Setup_Speed(AxisNbr.HAND, 0, lArmSpeed, lArmACC)
                    CtrlCard.Axis_Pmove(AxisNbr.ARM, armStroke * 2741)
                    CtrlCard.Axis_Pmove(AxisNbr.HAND, -(handStroke) * 1421)

                    ' หมุนตะกร้า-----------------------------------
                    Dim ibt As Integer = ib
                    If LGRN Then
                        ibt += 1
                    End If
                    If Not ibt Mod 2 = 0 Then
                        'Check ตำแหน่ง และเลือกทิศ
                        adt8940a1.get_command_pos(0, 4, lTurnLPos)
                        If lTurnLPos > 2147483648 Then
                            lTurnLPos = lTurnLPos - 4294967296
                        End If
                        ' รอองศาแขนพ้นตะกร้า
                        'Threading.Thread.Sleep(500)
                        'lArmLPos = 0
                        'adt8940a1.get_command_pos(0, 2, lArmLPos)
                        'If lArmLPos > 2147483648 Then
                        '    lArmLPos = lArmLPos - 4294967296
                        'End If
                        'frmMain.Text = lArmLPos
                        'สั่งหมุน
                        If lTurnLPos > 0 Then
                            tTurnNegative = New System.Threading.Thread(AddressOf procTurnNegative)
                            tTurnNegative.Start()
                            'procTurnNegative()
                        ElseIf lTurnLPos = 0 Then
                            tTurnPostive = New System.Threading.Thread(AddressOf procTurnPositive)
                            tTurnPostive.Start()
                            'procTurnPositive()
                        Else ' Less than zero
                            tTurnPostive = New System.Threading.Thread(AddressOf procTurnPositive)
                            tTurnPostive.Start()
                            'procTurnPositive()
                        End If
                    Else
                        'แจ้งตัว CVY ให้รับตะกร้าทำงานต่อไป เช่น แตะ
                        tKick = New System.Threading.Thread(AddressOf procKick)
                        tKick.Start()
                    End If

                    postID += 1
                    'If postID = 1 Then
                    '    isNG = True
                    'Else
                    '    isNG = False
                    'End If
                    If postID = 1 Then
                        sb1.Remove(0, sb1.Length)
                        sb1.Append("1, ")
                        firstTimeText = Now.ToString("HH_mm_ss")
                        sb1.Append(firstTimeText)
                        System.IO.File.AppendAllText("C:\log\picker\pick_" & firstTimeText & ".txt", sb1.ToString())
                    Else
                        sb2.Remove(0, sb2.Length)
                        sb2.Append(Environment.NewLine)
                        sb2.Append(postID.ToString)
                        sb2.Append(", ")
                        sb2.Append(Now.ToString("HH:mm:ss"))
                        System.IO.File.AppendAllText("C:\log\picker\pick_" & firstTimeText & ".txt", sb2.ToString())
                    End If

                    ' รอการกลับ 0 มือและแขน---------------------------------------------------------------------
                    'Times.Delay_ms(1000)
                    status(AxisNbr.ARM - 1) = 1
                    status(AxisNbr.HAND - 1) = 1
                    Do
                        Application.DoEvents()
                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.ARM, status(AxisNbr.ARM - 1), 0)
                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.HAND, status(AxisNbr.HAND - 1), 0)
                    Loop Until (status(AxisNbr.ARM - 1) = 0 And status(AxisNbr.HAND - 1) = 0) Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
                    'Threading.Thread.Sleep(50) '====================

                    ' Sudden stop
                    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If

                    ' Goto home
                    CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.HAND, 0, 0, 200000, 0.5)
                    CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.ARM, 0, 0, 50000, 2)

                    ' รอให้นิ่ง---------------------------------------------------------------------
                    status(AxisNbr.LIFT - 1) = 1
                    status(AxisNbr.HAND - 1) = 1
                    status(AxisNbr.ARM - 1) = 1
                    Do
                        Application.DoEvents()
                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.ARM, status(AxisNbr.ARM - 1), 0)
                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.HAND, status(AxisNbr.HAND - 1), 0)
                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.LIFT, status(AxisNbr.LIFT - 1), 0)
                    Loop Until (status(AxisNbr.ARM - 1) = 0 And status(AxisNbr.HAND - 1) = 0 And status(AxisNbr.LIFT - 1) = 0) Or IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

                    ' Sudden stop
                    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If

                Next ib

                ' Goto home
                status(AxisNbr.LIFT - 1) = 1
                status(AxisNbr.HAND - 1) = 1
                status(AxisNbr.ARM - 1) = 1
                CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.LIFT, 0, 0, 200000, 2)
                CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.ARM, 0, 0, 200000, 2)
                CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.HAND, 0, 0, 200000, 0.5)
                Do
                    Application.DoEvents()
                    CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.HAND, status(AxisNbr.HAND - 1), 0)
                    CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.ARM, status(AxisNbr.ARM - 1), 0)
                    CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.LIFT, status(AxisNbr.LIFT - 1), 0)
                Loop Until (status(AxisNbr.HAND - 1) = 0 And status(AxisNbr.ARM - 1) = 0 And status(AxisNbr.LIFT - 1) = 0) Or _
                    RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

                ' Sudden stop
                'adt8940a1.reset_fifo(0)
                'For a As Short = 1 To 4
                '    CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                'Next

                'Exit Do ' TEST********************************** ทำงานรอบเดียว
                ' ---------------------------------------------------------------------

            Loop Until IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

            'IsSUDDEN_STOP = True

            IsEnable = True

        Catch ex As Exception
            frmHome.LunchUI()
            ' Sudden stop
            adt8940a1.reset_fifo(0)
            For i = 1 To 4
                CtrlCard.adt8940a1_StopRun(i, 0) 'Stop each axis
            Next i
        End Try

        Debug.WriteLine("PICK FAST 2....DONE")

    End Sub

    Public Shared Sub PICK_FAST_Camera()
        Dim nLogPos As Long                   'Logical location
        Dim nActPos As Long                   'Actual location
        Dim nSpeed As Long                    'Running Speeed
        Debug.WriteLine("PICK camera 4....")
        Try
            postID = 0
            IsSUDDEN_STOP = False

            Dim lArmSpeed As Long = ARM_SPEED
            Dim lArmACC As Long = ARM_ACC

            ' -------------------------------------------------------------------------------
            ' สรุปค่าที่อ่านได้จากหน้าจอ
            ' -------------------------------------------------------------------------------
            'If MsgBox("Top = " & LIFT_10 & ", Dz = " & DY & ", UP = " & LIFT_UP & ", Angle = " & ARM_Range & " Arm speed/ACC = " & ARM_SPEED & ", " & ARM_ACC & Environment.NewLine & "ต้องการทำต่อใช่หรือไม่", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
            '    IsEnable = True
            '    Exit Sub
            'End If

            ' -------------------------------------------------------------------------------
            ' ตั้งการทำงานของ เซนเซอร์ คุมมอเตอร์
            ' -------------------------------------------------------------------------------
            CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.LIFT, 0, 0)
            CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.LIFT, 1, 0)
            CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.LIFT, 0, 0, 0)

            CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.ARM, 0, 0)
            CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.ARM, 1, 0)
            CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.ARM, 1, 1, 0)

            CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.HAND, 0, 0)
            CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.HAND, 1, 0)
            CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.HAND, 1, 1, 0)

            CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.TURN, 0, 0)
            CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.TURN, 1, 0)
            CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.TURN, 1, 1, 0)


            ' -------------------------------------------------------------------------------
            ' ส่วนวนซ้ำ ตั้งใหม่
            ' -------------------------------------------------------------------------------
            Do
                ' -------------------------------------------------------------------------------
                ' Reload
                ' -------------------------------------------------------------------------------
                Debug.WriteLine("Reloading...")
                If CtrlCard.adt8940a1_Read_Input(27) = 1 Then 'Bottom sensor = 27, 1 = OFF

                    Dim inPosition As Boolean = False
                    Dim reloaded As Boolean = False
                    Do
                        Application.DoEvents()

                        ' Reloaded
                        If CtrlCard.adt8940a1_Read_Input(27) = 0 Then 'in27 = reloaded sensor
                            reloaded = True
                            inPosition = False
                            Exit Do
                        End If

                        ' จอดแล้ว
                        If CtrlCard.adt8940a1_Read_Input(30) = 1 And CtrlCard.adt8940a1_Read_Input(5) = 0 Then
                            'in30 = cvy stop, ready to reload
                            ' in5 = cvy sensor, in-position, in5(xexp-) ก่อนเข้า
                            inPosition = True
                            Exit Do
                        End If

                    Loop Until IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

                    ' Sudden stop
                    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If

                    ' Reloaeding...
                    If inPosition Then
                        ' รอนิ่ง
                        Threading.Thread.Sleep(1000)

                        ' -------------------------------------------------------------------------------
                        ' Check color sensor
                        ' -------------------------------------------------------------------------------
                        Debug.WriteLine("Color Sensor")
                        If CtrlCard.adt8940a1_Read_Input(4) = 0 Then 'in4(xexp+)
                            LGRN = True
                            Debug.WriteLine("LGRN = TRUE")
                        Else
                            LGRN = False
                            Debug.WriteLine("LGRN = FALSE")
                        End If
                        ' Sudden stop
                        If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                            adt8940a1.reset_fifo(0)
                            For a As Short = 1 To 4
                                CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                            Next
                            Exit Try
                        End If

                        ' -------------------------------------------------------------------------------
                        ' Reload loop
                        ' -------------------------------------------------------------------------------
                        Do
                            'tReload = New System.Threading.Thread(AddressOf procReload)
                            'tReload.Start()
                            procReload()
                            Application.DoEvents()
                            Threading.Thread.Sleep(2000)
                        Loop Until CtrlCard.adt8940a1_Read_Input(27) = 0 Or IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
                        Threading.Thread.Sleep(600)

                    End If

                End If
                Debug.WriteLine("Reloaded")
                If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then Exit Do


                Application.DoEvents()

                ' -------------------------------------------------------------------------------
                ' Fast pick cycle
                ' -------------------------------------------------------------------------------
                Debug.WriteLine("Fast pick")

                Threading.Thread.Sleep(200)
                'Continue Do

                Dim status(0 To 3) As Short
                adt8940a1.reset_fifo(0)

                'Home2()
                'CtrlCard.adt8940a1_Setup_Pos(2, 0, 0)     'Logical position counter clear
                'CtrlCard.adt8940a1_Setup_Pos(2, 0, 1)     'Clear the actual position of the counter

                ' -------------------------------------------------------------------------------
                ' Hand off
                ' -------------------------------------------------------------------------------
                CtrlCard.adt8940a1_Write_Output(OUTPUT_HAND, 0)
                Times.Delay_ms(200)


                ' -------------------------------------------------------------------------------
                ' Lift up with first position by soft movement *******************************************
                ' -------------------------------------------------------------------------------
                Dim BNbr As Integer = FirstAdjust10()
                Debug.WriteLine("First up")

                ' --------------------------------------------------------------------------------
                ' LIFT LOOP
                ' --------------------------------------------------------------------------------
                For ib As Integer = 1 To BNbr

                    ' --------------------------------------------------------------------------------
                    ' ตรวจความว่างที่จานหมุน และ Ready to pick + Ready to Kick จาก PLC 
                    ' --------------------------------------------------------------------------------
                    Do
                        Application.DoEvents()
                        'in22 = aexp+ = Ready to Pick = acitve Low
                        'in28 = Ready to kick = Active low
                        'in23 = Disk empty sensor
                    Loop Until (CtrlCard.adt8940a1_Read_Input(22) = 0 And CtrlCard.adt8940a1_Read_Input(28) = 0 And CtrlCard.adt8940a1_Read_Input(23) = 1) Or IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

                    ' Sudden stop
                    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If

                    ' --------------------------------------------------------------------------------
                    ' จับ
                    ' --------------------------------------------------------------------------------
                    'Times.Delay_ms(500)
                    CtrlCard.adt8940a1_Write_Output(OUTPUT_HAND, 1)
                    Times.Delay_ms(250)

                    ' Sudden stop
                    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If

                    ' --------------------------------------------------------------------------------
                    ' ลง 
                    ' --------------------------------------------------------------------------------
                    If 1 <= ib And ib <= BNbr - 1 Then

                        CtrlCard.adt8940a1_Get_CurrentInfo(1, nLogPos, nActPos, nSpeed)
                        Debug.WriteLine(nLogPos.ToString() & ", " & nActPos.ToString & ", " & (nLogPos - nActPos).ToString)

                        status(AxisNbr.LIFT - 1) = 1
                        If ib = 1 Then
                            CtrlCard.adt8940a1_Setup_Speed(AxisNbr.LIFT, 0, LiftSpeed, LiftAcc1)
                            CtrlCard.Axis_Pmove(AxisNbr.LIFT, LIFT_DOWN)
                        Else
                            CtrlCard.adt8940a1_Setup_Speed(AxisNbr.LIFT, 0, LiftDownSpeed, LiftAcc2)
                            CtrlCard.Axis_Pmove(AxisNbr.LIFT, LIFT_DOWN)
                        End If
                        Do
                            Application.DoEvents()
                            CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.LIFT, status(AxisNbr.LIFT - 1), 0)
                        Loop Until status(AxisNbr.LIFT - 1) = 0 Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
                        Threading.Thread.Sleep(200)

                        ' Sudden stop
                        If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                            adt8940a1.reset_fifo(0)
                            For a As Short = 1 To 4
                                CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                            Next
                            Exit Try
                        End If
                    End If

                    CtrlCard.adt8940a1_Get_CurrentInfo(1, nLogPos, nActPos, nSpeed)
                    Debug.WriteLine(nLogPos.ToString() & ", " & nActPos.ToString & ", " & (nLogPos - nActPos).ToString)

                    ' สำหรับใบสุดท้าย ลดตัวยกได้ ---------------------------------------------------*****************
                    If ib = BNbr Then
                        tEndLift = New System.Threading.Thread(AddressOf procEndLift)
                        tEndLift.Start()
                    End If

                    ' เหวี่ยง 1 ******************************************************************************************************
                    Dim armStroke As Double = ARM_TO_CAM_RANGE
                    Dim handStroke As Double = HAND_TO_CAM_RANGE
                    ' บิดมือเข้า พร้อม ยกขึ้น ---------------------------------------------------------------------
                    status(AxisNbr.ARM - 1) = 1 'แขน
                    status(AxisNbr.HAND - 1) = 1 'มือ
                    'CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.ARM, 0, 0)
                    'CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.ARM, 1, 0)
                    'CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.ARM, 1, 1, 0)
                    CtrlCard.adt8940a1_Setup_Speed(AxisNbr.HAND, 0, lArmSpeed, lArmACC)
                    CtrlCard.adt8940a1_Setup_Speed(AxisNbr.ARM, 0, lArmSpeed, lArmACC)
                    CtrlCard.Axis_Pmove(AxisNbr.HAND, handStroke * 1421)
                    CtrlCard.Axis_Pmove(AxisNbr.ARM, -armStroke * 2741)
                    Threading.Thread.Sleep(200)


                    ' ยกขึ้นตามระยะ ---------------------------------------------------------------------
                    If 1 <= ib And ib <= BNbr - 1 Then
                        tLift = New Threading.Thread(AddressOf Lift)
                        tLift.Start()
                    End If

                    Do
                        Application.DoEvents()
                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.ARM, status(AxisNbr.ARM - 1), 0)
                    Loop Until status(AxisNbr.ARM - 1) = 0 Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
                    'Threading.Thread.Sleep(20)'==========

                    ' Sudden stop
                    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If

                    ' VISON -------------------------------------------------------------------------------------- VISION
                    ' ส่งสัญญาณ out14 
                    Threading.Thread.Sleep(1000) ' รอขยะหล่น
                    CtrlCard.adt8940a1_Write_Output(14, 1) 'สั่งถ่ายภาพ
                    'Threading.Thread.Sleep(200) ' รอถ่ายภาพ
                    'CtrlCard.adt8940a1_Write_Output(14, 0) 'หยุดสั่งถ่ายภาพ

                    ' รอผลวิเคราะห์
                    Threading.Thread.Sleep(100) 'ลวงไว้ก่อน
                    Dim isReceived As Boolean = False
                    Do
                        Application.DoEvents()
                        If CtrlCard.adt8940a1_Read_Input(16) = 0 Then
                            isReceived = True
                            Exit Do
                        End If
                    Loop Until RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
                    isReceived = True
                    CtrlCard.adt8940a1_Write_Output(14, 0) 'หยุดสั่งถ่ายภาพ

                    ' Sudden stop
                    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If

                    ' Received then command
                    If isReceived Then
                        If CtrlCard.adt8940a1_Read_Input(11) = 0 Then 'ON = 0 -> NG
                            isNG = True
                        Else
                            isNG = False '->OK
                        End If
                    End If

                    ' Caution *****
                    ' isNG = False ' ใช้งานตอนแตะ / kick

                    ' เหวี่ยงต่อไปขข -----------------------------------------------------------------------------------
                    armStroke = ARM_Range - ARM_TO_CAM_RANGE
                    handStroke = ARM_Range - HAND_TO_CAM_RANGE
                    status(AxisNbr.ARM - 1) = 1 'แขน
                    status(AxisNbr.HAND - 1) = 1 'มือ
                    CtrlCard.adt8940a1_Setup_Speed(AxisNbr.HAND, 0, lArmSpeed, lArmACC)
                    CtrlCard.adt8940a1_Setup_Speed(AxisNbr.ARM, 0, lArmSpeed, lArmACC)
                    CtrlCard.Axis_Pmove(AxisNbr.HAND, handStroke * 1421)
                    CtrlCard.Axis_Pmove(AxisNbr.ARM, -armStroke * 2741)
                    Threading.Thread.Sleep(200)

                    ' รอ นิ่ง
                    Do
                        Application.DoEvents()
                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.ARM, status(AxisNbr.ARM - 1), 0)
                    Loop Until status(AxisNbr.ARM - 1) = 0 Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
                    
                    ' Sudden stop
                    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If

                    ' POS - VISON -------------------------------------------------------------------------------- POS - VISION

                    ' ปล่อยมือ ---------------------------------------------------------------------
                    If ib = BNbr Then
                        Do
                            Application.DoEvents()
                            If RoboticControl.EndLift_InPosition Then
                                Exit Do
                            End If
                        Loop Until RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
                    End If

                    ' Sudden stop
                    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If

                    CtrlCard.adt8940a1_Write_Output(OUTPUT_HAND, 0)
                    System.Threading.Thread.Sleep(Time_HandOFFDelay)

                    If CtrlCard.adt8940a1_Read_Input(23) = 1 And Not IsEmptyPicking Then 'ไม่มีตะกร้า ****** empty picking
                        'in23 = disk sensor
                        IsSUDDEN_STOP = True
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If


                    '******************************************************************************************************
                    ' แขนกลับ 0 พร้อมกับ ---------------------------------------------------------------------
                    ' หมุนมือกลับ ---------------------------------------------------------------------
                    armStroke = ARM_Range
                    handStroke = ARM_Range
                    status(AxisNbr.ARM - 1) = 1
                    status(AxisNbr.HAND - 1) = 1
                    CtrlCard.adt8940a1_Setup_Speed(AxisNbr.ARM, 0, lArmSpeed, lArmACC)
                    CtrlCard.adt8940a1_Setup_Speed(AxisNbr.HAND, 0, lArmSpeed, lArmACC)
                    CtrlCard.Axis_Pmove(AxisNbr.ARM, armStroke * 2741)
                    CtrlCard.Axis_Pmove(AxisNbr.HAND, -(handStroke) * 1421)

                    ' หมุนตะกร้า-----------------------------------
                    Dim ibt As Integer = ib
                    If LGRN Then
                        ibt += 1
                    End If
                    If Not ibt Mod 2 = 0 Then
                        'Check ตำแหน่ง และเลือกทิศ
                        adt8940a1.get_command_pos(0, 4, lTurnLPos)
                        If lTurnLPos > 2147483648 Then
                            lTurnLPos = lTurnLPos - 4294967296
                        End If
                        ' รอองศาแขนพ้นตะกร้า
                        'Threading.Thread.Sleep(500)
                        'lArmLPos = 0
                        'adt8940a1.get_command_pos(0, 2, lArmLPos)
                        'If lArmLPos > 2147483648 Then
                        '    lArmLPos = lArmLPos - 4294967296
                        'End If
                        'frmMain.Text = lArmLPos
                        'สั่งหมุน
                        If lTurnLPos > 0 Then
                            tTurnNegative = New System.Threading.Thread(AddressOf procTurnNegative)
                            tTurnNegative.Start()
                            'procTurnNegative()
                        ElseIf lTurnLPos = 0 Then
                            tTurnPostive = New System.Threading.Thread(AddressOf procTurnPositive)
                            tTurnPostive.Start()
                            'procTurnPositive()
                        Else ' Less than zero
                            tTurnPostive = New System.Threading.Thread(AddressOf procTurnPositive)
                            tTurnPostive.Start()
                            'procTurnPositive()
                        End If
                    Else
                        'แจ้งตัว CVY ให้รับตะกร้าทำงานต่อไป เช่น แตะ
                        tKick = New System.Threading.Thread(AddressOf procKick)
                        tKick.Start()
                    End If

                    postID += 1
                    'If postID = 1 Then
                    '    isNG = True
                    'Else
                    '    isNG = False
                    'End If
                    If postID = 1 Then
                        sb1.Remove(0, sb1.Length)
                        sb1.Append("1, ")
                        firstTimeText = Now.ToString("HH_mm_ss")
                        sb1.Append(firstTimeText)
                        System.IO.File.AppendAllText("C:\log\picker\pick_" & firstTimeText & ".txt", sb1.ToString())
                    Else
                        sb2.Remove(0, sb2.Length)
                        sb2.Append(Environment.NewLine)
                        sb2.Append(postID.ToString)
                        sb2.Append(", ")
                        sb2.Append(Now.ToString("HH:mm:ss"))
                        System.IO.File.AppendAllText("C:\log\picker\pick_" & firstTimeText & ".txt", sb2.ToString())
                    End If

                    ' รอการกลับ 0 มือและแขน---------------------------------------------------------------------
                    'Times.Delay_ms(1000)
                    status(AxisNbr.ARM - 1) = 1
                    status(AxisNbr.HAND - 1) = 1
                    Do
                        Application.DoEvents()
                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.ARM, status(AxisNbr.ARM - 1), 0)
                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.HAND, status(AxisNbr.HAND - 1), 0)
                    Loop Until (status(AxisNbr.ARM - 1) = 0 And status(AxisNbr.HAND - 1) = 0) Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
                    'Threading.Thread.Sleep(50) '====================

                    ' Sudden stop
                    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If

                    ' Goto home
                    CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.HAND, 0, 0, 200000, 0.5)
                    CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.ARM, 0, 0, 50000, 2)

                    ' รอให้นิ่ง---------------------------------------------------------------------
                    status(AxisNbr.LIFT - 1) = 1
                    status(AxisNbr.HAND - 1) = 1
                    status(AxisNbr.ARM - 1) = 1
                    Do
                        Application.DoEvents()
                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.ARM, status(AxisNbr.ARM - 1), 0)
                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.HAND, status(AxisNbr.HAND - 1), 0)
                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.LIFT, status(AxisNbr.LIFT - 1), 0)
                    Loop Until (status(AxisNbr.ARM - 1) = 0 And status(AxisNbr.HAND - 1) = 0 And status(AxisNbr.LIFT - 1) = 0) Or IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

                    ' Sudden stop
                    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If

                Next ib

                ' Goto home
                status(AxisNbr.LIFT - 1) = 1
                status(AxisNbr.HAND - 1) = 1
                status(AxisNbr.ARM - 1) = 1
                CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.LIFT, 0, 0, 200000, 2)
                CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.ARM, 0, 0, 200000, 2)
                CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.HAND, 0, 0, 200000, 0.5)
                Do
                    Application.DoEvents()
                    CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.HAND, status(AxisNbr.HAND - 1), 0)
                    CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.ARM, status(AxisNbr.ARM - 1), 0)
                    CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.LIFT, status(AxisNbr.LIFT - 1), 0)
                Loop Until (status(AxisNbr.HAND - 1) = 0 And status(AxisNbr.ARM - 1) = 0 And status(AxisNbr.LIFT - 1) = 0) Or _
                    RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

                ' Sudden stop
                'adt8940a1.reset_fifo(0)
                'For a As Short = 1 To 4
                '    CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                'Next

                'Exit Do ' TEST********************************** ทำงานรอบเดียว
                ' ---------------------------------------------------------------------

            Loop Until IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

            'IsSUDDEN_STOP = True

            IsEnable = True

        Catch ex As Exception
            frmHome.LunchUI()
            ' Sudden stop
            adt8940a1.reset_fifo(0)
            For i = 1 To 4
                CtrlCard.adt8940a1_StopRun(i, 0) 'Stop each axis
            Next i
        End Try

        Debug.WriteLine("PICK FAST 2....DONE")

    End Sub

    Public Shared Sub PICK_Slow_Camera()
        Dim nLogPos As Long                   'Logical location
        Dim nActPos As Long                   'Actual location
        Dim nSpeed As Long                    'Running Speeed
        Debug.WriteLine("PICK camera 4....")
        Try
            postID = 0
            IsSUDDEN_STOP = False

            Dim lArmSpeed As Long = ARM_SPEED
            Dim lArmACC As Long = ARM_ACC

            ' -------------------------------------------------------------------------------
            ' สรุปค่าที่อ่านได้จากหน้าจอ
            ' -------------------------------------------------------------------------------
            'If MsgBox("Top = " & LIFT_10 & ", Dz = " & DY & ", UP = " & LIFT_UP & ", Angle = " & ARM_Range & " Arm speed/ACC = " & ARM_SPEED & ", " & ARM_ACC & Environment.NewLine & "ต้องการทำต่อใช่หรือไม่", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
            '    IsEnable = True
            '    Exit Sub
            'End If

            ' -------------------------------------------------------------------------------
            ' ตั้งการทำงานของ เซนเซอร์ คุมมอเตอร์
            ' -------------------------------------------------------------------------------
            CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.LIFT, 0, 0)
            CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.LIFT, 1, 0)
            CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.LIFT, 0, 0, 0)

            CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.ARM, 0, 0)
            CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.ARM, 1, 0)
            CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.ARM, 0, 0, 0)

            CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.HAND, 0, 0)
            CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.HAND, 1, 0)
            CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.HAND, 1, 1, 0)

            CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.TURN, 0, 0)
            CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.TURN, 1, 0)
            CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.TURN, 1, 1, 0)


            ' -------------------------------------------------------------------------------
            ' ส่วนวนซ้ำ ตั้งใหม่
            ' -------------------------------------------------------------------------------
            Do
                ' -------------------------------------------------------------------------------
                ' Reload
                ' -------------------------------------------------------------------------------
                Debug.WriteLine("Reloading...")
                If CtrlCard.adt8940a1_Read_Input(27) = 1 Then 'Bottom sensor = 27, 1 = OFF

                    Dim inPosition As Boolean = False
                    Dim reloaded As Boolean = False
                    Do
                        Application.DoEvents()

                        ' Reloaded
                        If CtrlCard.adt8940a1_Read_Input(27) = 0 Then 'in27 = reloaded sensor
                            reloaded = True
                            inPosition = False
                            Exit Do
                        End If

                        ' จอดแล้ว
                        If CtrlCard.adt8940a1_Read_Input(30) = 1 And CtrlCard.adt8940a1_Read_Input(5) = 0 Then
                            'in30 = cvy stop, ready to reload
                            ' in5 = cvy sensor, in-position, in5(xexp-) ก่อนเข้า
                            inPosition = True
                            Exit Do
                        End If

                    Loop Until IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

                    ' Sudden stop
                    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If

                    ' Reloaeding...
                    If inPosition Then
                        ' รอนิ่ง
                        Threading.Thread.Sleep(1000)

                        ' -------------------------------------------------------------------------------
                        ' Check color sensor
                        ' -------------------------------------------------------------------------------
                        Debug.WriteLine("Color Sensor")
                        If CtrlCard.adt8940a1_Read_Input(4) = 0 Then 'in4(xexp+)
                            LGRN = True
                            Debug.WriteLine("LGRN = TRUE")
                        Else
                            LGRN = False
                            Debug.WriteLine("LGRN = FALSE")
                        End If
                        ' Sudden stop
                        If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                            adt8940a1.reset_fifo(0)
                            For a As Short = 1 To 4
                                CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                            Next
                            Exit Try
                        End If

                        ' -------------------------------------------------------------------------------
                        ' Reload loop
                        ' -------------------------------------------------------------------------------
                        Do
                            'tReload = New System.Threading.Thread(AddressOf procReload)
                            'tReload.Start()
                            procReload()
                            Application.DoEvents()
                            Threading.Thread.Sleep(2000)
                        Loop Until CtrlCard.adt8940a1_Read_Input(27) = 0 Or IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
                        Threading.Thread.Sleep(600)

                    End If

                End If
                Debug.WriteLine("Reloaded")
                If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then Exit Do


                Application.DoEvents()

                ' -------------------------------------------------------------------------------
                ' Fast pick cycle
                ' -------------------------------------------------------------------------------
                Debug.WriteLine("Fast pick")

                Threading.Thread.Sleep(200)
                'Continue Do

                Dim status(0 To 3) As Short
                adt8940a1.reset_fifo(0)

                'Home2()
                'CtrlCard.adt8940a1_Setup_Pos(2, 0, 0)     'Logical position counter clear
                'CtrlCard.adt8940a1_Setup_Pos(2, 0, 1)     'Clear the actual position of the counter

                ' -------------------------------------------------------------------------------
                ' Hand off
                ' -------------------------------------------------------------------------------
                CtrlCard.adt8940a1_Write_Output(OUTPUT_HAND, 0)
                Times.Delay_ms(200)


                ' -------------------------------------------------------------------------------
                ' Lift up with first position by soft movement *******************************************
                ' -------------------------------------------------------------------------------
                Dim BNbr As Integer = FirstAdjust()
                Debug.WriteLine("First up")

                ' --------------------------------------------------------------------------------
                ' LIFT LOOP
                ' --------------------------------------------------------------------------------
                For ib As Integer = 1 To BNbr

                    ' --------------------------------------------------------------------------------
                    ' ตรวจความว่างที่จานหมุน และ Ready to pick + Ready to Kick จาก PLC 
                    ' --------------------------------------------------------------------------------
                    Do
                        Application.DoEvents()
                        'in22 = aexp+ = Ready to Pick = acitve Low
                        'in28 = Ready to kick = Active low
                        'in23 = Disk empty sensor
                    Loop Until (CtrlCard.adt8940a1_Read_Input(22) = 0 And CtrlCard.adt8940a1_Read_Input(28) = 0 And CtrlCard.adt8940a1_Read_Input(23) = 1) Or IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

                    ' Sudden stop
                    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If

                    ' --------------------------------------------------------------------------------
                    ' จับ
                    ' --------------------------------------------------------------------------------
                    'Times.Delay_ms(500)
                    CtrlCard.adt8940a1_Write_Output(OUTPUT_HAND, 1)
                    Times.Delay_ms(250)

                    ' Sudden stop
                    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If

                    ' --------------------------------------------------------------------------------
                    ' ลง 
                    ' --------------------------------------------------------------------------------
                    If 1 <= ib And ib <= BNbr - 1 Then

                        CtrlCard.adt8940a1_Get_CurrentInfo(1, nLogPos, nActPos, nSpeed)
                        Debug.WriteLine(nLogPos.ToString() & ", " & nActPos.ToString & ", " & (nLogPos - nActPos).ToString)

                        status(AxisNbr.LIFT - 1) = 1
                        If ib = 1 Then
                            CtrlCard.adt8940a1_Setup_Speed(AxisNbr.LIFT, 0, LiftSpeed, LiftAcc1)
                            CtrlCard.Axis_Pmove(AxisNbr.LIFT, LIFT_DOWN)
                        Else
                            CtrlCard.adt8940a1_Setup_Speed(AxisNbr.LIFT, 0, LiftDownSpeed, LiftAcc2)
                            CtrlCard.Axis_Pmove(AxisNbr.LIFT, LIFT_DOWN)
                        End If
                        Do
                            Application.DoEvents()
                            CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.LIFT, status(AxisNbr.LIFT - 1), 0)
                        Loop Until status(AxisNbr.LIFT - 1) = 0 Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
                        Threading.Thread.Sleep(200)

                        ' Sudden stop
                        If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                            adt8940a1.reset_fifo(0)
                            For a As Short = 1 To 4
                                CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                            Next
                            Exit Try
                        End If
                    End If

                    CtrlCard.adt8940a1_Get_CurrentInfo(1, nLogPos, nActPos, nSpeed)
                    Debug.WriteLine(nLogPos.ToString() & ", " & nActPos.ToString & ", " & (nLogPos - nActPos).ToString)

                    ' สำหรับใบสุดท้าย ลดตัวยกได้ ---------------------------------------------------*****************
                    If ib = BNbr Then
                        tEndLift = New System.Threading.Thread(AddressOf procEndLift)
                        tEndLift.Start()
                    End If

                    ' เหวี่ยง 1 ******************************************************************************************************
                    Dim armStroke As Double = ARM_TO_CAM_RANGE
                    Dim handStroke As Double = HAND_TO_CAM_RANGE
                    ' บิดมือเข้า พร้อม ยกขึ้น ---------------------------------------------------------------------
                    status(AxisNbr.ARM - 1) = 1 'แขน
                    status(AxisNbr.HAND - 1) = 1 'มือ
                    'CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.ARM, 0, 0)
                    'CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.ARM, 1, 0)
                    'CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.ARM, 1, 1, 0)
                    CtrlCard.adt8940a1_Setup_Speed(AxisNbr.HAND, 0, lArmSpeed, lArmACC)
                    CtrlCard.adt8940a1_Setup_Speed(AxisNbr.ARM, 0, lArmSpeed, lArmACC)
                    CtrlCard.Axis_Pmove(AxisNbr.HAND, handStroke * 1421)
                    CtrlCard.Axis_Pmove(AxisNbr.ARM, -armStroke * 2741)
                    Threading.Thread.Sleep(200)


                    ' ยกขึ้นตามระยะ ---------------------------------------------------------------------
                    If 1 <= ib And ib <= BNbr - 1 Then
                        tLift = New Threading.Thread(AddressOf Lift)
                        tLift.Start()
                    End If

                    Do
                        Application.DoEvents()
                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.ARM, status(AxisNbr.ARM - 1), 0)
                    Loop Until status(AxisNbr.ARM - 1) = 0 Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
                    'Threading.Thread.Sleep(20)'==========

                    ' Sudden stop
                    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If

                    ' VISON -------------------------------------------------------------------------------------- VISION
                    ' ส่งสัญญาณ out14 
                    Threading.Thread.Sleep(1000) ' รอขยะหล่น
                    CtrlCard.adt8940a1_Write_Output(14, 1) 'สั่งถ่ายภาพ
                    'Threading.Thread.Sleep(200) ' รอถ่ายภาพ
                    'CtrlCard.adt8940a1_Write_Output(14, 0) 'หยุดสั่งถ่ายภาพ

                    ' รอผลวิเคราะห์
                    'Threading.Thread.Sleep(100) 'ลวงไว้ก่อน
                    Dim isReceived As Boolean = False
                    Do
                        Application.DoEvents()
                        If CtrlCard.adt8940a1_Read_Input(16) = 0 Then
                            isReceived = True
                            Exit Do
                        End If
                    Loop Until RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
                    isReceived = True
                    CtrlCard.adt8940a1_Write_Output(14, 0) 'หยุดสั่งถ่ายภาพ

                    ' Sudden stop
                    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If

                    ' Received then command
                    If isReceived Then
                        If CtrlCard.adt8940a1_Read_Input(11) = 0 Then 'ON = 0 -> NG
                            isNG = True
                        Else
                            isNG = False '->OK
                        End If
                    End If

                    ' Caution *****
                    ' isNG = False ' ใช้งานตอนแตะ / kick

                    ' เหวี่ยงต่อไปขข -----------------------------------------------------------------------------------
                    armStroke = ARM_Range - ARM_TO_CAM_RANGE
                    handStroke = ARM_Range - HAND_TO_CAM_RANGE
                    status(AxisNbr.ARM - 1) = 1 'แขน
                    status(AxisNbr.HAND - 1) = 1 'มือ
                    CtrlCard.adt8940a1_Setup_Speed(AxisNbr.HAND, 0, lArmSpeed, lArmACC)
                    CtrlCard.adt8940a1_Setup_Speed(AxisNbr.ARM, 0, lArmSpeed, lArmACC)
                    CtrlCard.Axis_Pmove(AxisNbr.HAND, handStroke * 1421)
                    CtrlCard.Axis_Pmove(AxisNbr.ARM, -armStroke * 2741)
                    Threading.Thread.Sleep(200)

                    ' รอ นิ่ง
                    Do
                        Application.DoEvents()
                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.ARM, status(AxisNbr.ARM - 1), 0)
                    Loop Until status(AxisNbr.ARM - 1) = 0 Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

                    ' Sudden stop
                    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If

                    ' POS - VISON -------------------------------------------------------------------------------- POS - VISION

                    ' ปล่อยมือ ---------------------------------------------------------------------
                    If ib = BNbr Then
                        Do
                            Application.DoEvents()
                            If RoboticControl.EndLift_InPosition Then
                                Exit Do
                            End If
                        Loop Until RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
                    End If

                    ' Sudden stop
                    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If

                    CtrlCard.adt8940a1_Write_Output(OUTPUT_HAND, 0)
                    System.Threading.Thread.Sleep(Time_HandOFFDelay)

                    If CtrlCard.adt8940a1_Read_Input(23) = 1 And Not IsEmptyPicking Then 'ไม่มีตะกร้า ****** empty picking
                        'in23 = disk sensor
                        IsSUDDEN_STOP = True
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If


                    '******************************************************************************************************
                    ' แขนกลับ 0 พร้อมกับ ---------------------------------------------------------------------
                    ' หมุนมือกลับ ---------------------------------------------------------------------
                    armStroke = ARM_Range
                    handStroke = ARM_Range
                    status(AxisNbr.ARM - 1) = 1
                    status(AxisNbr.HAND - 1) = 1
                    CtrlCard.adt8940a1_Setup_Speed(AxisNbr.ARM, 0, lArmSpeed, lArmACC)
                    CtrlCard.adt8940a1_Setup_Speed(AxisNbr.HAND, 0, lArmSpeed, lArmACC)
                    CtrlCard.Axis_Pmove(AxisNbr.ARM, armStroke * 2741)
                    CtrlCard.Axis_Pmove(AxisNbr.HAND, -(handStroke) * 1421)

                    ' หมุนตะกร้า-----------------------------------
                    '-------------------------------------------------------------
                    'Nhoppasit, 8Jan2018, เพิ่ม  And (BNbr Mod 2 = 0) 
                    '-------------------------------------------------------------
                    Dim ibt As Integer = ib 'ผมลัพธ์เลขคู่=ไม่หมุน
                    Dim TURN_KEY As Integer = 0
                    If LGRN Then
                        If BNbr Mod 2 = 0 Then TURN_KEY = 0 Else TURN_KEY = 1
                    Else
                        If BNbr Mod 2 = 0 Then TURN_KEY = 1 Else TURN_KEY = 0
                    End If

                    ibt = ib + TURN_KEY


                    '-------------------------------------------------------------
                    'Nhoppasit, 8Jan2018, ส่วนนี้เหมือนเดิม
                    '-------------------------------------------------------------
                    Debug.WriteLine(String.Format("ibt = {0}", ibt))
                    If Not ibt Mod 2 = 0 Then
                        'Check ตำแหน่ง และเลือกทิศ
                        adt8940a1.get_command_pos(0, 4, lTurnLPos)
                        If lTurnLPos > 2147483648 Then
                            lTurnLPos = lTurnLPos - 4294967296
                        End If
                        ' รอองศาแขนพ้นตะกร้า
                        'Threading.Thread.Sleep(500)
                        'lArmLPos = 0
                        'adt8940a1.get_command_pos(0, 2, lArmLPos)
                        'If lArmLPos > 2147483648 Then
                        '    lArmLPos = lArmLPos - 4294967296
                        'End If
                        'frmMain.Text = lArmLPos
                        'สั่งหมุน
                        If lTurnLPos > 0 Then
                            tTurnNegative = New System.Threading.Thread(AddressOf procTurnNegative)
                            tTurnNegative.Start()
                            'procTurnNegative()
                        ElseIf lTurnLPos = 0 Then
                            tTurnPostive = New System.Threading.Thread(AddressOf procTurnPositive)
                            tTurnPostive.Start()
                            'procTurnPositive()
                        Else ' Less than zero
                            tTurnPostive = New System.Threading.Thread(AddressOf procTurnPositive)
                            tTurnPostive.Start()
                            'procTurnPositive()
                        End If
                    Else
                        'แจ้งตัว CVY ให้รับตะกร้าทำงานต่อไป เช่น แตะ
                        tKick = New System.Threading.Thread(AddressOf procKick)
                        tKick.Start()
                    End If

                    postID += 1
                    'If postID = 1 Then
                    '    isNG = True
                    'Else
                    '    isNG = False
                    'End If
                    If postID = 1 Then
                        sb1.Remove(0, sb1.Length)
                        sb1.Append("1, ")
                        firstTimeText = Now.ToString("HH_mm_ss")
                        sb1.Append(firstTimeText)
                        System.IO.File.AppendAllText("C:\log\picker\pick_" & firstTimeText & ".txt", sb1.ToString())
                    Else
                        sb2.Remove(0, sb2.Length)
                        sb2.Append(Environment.NewLine)
                        sb2.Append(postID.ToString)
                        sb2.Append(", ")
                        sb2.Append(Now.ToString("HH:mm:ss"))
                        System.IO.File.AppendAllText("C:\log\picker\pick_" & firstTimeText & ".txt", sb2.ToString())
                    End If

                    ' รอการกลับ 0 มือและแขน---------------------------------------------------------------------
                    'Times.Delay_ms(1000)
                    status(AxisNbr.ARM - 1) = 1
                    status(AxisNbr.HAND - 1) = 1
                    Do
                        Application.DoEvents()
                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.ARM, status(AxisNbr.ARM - 1), 0)
                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.HAND, status(AxisNbr.HAND - 1), 0)
                    Loop Until (status(AxisNbr.ARM - 1) = 0 And status(AxisNbr.HAND - 1) = 0) Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
                    'Threading.Thread.Sleep(50) '====================

                    ' Sudden stop
                    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If

                    ' Goto home
                    CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.HAND, 0, 0, 200000, 0.5)
                    CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.ARM, 0, 0, 50000, 2)

                    ' รอให้นิ่ง---------------------------------------------------------------------
                    status(AxisNbr.LIFT - 1) = 1
                    status(AxisNbr.HAND - 1) = 1
                    status(AxisNbr.ARM - 1) = 1
                    Do
                        Application.DoEvents()
                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.ARM, status(AxisNbr.ARM - 1), 0)
                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.HAND, status(AxisNbr.HAND - 1), 0)
                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.LIFT, status(AxisNbr.LIFT - 1), 0)
                    Loop Until (status(AxisNbr.ARM - 1) = 0 And status(AxisNbr.HAND - 1) = 0 And status(AxisNbr.LIFT - 1) = 0) Or IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

                    ' Sudden stop
                    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If

                Next ib

                ' Goto home
                status(AxisNbr.LIFT - 1) = 1
                status(AxisNbr.HAND - 1) = 1
                status(AxisNbr.ARM - 1) = 1
                CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.LIFT, 0, 0, 200000, 2)
                CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.ARM, 0, 0, 200000, 2)
                CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.HAND, 0, 0, 200000, 0.5)
                Do
                    Application.DoEvents()
                    CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.HAND, status(AxisNbr.HAND - 1), 0)
                    CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.ARM, status(AxisNbr.ARM - 1), 0)
                    CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.LIFT, status(AxisNbr.LIFT - 1), 0)
                Loop Until (status(AxisNbr.HAND - 1) = 0 And status(AxisNbr.ARM - 1) = 0 And status(AxisNbr.LIFT - 1) = 0) Or _
                    RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

                ' Sudden stop
                'adt8940a1.reset_fifo(0)
                'For a As Short = 1 To 4
                '    CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                'Next

                'Exit Do ' TEST********************************** ทำงานรอบเดียว
                ' ---------------------------------------------------------------------

            Loop Until IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

            'IsSUDDEN_STOP = True

            IsEnable = True

        Catch ex As Exception
            frmHome.LunchUI()
            ' Sudden stop
            adt8940a1.reset_fifo(0)
            For i = 1 To 4
                CtrlCard.adt8940a1_StopRun(i, 0) 'Stop each axis
            Next i
        End Try

        Debug.WriteLine("PICK FAST 2....DONE")

    End Sub

    Public Shared Sub PICK_SLOW_NO_CAMERA()
        Dim nLogPos As Long                   'Logical location
        Dim nActPos As Long                   'Actual location
        Dim nSpeed As Long                    'Running Speeed
        Debug.WriteLine("PICK SLOW NO CAMERA....")
        Try
            postID = 0
            IsSUDDEN_STOP = False

            Dim lArmSpeed As Long = ARM_SPEED
            Dim lArmACC As Long = ARM_ACC

            ' -------------------------------------------------------------------------------
            ' สรุปค่าที่อ่านได้จากหน้าจอ
            ' -------------------------------------------------------------------------------
            'If MsgBox("Top = " & LIFT_10 & ", Dz = " & DY & ", UP = " & LIFT_UP & ", Angle = " & ARM_Range & " Arm speed/ACC = " & ARM_SPEED & ", " & ARM_ACC & Environment.NewLine & "ต้องการทำต่อใช่หรือไม่", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
            '    IsEnable = True
            '    Exit Sub
            'End If

            ' -------------------------------------------------------------------------------
            ' ตั้งการทำงานของ เซนเซอร์ คุมมอเตอร์
            ' -------------------------------------------------------------------------------
            CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.LIFT, 0, 0)
            CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.LIFT, 1, 0)
            CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.LIFT, 0, 0, 0)

            CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.ARM, 0, 0)
            CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.ARM, 1, 0)
            CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.ARM, 0, 0, 0)

            CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.HAND, 0, 0)
            CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.HAND, 1, 0)
            CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.HAND, 1, 1, 0)

            CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.TURN, 0, 0)
            CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.TURN, 1, 0)
            CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.TURN, 1, 1, 0)


            ' -------------------------------------------------------------------------------
            ' ส่วนวนซ้ำ ตั้งใหม่
            ' -------------------------------------------------------------------------------
            Do
                ' -------------------------------------------------------------------------------
                ' Reload
                ' -------------------------------------------------------------------------------
                Debug.WriteLine("Reloading...")
                If CtrlCard.adt8940a1_Read_Input(27) = 1 Then 'Bottom sensor = 27, 1 = OFF

                    Dim inPosition As Boolean = False
                    Dim reloaded As Boolean = False
                    Do
                        Application.DoEvents()

                        ' Reloaded
                        If CtrlCard.adt8940a1_Read_Input(27) = 0 Then 'in27 = reloaded sensor
                            reloaded = True
                            inPosition = False
                            Exit Do
                        End If

                        ' จอดแล้ว
                        If CtrlCard.adt8940a1_Read_Input(30) = 1 And CtrlCard.adt8940a1_Read_Input(5) = 0 Then
                            'in30 = cvy stop, ready to reload - 
                            'A-1IN30-P1CVY-PLC2-Y0-ADTA-U24
                            'B-2IN30-P5CVY-PLC2-Y2-ADTB-U24
                            '
                            ' in5 = cvy sensor, in-position, in5(xexp-) - 
                            'A-1IN5-J2-PHOTO-P1CVY-PLC2-X2-ADTA-T25
                            'B-2IN5-J5-Photo-P3CVY-PLC2-X5-ADTB-T25
                            inPosition = True
                            Exit Do
                        End If

                    Loop Until IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

                    ' Sudden stop
                    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If

                    ' Reloaeding...
                    If inPosition Then
                        ' รอนิ่ง
                        Threading.Thread.Sleep(1000)

                        ' -------------------------------------------------------------------------------
                        ' Check color sensor
                        ' -------------------------------------------------------------------------------
                        Debug.WriteLine("Color Sensor")
                        If CtrlCard.adt8940a1_Read_Input(4) = 0 Then 'in4(xexp+)
                            LGRN = True
                            Debug.WriteLine("LGRN = TRUE")
                        Else
                            LGRN = False
                            Debug.WriteLine("LGRN = FALSE")
                        End If
                        ' Sudden stop
                        If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                            adt8940a1.reset_fifo(0)
                            For a As Short = 1 To 4
                                CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                            Next
                            Exit Try
                        End If

                        ' -------------------------------------------------------------------------------
                        ' Reload loop
                        ' -------------------------------------------------------------------------------
                        Do
                            'tReload = New System.Threading.Thread(AddressOf procReload)
                            'tReload.Start()
                            procReload()
                            Application.DoEvents()
                            Threading.Thread.Sleep(2000)
                        Loop Until CtrlCard.adt8940a1_Read_Input(27) = 0 Or IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
                        Threading.Thread.Sleep(600)

                    End If

                End If
                Debug.WriteLine("Reloaded")
                If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then Exit Do


                Application.DoEvents()

                ' -------------------------------------------------------------------------------
                ' Fast pick cycle
                ' -------------------------------------------------------------------------------
                Debug.WriteLine("Slow pick")

                Threading.Thread.Sleep(200)
                'Continue Do

                Dim status(0 To 3) As Short
                adt8940a1.reset_fifo(0)

                'Home2()
                'CtrlCard.adt8940a1_Setup_Pos(2, 0, 0)     'Logical position counter clear
                'CtrlCard.adt8940a1_Setup_Pos(2, 0, 1)     'Clear the actual position of the counter

                ' -------------------------------------------------------------------------------
                ' Hand off
                ' -------------------------------------------------------------------------------
                CtrlCard.adt8940a1_Write_Output(OUTPUT_HAND, 0)
                Times.Delay_ms(200)


                ' -------------------------------------------------------------------------------
                ' Lift up with first position by soft movement *******************************************
                ' -------------------------------------------------------------------------------
                Dim BNbr As Integer = FirstAdjust()
                Debug.WriteLine("First up")

                ' --------------------------------------------------------------------------------
                ' LIFT LOOP
                ' --------------------------------------------------------------------------------
                For ib As Integer = 1 To BNbr

                    ' --------------------------------------------------------------------------------
                    ' ตรวจความว่างที่จานหมุน และ Ready to pick + Ready to Kick จาก PLC 
                    ' --------------------------------------------------------------------------------
                    Do
                        Application.DoEvents()
                        'in22 = aexp+ = Ready to Pick = acitve Low 
                        'A-1RDP-1IN22/1AEXP+(T44)-PLC2-Y45
                        'B-1RDP-2IN22/2AEXP+(T44)-PLC2-Y46
                        '
                        'in28 = Ready to kick = Active low
                        'A-1RDK-1IN28(U22)-PLC2-Y15
                        'B-2RDK-2IN28(U22)-PLC2-Y16
                        '
                        'in23 = Disk empty sensor
                        'A-K1/1ROT-1IN23/1AEXP-(T45)-PHOTO-ON-DISK-PLC2-X15
                        'B-K2/2ROT-2IN23/2AEXP-(T45)-PHOTO-ON-DISK-PLC2-X13
                        '
                    Loop Until (CtrlCard.adt8940a1_Read_Input(22) = 0 And CtrlCard.adt8940a1_Read_Input(28) = 0 And CtrlCard.adt8940a1_Read_Input(23) = 1) Or IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

                    ' --------------------------------------------------------------------------------
                    ' จับ
                    ' --------------------------------------------------------------------------------
                    'Times.Delay_ms(500)
                    CtrlCard.adt8940a1_Write_Output(OUTPUT_HAND, 1)
                    Times.Delay_ms(250)


                    ' --------------------------------------------------------------------------------
                    ' ลง 
                    ' --------------------------------------------------------------------------------
                    If 1 <= ib And ib <= BNbr - 1 Then

                        CtrlCard.adt8940a1_Get_CurrentInfo(1, nLogPos, nActPos, nSpeed)
                        Debug.WriteLine(nLogPos.ToString() & ", " & nActPos.ToString & ", " & (nLogPos - nActPos).ToString)

                        status(AxisNbr.LIFT - 1) = 1
                        If ib = 1 Then
                            CtrlCard.adt8940a1_Setup_Speed(AxisNbr.LIFT, 0, LiftSpeed, LiftAcc1)
                            CtrlCard.Axis_Pmove(AxisNbr.LIFT, LIFT_DOWN)
                        Else
                            CtrlCard.adt8940a1_Setup_Speed(AxisNbr.LIFT, 0, LiftDownSpeed, LiftAcc2)
                            CtrlCard.Axis_Pmove(AxisNbr.LIFT, LIFT_DOWN)
                        End If
                        Do
                            Application.DoEvents()
                            CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.LIFT, status(AxisNbr.LIFT - 1), 0)
                        Loop Until status(AxisNbr.LIFT - 1) = 0 Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
                        Threading.Thread.Sleep(200)

                        ' Sudden stop
                        If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                            adt8940a1.reset_fifo(0)
                            For a As Short = 1 To 4
                                CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                            Next
                            Exit Try
                        End If
                    End If

                    CtrlCard.adt8940a1_Get_CurrentInfo(1, nLogPos, nActPos, nSpeed)
                    Debug.WriteLine(nLogPos.ToString() & ", " & nActPos.ToString & ", " & (nLogPos - nActPos).ToString)

                    ' สำหรับใบสุดท้าย ลดตัวยกได้ ---------------------------------------------------*****************
                    If ib = BNbr Then
                        tEndLift = New System.Threading.Thread(AddressOf procEndLift)
                        tEndLift.Start()
                    End If

                    '******************************************************************************************************
                    Dim armStroke As Double = ARM_Range
                    Dim handStroke As Double = ARM_Range
                    ' บิดมือเข้า พร้อม ยกขึ้น ---------------------------------------------------------------------
                    status(AxisNbr.ARM - 1) = 1 'แขน
                    status(AxisNbr.HAND - 1) = 1 'มือ
                    'CtrlCard.adt8940a1_Setup_Stop0Mode(AxisNbr.ARM, 0, 0)
                    'CtrlCard.adt8940a1_Setup_Stop1Mode(AxisNbr.ARM, 1, 0)
                    'CtrlCard.adt8940a1_Setup_LimitMode(AxisNbr.ARM, 1, 1, 0)
                    CtrlCard.adt8940a1_Setup_Speed(AxisNbr.HAND, 0, lArmSpeed, lArmACC)
                    CtrlCard.adt8940a1_Setup_Speed(AxisNbr.ARM, 0, lArmSpeed, lArmACC)
                    CtrlCard.Axis_Pmove(AxisNbr.HAND, handStroke * 1421)
                    CtrlCard.Axis_Pmove(AxisNbr.ARM, -armStroke * 2741)
                    Threading.Thread.Sleep(200)


                    ' ยกขึ้นตามระยะ ---------------------------------------------------------------------
                    If 1 <= ib And ib <= BNbr - 1 Then
                        tLift = New Threading.Thread(AddressOf Lift)
                        tLift.Start()
                    End If


                    Do
                        Application.DoEvents()
                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.ARM, status(AxisNbr.ARM - 1), 0)
                    Loop Until status(AxisNbr.ARM - 1) = 0 Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
                    'Threading.Thread.Sleep(20)'==========

                    ' Sudden stop
                    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If


                    ' ปล่อยมือ ---------------------------------------------------------------------
                    If ib = BNbr Then
                        Do
                            Application.DoEvents()
                            If RoboticControl.EndLift_InPosition Then
                                Exit Do
                            End If
                        Loop Until RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
                    End If

                    ' Sudden stop
                    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If

                    CtrlCard.adt8940a1_Write_Output(OUTPUT_HAND, 0)
                    System.Threading.Thread.Sleep(Time_HandOFFDelay)

                    If CtrlCard.adt8940a1_Read_Input(23) = 1 And Not IsEmptyPicking Then 'ไม่มีตะกร้า ****** empty picking
                        'in23 = disk sensor
                        IsSUDDEN_STOP = True
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If


                    '******************************************************************************************************
                    ' แขนกลับ 0 พร้อมกับ ---------------------------------------------------------------------
                    ' หมุนมือกลับ ---------------------------------------------------------------------
                    status(AxisNbr.ARM - 1) = 1
                    status(AxisNbr.HAND - 1) = 1
                    CtrlCard.adt8940a1_Setup_Speed(AxisNbr.ARM, 0, lArmSpeed, lArmACC)
                    CtrlCard.adt8940a1_Setup_Speed(AxisNbr.HAND, 0, lArmSpeed, lArmACC)
                    CtrlCard.Axis_Pmove(AxisNbr.ARM, armStroke * 2741)
                    CtrlCard.Axis_Pmove(AxisNbr.HAND, -(handStroke) * 1421)

                    ' หมุนตะกร้า-----------------------------------
                    '-------------------------------------------------------------
                    'Nhoppasit, 8Jan2018, เพิ่ม  And (BNbr Mod 2 = 0) 
                    '-------------------------------------------------------------
                    Dim ibt As Integer = ib 'ผมลัพธ์เลขคู่=ไม่หมุน
                    Dim TURN_KEY As Integer = 0
                    If LGRN Then
                        If BNbr Mod 2 = 0 Then TURN_KEY = 0 Else TURN_KEY = 1
                    Else
                        If BNbr Mod 2 = 0 Then TURN_KEY = 1 Else TURN_KEY = 0
                    End If
                    
                    ibt = ib + TURN_KEY


                    '-------------------------------------------------------------
                    'Nhoppasit, 8Jan2018, ส่วนนี้เหมือนเดิม
                    '-------------------------------------------------------------
                    Debug.WriteLine(String.Format("ibt = {0}", ibt))
                    If Not ibt Mod 2 = 0 Then
                        Debug.WriteLine("TURN THE DISK")

                        'Check ตำแหน่ง และเลือกทิศ
                        adt8940a1.get_command_pos(0, 4, lTurnLPos)
                        If lTurnLPos > 2147483648 Then
                            lTurnLPos = lTurnLPos - 4294967296
                        End If
                        ' รอองศาแขนพ้นตะกร้า
                        'Threading.Thread.Sleep(500)
                        'lArmLPos = 0
                        'adt8940a1.get_command_pos(0, 2, lArmLPos)
                        'If lArmLPos > 2147483648 Then
                        '    lArmLPos = lArmLPos - 4294967296
                        'End If
                        'frmMain.Text = lArmLPos
                        'สั่งหมุน
                        If lTurnLPos > 0 Then
                            tTurnNegative = New System.Threading.Thread(AddressOf procTurnNegative)
                            tTurnNegative.Start()
                            'procTurnNegative()
                        ElseIf lTurnLPos = 0 Then
                            tTurnPostive = New System.Threading.Thread(AddressOf procTurnPositive)
                            tTurnPostive.Start()
                            'procTurnPositive()
                        Else ' Less than zero
                            tTurnPostive = New System.Threading.Thread(AddressOf procTurnPositive)
                            tTurnPostive.Start()
                            'procTurnPositive()
                        End If
                    Else
                        Debug.WriteLine("NO TURN THE DISK")

                        'แจ้งตัว CVY ให้รับตะกร้าทำงานต่อไป เช่น แตะ
                        tKick = New System.Threading.Thread(AddressOf procKick)
                        tKick.Start()
                    End If

                    postID += 1
                    'If postID = 1 Then
                    '    isNG = True
                    'Else
                    '    isNG = False
                    'End If
                    If postID = 1 Then
                        sb1.Remove(0, sb1.Length)
                        sb1.Append("1, ")
                        firstTimeText = Now.ToString("HH_mm_ss")
                        sb1.Append(firstTimeText)
                        System.IO.File.AppendAllText("C:\log\picker\pick_" & firstTimeText & ".txt", sb1.ToString())
                    Else
                        sb2.Remove(0, sb2.Length)
                        sb2.Append(Environment.NewLine)
                        sb2.Append(postID.ToString)
                        sb2.Append(", ")
                        sb2.Append(Now.ToString("HH:mm:ss"))
                        System.IO.File.AppendAllText("C:\log\picker\pick_" & firstTimeText & ".txt", sb2.ToString())
                    End If

                    ' รอการกลับ 0 มือและแขน---------------------------------------------------------------------
                    'Times.Delay_ms(1000)
                    status(AxisNbr.ARM - 1) = 1
                    status(AxisNbr.HAND - 1) = 1
                    Do
                        Application.DoEvents()
                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.ARM, status(AxisNbr.ARM - 1), 0)
                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.HAND, status(AxisNbr.HAND - 1), 0)
                    Loop Until (status(AxisNbr.ARM - 1) = 0 And status(AxisNbr.HAND - 1) = 0) Or RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
                    'Threading.Thread.Sleep(50) '====================

                    ' Sudden stop
                    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If

                    ' Goto home
                    CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.HAND, 0, 0, 200000, 0.5)
                    CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.ARM, 0, 0, 50000, 2)

                    ' รอให้นิ่ง---------------------------------------------------------------------
                    status(AxisNbr.LIFT - 1) = 1
                    status(AxisNbr.HAND - 1) = 1
                    status(AxisNbr.ARM - 1) = 1
                    Do
                        Application.DoEvents()
                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.ARM, status(AxisNbr.ARM - 1), 0)
                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.HAND, status(AxisNbr.HAND - 1), 0)
                        CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.LIFT, status(AxisNbr.LIFT - 1), 0)
                    Loop Until (status(AxisNbr.ARM - 1) = 0 And status(AxisNbr.HAND - 1) = 0 And status(AxisNbr.LIFT - 1) = 0) Or IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

                    ' Sudden stop
                    If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
                        adt8940a1.reset_fifo(0)
                        For a As Short = 1 To 4
                            CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                        Next
                        Exit Try
                    End If

                Next ib

                ' Goto home
                status(AxisNbr.LIFT - 1) = 1
                status(AxisNbr.HAND - 1) = 1
                status(AxisNbr.ARM - 1) = 1
                CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.LIFT, 0, 0, 200000, 2)
                CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.ARM, 0, 0, 200000, 2)
                CtrlCard.adt8940a1_Sym_AbsoluteMove(AxisNbr.HAND, 0, 0, 200000, 0.5)
                Do
                    Application.DoEvents()
                    CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.HAND, status(AxisNbr.HAND - 1), 0)
                    CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.ARM, status(AxisNbr.ARM - 1), 0)
                    CtrlCard.adt8940a1_Get_MoveStatus(AxisNbr.LIFT, status(AxisNbr.LIFT - 1), 0)
                Loop Until (status(AxisNbr.HAND - 1) = 0 And status(AxisNbr.ARM - 1) = 0 And status(AxisNbr.LIFT - 1) = 0) Or _
                    RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

                ' Sudden stop
                'adt8940a1.reset_fifo(0)
                'For a As Short = 1 To 4
                '    CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
                'Next

                'Exit Do ' TEST********************************** ทำงานรอบเดียว
                ' ---------------------------------------------------------------------

            Loop Until IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1

            'IsSUDDEN_STOP = True

            IsEnable = True

        Catch ex As Exception
            frmHome.LunchUI()
            ' Sudden stop
            adt8940a1.reset_fifo(0)
            For i = 1 To 4
                CtrlCard.adt8940a1_StopRun(i, 0) 'Stop each axis
            Next i
        End Try

        Debug.WriteLine("PICK FAST 2....DONE")

    End Sub

    Public Shared Sub TestVision()
        IsSUDDEN_STOP = False
        CtrlCard.adt8940a1_Write_Output(14, 1) 'สั่งถ่ายภาพ
        'Threading.Thread.Sleep(200) ' รอถ่ายภาพ


        ' รอผลวิเคราะห์
        'Threading.Thread.Sleep(100) 'ลวงไว้ก่อน
        Dim isReceived As Boolean = False
        Do
            Application.DoEvents()
            If CtrlCard.adt8940a1_Read_Input(16) = 0 Then
                isReceived = True
                Exit Do
            End If
        Loop Until RoboticControl.IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1
        isReceived = True
        CtrlCard.adt8940a1_Write_Output(14, 0) 'หยุดสั่งถ่ายภาพ

        ' Sudden stop
        If IsSUDDEN_STOP Or CtrlCard.adt8940a1_Read_Input(20) = 1 Then
            adt8940a1.reset_fifo(0)
            For a As Short = 1 To 4
                CtrlCard.adt8940a1_StopRun(a, 0) 'Stop each axis
            Next
            Exit Sub
        End If

        ' Received then command
        If isReceived Then
            If CtrlCard.adt8940a1_Read_Input(11) = 0 Then 'ON = 0 -> NG
                isNG = True
            Else
                isNG = False '->OK
            End If
            frmHome.chkNG.Checked = isNG
        End If
    End Sub

End Class
