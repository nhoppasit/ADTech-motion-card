Imports System.Math
Imports ADTMotionControl
Module MotionInterpolationModule

    Dim FFormat As String = My.Settings.FFormat

    'ใช้สำหรับเก็บจุดของหุ่นยนต์ เมื่อใช้กับ VectorTarjectory Structure จะเก็บและแปลงค่ามุมของ tw และ be
    Public Structure ElectronicsPosition
        Public T As Long
        Public K As Long
        Public V As Long
        Public Tw As Long
        Public Be As Long

        'dimensional ratio : mm per pulse
        Public ppmmT As Double
        Public ppmmK As Double
        Public ppmmV As Double

        Public T_mm As Double
        Public K_mm As Double
        Public V_mm As Double

        Public TwistingCPR As Long 'Pulse per revolution
        Public BendionCPR As Long 'Pulse per revolution

        Public Tw_deg As Double
        Public Tw_rad As Double

        Public Be_deg As Double
        Public Be_rad As Double

        Public Sub New(ByVal x As Long, ByVal y As Long, ByVal z As Long, ByVal a As Long, ByVal b As Long, _
                       ByVal tw_cpr As Long, ByVal be_cpr As Long, _
                       ByVal ppmmx As Double, ByVal ppmmy As Double, ByVal ppmmz As Double)

            T = x
            K = y
            V = z
            Tw = a
            Be = b

            TwistingCPR = tw_cpr
            BendionCPR = be_cpr

            Tw_deg = a / tw_cpr * 360
            Be_deg = b / be_cpr * 360
            Tw_rad = Tw_deg * PI / 180
            Be_rad = Be_deg * PI / 360

            ppmmT = ppmmx
            ppmmK = ppmmy
            ppmmV = ppmmz

            T_mm = T / ppmmT
            K_mm = K / ppmmK
            V_mm = V / ppmmV

        End Sub

        Public Overrides Function ToString() As String
            Return T.ToString & "," & K.ToString & "," & V.ToString & "," & Tw.ToString & "," & Be.ToString
        End Function

        Public Function ToString_Eng() As String
            Return T_mm.ToString(FFormat) & "," & K_mm.ToString(FFormat) & "," & V_mm.ToString(FFormat) & "," & Tw_deg.ToString(FFormat) & "," & Be_deg.ToString(FFormat)
        End Function

    End Structure

    'เป็นตัวแปรสำคัญสำหรับ คำนวณเส้นทางการเคลื่อนที่ระหว่าง Electronics Position สองจุด แบบ Linear
    Public Structure VectorLinearTarjectory

        '***สำคัญมาก เกิดจาก vector interpolation ระหว่าง ElecStart และ ElecEnd
        Public Interp_Elec_Positions() As ElectronicsPosition

        'ระยะของแกนหมุน tw และ be
        Public Tw_Length As Double
        Public Be_Length As Double

        'จำนวน vector ระหว่างการเคลื่อนที่
        Public StepNumber As Integer

        'จุดเริ่มและปลาย ได้จากการอ่านจากหุ่นยนต์
        Public ElecStart As ElectronicsPosition
        Public ElecEnd As ElectronicsPosition

        'ข้อมูลกำกับการเคลื่อนที่ทั่วไป เคลื่อนที่แบบสม่ำเสมอ
        Public StartSpeed As Long
        Public LastSpeed As Long
        Public TAcc As Double
        Public ACR As Long

        'ข้อมูลเวกเตอร์ จุดเริ่ม และ จุดปลาย
        Public TranslateVec1 As VectorCalculation.Vec3
        Public Twisting1 As VectorCalculation.Vec3
        Public Arm1 As VectorCalculation.Vec3
        Public Bending1 As VectorCalculation.Vec3
        Public Pointing1 As VectorCalculation.Vec3
        '---
        Public TranslateVec2 As VectorCalculation.Vec3
        Public Twisting2 As VectorCalculation.Vec3
        Public Arm2 As VectorCalculation.Vec3
        Public Bending2 As VectorCalculation.Vec3
        Public Pointing2 As VectorCalculation.Vec3
        '
        ' รับค่าจากรายการจุดที่ต้องเคลื่อนที่ เช่น list view แล้วคำนวณ interpolated vector ทันที
        ' หน่วย pulse
        Public Sub New(ByVal x0 As Long, ByVal y0 As Long, ByVal z0 As Long, ByVal tw0 As Long, ByVal be0 As Long, _
                       ByVal x1 As Long, ByVal y1 As Long, ByVal z1 As Long, ByVal tw1 As Long, ByVal be1 As Long, _
                       ByVal u As Long, ByVal v As Long, _
                       ByVal dt As Double, ByVal change As Long, _
                       ByVal tw_cpr As Long, ByVal be_cpr As Long, _
                       ByVal step_number As Integer,
                       ByVal tw_len As Double, ByVal be_len As Double, _
                       ByVal ppmmx As Double, ByVal ppmmy As Double, ByVal ppmmz As Double)
            Try
                '-------------------------------------------------------------------------------------
                'รับค่า
                '-------------------------------------------------------------------------------------
                Tw_Length = tw_len 'ความยาวรัศมีแกน tw และ be
                Be_Length = be_len

                StepNumber = step_number 'จำนวนที่จะสร้าง interpolation

                ElecStart = New ElectronicsPosition(x0, y0, z0, tw0, be0, tw_cpr, be_cpr, ppmmx, ppmmy, ppmmz) 'จุดเริ่มต้น
                ElecEnd = New ElectronicsPosition(x1, y1, z1, tw1, be1, tw_cpr, be_cpr, ppmmx, ppmmy, ppmmz) 'จุดสุดท้าย

                StartSpeed = u 'ความเร็วต้น
                LastSpeed = v 'ความเร็วปลาย ควรเท่ากับความเร็วต้น (ระยะทดสอบ)

                TAcc = dt 'เวลาเร่ง
                ACR = change 'การเปลี่ยนความเร็ว

                '-------------------------------------------------------------------------------------
                'คำนวณ vector C A D สำหรับจุด Start (mm)
                '-------------------------------------------------------------------------------------
                Dim vC1 As New VectorCalculation.Vec3(ElecStart.T_mm, ElecStart.K_mm, ElecStart.V_mm)
                Dim vTw1 As New VectorCalculation.Vec3(Tw_Length, 0, 0) ' Twisting ** ต้องกำหนดให้ถูกต้อง ตาม Homing
                vTw1 = vTw1.RotateAroundZ(ElecStart.Tw_deg)
                Dim vA1 As VectorCalculation.Vec3 = vC1 + vTw1
                Dim vBe1 As New VectorCalculation.Vec3(0, 0, -Be_Length) ' Bending ** ต้องกำหนดให้ถูกต้อง ตาม Homing
                vBe1 = vBe1.RotateAroundZ(ElecStart.Tw_deg)
                vBe1 = vBe1.Rotate(vTw1.Normalization, ElecStart.Be_deg)
                Dim vD1 As VectorCalculation.Vec3 = vA1 + vBe1

                '-------------------------------------------------------------------------------------
                'คำนวณ vector C A D สำหรับจุด End (mm)
                '-------------------------------------------------------------------------------------
                Dim vC2 As New VectorCalculation.Vec3(ElecEnd.T_mm, ElecEnd.K_mm, ElecEnd.V_mm)
                Dim vTw2 As New VectorCalculation.Vec3(Tw_Length, 0, 0) ' Twisting ** ต้องกำหนดให้ถูกต้อง ตาม Homing
                vTw2 = vTw2.RotateAroundZ(ElecEnd.Tw_deg)
                Dim vA2 As VectorCalculation.Vec3 = vC2 + vTw2
                Dim vBe2 As New VectorCalculation.Vec3(0, 0, -Be_Length) ' Bending ** ต้องกำหนดให้ถูกต้อง ตาม Homing
                vBe2 = vBe2.RotateAroundZ(ElecEnd.Tw_deg)
                vBe2 = vBe2.Rotate(vTw2.Normalization, ElecEnd.Be_deg)
                Dim vD2 As VectorCalculation.Vec3 = vA2 + vBe2

                '-------------------------------------------------------------------------------------
                'ส่งค่าให้ public
                '-------------------------------------------------------------------------------------
                TranslateVec1 = vC1
                Twisting1 = vTw1
                Arm1 = vA1
                Bending1 = vBe1
                Pointing1 = vD1
                '...
                TranslateVec2 = vC2
                Twisting2 = vTw2
                Arm2 = vA2
                Bending2 = vBe2
                Pointing2 = vD2
                '
                '-------------------------------------------------------------------------------------
                'คำนวณ increment
                '-------------------------------------------------------------------------------------
                Dim VHead As VectorCalculation.Vec3 = (vD2 - vD1)
                Dim dVecHead As VectorCalculation.Vec3 = VHead / (StepNumber - 1) 'delta vector of head 

                '-------------------------------------------------------------------------------------
                'คำนวณ constrant ของการบิด
                '-------------------------------------------------------------------------------------
                Dim nZNorm As VectorCalculation.Vec3 = New VectorCalculation.Vec3(0, 0, -1)
                Dim XNorm As VectorCalculation.Vec3 = New VectorCalculation.Vec3(1, 0, 0)
                Dim YNorm As VectorCalculation.Vec3 = New VectorCalculation.Vec3(0, 1, 0)

                '-------------------------------------------------------------------------------------
                'คำนวณ interpolation (mm) --> (PULSE)
                '-------------------------------------------------------------------------------------
                ReDim Interp_Elec_Positions(StepNumber - 1)
                For I As Integer = 0 To StepNumber - 1

                    If (I = 0) Then
                        Interp_Elec_Positions(I) = New ElectronicsPosition(x0, y0, z0, tw0, be0, tw_cpr, be_cpr, ppmmx, ppmmy, ppmmz)

                    ElseIf (I = StepNumber - 1) Then
                        Interp_Elec_Positions(I) = New ElectronicsPosition(x1, y1, z1, tw1, be1, tw_cpr, be_cpr, ppmmx, ppmmy, ppmmz)

                    Else

                        '*** FIND OUT RESULT ALL VECTOR (mm)                    
                        Dim Di As VectorCalculation.Vec3 = vD1 + (I * dVecHead)
                        Dim N2 As Double = (Di - vD1).Length / VHead.Length
                        Dim N1 As Double = 1 - N2
                        Dim BeiAng_deg As Double = N1 * ElecStart.Be_deg + N2 * ElecEnd.Be_deg
                        Dim TwiAng_deg As Double = N1 * ElecStart.Tw_deg + N2 * ElecEnd.Tw_deg
                        Dim Twi As New VectorCalculation.Vec3(Tw_Length, 0, 0) ' Twisting ** ต้องกำหนดให้ถูกต้อง ตาม Homing
                        Dim Bei As New VectorCalculation.Vec3(0, 0, -Be_Length) ' Bending ** ต้องกำหนดให้ถูกต้อง ตาม Homing
                        Bei = Bei.Rotate(XNorm, BeiAng_deg) ' Bend the Bei vector ** ต้องกำหนดให้ถูกต้อง ตาม Homing
                        Dim Twi_Bei As VectorCalculation.Vec3 = Twi + Bei
                        Twi = Twi.RotateAroundZ(TwiAng_deg) 'Twist two vector of Twi and Twi+Bei
                        Twi_Bei = Twi_Bei.RotateAroundZ(TwiAng_deg)
                        Bei = Twi_Bei - Twi 'Find final Bei
                        Dim Ai As VectorCalculation.Vec3 = Di - Bei
                        Dim Ci As VectorCalculation.Vec3 = Ai - Twi

                        '*** Remember the finally unit must be (PULSE)
                        Interp_Elec_Positions(I) = New ElectronicsPosition(Ci.X * ppmmx, _
                                                                           Ci.Y * ppmmy, _
                                                                           Ci.Z * ppmmz, _
                                                                           TwiAng_deg * tw_cpr / 360, _
                                                                           BeiAng_deg * be_cpr / 360, _
                                                                           tw_cpr, be_cpr, _
                                                                           ppmmx, ppmmy, ppmmz)

                    End If

                Next
            Catch ex As Exception
                MsgBox("ผิดพลาด การแปลงค่าความเร็วล้มเหลว ไม่สามารถทดสอบการเคลื่อนที่ได้")
                MsgBox(ex.Message)
                Exit Sub
            End Try

        End Sub

        Public Sub AbsoluteMoveToEnd()
            RoboticControl.IsSUDDEN_STOP = False
            CtrlCard.adt8940a1_Sym_AbsoluteLine4(ElecEnd.T, ElecEnd.K, ElecEnd.V, ElecEnd.Tw, Me.StartSpeed, Me.LastSpeed, Me.TAcc)
            Dim nStatus(0 To 6) As Single
            Do
                Application.DoEvents()
                CtrlCard.adt8940a1_Get_MoveStatus(1, nStatus(0), 1)
                CtrlCard.adt8940a1_Get_MoveStatus(2, nStatus(1), 1)
                CtrlCard.adt8940a1_Get_MoveStatus(3, nStatus(2), 1)
                CtrlCard.adt8940a1_Get_MoveStatus(4, nStatus(3), 1)
            Loop Until (nStatus(0) = 0 And nStatus(1) = 0 And nStatus(2) = 0 And nStatus(3) = 0) Or RoboticControl.IsSUDDEN_STOP
            RoboticControl.ShowAlert("Done move to end point.", 1500)
        End Sub

        Public Sub AbsoluteMoveToStart()
            RoboticControl.IsSUDDEN_STOP = False
            CtrlCard.SensorAllineffective()
            CtrlCard.adt8940a1_Sym_AbsoluteLine4(ElecStart.T, ElecStart.K, ElecStart.V, ElecStart.Tw, Me.StartSpeed, Me.LastSpeed, Me.TAcc)
            Dim nStatus(0 To 6) As Single
            Do
                Application.DoEvents()
                CtrlCard.adt8940a1_Get_MoveStatus(1, nStatus(0), 1)
                CtrlCard.adt8940a1_Get_MoveStatus(2, nStatus(1), 1)
                CtrlCard.adt8940a1_Get_MoveStatus(3, nStatus(2), 1)
                CtrlCard.adt8940a1_Get_MoveStatus(4, nStatus(3), 1)
            Loop Until (nStatus(0) = 0 And nStatus(1) = 0 And nStatus(2) = 0 And nStatus(3) = 0) Or RoboticControl.IsSUDDEN_STOP
            CtrlCard.SensorAlleffective()
            RoboticControl.ShowAlert("Done move to start point.", 1500)
        End Sub

        Public Sub MoveLine(ByVal valve As Object, ByVal delay As Integer)

            Dim TheValve As ValveStructureModule.ValveCommand
            TheValve = CType(valve, ValveStructureModule.ValveCommand)

            '-----------------------------------------------------------------------------------
            'สั่งเคลื่อนที่
            '-----------------------------------------------------------------------------------
            RoboticControl.IsSUDDEN_STOP = False
            Dim nStatus(0 To 5) As Single
            Dim epoint As ElectronicsPosition

            Dim dSpeed As Long = (Me.LastSpeed - Me.StartSpeed) / ACR
            Dim SpeedSeries(0 To Me.Interp_Elec_Positions.Count - 1) As Long
            Dim g As Integer = 0
            For jj As Integer = 0 To ACR - 1
                SpeedSeries(jj) = Me.StartSpeed + g * dSpeed
                g += 1
            Next
            For jj As Integer = ACR To Me.Interp_Elec_Positions.Count - ACR - 1
                SpeedSeries(jj) = Me.LastSpeed
            Next
            g = 0
            For jj As Integer = Me.Interp_Elec_Positions.Count - ACR To Me.Interp_Elec_Positions.Count - 1
                SpeedSeries(jj) = Me.LastSpeed - g * dSpeed
                g += 1
            Next

            For jj As Integer = 0 To Me.Interp_Elec_Positions.Count - 1 'Each epoint As ElectronicsPosition In interp.Interp_Elec_Positions

                epoint = Me.Interp_Elec_Positions(jj)

                CtrlCard.adt8940a1_Sym_AbsoluteLine4( _
                                     epoint.T, _
                                     epoint.K, _
                                     epoint.V, _
                                     epoint.Tw, _
                                     SpeedSeries(jj), Me.LastSpeed, _
                                     Me.TAcc)

                Do
                    Application.DoEvents()
                    CtrlCard.adt8940a1_Get_MoveStatus(1, nStatus(0), 1)
                    CtrlCard.adt8940a1_Get_MoveStatus(2, nStatus(1), 1)
                    CtrlCard.adt8940a1_Get_MoveStatus(3, nStatus(2), 1)
                    CtrlCard.adt8940a1_Get_MoveStatus(4, nStatus(3), 1)
                Loop Until (nStatus(0) = 0 And nStatus(1) = 0 And nStatus(2) = 0 And nStatus(3) = 0) Or RoboticControl.IsSUDDEN_STOP

                If Not RoboticControl.IsSUDDEN_STOP And jj = 0 Then
                    TheValve.SetPressure()
                    TheValve.TurnValve()
                    If 1 <= delay Then
                        Times.Delay_ms(delay)
                    End If
                End If

                ' Check stop command
                If RoboticControl.IsSUDDEN_STOP Then Exit For
            Next

        End Sub

        Public Sub AbsoluteMove()

            '-----------------------------------------------------------------------------------
            'สั่งเคลื่อนที่
            '-----------------------------------------------------------------------------------
            RoboticControl.IsSUDDEN_STOP = False
            Dim nStatus(0 To 5) As Single
            Dim epoint As ElectronicsPosition
            epoint = Me.ElecEnd

            CtrlCard.adt8940a1_Sym_AbsoluteLine4( _
                                 epoint.T, _
                                 epoint.K, _
                                 epoint.V, _
                                 epoint.Tw, _
                                 Me.StartSpeed, Me.LastSpeed, _
                                 Me.TAcc)
            Do
                Application.DoEvents()
                CtrlCard.adt8940a1_Get_MoveStatus(1, nStatus(0), 0)
                CtrlCard.adt8940a1_Get_MoveStatus(2, nStatus(1), 0)
                CtrlCard.adt8940a1_Get_MoveStatus(3, nStatus(2), 0)
                CtrlCard.adt8940a1_Get_MoveStatus(4, nStatus(3), 0)
            Loop Until (nStatus(0) = 0 And nStatus(1) = 0 And nStatus(2) = 0 And nStatus(3) = 0) Or RoboticControl.IsSUDDEN_STOP
            'Loop Until nStatus(0) = 0 Or RoboticControl.IsSUDDEN_STOP
            ' Check stop command
            If RoboticControl.IsSUDDEN_STOP Then Exit Sub

        End Sub

    End Structure

    'เป็นตัวแปรสำคัญสำหรับ คำนวณเส้นทางการเคลื่อนที่ระหว่าง Electronics Position สองจุด แบบ Pointing
    Public Structure VectorPointingTarjectory

        '***สำคัญมาก เกิดจาก vector interpolation ระหว่าง ElecStart และ ElecEnd
        Public Interp_Elec_Positions() As ElectronicsPosition

        'ระยะของแกนหมุน tw และ be
        Public Tw_Length As Double
        Public Be_Length As Double

        'ค่าพิกัดการเคลื่อนที่
        Private _ppmmx As Double
        Private _ppmmy As Double
        Private _ppmmz As Double
        Private _tw_cpr As Long
        Private _be_cpr As Long

        'จำนวน vector ระหว่างการเคลื่อนที่
        Public StepNumber As Integer

        'จุดเริ่มและปลาย ได้จากการอ่านจากหุ่นยนต์
        Public ElecStart As ElectronicsPosition
        Public ElecEnd As ElectronicsPosition

        'ข้อมูลกำกับการเคลื่อนที่ทั่วไป เคลื่อนที่แบบสม่ำเสมอ
        Public LSpeed As Long
        Public HSpeed As Long
        Public TAcc As Double
        Public CAcc As Long

        'ข้อมูลเวกเตอร์ จุดเริ่ม และ จุดปลาย
        Public TranslateVec1 As VectorCalculation.Vec3
        Public Twisting1 As VectorCalculation.Vec3
        Public Arm1 As VectorCalculation.Vec3
        Public Bending1 As VectorCalculation.Vec3
        Public Pointing1 As VectorCalculation.Vec3
        '---
        Public TranslateVec2 As VectorCalculation.Vec3
        Public Twisting2 As VectorCalculation.Vec3
        Public Arm2 As VectorCalculation.Vec3
        Public Bending2 As VectorCalculation.Vec3
        Public Pointing2 As VectorCalculation.Vec3

        'ผลการวิเคราะห์ Pointing
        Public PointingDift As VectorCalculation.Vec3

        'รับค่าจากรายการจุดที่ต้องเคลื่อนที่ เช่น list view แล้วคำนวณ interpolated vector ทันที
        Public Sub New(ByVal x0 As Long, ByVal y0 As Long, ByVal z0 As Long, ByVal tw0 As Long, ByVal be0 As Long, _
                       ByVal x1 As Long, ByVal y1 As Long, ByVal z1 As Long, ByVal tw1 As Long, ByVal be1 As Long, _
                       ByVal u As Long, ByVal v As Long, _
                       ByVal dt As Double, ByVal change As Long, _
                       ByVal tw_cpr As Long, ByVal be_cpr As Long, _
                       ByVal step_number As Integer,
                       ByVal tw_len As Double, ByVal be_len As Double, _
                       ByVal ppmmx As Double, ByVal ppmmy As Double, ByVal ppmmz As Double)

            '-------------------------------------------------------------------------------------
            'รับค่า
            '-------------------------------------------------------------------------------------
            Tw_Length = tw_len 'ความยาวรัศมีแกน tw และ be
            Be_Length = be_len

            _ppmmx = ppmmx
            _ppmmy = ppmmy
            _ppmmz = ppmmz
            _tw_cpr = tw_cpr
            _be_cpr = be_cpr

            StepNumber = step_number 'จำนวนที่จะสร้าง interpolation

            ElecStart = New ElectronicsPosition(x0, y0, z0, tw0, be0, tw_cpr, be_cpr, ppmmx, ppmmy, ppmmz) 'ข้อมูลจุดเริ่มต้น
            ElecEnd = New ElectronicsPosition(x1, y1, z1, tw1, be1, tw_cpr, be_cpr, ppmmx, ppmmy, ppmmz) 'ข้อมูลจุดสุดท้าย

            LSpeed = u 'ความเร็วต้น
            HSpeed = v 'ความเร็วปลาย ควรเท่ากับความเร็วต้น (ระยะทดสอบ)

            TAcc = dt 'เวลาเร่ง
            CAcc = change 'การเปลี่ยนความเร่ง

            '-------------------------------------------------------------------------------------
            'คำนวณ vector C A D สำหรับจุด Start (mm)
            '-------------------------------------------------------------------------------------
            Dim vC1 As New VectorCalculation.Vec3(ElecStart.T_mm, ElecStart.K_mm, ElecStart.V_mm)
            Dim vTw1 As New VectorCalculation.Vec3(-Tw_Length, 0, 0) 'Twisting 
            vTw1 = vTw1.RotateAroundZ(ElecStart.Tw_deg)
            Dim vA1 As VectorCalculation.Vec3 = vC1 + vTw1
            Dim vBe1 As New VectorCalculation.Vec3(0, Be_Length, 0)
            vBe1 = vBe1.RotateAroundZ(ElecStart.Tw_deg)
            vBe1 = vBe1.Rotate(vTw1.Normalization, ElecStart.Be_deg)
            Dim vD1 As VectorCalculation.Vec3 = vA1 + vBe1

            '-------------------------------------------------------------------------------------
            'คำนวณ vector C A D สำหรับจุด End (mm)
            '-------------------------------------------------------------------------------------
            Dim vC2 As New VectorCalculation.Vec3(ElecEnd.T_mm, ElecEnd.K_mm, ElecEnd.V_mm)
            Dim vTw2 As New VectorCalculation.Vec3(-Tw_Length, 0, 0) 'Twisting 
            vTw2 = vTw2.RotateAroundZ(ElecEnd.Tw_deg)
            Dim vA2 As VectorCalculation.Vec3 = vC2 + vTw2
            Dim vBe2 As New VectorCalculation.Vec3(0, Be_Length, 0)
            vBe2 = vBe2.RotateAroundZ(ElecEnd.Tw_deg)
            vBe2 = vBe2.Rotate(vTw2.Normalization, ElecEnd.Be_deg)
            Dim vD2 As VectorCalculation.Vec3 = vA2 + vBe2

            '-------------------------------------------------------------------------------------
            'ส่งค่าให้ public
            '-------------------------------------------------------------------------------------
            TranslateVec1 = vC1
            Twisting1 = vTw1
            Arm1 = vA1
            Bending1 = vBe1
            Pointing1 = vD1
            '...
            TranslateVec2 = vC2
            Twisting2 = vTw2
            Arm2 = vA2
            Bending2 = vBe2
            Pointing2 = vD2

            '-------------------------------------------------------------------------------------
            'ตรวจสอบความสมบูรณ์ของ Pointing
            '-------------------------------------------------------------------------------------
            PointingDift = vD2 - vD1
            If PointingDift.Length > 0.01 Then
                Exit Sub
            End If

            '-------------------------------------------------------------------------------------
            'คำนวณ constrant ของการบิด
            '-------------------------------------------------------------------------------------
            Dim nZNorm As VectorCalculation.Vec3 = New VectorCalculation.Vec3(0, 0, -1)
            Dim nXNorm As VectorCalculation.Vec3 = New VectorCalculation.Vec3(-1, 0, 0)
            Dim YNorm As VectorCalculation.Vec3 = New VectorCalculation.Vec3(0, 1, 0)

            '-------------------------------------------------------------------------------------
            'คำนวณ interpolation (mm) --> (PULSE)
            '-------------------------------------------------------------------------------------
            ReDim Interp_Elec_Positions(StepNumber - 1)
            For I As Integer = 0 To StepNumber - 1

                If (I = 0) Then
                    Interp_Elec_Positions(I) = New ElectronicsPosition(x0, y0, z0, tw0, be0, tw_cpr, be_cpr, ppmmx, ppmmy, ppmmz)

                ElseIf (I = StepNumber - 1) Then
                    Interp_Elec_Positions(I) = New ElectronicsPosition(x1, y1, z1, tw1, be1, tw_cpr, be_cpr, ppmmx, ppmmy, ppmmz)

                Else

                    '*** FIND OUT RESULT ALL VECTOR (mm)                    
                    Dim Di As VectorCalculation.Vec3 = vD1
                    Dim N2 As Double = I / (StepNumber - 1)
                    Dim N1 As Double = 1 - N2
                    Dim BeiAng_deg As Double = N1 * ElecStart.Be_deg + N2 * ElecEnd.Be_deg
                    Dim TwiAng_deg As Double = N1 * ElecStart.Tw_deg + N2 * ElecEnd.Tw_deg
                    Dim Twi As New VectorCalculation.Vec3(-Tw_Length, 0, 0)
                    Dim Bei As New VectorCalculation.Vec3(0, Be_Length, 0)
                    Bei = Bei.Rotate(nXNorm, BeiAng_deg) 'Bend the Bei vector only
                    Dim Twi_Bei As VectorCalculation.Vec3 = Twi + Bei
                    Twi = Twi.RotateAroundZ(TwiAng_deg) 'Twist two vector of Twi and Twi+Bei
                    Twi_Bei = Twi_Bei.RotateAroundZ(TwiAng_deg)
                    Bei = Twi_Bei - Twi 'Find final Bei
                    Dim Ai As VectorCalculation.Vec3 = Di - Bei
                    Dim Ci As VectorCalculation.Vec3 = Ai - Twi

                    '*** Remember the finally unit must be (PULSE)
                    Interp_Elec_Positions(I) = New ElectronicsPosition(Ci.X * ppmmx, _
                                                                       Ci.Y * ppmmy, _
                                                                       Ci.Z * ppmmz, _
                                                                       TwiAng_deg * tw_cpr / 360, _
                                                                       BeiAng_deg * be_cpr / 360, _
                                                                       tw_cpr, be_cpr, _
                                                                       ppmmx, ppmmy, ppmmz)

                End If

            Next

        End Sub

        'เลื่อนจุด POINTING ให้ตรงกัน
        Public Function CalculateNewPointing() As ElectronicsPosition

            '-------------------------------------------------------------------------------------
            'คำนวณ constrant ของการบิด
            '-------------------------------------------------------------------------------------
            Dim nZNorm As VectorCalculation.Vec3 = New VectorCalculation.Vec3(0, 0, -1)
            Dim nXNorm As VectorCalculation.Vec3 = New VectorCalculation.Vec3(-1, 0, 0)
            Dim YNorm As VectorCalculation.Vec3 = New VectorCalculation.Vec3(0, 1, 0)

            '-------------------------------------------------------------------------------------
            'ดึงค่าข้อมูลปัจจุบัน
            '-------------------------------------------------------------------------------------
            Dim C1 As VectorCalculation.Vec3 = TranslateVec1
            Dim Tw1 As VectorCalculation.Vec3 = Twisting1
            Dim A1 As VectorCalculation.Vec3 = Arm1
            Dim Be1 As VectorCalculation.Vec3 = Bending1
            Dim D1 As VectorCalculation.Vec3 = Pointing1
            '...
            Dim C2 As VectorCalculation.Vec3 = TranslateVec2
            Dim Tw2 As VectorCalculation.Vec3 = Twisting2
            Dim A2 As VectorCalculation.Vec3 = Arm2
            Dim Be2 As VectorCalculation.Vec3 = Bending2
            Dim D2 As VectorCalculation.Vec3 = Pointing2

            '-------------------------------------------------------------------------------------
            'ปรับค่าให้ Pointing ตรงกัน
            '-------------------------------------------------------------------------------------
            Dim Di As VectorCalculation.Vec3 = D1
            Dim BeiAng_deg As Double = ElecEnd.Be_deg
            Dim TwiAng_deg As Double = ElecEnd.Tw_deg
            Dim Twi As New VectorCalculation.Vec3(-Tw_Length, 0, 0)
            Dim Bei As New VectorCalculation.Vec3(0, Be_Length, 0)
            Bei = Bei.Rotate(nXNorm, BeiAng_deg) 'Bend the Bei vector only
            Dim Twi_Bei As VectorCalculation.Vec3 = Twi + Bei
            Twi = Twi.RotateAroundZ(TwiAng_deg) 'Twist two vector of Twi and Twi+Bei
            Twi_Bei = Twi_Bei.RotateAroundZ(TwiAng_deg)
            Bei = Twi_Bei - Twi 'Find final Bei
            Dim Ai As VectorCalculation.Vec3 = Di - Bei
            Dim Ci As VectorCalculation.Vec3 = Ai - Twi

            Dim Elec_Positions As New ElectronicsPosition(Ci.X * _ppmmx, _
                                                            Ci.Y * _ppmmy, _
                                                            Ci.Z * _ppmmz, _
                                                            TwiAng_deg * _tw_cpr / 360, _
                                                            BeiAng_deg * _be_cpr / 360, _
                                                            _tw_cpr, _be_cpr, _
                                                            _ppmmx, _ppmmy, _ppmmz)

            Return Elec_Positions

        End Function

    End Structure

End Module
