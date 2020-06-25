Public Class Times

    Public Declare Function timeGetTime Lib "winmm.dll" () As Long

    Public Shared Sub Delay_ms(ByVal ms As Long)
        RoboticControl.IsSUDDEN_STOP = False
        Dim c As Long
        c = timeGetTime
        Do
            Application.DoEvents()
        Loop Until (timeGetTime - c >= ms) Or RoboticControl.IsSUDDEN_STOP
    End Sub

    Public Shared Sub Delay_ms2(ByVal ms As Long)
        Dim c As Long
        c = timeGetTime
        Do
            Application.DoEvents()
        Loop Until (timeGetTime - c >= ms)
    End Sub

End Class
