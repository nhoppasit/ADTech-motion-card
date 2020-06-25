Option Strict Off
Option Explicit On

Public Class CtrlCard

    '********************** Motion control module ********************
    'For simplicity, quickly and easily develop a common good, scalable,
    'easy maintenance of applications, we have the basis for control ofthe library card will be
    ' all library functions were classified package. The following example uses a motion
    'control card
    '**************************************** ****************

    Public Shared Result As Short 'Return Value
    Public Shared adt8940a1_Result As Integer 'Return Value
    Const MAXAXIS As Short = 6 'Maximum number of axes
    Const adt8940a1_MAXAXIS As Short = 4 'Maximum number of axes

    Public Shared Function adt8940a1_Init_Card() As Short
        Dim i As Object
        Try
            adt8940a1_Result = adt8940a1.adt8940a1_initial 'Card initialization
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        If adt8940a1_Result <= 0 Then

            adt8940a1_Init_Card = adt8940a1_Result

            Exit Function

        End If

        For i = 1 To adt8940a1_MAXAXIS

            adt8940a1.set_limit_mode(0, i, 1, 1, 0)

            adt8940a1.set_command_pos(0, i, 0) 'Logical position counter is cleared

            adt8940a1.set_actual_pos(0, i, 0) 'Real position counter clear bit

            adt8940a1.set_startv(0, i, 0) 'Set the initial speed

            adt8940a1.set_speed(0, i, 1000) 'Set the drive speed

            adt8940a1.set_acc(0, i, 625) 'Set Acceleration

            'adt8940a1.set_symmetry_speed(0, i, 6400, 6400, 0.01)
        Next i

        adt8940a1_Init_Card = adt8940a1_Result

    End Function

    Public Shared Function adt8940a1_Get_Version() As Single

        Dim libver As Single

        libver = adt8940a1.get_hardware_ver(0)

        Return libver

    End Function

    'Public Shared Function adt8940a1_Setup_Speed(ByVal axis As Short, ByVal startv As Integer, ByVal speed As Integer, ByVal add As Integer, ByVal mode As Short) As Short

    '    Dim ratio As Double = 1 'See also on tech. man.

    '    'Dim time As Double
    '    'Dim addvar As Double
    '    'Dim k As Integer 'The results to be calculated 'Acceleration of the rate of change 'The definition of time
    '    If (startv - speed >= 0) Then 'Make uniform

    '        Result = adt8940a1.set_startv(0, axis, startv)

    '        adt8940a1.set_speed(0, axis, startv)

    '    Else

    '        Result = adt8940a1.set_startv(0, axis, startv)

    '        adt8940a1.set_speed(0, axis, speed)

    '        adt8940a1.set_acc(0, axis, add)

    '    End If

    '    Return 0

    'End Function

    Public Shared Function adt8940a1_StopRun(ByVal axis As Short, ByVal mode As Short) As Short

        If mode = 0 Then

            Result = adt8940a1.sudden_stop(0, axis)

        Else

            Result = adt8940a1.dec_stop(0, axis)

        End If

        adt8940a1_StopRun = Result

    End Function

    Public Shared Function adt8940a1_Setup_Pos(ByVal axis As Short, ByVal pos As Integer, ByVal mode As Short) As Short

        If mode = 0 Then

            Result = adt8940a1.set_command_pos(0, axis, pos)

        Else

            Result = adt8940a1.set_actual_pos(0, axis, pos)

        End If

        Return Result

    End Function

    Public Shared Function adt8940a1_Get_CurrentInfo(ByVal axis As Integer, ByRef LogPos As Long, ByRef actpos As Long, ByRef speed As Long) As Integer
        ' value that receive fron adt8940a1.libbis 2^32 maximum and non negative

        adt8940a1_Result = adt8940a1.get_command_pos(0, axis, LogPos)
        If LogPos > 2147483647 Then
            LogPos = LogPos - 4294967295
        End If
        If LogPos < -2147483647 Then
            LogPos = LogPos + 4294967295
        End If
        adt8940a1.get_actual_pos(0, axis, actpos)
        If actpos > 2147483647 Then
            actpos = actpos - 4294967295
        End If
        If actpos < -2147483647 Then
            actpos = actpos + 4294967295
        End If
        adt8940a1.get_speed(0, axis, speed)
        If speed > 2147483647 Then
            speed = speed - 4294967295
        End If
        adt8940a1_Get_CurrentInfo = adt8940a1_Result


    End Function


    Public Shared Function adt8940a1_Get_MoveStatus(ByVal axis As Short, ByRef value As Short, ByVal mode As Short) As Short

        Dim GetMove_Status As Short

        If mode = 0 Then

            'UPGRADE_WARNING: Couldn't resolve default property of object GetMove_Status. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            GetMove_Status = adt8940a1.get_status(0, axis, value)

        Else

            'UPGRADE_WARNING: Couldn't resolve default property of object GetMove_Status. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            GetMove_Status = adt8940a1.get_inp_status(0, value)

        End If

        Return GetMove_Status

    End Function

    Public Shared Function adt8940a1_Read_Input(ByVal number As Long) As Integer

        adt8940a1_Read_Input = adt8940a1.read_bit(0, number)

    End Function

    Public Shared Function adt8940a1_Write_Output(ByVal number As Integer, ByVal value As Integer) As Integer

        adt8940a1_Write_Output = adt8940a1.write_bit(0, number, value)

    End Function

    Public Shared Function adt8940a1_Setup_LimitMode(ByVal axis As Short, ByVal value1 As Short, ByVal value2 As Short, ByVal logic As Short) As Short

        adt8940a1_Setup_LimitMode = adt8940a1.set_limit_mode(0, axis, value1, value2, logic)

    End Function

    Public Shared Function adt8940a1_Setup_Stop0Mode(ByVal axis As Short, ByVal value As Short, ByVal logic As Short) As Short

        adt8940a1_Setup_Stop0Mode = adt8940a1.set_stop0_mode(0, axis, value, logic)

    End Function

    Public Shared Function adt8940a1_Setup_Stop1Mode(ByVal axis As Short, ByVal value As Short, ByVal logic As Short) As Short

        adt8940a1_Setup_Stop1Mode = adt8940a1.set_stop1_mode(0, axis, value, logic)

    End Function

    Public Shared Function adt8940a1_Sym_RelativeMove(ByVal axis As Integer, ByVal pulse As Long, ByVal lspd As Long, ByVal hspd As Long, ByVal tacc As Double) As Integer

        adt8940a1_Sym_RelativeMove = adt8940a1.symmetry_relative_move(0, axis, pulse, hspd, lspd, tacc) ' ok
        'If adt8940a1_Sym_RelativeMove = 0 Then
        'MsgBox("relative move adt8940a1 error")
        'End If

    End Function
    
    Public Shared Function adt8940a1_Sym_AbsoluteMove(ByVal axis As Short, ByVal pulse As Long, ByVal hspd As Long, ByVal lspd As Long, ByVal tacc As Double) As Short

        adt8940a1_Sym_AbsoluteMove = adt8940a1.symmetry_absolute_move(0, axis, pulse, lspd, hspd, tacc)

    End Function
    
    Public Shared Function adt8940a1_Sym_RelativeLine2(ByVal axis1 As Integer, ByVal axis2 As Integer, ByVal pulse1 As Integer, ByVal pulse2 As Long, ByVal lspd As Long, ByVal hspd As Long, ByVal tacc As Double) As Integer

        adt8940a1_Sym_RelativeLine2 = adt8940a1.symmetry_relative_line2(0, axis1, axis2, pulse1, pulse2, lspd, hspd, tacc)

    End Function

    Public Shared Function adt8940a1_Sym_AbsoluteLine2(ByVal axis1 As Short, ByVal axis2 As Short, ByVal pulse1 As Integer, ByVal pulse2 As Long, ByVal lspd As Long, ByVal hspd As Long, ByVal tacc As Double) As Short

        adt8940a1_Sym_AbsoluteLine2 = adt8940a1.symmetry_absolute_line2(0, axis1, axis2, pulse1, pulse2, lspd, hspd, tacc)

    End Function

    Public Shared Function adt8940a1_Sym_RelativeLine4(ByVal pulse1 As Integer, ByVal pulse2 As Integer, ByVal pulse3 As Integer, ByVal pulse4 As Integer, ByVal lspd As Integer, ByVal hspd As Integer, ByVal tacc As Double) As Integer

        adt8940a1_Sym_RelativeLine4 = adt8940a1.symmetry_relative_line4(0, pulse1, pulse2, pulse3, pulse4, lspd, hspd, tacc)

    End Function
    
    Public Shared Function adt8940a1_Sym_AbsoluteLine4(ByVal pulse1 As Integer, ByVal pulse2 As Integer, ByVal pulse3 As Integer, ByVal pulse4 As Integer, ByVal lspd As Integer, ByVal hspd As Integer, ByVal tacc As Double) As Integer

        adt8940a1_Sym_AbsoluteLine4 = adt8940a1.symmetry_absolute_line4(0, pulse1, pulse2, pulse3, pulse4, lspd, hspd, tacc)

    End Function

    Public Shared Function adt8940a1_Manu_Pmove(ByVal axis As Short, ByVal pulse As Integer) As Short

        Result = adt8940a1.manual_pmove(0, axis, pulse)

        adt8940a1_Manu_Pmove = Result

    End Function

    Public Shared Function adt8940a1_Manu_Continue(ByVal axis As Short) As Short

        Result = adt8940a1.manual_continue(0, axis)

        adt8940a1_Manu_Continue = Result

    End Function

    Public Shared Function adt8940a1_Disable_Manu(ByVal axis As Short) As Short

        Result = adt8940a1.manual_disable(0, axis)

        adt8940a1_Disable_Manu = Result

    End Function

    Public Shared Function SensorAllineffective()
        adt8940a1_Setup_LimitMode(1, 1, 1, 0)
        adt8940a1_Setup_LimitMode(2, 1, 1, 0)
        adt8940a1_Setup_LimitMode(3, 1, 1, 0)
        adt8940a1_Setup_LimitMode(4, 1, 1, 0)
        'Setup_LimitMode(5, 1, 1, 0)
        'adt8940a1_Setup_LimitMode(1, 1, 1, 0)
        'adt8940a1_Setup_LimitMode(2, 1, 1, 0)
        'adt8940a1_Setup_LimitMode(3, 1, 1, 0)

    End Function

    Public Shared Function SensorAlleffective()
        adt8940a1_Setup_LimitMode(1, 0, 0, 0)
        adt8940a1_Setup_LimitMode(2, 0, 1, 0)
        adt8940a1_Setup_LimitMode(3, 1, 1, 0)
        adt8940a1_Setup_LimitMode(4, 1, 1, 0)
        'Setup_LimitMode(5, 0, 0, 0)
        'adt8940a1_Setup_LimitMode(1, 0, 0, 0)
        'adt8940a1_Setup_LimitMode(2, 0, 0, 0)
        'adt8940a1_Setup_LimitMode(3, 0, 0, 0)
    End Function

    Public Shared Function adt8940a1_Setup_Speed(ByVal axis As Short, ByVal u As Long, ByVal v As Long, ByVal add As Long) As Short
        If 0 <= u - v Then 'constant speed motion
            Result = adt8940a1.set_startv(0, axis, u)
            Result += adt8940a1.set_speed(0, axis, u)
        Else 'trapezoidal acceleration/deceleration
            Result = adt8940a1.set_startv(0, axis, u)
            Result += adt8940a1.set_speed(0, axis, v)
            Result += adt8940a1.set_acc(0, axis, add)
        End If
        Return Result
    End Function

    Public Shared Function Axis_Pmove(ByVal axis As Short, ByVal pulse As Long) As Short
        Return adt8940a1.pmove(0, axis, pulse)
    End Function

End Class