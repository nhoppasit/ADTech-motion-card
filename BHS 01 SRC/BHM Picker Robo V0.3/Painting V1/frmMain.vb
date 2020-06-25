Imports ADTMotionControl
Public Class frmMain

#Region "Cross-thread interfacing"

    Private Delegate Sub VoidDelegate()

    Public Sub PostText(ByVal text As String)
        If Me.InvokeRequired Then
            Me.Invoke(New VoidDelegate(Sub()
                                           If text = "" Then
                                               Me.Text = "-"
                                           Else
                                               Me.Text = text
                                           End If
                                       End Sub))
        Else
            If text = "" Then
                Me.Text = "-"
            Else
                Me.Text = text
            End If
        End If
    End Sub

#End Region

#Region "ตัวแปร และ ค่าคงที่ ต่างๆ"

    Dim adt8940a1_g_nLibVer As Double
    Dim adt8940a1_g_nHardWareVer As Double
    Dim EMG As Boolean = False
    Dim EMGEXIT As Boolean = False
    Dim dlgEMG As Boolean = False

    Dim remote As Boolean = False
    Dim remotebegin As Boolean = False
    Dim InTeach As Boolean = False
    Public Shared InEdit As Boolean = False
    Dim teachmode As String = "-"
    Dim ServoOn As Boolean = False

    'Dim g_LineinpMove As Boolean
    'Dim g_CompMove As Boolean
    'Dim nDirMode As Integer

    'Dim g_CheckManual As Boolean
    'Dim g_bStopFlag As Boolean

    'Public effectlogic_x As Integer
    'Public effectlogic_y As Integer
    'Public effectlogic_z As Integer
    'Public effectlogic_a As Integer
    'Public effectlogic_b As Integer
    'Public effectlogic_c As Integer
    'Public registermode As Integer

    Dim IDC_IO() As Integer = {1069, 1070, 1071, 1072, 1073, 1074, 1075, 1076,
                               1078, 1079, 1080, 1081, 1082, 1083, 1084, 1085}
    ' 1077 is out0
#End Region

#Region "ออกจากโปรแกรม"

    Sub FinializeSystem()

        Dim _Quit As New dlgMsgYESNO
        _Quit.rtbText.Text = "คุณต้องการปิดระบบ ใช่หรือไม่ ?" & Environment.NewLine & _
                             "ปิดระบบ = YES, ไม่ต้องการ = NO"
        If _Quit.ShowDialog = Windows.Forms.DialogResult.Yes Then
            
            '------------------------------------
            ' สั่งปิด
            '------------------------------------
            Dim dlg As dlgMessage
            dlg = New dlgMessage(5000)
            dlg.rtbText.Text = "กำลังปิดระบบ..."
            dlg.Show()
            dlg.TopMost = True

            'V-Break
            CtrlCard.adt8940a1_Write_Output(RoboticControl.ContactNo.V_Break, 0)
            Times.Delay_ms2(1000)

            'SON to OFF
            CtrlCard.adt8940a1_Write_Output(RoboticControl.ContactNo.SON, 0)
            Times.Delay_ms2(1000)

            'MC OFF
            CtrlCard.adt8940a1_Write_Output(RoboticControl.ContactNo.MC_OFF, 1) 'Select on activation
            Times.Delay_ms2(1000)
            CtrlCard.adt8940a1_Write_Output(RoboticControl.ContactNo.MC_OFF, 0) 'Select on activation
            Times.Delay_ms2(1000)

            ' Show
            dlg.Dispose()
            dlg = Nothing
            _Quit.Dispose()
            _Quit = Nothing

            End 'ปิดระบบ
            '
        Else
            _Quit.Dispose()
            _Quit = Nothing
        End If
    End Sub

    Private Sub btnQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuit.Click

        FinializeSystem()

    End Sub

    Private Sub frmMainMenu_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        e.Cancel = True
        FinializeSystem()

    End Sub

#End Region

#Region "เริ่มโปรแกรม"

    Private Sub InitBoard()

        If Not My.Settings.UseCard Then Exit Sub

        Dim count As Integer
        Dim count1 As Integer
        count1 = CtrlCard.adt8940a1_Init_Card()
        Dim b As Single = adt8940a1.get_lib_version(0)


        If count1 < 1 Then MsgBox("adt8940 card is not installed correctly")

        adt8940a1_g_nHardWareVer = CtrlCard.adt8940a1_Get_Version()
        Dim sb As New System.Text.StringBuilder
        sb.Append(String.Format("Card Hardwre Version: {0:0.00}", adt8940a1_g_nHardWareVer))
        sb.Append(Environment.NewLine)
        sb.Append(String.Format("Card Library Version: {0}", b))
        sb.Append(Environment.NewLine)
        sb.Append(String.Format("A Softward Version: {0}-{1}", "11JAN2018", Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(4)))
        lblCardVersion.Text = sb.ToString

    End Sub

    Private Sub InitSystem()

        Try
            If Not My.Settings.UseCard Then Exit Sub

            Dim ret As Short

            'MC OFF
            CtrlCard.adt8940a1_Write_Output(RoboticControl.ContactNo.MC_OFF, 1) 'Select on activation
            Times.Delay_ms2(1000)
            CtrlCard.adt8940a1_Write_Output(RoboticControl.ContactNo.MC_OFF, 0) 'Select on activation
            Times.Delay_ms2(1000)

            'Clear
            CtrlCard.adt8940a1_Write_Output(RoboticControl.ContactNo.ARST, 1)
            Times.Delay_ms2(1000)
            CtrlCard.adt8940a1_Write_Output(RoboticControl.ContactNo.ARST, 0)

            ' MC ON
            CtrlCard.adt8940a1_Write_Output(RoboticControl.ContactNo.MC_ON, 1)
            Times.Delay_ms2(1000)
            CtrlCard.adt8940a1_Write_Output(RoboticControl.ContactNo.MC_ON, 0)
            Times.Delay_ms2(1000)

            'Clear
            CtrlCard.adt8940a1_Write_Output(RoboticControl.ContactNo.ARST, 1)
            Times.Delay_ms2(1000)
            CtrlCard.adt8940a1_Write_Output(RoboticControl.ContactNo.ARST, 0)

            'V-Break on
            CtrlCard.adt8940a1_Write_Output(RoboticControl.ContactNo.SON, 1)
            Times.Delay_ms2(1000)

            ' Reset
            CtrlCard.adt8940a1_Write_Output(RoboticControl.ContactNo.ARST, 1)
            Times.Delay_ms2(1000)
            CtrlCard.adt8940a1_Write_Output(RoboticControl.ContactNo.ARST, 0)

            ' Break on
            CtrlCard.adt8940a1_Write_Output(RoboticControl.ContactNo.V_Break, 1)
            Times.Delay_ms2(1000)

            'Limit sensor
            ret = CtrlCard.adt8940a1_Setup_LimitMode(1, 0, 0, 0) 'PNP
            CtrlCard.adt8940a1_Setup_LimitMode(2, 0, 0, 0)
            CtrlCard.adt8940a1_Setup_LimitMode(3, 0, 0, 0)
            CtrlCard.adt8940a1_Setup_LimitMode(4, 0, 0, 0) 'NPN

            'SON
            CtrlCard.adt8940a1_Write_Output(RoboticControl.ContactNo.SON, 1)
            ServoOn = True
            Times.Delay_ms2(1500)
            'V-Break on
            CtrlCard.adt8940a1_Write_Output(RoboticControl.ContactNo.V_Break, 1)

            'ARST
            CtrlCard.adt8940a1_Write_Output(RoboticControl.ContactNo.ARST, 1)
            Times.Delay_ms2(200)
            CtrlCard.adt8940a1_Write_Output(RoboticControl.ContactNo.ARST, 0)
            Times.Delay_ms2(200)

            ' Hand off
            CtrlCard.adt8940a1_Write_Output(1, 0)
            Times.Delay_ms2(200)

            timerRunning.Start()
        Catch ex As Exception

            MsgBox(ex.Message)
        End Try


    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '---------------------------------------------------------
        ' Startup
        '---------------------------------------------------------
        Me.Enabled = False
        InitBoard()
        InitSystem() 'อาจมีคำสั่งปิดไว้ที่ timer สำหรับแสดง info

        ShowForm(frmSystemInfo, pnSystemInfo)
        ShowForm(frmJogging, pnJogging)
        ShowForm(frmHome, pnMain)
        '
        Application.DoEvents()
        Me.WindowState = FormWindowState.Maximized
        Me.Show()
        Application.DoEvents()

        '---------------------------------------------------------
        ' Home asking
        '---------------------------------------------------------
        If My.Settings.UseCard Then
            CtrlCard.adt8940a1_Setup_Stop0Mode(1, 1, 0)
            CtrlCard.adt8940a1_Setup_Stop1Mode(1, 1, 0)
            CtrlCard.adt8940a1_Setup_LimitMode(1, 0, 0, 0)

            CtrlCard.adt8940a1_Setup_Stop0Mode(2, 1, 0)
            CtrlCard.adt8940a1_Setup_Stop1Mode(2, 1, 0)
            CtrlCard.adt8940a1_Setup_LimitMode(2, 0, 0, 0)

            CtrlCard.adt8940a1_Setup_Stop0Mode(3, 1, 0)
            CtrlCard.adt8940a1_Setup_Stop1Mode(3, 1, 0)
            CtrlCard.adt8940a1_Setup_LimitMode(3, 0, 0, 0)

            CtrlCard.adt8940a1_Setup_Stop0Mode(4, 1, 0)
            CtrlCard.adt8940a1_Setup_Stop1Mode(4, 1, 0)
            CtrlCard.adt8940a1_Setup_LimitMode(4, 0, 0, 0)
        End If

        Me.Enabled = True

    End Sub

#End Region

#Region "คำสั่งหลักของ frmMain"

    Public Sub FreezMenu()

        panMenu.Enabled = False

    End Sub

    Public Sub LunchMenu()

        panMenu.Enabled = True

    End Sub

#End Region

#Region "IO test"

    Private Sub btnIOTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ShowForm(frmIOTest, pnMain)
        frmJogging.ShowJogging()
    End Sub

#End Region

#Region "Parameters"

    Private Sub btnParameter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ShowForm(frmParam, pnMain)
        frmJogging.ShowJogging()
    End Sub

#End Region

#Region "Home"

    Private Sub btnHome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHome.Click
        ShowForm(frmHome, pnMain)
        frmJogging.ShowJogging()
    End Sub

#End Region

#Region "Running Parameters + Timer"

    Public ControlPannelFlag As Boolean = True

    Private Sub timerRunning_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerRunning.Tick
        If Not My.Settings.UseCard Then Exit Sub
        DoCheckParameters()

        '--------------------------------------------------------'
        '-----------------------EMG_Check---------------------------
        '--------------------------------------------------------'
        If CtrlCard.adt8940a1_Read_Input(20) = 1 Then
            RoboticControl.IsEnable = True
            StopLib.DecelStopAll()
            EMG = True
        End If

        '--------------------------------------------------------'
        ' SYSTEM CONTROL PANEL
        ' IN24 RESET
        ' IN25 HOME
        ' IN26 START/RUN
        '--------------------------------------------------------'
        ' RESET
        If ControlPannelFlag And CtrlCard.adt8940a1_Read_Input(24) = 0 And CtrlCard.adt8940a1_Read_Input(25) = 1 And CtrlCard.adt8940a1_Read_Input(26) = 1 And RoboticControl.IsEnable = True Then
            ControlPannelFlag = False
            frmHome.Button3.Enabled = False
            frmHome.FreezUI()
            CtrlCard.adt8940a1_Write_Output(15, 1)
            RoboticControl.ResetControlBox()
            frmHome.LunchUI()
            frmHome.Button3.Enabled = True
            ControlPannelFlag = True
        End If
        ' HOME
        If ControlPannelFlag And CtrlCard.adt8940a1_Read_Input(24) = 1 And CtrlCard.adt8940a1_Read_Input(25) = 0 And CtrlCard.adt8940a1_Read_Input(26) = 1 And RoboticControl.IsEnable = True Then
            ControlPannelFlag = False
            frmHome.DoHomeLift()
            frmHome.DoHomeARM()
            frmHome.DoHomeHAND()
            frmHome.DoHomeTURN()
            ControlPannelFlag = True
        End If
        ' START/RUN
        If ControlPannelFlag And CtrlCard.adt8940a1_Read_Input(24) = 1 And CtrlCard.adt8940a1_Read_Input(25) = 1 And CtrlCard.adt8940a1_Read_Input(26) = 0 And RoboticControl.IsEnable = True And frmHome.CurrentRunCommand <> frmHome.RunCommmand.NONE Then
            ControlPannelFlag = False
            Select Case frmHome.CurrentRunCommand
                Case frmHome.RunCommmand.FastStartNoCamera
                    frmHome.btnFastStartNoCamera_Click(sender, New EventArgs)
                Case frmHome.RunCommmand.SlowStartNoCamera
                    frmHome.btnSlowStartNoCamera_Click(sender, New EventArgs)
                Case frmHome.RunCommmand.FastStartWithCamera
                    frmHome.btnFastStartWithCamera_Click(sender, New EventArgs)
                Case frmHome.RunCommmand.SlowStartWithCamera
                    frmHome.btnSlowStartWithCamera_Click(sender, New EventArgs)
            End Select
            ControlPannelFlag = True
        End If

        If (CtrlCard.adt8940a1_Read_Input(3) = 1 Or CtrlCard.adt8940a1_Read_Input(9) = 1 Or CtrlCard.adt8940a1_Read_Input(15) = 1 Or CtrlCard.adt8940a1_Read_Input(21) = 1) Then
            CtrlCard.adt8940a1_Write_Output(15, 1)
        Else
            If ControlPannelFlag Then CtrlCard.adt8940a1_Write_Output(15, 0)
        End If

        If RoboticControl.IsEnable = True Then
            CtrlCard.adt8940a1_Write_Output(11, 0)
        Else
            CtrlCard.adt8940a1_Write_Output(11, 1)
        End If

        'ข้อความแสดงวัฏจักรที่เลือก
        Select Case frmHome.CurrentRunCommand
            Case frmHome.RunCommmand.NONE : frmHome.RunCommandText("ยังไม่ได้เลือกวัฏจักรการทำงาน")
            Case frmHome.RunCommmand.FastStartNoCamera : frmHome.RunCommandText("เลือกทำงาน - ป้อนสิบใบเท่านั้น ไม่มีกล้อง")
            Case frmHome.RunCommmand.FastStartWithCamera : frmHome.RunCommandText("เลือกทำงาน - ป้อนสิบใบเท่านั้น ใช้กล้องด้วย")
            Case frmHome.RunCommmand.SlowStartNoCamera : frmHome.RunCommandText("เลือกทำงาน - ป้อนกี่ใบก็ได้ ไม่มีกล้อง")
            Case frmHome.RunCommmand.SlowStartWithCamera : frmHome.RunCommandText("เลือกทำงาน - ป้อนกี่ใบก็ได้ ใช้กล้องด้วย")
        End Select

        '--------------------------------------------------------'
        ' เปลี่ยนสถานะให้ Robocontrol ทำงานได้ต่อไป
        '--------------------------------------------------------'
        If RoboticControl.IsSUDDEN_STOP Then
            RoboticControl.IsEnable = True
        End If

    End Sub

    Sub DoCheckParameters()

        Dim nStatus(0 To 7) As Integer

        With frmSystemInfo
            RoboticControl.CheckAxis(1, nStatus(0), .lblLogicPos1, .lblRealPos1, .lblRunningSpeed1, .lblLimitPlus1, .lblLimitMinus1, .lblStop0_1, .lblStop1_1)
            RoboticControl.CheckAxis(2, nStatus(1), .lblLogicPos2, .lblRealPos2, .lblRunningSpeed2, .lblLimitPlus2, .lblLimitMinus2, .lblStop0_2, .lblStop1_2)
            RoboticControl.CheckAxis(3, nStatus(2), .lblLogicPos3, .lblRealPos3, .lblRunningSpeed3, .lblLimitPlus3, .lblLimitMinus3, .lblStop0_3, .lblStop1_3)
            RoboticControl.CheckAxis(4, nStatus(3), .lblLogicPos4, .lblRealPos4, .lblRunningSpeed4, .lblLimitPlus4, .lblLimitMinus4, .lblStop0_4, .lblStop1_4)
            RoboticControl.CheckAxis(5, nStatus(4), .lblLogicPos5, .lblRealPos5, .lblRunningSpeed5, .lblLimitPlus5, .lblLimitMinus5, .lblStop0_5, .lblStop1_5)
        End With
        'If nStatus(0) = 0 And nStatus(1) = 0 And nStatus(2) = 0 And nStatus(3) = 0 And nStatus(4) = 0 And nStatus(5) = 0 Then
        '    Me.LunchMenu()
        'Else
        '    Me.FreezMenu()
        'End If

    End Sub

#End Region

#Region "Show from"

    Public Sub ShowForm(ByVal frm As Form, ByVal pn As Panel)
        frm.Text = String.Empty
        frm.ControlBox = False
        frm.TopLevel = False
        frm.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        frm.Size = New Size(pn.ClientSize.Width, pn.ClientSize.Height)
        frm.Location = New Point(0, 0)
        pn.Controls.Add(frm)
        frm.Show()
        frm.BringToFront()
    End Sub

#End Region

End Class
