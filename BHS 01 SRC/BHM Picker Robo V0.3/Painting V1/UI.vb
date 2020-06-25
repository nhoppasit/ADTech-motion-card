Public Class UI

    ' ส่วนคำสั่ง
    '#Region "Cross-thread interfacing"

    '    Private Delegate Sub VoidDelegate()

    '    Public Sub PostText(ByVal text As String)
    '        If Me.InvokeRequired Then
    '            Me.Invoke(New VoidDelegate(Sub()
    '                                           If text = "" Then
    '                                               Me.Text = "Parallel port testing."
    '                                           Else
    '                                               Me.Text = text
    '                                           End If
    '                                       End Sub))
    '        Else
    '            If text = "" Then
    '                Me.Text = "Parallel port testing."
    '            Else
    '                Me.Text = text
    '            End If
    '        End If
    '    End Sub

    '#End Region

    'ส่วนประกาศ
    Public Delegate Sub UpdateTitleCallback(ByVal text As String)
    'ตัวแทนทำงาน
    Public Shared UpdateTitle As UpdateTitleCallback
    'กำเนิดตัวแทน อ้างอิงไปที่ invoke
    Public Shared Sub Initialize()
        UpdateTitle = New UpdateTitleCallback(AddressOf frmMain.PostText)
    End Sub

End Class
