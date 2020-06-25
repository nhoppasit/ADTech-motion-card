<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgMsgYESNO
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
        Me.rtbText = New System.Windows.Forms.RichTextBox()
        Me.btnYes = New System.Windows.Forms.Button()
        Me.btnNo = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'rtbText
        '
        Me.rtbText.BackColor = System.Drawing.Color.Gray
        Me.rtbText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.rtbText.ForeColor = System.Drawing.Color.Lime
        Me.rtbText.Location = New System.Drawing.Point(15, 15)
        Me.rtbText.Margin = New System.Windows.Forms.Padding(6)
        Me.rtbText.Name = "rtbText"
        Me.rtbText.ReadOnly = True
        Me.rtbText.Size = New System.Drawing.Size(742, 174)
        Me.rtbText.TabIndex = 0
        Me.rtbText.Text = "คุณต้องการปิดระบบ IHR3+2 ใช่หรือไม่ ?" & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(10) & "ปิดระบบ = YES, ไม่ต้องการ = NO"
        '
        'btnYes
        '
        Me.btnYes.Location = New System.Drawing.Point(15, 198)
        Me.btnYes.Name = "btnYes"
        Me.btnYes.Size = New System.Drawing.Size(125, 62)
        Me.btnYes.TabIndex = 1
        Me.btnYes.Text = "YES"
        Me.btnYes.UseVisualStyleBackColor = True
        '
        'btnNo
        '
        Me.btnNo.Location = New System.Drawing.Point(146, 198)
        Me.btnNo.Name = "btnNo"
        Me.btnNo.Size = New System.Drawing.Size(126, 62)
        Me.btnNo.TabIndex = 2
        Me.btnNo.Text = "NO"
        Me.btnNo.UseVisualStyleBackColor = True
        '
        'dlgMsgYESNO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Turquoise
        Me.ClientSize = New System.Drawing.Size(772, 272)
        Me.Controls.Add(Me.btnNo)
        Me.Controls.Add(Me.btnYes)
        Me.Controls.Add(Me.rtbText)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(6)
        Me.Name = "dlgMsgYESNO"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "dlgQuit"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rtbText As System.Windows.Forms.RichTextBox
    Friend WithEvents btnYes As System.Windows.Forms.Button
    Friend WithEvents btnNo As System.Windows.Forms.Button
End Class
