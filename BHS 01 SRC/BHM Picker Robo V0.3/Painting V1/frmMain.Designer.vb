<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.btnTeaching = New System.Windows.Forms.Button()
        Me.btnHome = New System.Windows.Forms.Button()
        Me.pnMain = New System.Windows.Forms.Panel()
        Me.emgmassage = New System.Windows.Forms.Label()
        Me.btnQuit = New System.Windows.Forms.Button()
        Me.lblCardVersion = New System.Windows.Forms.Label()
        Me.timerRunning = New System.Windows.Forms.Timer(Me.components)
        Me.panMenu = New System.Windows.Forms.FlowLayoutPanel()
        Me.pnSystemInfo = New System.Windows.Forms.Panel()
        Me.pnJogging = New System.Windows.Forms.Panel()
        Me.pnMain.SuspendLayout()
        Me.panMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnTeaching
        '
        Me.btnTeaching.BackColor = System.Drawing.Color.MediumAquamarine
        Me.btnTeaching.Font = New System.Drawing.Font("Courier New", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTeaching.ForeColor = System.Drawing.Color.Black
        Me.btnTeaching.Location = New System.Drawing.Point(111, 3)
        Me.btnTeaching.Name = "btnTeaching"
        Me.btnTeaching.Size = New System.Drawing.Size(100, 44)
        Me.btnTeaching.TabIndex = 1
        Me.btnTeaching.Text = "I/O"
        Me.btnTeaching.UseVisualStyleBackColor = False
        '
        'btnHome
        '
        Me.btnHome.BackColor = System.Drawing.Color.MediumTurquoise
        Me.btnHome.Font = New System.Drawing.Font("Courier New", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHome.ForeColor = System.Drawing.Color.Black
        Me.btnHome.Location = New System.Drawing.Point(3, 3)
        Me.btnHome.Name = "btnHome"
        Me.btnHome.Size = New System.Drawing.Size(102, 44)
        Me.btnHome.TabIndex = 2
        Me.btnHome.Text = "HOME"
        Me.btnHome.UseVisualStyleBackColor = False
        '
        'pnMain
        '
        Me.pnMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnMain.AutoScroll = True
        Me.pnMain.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnMain.Controls.Add(Me.emgmassage)
        Me.pnMain.Location = New System.Drawing.Point(12, 251)
        Me.pnMain.Name = "pnMain"
        Me.pnMain.Size = New System.Drawing.Size(993, 146)
        Me.pnMain.TabIndex = 4
        '
        'emgmassage
        '
        Me.emgmassage.AutoSize = True
        Me.emgmassage.Location = New System.Drawing.Point(855, 149)
        Me.emgmassage.Name = "emgmassage"
        Me.emgmassage.Size = New System.Drawing.Size(0, 13)
        Me.emgmassage.TabIndex = 0
        '
        'btnQuit
        '
        Me.btnQuit.BackColor = System.Drawing.Color.Red
        Me.btnQuit.Font = New System.Drawing.Font("Courier New", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnQuit.ForeColor = System.Drawing.Color.Black
        Me.btnQuit.Image = CType(resources.GetObject("btnQuit.Image"), System.Drawing.Image)
        Me.btnQuit.Location = New System.Drawing.Point(217, 3)
        Me.btnQuit.Name = "btnQuit"
        Me.btnQuit.Size = New System.Drawing.Size(105, 44)
        Me.btnQuit.TabIndex = 7
        Me.btnQuit.Text = "QUIT"
        Me.btnQuit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnQuit.UseVisualStyleBackColor = False
        '
        'lblCardVersion
        '
        Me.lblCardVersion.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCardVersion.BackColor = System.Drawing.Color.Lime
        Me.lblCardVersion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCardVersion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCardVersion.Location = New System.Drawing.Point(328, 0)
        Me.lblCardVersion.Name = "lblCardVersion"
        Me.lblCardVersion.Size = New System.Drawing.Size(259, 47)
        Me.lblCardVersion.TabIndex = 8
        Me.lblCardVersion.Text = "Hardware version number ###0.00" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Software version number 2.0.0.1"
        Me.lblCardVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'timerRunning
        '
        '
        'panMenu
        '
        Me.panMenu.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.panMenu.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panMenu.Controls.Add(Me.btnHome)
        Me.panMenu.Controls.Add(Me.btnTeaching)
        Me.panMenu.Controls.Add(Me.btnQuit)
        Me.panMenu.Controls.Add(Me.lblCardVersion)
        Me.panMenu.Dock = System.Windows.Forms.DockStyle.Top
        Me.panMenu.Location = New System.Drawing.Point(0, 0)
        Me.panMenu.Name = "panMenu"
        Me.panMenu.Size = New System.Drawing.Size(1017, 55)
        Me.panMenu.TabIndex = 11
        '
        'pnSystemInfo
        '
        Me.pnSystemInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnSystemInfo.Location = New System.Drawing.Point(12, 61)
        Me.pnSystemInfo.Name = "pnSystemInfo"
        Me.pnSystemInfo.Size = New System.Drawing.Size(549, 185)
        Me.pnSystemInfo.TabIndex = 12
        '
        'pnJogging
        '
        Me.pnJogging.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnJogging.AutoScroll = True
        Me.pnJogging.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnJogging.Location = New System.Drawing.Point(567, 61)
        Me.pnJogging.Name = "pnJogging"
        Me.pnJogging.Size = New System.Drawing.Size(438, 185)
        Me.pnJogging.TabIndex = 13
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(1017, 420)
        Me.Controls.Add(Me.pnJogging)
        Me.Controls.Add(Me.pnSystemInfo)
        Me.Controls.Add(Me.panMenu)
        Me.Controls.Add(Me.pnMain)
        Me.DoubleBuffered = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(932, 447)
        Me.Name = "frmMain"
        Me.Text = " ADRC  Painting Robot Control"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnMain.ResumeLayout(False)
        Me.pnMain.PerformLayout()
        Me.panMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnTeaching As System.Windows.Forms.Button
    Friend WithEvents btnHome As System.Windows.Forms.Button
    Friend WithEvents pnMain As System.Windows.Forms.Panel
    Friend WithEvents btnQuit As System.Windows.Forms.Button
    Friend WithEvents lblCardVersion As System.Windows.Forms.Label
    Friend WithEvents timerRunning As System.Windows.Forms.Timer
    Friend WithEvents panMenu As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents pnSystemInfo As System.Windows.Forms.Panel
    Friend WithEvents pnJogging As System.Windows.Forms.Panel
    Friend WithEvents emgmassage As System.Windows.Forms.Label

End Class
