Imports ADTMotionControl
Public Class frmJogging

    Private Sub btnTCCW_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnXCCW.MouseDown
        Jog1.MinusMove()
    End Sub
    Private Sub btnTCCW_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnXCCW.MouseUp
        StopLib.SuddenStopAll()
    End Sub

    Private Sub btnTCW_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnXCW.MouseDown
        Jog1.PlusMove()
    End Sub
    Private Sub btnTCW_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnXCW.MouseUp
        StopLib.SuddenStopAll()
    End Sub

    Private Sub btnKCCW_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnYCCW.MouseDown
        Jog2.MinusMove()
    End Sub

    Private Sub btnKCCW_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnYCCW.MouseUp
        StopLib.SuddenStopAll()
    End Sub
    Private Sub btnKCW_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnYCW.MouseDown
        Jog2.PlusMove()
    End Sub

    Private Sub btnKCW_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnYCW.MouseUp
        StopLib.SuddenStopAll()
    End Sub
    Private Sub btnVCCW_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnZCCW.MouseDown
        Jog3.MinusMove()
    End Sub

    Private Sub btnVCCW_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnZCCW.MouseUp
        StopLib.SuddenStopAll()
    End Sub

    Private Sub btnVCW_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnZCW.MouseDown
        Jog3.PlusMove()
    End Sub

    Private Sub btnVCW_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnZCW.MouseUp
        StopLib.SuddenStopAll()
    End Sub

    Private Sub btnTwistingPlus_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnACCW.MouseDown
        Jog4.PlusMove()
    End Sub

    Private Sub btnTwistingPlus_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnACCW.MouseUp
        StopLib.SuddenStopAll()
    End Sub
    Private Sub btnTwistingMinus_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnACW.MouseDown
        Jog4.MinusMove()
    End Sub

    Private Sub btnTwistingMinus_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnACW.MouseUp
        StopLib.SuddenStopAll()
    End Sub

#Region "Decel stop"

    Private Sub btnSuddenStop_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnSuddenStop.MouseDown
        StopLib.DecelStopAll()
    End Sub

#End Region

    Sub ShowJogging()
        btnXCCW.Visible = True
        btnXCW.Visible = True
        btnYCCW.Visible = True
        btnYCW.Visible = True
        btnZCCW.Visible = True
        btnZCW.Visible = True
        btnACCW.Visible = True
        btnACW.Visible = True
    End Sub

End Class