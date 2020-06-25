Imports ADTMotionControl
Public Class Jog4

    Shared mAxis As Integer = 4

    Private Shared JogRange_physical As Double = My.Settings.AJogRange
    Private Shared JogRange As Long = JogRange_physical * My.Settings.Appmm_Numerator / My.Settings.Appmm_Denominator
    Private Shared JogSpeed As Long = My.Settings.AJogSpeed
    Private Shared TAcc As Double = My.Settings.AJogDt

    ''' <summary>
    ''' Set jogging parameters
    ''' </summary>
    ''' <param name="range">Degree unit</param>
    ''' <param name="speed">RPM unit</param>
    ''' <param name="dt">Second unit</param>
    ''' <remarks></remarks>
    Public Shared Sub Setup(ByVal range As Double, ByVal speed As Long, ByVal dt As Double)
        JogRange_physical = range
        JogRange = JogRange_physical * My.Settings.Appmm_Numerator / My.Settings.Appmm_Denominator
        JogSpeed = speed
        TAcc = dt
    End Sub

    ''' <summary>
    ''' Kick stroke of +Y
    ''' ratio = Zppmm_N / Zppmm_D
    ''' See app settings
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub PlusMove()
        Dim status As Integer = 1
        CtrlCard.adt8940a1_Setup_Stop0Mode(mAxis, 0, 0)
        CtrlCard.adt8940a1_Setup_Stop1Mode(mAxis, 1, 0)
        CtrlCard.adt8940a1_Setup_LimitMode(mAxis, 1, 1, 0)
        RoboticControl.IsSUDDEN_STOP = False
        CtrlCard.adt8940a1_Sym_RelativeMove(mAxis, +JogRange, 0, JogSpeed, TAcc)
        Do
            Application.DoEvents()
            CtrlCard.adt8940a1_Get_MoveStatus(mAxis, status, 0)
        Loop Until status = 0 Or RoboticControl.IsSUDDEN_STOP
    End Sub

    ''' <summary>
    ''' Kick stroke of -Y
    ''' ratio = Zppmm_N / Zppmm_D
    ''' See app settings
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub MinusMove()
        Dim status As Integer = 1
        CtrlCard.adt8940a1_Setup_Stop0Mode(mAxis, 0, 0)
        CtrlCard.adt8940a1_Setup_Stop1Mode(mAxis, 1, 0)
        CtrlCard.adt8940a1_Setup_LimitMode(mAxis, 1, 1, 0)
        RoboticControl.IsSUDDEN_STOP = False
        CtrlCard.adt8940a1_Sym_RelativeMove(mAxis, -JogRange, 0, JogSpeed, TAcc)
        Do
            Application.DoEvents()
            CtrlCard.adt8940a1_Get_MoveStatus(mAxis, status, 0)
        Loop Until status = 0 Or RoboticControl.IsSUDDEN_STOP
    End Sub

End Class
