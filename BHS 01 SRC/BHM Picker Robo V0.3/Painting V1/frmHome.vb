Imports ADTMotionControl
Public Class frmHome

#Region "UI"

    Private Delegate Sub VoidDelegate()

    Public Sub PostText(ByVal text As String)
        If Me.InvokeRequired Then
            Me.Invoke(New VoidDelegate(Sub()
                                           If text = "" Then
                                               lblStatus.Text = "-"
                                           Else
                                               lblStatus.Text = text
                                           End If
                                       End Sub))
        Else
            If text = "" Then
                lblStatus.Text = "-"
            Else
                lblStatus.Text = text
            End If
        End If
    End Sub

    Public Sub PostProcessText(ByVal text As String)
        If Me.InvokeRequired Then
            Me.Invoke(New VoidDelegate(Sub()
                                           If text = "" Then
                                               lbProcess.Text = "-"
                                           Else
                                               lbProcess.Text = text
                                           End If
                                       End Sub))
        Else
            If text = "" Then
                lbProcess.Text = "-"
            Else
                lbProcess.Text = text
            End If
        End If
    End Sub

    Public Sub RunCommandText(ByVal text As String)
        If lblCurrentRunCommand.InvokeRequired Then
            lblCurrentRunCommand.Invoke(New VoidDelegate(Sub()
                                                             If text = "" Then
                                                                 lblCurrentRunCommand.Text = "-"
                                                             Else
                                                                 lblCurrentRunCommand.Text = text
                                                             End If
                                                         End Sub))
        Else
            If text = "" Then
                lblCurrentRunCommand.Text = "-"
            Else
                lblCurrentRunCommand.Text = text
            End If
        End If
    End Sub

    Public Sub FreezUI()
        If Me.InvokeRequired Then
            Me.Invoke(New VoidDelegate(Sub()
                                           GroupBox1.Enabled = False
                                           btnClearPosition.Enabled = False
                                           TabControl1.Enabled = False
                                       End Sub))
        Else
            GroupBox1.Enabled = False
            btnClearPosition.Enabled = False
            TabControl1.Enabled = False
        End If
    End Sub

    Public Sub LunchUI()
        If Me.InvokeRequired Then
            Me.Invoke(New VoidDelegate(Sub()
                                           GroupBox1.Enabled = True
                                           btnClearPosition.Enabled = True
                                           TabControl1.Enabled = True
                                       End Sub))
        Else
            GroupBox1.Enabled = True
            btnClearPosition.Enabled = True
            TabControl1.Enabled = True
        End If
    End Sub

#End Region

#Region "Home 1-5"

    Private Sub btn1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn1.Click
        DoHomeLift()
    End Sub

    Private Sub btn2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn2.Click
        DoHomeARM()
    End Sub

    Private Sub btn3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn3.Click
        DoHomeHAND()
    End Sub

    Private Sub btn4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn4.Click
        DoHomeTURN()
    End Sub

#End Region

#Region "Home control"

    Public Sub DoHomeLift()

        lblStatus.Text = "Lift Refering to Zero..."
        frmMain.FreezMenu()
        Me.FreezUI()

        RoboticControl.Home1()

        'MsgBox ("X Homing has completed")
        lblStatus.Text = "Lift Referenced to Zero"
        Me.LunchUI()
        frmMain.LunchMenu()

    End Sub

    Public Sub DoHomeARM()

        lblStatus.Text = "Lift Reference to Zero"
        frmMain.FreezMenu()
        Me.FreezUI()

        RoboticControl.Home2()

        'MsgBox ("Y Homing has completed")
        lblStatus.Text = "Lift Reference to Zero"
        Me.LunchUI()
        frmMain.LunchMenu()

    End Sub

    Public Sub DoHomeHAND()

        lblStatus.Text = "Lift Reference to Zero"
        frmMain.FreezMenu()
        Me.FreezUI()

        RoboticControl.Home3()

        'MsgBox ("Z Homing has completed")
        lblStatus.Text = "Lift Reference to Zero"
        Me.LunchUI()
        frmMain.LunchMenu()

    End Sub

    Public Sub DoHomeTURN()

        lblStatus.Text = "Lift Reference to Zero"
        frmMain.FreezMenu()
        Me.FreezUI()

        RoboticControl.Home4()

        'MsgBox ("A Homing has completed")
        lblStatus.Text = "Lift Reference to Zero"
        Me.LunchUI()
        frmMain.LunchMenu()

    End Sub

#End Region

#Region "Home All"

    Private Sub btnAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' Home แขนพ่น
        RoboticControl.IsSUDDEN_STOP = False
        RoboticControl.IsSUDDEN_STOP = False

        RoboticControl.Home1()
        If RoboticControl.IsSUDDEN_STOP Then
            RoboticControl.IsSUDDEN_STOP = False
            Exit Sub
        End If
        RoboticControl.Home2()
        If RoboticControl.IsSUDDEN_STOP Then
            RoboticControl.IsSUDDEN_STOP = False
            Exit Sub
        End If
        RoboticControl.Home3()
        If RoboticControl.IsSUDDEN_STOP Then
            RoboticControl.IsSUDDEN_STOP = False
            Exit Sub
        End If
        RoboticControl.Home4()
        If RoboticControl.IsSUDDEN_STOP Then
            RoboticControl.IsSUDDEN_STOP = False
            Exit Sub
        End If

        RoboticControl.ClearAllPosition()

        RoboticControl.ShowAlert("Home แขนพ่น เสร็จแล้ว", 1500)

    End Sub

#End Region

#Region "Sudden Stop"

    Private Sub btnSuddenStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        StopLib.SuddenStopAll()

        Me.Cursor = Cursors.Default
        lblStatus.Text = "All axes have been stopped."
        Me.LunchUI()
    End Sub

#End Region

#Region "Clear position"

    Private Sub btnClearPosition_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearPosition.Click
        Dim ClearPosition As New dlgMsgYESNO
        ClearPosition.rtbText.Text = "Do you want to clear positions?"
        If ClearPosition.ShowDialog = Windows.Forms.DialogResult.Yes Then
            RoboticControl.ClearAllPosition()
        End If

    End Sub

#End Region

#Region "Free A/B"

    'Private Sub chkFreeA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim dlg As dlgMessage
    '    dlg = New dlgMessage(1000)
    '    If chkFreeA.Checked Then
    '        CtrlCard.adt8940a1_Write_Output(RoboticControl.FreeNo.A_Free, 1)
    '        dlg.rtbText.Text = "ปลดล็อคแกน A แล้ว"
    '    Else
    '        CtrlCard.adt8940a1_Write_Output(RoboticControl.FreeNo.A_Free, 0)
    '        dlg.rtbText.Text = "ล็อคแกน A แล้ว"
    '    End If
    '    dlg.ShowDialog()
    '    dlg.Dispose()
    '    dlg = Nothing
    'End Sub

    'Private Sub chkFreeB_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim dlg As dlgMessage
    '    dlg = New dlgMessage(1000)
    '    If chkFreeB.Checked Then
    '        CtrlCard.adt8940a1_Write_Output(RoboticControl.FreeNo.B_Free, 1)
    '        dlg.rtbText.Text = "ปลดล็อคแกน B แล้ว"
    '    Else
    '        CtrlCard.adt8940a1_Write_Output(RoboticControl.FreeNo.B_Free, 0)
    '        dlg.rtbText.Text = "ล็อคแกน B แล้ว"
    '    End If
    '    dlg.ShowDialog()
    '    dlg.Dispose()
    '    dlg = Nothing
    'End Sub

#End Region

#Region "Move to home position"

    Private Sub btnTGotoHome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTGotoHome.Click
        HomeLib.MoveToHomeTraverse()
        RoboticControl.ShowAlert("Goto home ของแกน T เสร็จแล้ว", 1500)
    End Sub

    Private Sub btnKGotoHome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKGotoHome.Click
        HomeLib.MoveToHomeKick()
        RoboticControl.ShowAlert("Goto home ของแกน K เสร็จแล้ว", 1500)
    End Sub

    Private Sub btnVGotoHome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVGotoHome.Click
        HomeLib.MoveToHomeVertical()
        RoboticControl.ShowAlert("Goto home ของแกน V เสร็จแล้ว", 1500)
    End Sub

    Private Sub btnTwistingGotoHome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTwistingGotoHome.Click
        HomeLib.MoveToHomeTwist()
        RoboticControl.ShowAlert("Goto home ของแกน Twisting เสร็จแล้ว", 1500)
    End Sub

    Private Sub btnBendingGotoHome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnAllGotoHome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllGotoHome.Click
        HomeLib.MoveToHomeAll_Arm_BlowGun()
        RoboticControl.ShowAlert("Goto home ของทุกแกน เสร็จแล้ว", 1500)
    End Sub

    Private Sub btnTAGotoHome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnR11GotoHome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnR12GotoHome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

#End Region


    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'If CtrlCard.adt8940a1_Read_Input(24) = 1 Then
        '    CtrlCard.adt8940a1_Write_Output(1, 1)
        'Else '
        '    CtrlCard.adt8940a1_Write_Output(1, 0)
        'End If
        CtrlCard.adt8940a1_Write_Output(My.Settings.Output_Hand, 1)
    End Sub

    Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.FreezUI()
        If MsgBox("ต้องการรีเซตตู้ควบคุมใช่หรือไม่", MsgBoxStyle.YesNo, "รีเซต") = MsgBoxResult.Yes Then
            Button3.Enabled = False
            frmMain.ControlPannelFlag = False
            RoboticControl.ResetControlBox()
            frmMain.ControlPannelFlag = True
            Button3.Enabled = True
        End If
        Me.LunchUI()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        RoboticControl.procKick()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        CtrlCard.adt8940a1_Write_Output(My.Settings.Output_Hand, 0)
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        RoboticControl.GetUI()
        RoboticControl.FirstAdjust()
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        RoboticControl.tTurnPostive = New System.Threading.Thread(AddressOf RoboticControl.procTurnPositive)
        RoboticControl.tTurnPostive.Start()
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        RoboticControl.tTurnNegative = New System.Threading.Thread(AddressOf RoboticControl.procTurnNegative)
        RoboticControl.tTurnNegative.Start()
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        RoboticControl.tReload = New System.Threading.Thread(AddressOf RoboticControl.procReload)
        RoboticControl.tReload.Start()
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        RoboticControl.tTestReload = New System.Threading.Thread(AddressOf RoboticControl.procTestReload)
        RoboticControl.tTestReload.Start()
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        RoboticControl.tClearReload = New System.Threading.Thread(AddressOf RoboticControl.procClearReload)
        RoboticControl.tClearReload.Start()
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        RoboticControl.procSTEPMoveNEG()
    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        RoboticControl.procSTEPMovePOS()
    End Sub

    Public Sub btnSlowStartWithCamera_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSlowStartWithCamera.Click
        If RoboticControl.IsEnable Then
            Me.FreezUI()
            RoboticControl.GetUI()
            Dim tSlowCycle As New Threading.Thread(AddressOf RoboticControl.PICK_Slow_Camera)
            tSlowCycle.Start()
            RoboticControl.IsEnable = False
            timerRoboticEnable.Start()
            mCurrentRunCommand = RunCommmand.SlowStartWithCamera
        End If
    End Sub

    Public Sub btnSlowStartNoCamera_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSlowStartNoCamera.Click
        If RoboticControl.IsEnable Then
            Me.FreezUI()
            RoboticControl.GetUI()
            Dim tFASTCycle As New System.Threading.Thread(AddressOf RoboticControl.PICK_SLOW_NO_CAMERA)
            tFASTCycle.Start()
            RoboticControl.IsEnable = False
            timerRoboticEnable.Start()
            mCurrentRunCommand = RunCommmand.SlowStartNoCamera
        End If
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        txtArmACC.Text = My.Settings.Arm_ACC
        txtArmSpeed.Text = My.Settings.Arm_Speed
        txtFirstAdjust.Text = My.Settings.Lift_10
        txtLiftUP.Text = My.Settings.Lift_up
        txtUI_LiftDown.Text = My.Settings.LIFT_Down
        txtLiftSpeed.Text = My.Settings.LIFT_SPEED
        txtUILiftDownSpeed.Text = My.Settings.LIFT_DOWN_SPEED
        txtLiftAcc1.Text = My.Settings.LIFT_ACC1
        txtLiftAcc2.Text = My.Settings.LIFT_ACC2
        txtUIArmRange.Text = My.Settings.ARM_RANGE
        txtUIHandOFFDelay.Text = My.Settings.Time_HandOFF
        txtUIArmToCam.Text = My.Settings.ARM_TO_CAM_RANGE
        txtHandToCam.Text = My.Settings.HAND_TO_CAM_RANGE
    End Sub

    Private Sub chkNG_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNG.CheckedChanged
        RoboticControl.isNG = chkNG.Checked
    End Sub

    Enum RunCommmand
        NONE
        FastStartNoCamera
        FastStartWithCamera
        SlowStartNoCamera
        SlowStartWithCamera
    End Enum

    Private mCurrentRunCommand As RunCommmand = RunCommmand.SlowStartNoCamera
    Public Property CurrentRunCommand As RunCommmand
        Get
            Return mCurrentRunCommand
        End Get
        Set(ByVal value As RunCommmand)
            mCurrentRunCommand = value
        End Set
    End Property

    Public Sub btnFastStartNoCamera_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFastStartNoCamera.Click
        If RoboticControl.IsEnable Then
            btnFastStartNoCamera.Enabled = False
            Me.FreezUI()
            RoboticControl.GetUI()
            Dim tFASTCycle As New System.Threading.Thread(AddressOf RoboticControl.PICK_FAST_NewLiftMove)
            tFASTCycle.Start()
            btnFastStartNoCamera.Enabled = True
            RoboticControl.IsEnable = False
            timerRoboticEnable.Start()
            mCurrentRunCommand = RunCommmand.FastStartNoCamera
        End If
    End Sub

    Private Sub timerRoboticEnable_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerRoboticEnable.Tick
        If RoboticControl.IsEnable Then
            'mCurrentRunCommand = RunCommmand.NONE
            timerRoboticEnable.Enabled = False
            'MsgBox("Cycle is ended.", MsgBoxStyle.Information, "Robotic")
            Me.LunchUI()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUIArmToCam.TextChanged

    End Sub

    Private Sub frmHome_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Label12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label12.Click

    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtHandToCam.TextChanged

    End Sub

    Public Sub btnFastStartWithCamera_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFastStartWithCamera.Click
        If RoboticControl.IsEnable Then
            btnFastStartNoCamera.Enabled = False
            Me.FreezUI()
            RoboticControl.GetUI()
            Dim tFASTCycle As New System.Threading.Thread(AddressOf RoboticControl.PICK_FAST_Camera)
            tFASTCycle.Start()
            btnFastStartNoCamera.Enabled = True
            RoboticControl.IsEnable = False
            timerRoboticEnable.Start()
            mCurrentRunCommand = RunCommmand.FastStartWithCamera
        End If
    End Sub

    Private Sub btnVision_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVision.Click
        RoboticControl.TestVision()
    End Sub

    Private Sub btnAllReference_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllReference.Click
        btnAllReference.Enabled = False
        DoHomeLift()
        DoHomeARM()
        DoHomeHAND()
        DoHomeTURN()
        btnAllReference.Enabled = True
    End Sub

End Class