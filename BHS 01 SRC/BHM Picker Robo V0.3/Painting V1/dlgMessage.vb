Public Class dlgMessage

    Dim TimerClose As Timer

    Public Sub New(ByVal Interval As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        TimerClose = New Timer
        AddHandler TimerClose.Tick, AddressOf TimerClose_Tick
        TimerClose.Interval = Interval
        TimerClose.Enabled = True
    End Sub

    Sub TimerClose_Tick(ByVal sender As Object, ByVal e As EventArgs)
        TimerClose.Enabled = False
        Close()
    End Sub

End Class