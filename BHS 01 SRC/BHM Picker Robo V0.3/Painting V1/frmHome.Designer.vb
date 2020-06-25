<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHome
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnAllReference = New System.Windows.Forms.Button()
        Me.btn4 = New System.Windows.Forms.Button()
        Me.btn3 = New System.Windows.Forms.Button()
        Me.btn2 = New System.Windows.Forms.Button()
        Me.btn1 = New System.Windows.Forms.Button()
        Me.btnClearPosition = New System.Windows.Forms.Button()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnAllGotoHome = New System.Windows.Forms.Button()
        Me.btnTwistingGotoHome = New System.Windows.Forms.Button()
        Me.btnVGotoHome = New System.Windows.Forms.Button()
        Me.btnKGotoHome = New System.Windows.Forms.Button()
        Me.btnTGotoHome = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.btnSlowStartWithCamera = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.Button10 = New System.Windows.Forms.Button()
        Me.Button11 = New System.Windows.Forms.Button()
        Me.Button12 = New System.Windows.Forms.Button()
        Me.txtFirstAdjust = New System.Windows.Forms.TextBox()
        Me.txtLiftUP = New System.Windows.Forms.TextBox()
        Me.Button14 = New System.Windows.Forms.Button()
        Me.Button15 = New System.Windows.Forms.Button()
        Me.txtIncrementACC = New System.Windows.Forms.TextBox()
        Me.txtIncrementSpeed = New System.Windows.Forms.TextBox()
        Me.txtIncrmentDistance = New System.Windows.Forms.TextBox()
        Me.numAxis = New System.Windows.Forms.NumericUpDown()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.btnFastStartWithCamera = New System.Windows.Forms.Button()
        Me.btnFastStartNoCamera = New System.Windows.Forms.Button()
        Me.lbProcess = New System.Windows.Forms.Label()
        Me.btnSlowStartNoCamera = New System.Windows.Forms.Button()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.btnVision = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.chkEmptyPicking = New System.Windows.Forms.CheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtUILiftDownSpeed = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtUIArmRange = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtLiftAcc1 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtLiftAcc2 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtLiftSpeed = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtArmSpeed = New System.Windows.Forms.TextBox()
        Me.txtArmACC = New System.Windows.Forms.TextBox()
        Me.chkNG = New System.Windows.Forms.CheckBox()
        Me.timerRoboticEnable = New System.Windows.Forms.Timer(Me.components)
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtUIHandOFFDelay = New System.Windows.Forms.TextBox()
        Me.txtUIArmToCam = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtHandToCam = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtUI_LiftDown = New System.Windows.Forms.TextBox()
        Me.lblCurrentRunCommand = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.numAxis, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnAllReference)
        Me.GroupBox1.Controls.Add(Me.btn4)
        Me.GroupBox1.Controls.Add(Me.btn3)
        Me.GroupBox1.Controls.Add(Me.btn2)
        Me.GroupBox1.Controls.Add(Me.btn1)
        Me.GroupBox1.Controls.Add(Me.btnClearPosition)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(224, 181)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Homing Control"
        '
        'btnAllReference
        '
        Me.btnAllReference.Location = New System.Drawing.Point(119, 64)
        Me.btnAllReference.Name = "btnAllReference"
        Me.btnAllReference.Size = New System.Drawing.Size(94, 105)
        Me.btnAllReference.TabIndex = 61
        Me.btnAllReference.Text = "All Reference"
        Me.btnAllReference.UseVisualStyleBackColor = True
        '
        'btn4
        '
        Me.btn4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn4.Location = New System.Drawing.Point(19, 132)
        Me.btn4.Name = "btn4"
        Me.btn4.Size = New System.Drawing.Size(94, 37)
        Me.btn4.TabIndex = 3
        Me.btn4.Text = "Reference TURN"
        Me.btn4.UseVisualStyleBackColor = True
        '
        'btn3
        '
        Me.btn3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn3.Location = New System.Drawing.Point(19, 95)
        Me.btn3.Name = "btn3"
        Me.btn3.Size = New System.Drawing.Size(94, 37)
        Me.btn3.TabIndex = 2
        Me.btn3.Text = "Reference HAND"
        Me.btn3.UseVisualStyleBackColor = True
        '
        'btn2
        '
        Me.btn2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn2.Location = New System.Drawing.Point(19, 58)
        Me.btn2.Name = "btn2"
        Me.btn2.Size = New System.Drawing.Size(94, 37)
        Me.btn2.TabIndex = 1
        Me.btn2.Text = "Reference ARM"
        Me.btn2.UseVisualStyleBackColor = True
        '
        'btn1
        '
        Me.btn1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn1.Location = New System.Drawing.Point(19, 21)
        Me.btn1.Name = "btn1"
        Me.btn1.Size = New System.Drawing.Size(94, 37)
        Me.btn1.TabIndex = 0
        Me.btn1.Text = "Reference LIFT"
        Me.btn1.UseVisualStyleBackColor = True
        '
        'btnClearPosition
        '
        Me.btnClearPosition.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClearPosition.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearPosition.ForeColor = System.Drawing.Color.White
        Me.btnClearPosition.Location = New System.Drawing.Point(119, 19)
        Me.btnClearPosition.Name = "btnClearPosition"
        Me.btnClearPosition.Size = New System.Drawing.Size(94, 41)
        Me.btnClearPosition.TabIndex = 6
        Me.btnClearPosition.Text = "Clear position"
        Me.btnClearPosition.UseVisualStyleBackColor = False
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.ForeColor = System.Drawing.Color.Maroon
        Me.lblStatus.Location = New System.Drawing.Point(28, 249)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(0, 13)
        Me.lblStatus.TabIndex = 5
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnAllGotoHome)
        Me.GroupBox2.Controls.Add(Me.btnTwistingGotoHome)
        Me.GroupBox2.Controls.Add(Me.btnVGotoHome)
        Me.GroupBox2.Controls.Add(Me.btnKGotoHome)
        Me.GroupBox2.Controls.Add(Me.btnTGotoHome)
        Me.GroupBox2.Location = New System.Drawing.Point(242, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(213, 181)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Goto Home"
        '
        'btnAllGotoHome
        '
        Me.btnAllGotoHome.BackColor = System.Drawing.Color.Salmon
        Me.btnAllGotoHome.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAllGotoHome.ForeColor = System.Drawing.Color.White
        Me.btnAllGotoHome.Location = New System.Drawing.Point(106, 22)
        Me.btnAllGotoHome.Name = "btnAllGotoHome"
        Me.btnAllGotoHome.Size = New System.Drawing.Size(94, 80)
        Me.btnAllGotoHome.TabIndex = 8
        Me.btnAllGotoHome.Text = "GOTO HOME " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "ALL"
        Me.btnAllGotoHome.UseVisualStyleBackColor = False
        '
        'btnTwistingGotoHome
        '
        Me.btnTwistingGotoHome.BackColor = System.Drawing.Color.MediumSeaGreen
        Me.btnTwistingGotoHome.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTwistingGotoHome.ForeColor = System.Drawing.Color.White
        Me.btnTwistingGotoHome.Location = New System.Drawing.Point(6, 133)
        Me.btnTwistingGotoHome.Name = "btnTwistingGotoHome"
        Me.btnTwistingGotoHome.Size = New System.Drawing.Size(94, 37)
        Me.btnTwistingGotoHome.TabIndex = 8
        Me.btnTwistingGotoHome.Text = "TURN goto ZERO"
        Me.btnTwistingGotoHome.UseVisualStyleBackColor = False
        '
        'btnVGotoHome
        '
        Me.btnVGotoHome.BackColor = System.Drawing.Color.MediumSeaGreen
        Me.btnVGotoHome.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnVGotoHome.ForeColor = System.Drawing.Color.White
        Me.btnVGotoHome.Location = New System.Drawing.Point(6, 96)
        Me.btnVGotoHome.Name = "btnVGotoHome"
        Me.btnVGotoHome.Size = New System.Drawing.Size(94, 37)
        Me.btnVGotoHome.TabIndex = 7
        Me.btnVGotoHome.Text = "HAND goto ZERO"
        Me.btnVGotoHome.UseVisualStyleBackColor = False
        '
        'btnKGotoHome
        '
        Me.btnKGotoHome.BackColor = System.Drawing.Color.MediumSeaGreen
        Me.btnKGotoHome.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnKGotoHome.ForeColor = System.Drawing.Color.White
        Me.btnKGotoHome.Location = New System.Drawing.Point(6, 59)
        Me.btnKGotoHome.Name = "btnKGotoHome"
        Me.btnKGotoHome.Size = New System.Drawing.Size(94, 37)
        Me.btnKGotoHome.TabIndex = 6
        Me.btnKGotoHome.Text = "ARM goto ZERO"
        Me.btnKGotoHome.UseVisualStyleBackColor = False
        '
        'btnTGotoHome
        '
        Me.btnTGotoHome.BackColor = System.Drawing.Color.MediumSeaGreen
        Me.btnTGotoHome.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTGotoHome.ForeColor = System.Drawing.Color.White
        Me.btnTGotoHome.Location = New System.Drawing.Point(6, 22)
        Me.btnTGotoHome.Name = "btnTGotoHome"
        Me.btnTGotoHome.Size = New System.Drawing.Size(94, 37)
        Me.btnTGotoHome.TabIndex = 5
        Me.btnTGotoHome.Text = "LIFT goto ZERO"
        Me.btnTGotoHome.UseVisualStyleBackColor = False
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(92, 81)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(76, 30)
        Me.Button6.TabIndex = 14
        Me.Button6.Text = "Hand Off"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(10, 81)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(76, 30)
        Me.Button2.TabIndex = 13
        Me.Button2.Text = "Hand ON"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'btnSlowStartWithCamera
        '
        Me.btnSlowStartWithCamera.Location = New System.Drawing.Point(6, 211)
        Me.btnSlowStartWithCamera.Name = "btnSlowStartWithCamera"
        Me.btnSlowStartWithCamera.Size = New System.Drawing.Size(100, 48)
        Me.btnSlowStartWithCamera.TabIndex = 18
        Me.btnSlowStartWithCamera.Text = "SLOW - START" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "CAMERA"
        Me.btnSlowStartWithCamera.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.Yellow
        Me.Button3.Location = New System.Drawing.Point(12, 195)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(224, 41)
        Me.Button3.TabIndex = 19
        Me.Button3.Text = "ALARM CLEAR"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(10, 153)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(76, 30)
        Me.Button5.TabIndex = 21
        Me.Button5.Text = "KICK"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(10, 45)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(76, 30)
        Me.Button7.TabIndex = 22
        Me.Button7.Text = "First Adjust"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.Location = New System.Drawing.Point(10, 117)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(76, 30)
        Me.Button8.TabIndex = 23
        Me.Button8.Text = "Turn+"
        Me.Button8.UseVisualStyleBackColor = True
        '
        'Button9
        '
        Me.Button9.Location = New System.Drawing.Point(92, 117)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(76, 30)
        Me.Button9.TabIndex = 24
        Me.Button9.Text = "Turn-"
        Me.Button9.UseVisualStyleBackColor = True
        '
        'Button10
        '
        Me.Button10.Location = New System.Drawing.Point(10, 9)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(76, 30)
        Me.Button10.TabIndex = 25
        Me.Button10.Text = "RELOAD"
        Me.Button10.UseVisualStyleBackColor = True
        '
        'Button11
        '
        Me.Button11.Location = New System.Drawing.Point(92, 9)
        Me.Button11.Name = "Button11"
        Me.Button11.Size = New System.Drawing.Size(76, 30)
        Me.Button11.TabIndex = 26
        Me.Button11.Text = "Test Relad"
        Me.Button11.UseVisualStyleBackColor = True
        '
        'Button12
        '
        Me.Button12.Location = New System.Drawing.Point(174, 9)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(76, 30)
        Me.Button12.TabIndex = 27
        Me.Button12.Text = "Clear Reload"
        Me.Button12.UseVisualStyleBackColor = True
        '
        'txtFirstAdjust
        '
        Me.txtFirstAdjust.Location = New System.Drawing.Point(808, 35)
        Me.txtFirstAdjust.Name = "txtFirstAdjust"
        Me.txtFirstAdjust.Size = New System.Drawing.Size(89, 20)
        Me.txtFirstAdjust.TabIndex = 0
        Me.txtFirstAdjust.Text = "103170"
        '
        'txtLiftUP
        '
        Me.txtLiftUP.Location = New System.Drawing.Point(808, 61)
        Me.txtLiftUP.Name = "txtLiftUP"
        Me.txtLiftUP.Size = New System.Drawing.Size(89, 20)
        Me.txtLiftUP.TabIndex = 1
        Me.txtLiftUP.Text = "115675"
        '
        'Button14
        '
        Me.Button14.Location = New System.Drawing.Point(145, 8)
        Me.Button14.Name = "Button14"
        Me.Button14.Size = New System.Drawing.Size(75, 32)
        Me.Button14.TabIndex = 31
        Me.Button14.Text = "MOVE +++"
        Me.Button14.UseVisualStyleBackColor = True
        '
        'Button15
        '
        Me.Button15.Location = New System.Drawing.Point(64, 8)
        Me.Button15.Name = "Button15"
        Me.Button15.Size = New System.Drawing.Size(75, 32)
        Me.Button15.TabIndex = 32
        Me.Button15.Text = "MOVE ----"
        Me.Button15.UseVisualStyleBackColor = True
        '
        'txtIncrementACC
        '
        Me.txtIncrementACC.Location = New System.Drawing.Point(70, 50)
        Me.txtIncrementACC.Name = "txtIncrementACC"
        Me.txtIncrementACC.Size = New System.Drawing.Size(57, 20)
        Me.txtIncrementACC.TabIndex = 33
        Me.txtIncrementACC.Text = "3000"
        '
        'txtIncrementSpeed
        '
        Me.txtIncrementSpeed.Location = New System.Drawing.Point(133, 50)
        Me.txtIncrementSpeed.Name = "txtIncrementSpeed"
        Me.txtIncrementSpeed.Size = New System.Drawing.Size(80, 20)
        Me.txtIncrementSpeed.TabIndex = 34
        Me.txtIncrementSpeed.Text = "150000"
        '
        'txtIncrmentDistance
        '
        Me.txtIncrmentDistance.Location = New System.Drawing.Point(70, 84)
        Me.txtIncrmentDistance.Name = "txtIncrmentDistance"
        Me.txtIncrmentDistance.Size = New System.Drawing.Size(80, 20)
        Me.txtIncrmentDistance.TabIndex = 35
        Me.txtIncrmentDistance.Text = "50000"
        '
        'numAxis
        '
        Me.numAxis.Location = New System.Drawing.Point(6, 16)
        Me.numAxis.Maximum = New Decimal(New Integer() {4, 0, 0, 0})
        Me.numAxis.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numAxis.Name = "numAxis"
        Me.numAxis.Size = New System.Drawing.Size(52, 20)
        Me.numAxis.TabIndex = 36
        Me.numAxis.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(461, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(249, 328)
        Me.TabControl1.TabIndex = 37
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Label17)
        Me.TabPage3.Controls.Add(Me.Label16)
        Me.TabPage3.Controls.Add(Me.Label15)
        Me.TabPage3.Controls.Add(Me.Label14)
        Me.TabPage3.Controls.Add(Me.btnFastStartWithCamera)
        Me.TabPage3.Controls.Add(Me.btnFastStartNoCamera)
        Me.TabPage3.Controls.Add(Me.lbProcess)
        Me.TabPage3.Controls.Add(Me.btnSlowStartWithCamera)
        Me.TabPage3.Controls.Add(Me.btnSlowStartNoCamera)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(241, 302)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "CYCLE"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(112, 72)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(74, 26)
        Me.Label17.TabIndex = 63
        Me.Label17.Text = "ป้อนกี่ใบก็ได้ " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "แล้วยกผ่านไป"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(112, 173)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(66, 26)
        Me.Label16.TabIndex = 62
        Me.Label16.Text = "ป้อน 10 ใบ " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "แล้วถ่ายภาพ"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(112, 222)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(71, 26)
        Me.Label15.TabIndex = 61
        Me.Label15.Text = "ป้อนกี่ใบก็ได้ " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "แล้วถ่ายภาพ"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(112, 24)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(74, 26)
        Me.Label14.TabIndex = 60
        Me.Label14.Text = "ป้อน 10 ใบ " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "แล้วยกผ่านไป"
        '
        'btnFastStartWithCamera
        '
        Me.btnFastStartWithCamera.Location = New System.Drawing.Point(6, 162)
        Me.btnFastStartWithCamera.Name = "btnFastStartWithCamera"
        Me.btnFastStartWithCamera.Size = New System.Drawing.Size(100, 42)
        Me.btnFastStartWithCamera.TabIndex = 59
        Me.btnFastStartWithCamera.Text = "FAST - START" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "CAMERA"
        Me.btnFastStartWithCamera.UseVisualStyleBackColor = True
        '
        'btnFastStartNoCamera
        '
        Me.btnFastStartNoCamera.Location = New System.Drawing.Point(6, 15)
        Me.btnFastStartNoCamera.Name = "btnFastStartNoCamera"
        Me.btnFastStartNoCamera.Size = New System.Drawing.Size(100, 42)
        Me.btnFastStartNoCamera.TabIndex = 34
        Me.btnFastStartNoCamera.Text = "FAST -START"
        Me.btnFastStartNoCamera.UseVisualStyleBackColor = True
        '
        'lbProcess
        '
        Me.lbProcess.AutoSize = True
        Me.lbProcess.Location = New System.Drawing.Point(13, 118)
        Me.lbProcess.Name = "lbProcess"
        Me.lbProcess.Size = New System.Drawing.Size(10, 13)
        Me.lbProcess.TabIndex = 31
        Me.lbProcess.Text = "-"
        '
        'btnSlowStartNoCamera
        '
        Me.btnSlowStartNoCamera.Location = New System.Drawing.Point(6, 63)
        Me.btnSlowStartNoCamera.Name = "btnSlowStartNoCamera"
        Me.btnSlowStartNoCamera.Size = New System.Drawing.Size(100, 48)
        Me.btnSlowStartNoCamera.TabIndex = 30
        Me.btnSlowStartNoCamera.Text = "Slow - START"
        Me.btnSlowStartNoCamera.UseVisualStyleBackColor = True
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.btnVision)
        Me.TabPage1.Controls.Add(Me.Button7)
        Me.TabPage1.Controls.Add(Me.Button6)
        Me.TabPage1.Controls.Add(Me.Button8)
        Me.TabPage1.Controls.Add(Me.Button5)
        Me.TabPage1.Controls.Add(Me.Button9)
        Me.TabPage1.Controls.Add(Me.Button2)
        Me.TabPage1.Controls.Add(Me.Button10)
        Me.TabPage1.Controls.Add(Me.Button11)
        Me.TabPage1.Controls.Add(Me.Button12)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(241, 302)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TEST"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'btnVision
        '
        Me.btnVision.Location = New System.Drawing.Point(10, 187)
        Me.btnVision.Name = "btnVision"
        Me.btnVision.Size = New System.Drawing.Size(76, 30)
        Me.btnVision.TabIndex = 28
        Me.btnVision.Text = "VISION"
        Me.btnVision.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.numAxis)
        Me.TabPage2.Controls.Add(Me.Button14)
        Me.TabPage2.Controls.Add(Me.txtIncrmentDistance)
        Me.TabPage2.Controls.Add(Me.Button15)
        Me.TabPage2.Controls.Add(Me.txtIncrementSpeed)
        Me.TabPage2.Controls.Add(Me.txtIncrementACC)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(241, 302)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "STEP MOVE"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'chkEmptyPicking
        '
        Me.chkEmptyPicking.AutoSize = True
        Me.chkEmptyPicking.Location = New System.Drawing.Point(348, 244)
        Me.chkEmptyPicking.Name = "chkEmptyPicking"
        Me.chkEmptyPicking.Size = New System.Drawing.Size(93, 17)
        Me.chkEmptyPicking.TabIndex = 53
        Me.chkEmptyPicking.Text = "Empty Picking"
        Me.chkEmptyPicking.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(716, 142)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(86, 13)
        Me.Label9.TabIndex = 51
        Me.Label9.Text = "Lift Down Speed"
        '
        'txtUILiftDownSpeed
        '
        Me.txtUILiftDownSpeed.Location = New System.Drawing.Point(808, 139)
        Me.txtUILiftDownSpeed.Name = "txtUILiftDownSpeed"
        Me.txtUILiftDownSpeed.Size = New System.Drawing.Size(89, 20)
        Me.txtUILiftDownSpeed.TabIndex = 4
        Me.txtUILiftDownSpeed.Text = "300000"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(742, 323)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(60, 13)
        Me.Label7.TabIndex = 47
        Me.Label7.Text = "Arm Range"
        '
        'txtUIArmRange
        '
        Me.txtUIArmRange.Location = New System.Drawing.Point(808, 320)
        Me.txtUIArmRange.Name = "txtUIArmRange"
        Me.txtUIArmRange.Size = New System.Drawing.Size(89, 20)
        Me.txtUIArmRange.TabIndex = 11
        Me.txtUIArmRange.Text = "170"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(745, 38)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(58, 13)
        Me.Label5.TabIndex = 45
        Me.Label5.Text = "First Adjust"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(744, 168)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(58, 13)
        Me.Label6.TabIndex = 43
        Me.Label6.Text = "Lift Acc.  1"
        '
        'txtLiftAcc1
        '
        Me.txtLiftAcc1.Location = New System.Drawing.Point(808, 165)
        Me.txtLiftAcc1.Name = "txtLiftAcc1"
        Me.txtLiftAcc1.Size = New System.Drawing.Size(89, 20)
        Me.txtLiftAcc1.TabIndex = 5
        Me.txtLiftAcc1.Text = "6000"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(756, 194)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 13)
        Me.Label4.TabIndex = 40
        Me.Label4.Text = "Lift Acc."
        '
        'txtLiftAcc2
        '
        Me.txtLiftAcc2.Location = New System.Drawing.Point(808, 191)
        Me.txtLiftAcc2.Name = "txtLiftAcc2"
        Me.txtLiftAcc2.Size = New System.Drawing.Size(89, 20)
        Me.txtLiftAcc2.TabIndex = 6
        Me.txtLiftAcc2.Text = "6000"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(741, 116)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 13)
        Me.Label3.TabIndex = 38
        Me.Label3.Text = "Lift Speed"
        '
        'txtLiftSpeed
        '
        Me.txtLiftSpeed.Location = New System.Drawing.Point(808, 113)
        Me.txtLiftSpeed.Name = "txtLiftSpeed"
        Me.txtLiftSpeed.Size = New System.Drawing.Size(89, 20)
        Me.txtLiftSpeed.TabIndex = 3
        Me.txtLiftSpeed.Text = "300000"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(724, 245)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 13)
        Me.Label2.TabIndex = 36
        Me.Label2.Text = "Arm Acc./Dec."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(743, 220)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 35
        Me.Label1.Text = "Arm Speed"
        '
        'txtArmSpeed
        '
        Me.txtArmSpeed.Location = New System.Drawing.Point(808, 217)
        Me.txtArmSpeed.Name = "txtArmSpeed"
        Me.txtArmSpeed.Size = New System.Drawing.Size(89, 20)
        Me.txtArmSpeed.TabIndex = 7
        Me.txtArmSpeed.Text = "400000"
        '
        'txtArmACC
        '
        Me.txtArmACC.Location = New System.Drawing.Point(808, 242)
        Me.txtArmACC.Name = "txtArmACC"
        Me.txtArmACC.Size = New System.Drawing.Size(89, 20)
        Me.txtArmACC.TabIndex = 8
        Me.txtArmACC.Text = "6000"
        '
        'chkNG
        '
        Me.chkNG.AutoSize = True
        Me.chkNG.Location = New System.Drawing.Point(348, 221)
        Me.chkNG.Name = "chkNG"
        Me.chkNG.Size = New System.Drawing.Size(42, 17)
        Me.chkNG.TabIndex = 30
        Me.chkNG.Text = "NG"
        Me.chkNG.UseVisualStyleBackColor = True
        '
        'timerRoboticEnable
        '
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(770, 64)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(32, 13)
        Me.Label8.TabIndex = 52
        Me.Label8.Text = "Delta"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(716, 347)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(87, 13)
        Me.Label10.TabIndex = 54
        Me.Label10.Text = "Hand OFF delay:"
        '
        'txtUIHandOFFDelay
        '
        Me.txtUIHandOFFDelay.Location = New System.Drawing.Point(809, 347)
        Me.txtUIHandOFFDelay.Name = "txtUIHandOFFDelay"
        Me.txtUIHandOFFDelay.Size = New System.Drawing.Size(89, 20)
        Me.txtUIHandOFFDelay.TabIndex = 12
        Me.txtUIHandOFFDelay.Text = "200"
        '
        'txtUIArmToCam
        '
        Me.txtUIArmToCam.Location = New System.Drawing.Point(808, 268)
        Me.txtUIArmToCam.Name = "txtUIArmToCam"
        Me.txtUIArmToCam.Size = New System.Drawing.Size(89, 20)
        Me.txtUIArmToCam.TabIndex = 9
        Me.txtUIArmToCam.Text = "108"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(722, 271)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(80, 13)
        Me.Label11.TabIndex = 56
        Me.Label11.Text = "Arm To Camera"
        '
        'txtHandToCam
        '
        Me.txtHandToCam.Location = New System.Drawing.Point(808, 294)
        Me.txtHandToCam.Name = "txtHandToCam"
        Me.txtHandToCam.Size = New System.Drawing.Size(89, 20)
        Me.txtHandToCam.TabIndex = 10
        Me.txtHandToCam.Text = "200"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(714, 297)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(88, 13)
        Me.Label12.TabIndex = 58
        Me.Label12.Text = "Hand To Camera"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(750, 90)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(52, 13)
        Me.Label13.TabIndex = 60
        Me.Label13.Text = "Lift Down"
        '
        'txtUI_LiftDown
        '
        Me.txtUI_LiftDown.Location = New System.Drawing.Point(808, 87)
        Me.txtUI_LiftDown.Name = "txtUI_LiftDown"
        Me.txtUI_LiftDown.Size = New System.Drawing.Size(89, 20)
        Me.txtUI_LiftDown.TabIndex = 2
        Me.txtUI_LiftDown.Text = "120000"
        '
        'lblCurrentRunCommand
        '
        Me.lblCurrentRunCommand.AutoSize = True
        Me.lblCurrentRunCommand.Location = New System.Drawing.Point(472, 360)
        Me.lblCurrentRunCommand.Name = "lblCurrentRunCommand"
        Me.lblCurrentRunCommand.Size = New System.Drawing.Size(0, 13)
        Me.lblCurrentRunCommand.TabIndex = 61
        '
        'frmHome
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1019, 510)
        Me.Controls.Add(Me.lblCurrentRunCommand)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.chkEmptyPicking)
        Me.Controls.Add(Me.txtUI_LiftDown)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtHandToCam)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtUIArmToCam)
        Me.Controls.Add(Me.chkNG)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtUIHandOFFDelay)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.txtUILiftDownSpeed)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.txtUIArmRange)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtLiftUP)
        Me.Controls.Add(Me.txtLiftAcc1)
        Me.Controls.Add(Me.txtFirstAdjust)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtArmACC)
        Me.Controls.Add(Me.txtLiftAcc2)
        Me.Controls.Add(Me.txtArmSpeed)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtLiftSpeed)
        Me.Controls.Add(Me.Label2)
        Me.Name = "frmHome"
        Me.Text = "frmHome"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.numAxis, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btn4 As System.Windows.Forms.Button
    Friend WithEvents btn3 As System.Windows.Forms.Button
    Friend WithEvents btn2 As System.Windows.Forms.Button
    Friend WithEvents btn1 As System.Windows.Forms.Button
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents btnClearPosition As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnAllGotoHome As System.Windows.Forms.Button
    Friend WithEvents btnTwistingGotoHome As System.Windows.Forms.Button
    Friend WithEvents btnVGotoHome As System.Windows.Forms.Button
    Friend WithEvents btnKGotoHome As System.Windows.Forms.Button
    Friend WithEvents btnTGotoHome As System.Windows.Forms.Button
    Friend WithEvents btnSlowStartWithCamera As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents Button9 As System.Windows.Forms.Button
    Friend WithEvents Button10 As System.Windows.Forms.Button
    Friend WithEvents Button11 As System.Windows.Forms.Button
    Friend WithEvents Button12 As System.Windows.Forms.Button
    Friend WithEvents txtFirstAdjust As System.Windows.Forms.TextBox
    Friend WithEvents txtLiftUP As System.Windows.Forms.TextBox
    Friend WithEvents Button14 As System.Windows.Forms.Button
    Friend WithEvents Button15 As System.Windows.Forms.Button
    Friend WithEvents txtIncrementACC As System.Windows.Forms.TextBox
    Friend WithEvents txtIncrementSpeed As System.Windows.Forms.TextBox
    Friend WithEvents txtIncrmentDistance As System.Windows.Forms.TextBox
    Friend WithEvents numAxis As System.Windows.Forms.NumericUpDown
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents lbProcess As System.Windows.Forms.Label
    Friend WithEvents txtArmSpeed As System.Windows.Forms.TextBox
    Friend WithEvents txtArmACC As System.Windows.Forms.TextBox
    Friend WithEvents chkNG As System.Windows.Forms.CheckBox
    Friend WithEvents btnFastStartNoCamera As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtLiftAcc2 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtLiftSpeed As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtLiftAcc1 As System.Windows.Forms.TextBox
    Friend WithEvents timerRoboticEnable As System.Windows.Forms.Timer
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtUIArmRange As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtUILiftDownSpeed As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents chkEmptyPicking As System.Windows.Forms.CheckBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtUIHandOFFDelay As System.Windows.Forms.TextBox
    Friend WithEvents txtUIArmToCam As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtHandToCam As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btnFastStartWithCamera As System.Windows.Forms.Button
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtUI_LiftDown As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents btnVision As System.Windows.Forms.Button
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents btnSlowStartNoCamera As System.Windows.Forms.Button
    Friend WithEvents btnAllReference As System.Windows.Forms.Button
    Friend WithEvents lblCurrentRunCommand As System.Windows.Forms.Label
End Class
