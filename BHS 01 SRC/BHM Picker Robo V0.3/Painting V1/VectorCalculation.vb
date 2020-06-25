Imports System.Math

Class VectorCalculation

#Region "Structure ที่ใช้คำนวณเวกเตอร์"

    Structure Mat3

        Public M11 As Double
        Public M12 As Double
        Public M13 As Double

        Public M21 As Double
        Public M22 As Double
        Public M23 As Double

        Public M31 As Double
        Public M32 As Double
        Public M33 As Double

        Public Sub New(ByVal a11 As Double, ByVal a12 As Double, ByVal a13 As Double, _
                       ByVal a21 As Double, ByVal a22 As Double, ByVal a23 As Double, _
                       ByVal a31 As Double, ByVal a32 As Double, ByVal a33 As Double)
            M11 = a11 : M12 = a12 : M13 = a13
            M21 = a21 : M22 = a22 : M23 = a23
            M31 = a31 : M32 = a32 : M33 = a33
        End Sub



    End Structure

    Structure Vec3

        Public X As Double
        Public Y As Double
        Public Z As Double

        Public Sub New(ByVal v1 As Double, ByVal v2 As Double, ByVal v3 As Double)
            X = v1
            Y = v2
            Z = v3
        End Sub

#Region "To string"

        Public Overrides Function ToString() As String
            Return "<" & X.ToString & ", " & Y.ToString & ", " & Z.ToString & "> : " & Me.Length
        End Function

        Public Overloads Function ToString(ByVal e As String) As String
            Return "<" & X.ToString(e) & ", " & Y.ToString(e) & ", " & Z.ToString(e) & "> : " & Me.Length
        End Function

#End Region

#Region "++++ (+)"

        Public Shared Operator +(ByVal a As Double, ByVal b As Vec3) As Vec3
            Return New Vec3(a + b.X, _
                            a + b.Y, _
                            a + b.Z)
        End Operator

        Public Shared Operator +(ByVal a As Vec3, ByVal b As Vec3) As Vec3
            Return New Vec3(a.X + b.X, _
                            a.Y + b.Y, _
                            a.Z + b.Z)
        End Operator

#End Region

#Region "----- (-)"


        Public Shared Operator -(ByVal a As Vec3) As Vec3 'Negation
            Return New Vec3(-a.X, _
                            -a.Y, _
                            -a.Z)
        End Operator

        Public Shared Operator -(ByVal a As Vec3, ByVal b As Vec3) As Vec3
            Return New Vec3(a.X - b.X, _
                            a.Y - b.Y, _
                            a.Z - b.Z)
        End Operator

#End Region

#Region "Multiply (*)"

        Public Shared Operator *(ByVal a As Vec3, ByVal b As Vec3) As Vec3
            Return New Vec3(a.X * b.X, _
                            a.Y * b.Y, _
                            a.Z * b.Z)
        End Operator

        Public Shared Operator *(ByVal a As Double, ByVal b As Vec3) As Vec3
            Return New Vec3(a * b.X, _
                            a * b.Y, _
                            a * b.Z)
        End Operator

        Public Shared Operator *(ByVal b As Vec3, ByVal a As Double) As Vec3
            Return New Vec3(a * b.X, _
                            a * b.Y, _
                            a * b.Z)
        End Operator

        Public Shared Operator *(ByVal m As Mat3, ByVal a As Vec3) As Vec3
            Return New Vec3(m.M11 * a.X + m.M12 * a.Y + m.M13 * a.Z, _
                            m.M21 * a.X + m.M22 * a.Y + m.M23 * a.Z, _
                            m.M31 * a.X + m.M32 * a.Y + m.M33 * a.Z)
        End Operator

#End Region

#Region "Divide (/)"

        Public Shared Operator /(ByVal v As Vec3, ByVal a As Double) As Vec3
            Return New Vec3(v.X / a, _
                            v.Y / a, _
                            v.Z / a)
        End Operator

#End Region

#Region "Length"

        Public Shared Function Length(ByVal a As Vec3) As Double
            Return Math.Sqrt((a.X ^ 2) + (a.Y ^ 2) + (a.Z ^ 2))
        End Function

        Public Function Length() As Double
            Return Math.Sqrt((X ^ 2) + (Y ^ 2) + (Z ^ 2))
        End Function

#End Region

#Region "Normalization : unit vector"

        Public Shared Function Normalization(ByVal a As Vec3) As Vec3
            Dim L As Double = Length(a)
            Return New Vec3(a.X / L, _
                            a.Y / L, _
                            a.Z / L)
        End Function

        Public Function Normalization() As Vec3
            Dim L As Double = Length()
            Return New Vec3(X / L, _
                            Y / L, _
                            Z / L)
        End Function

#End Region

#Region "Axial X Rotation"

        Private Shared Function MatAx(ByVal rad As Double) As Mat3
            Return New Mat3(1, 0, 0, _
                            0, Cos(rad), -Sin(rad), _
                            0, Sin(rad), Cos(rad))

        End Function

        Public Shared Function RotateAroundX(ByVal a As Vec3, ByVal deg As Double) As Vec3
            Dim rad As Double = deg * Math.PI / 180
            Dim Ax As Mat3 = MatAx(rad)
            Return Ax * a
        End Function

        Public Function RotateAroundX(ByVal deg As Double) As Vec3
            Dim rad As Double = deg * Math.PI / 180
            Dim Ax As Mat3 = MatAx(rad)
            Return Ax * Me
        End Function

#End Region

#Region "Axial y Rotation"

        Private Shared Function MatAy(ByVal rad As Double) As Mat3
            Return New Mat3(Cos(rad), 0, Sin(rad), _
                            0, 1, 0, _
                            -Sin(rad), 0, Cos(rad))

        End Function

        Public Shared Function RotateAroundY(ByVal a As Vec3, ByVal deg As Double) As Vec3
            Dim rad As Double = deg * Math.PI / 180
            Dim Ay As Mat3 = MatAy(rad)
            Return Ay * a
        End Function

        Public Function RotateAroundY(ByVal deg As Double) As Vec3
            Dim rad As Double = deg * Math.PI / 180
            Dim Ay As Mat3 = MatAy(rad)
            Return Ay * Me
        End Function

#End Region

#Region "Axial Z Rotation"

        Private Shared Function MatAz(ByVal rad As Double) As Mat3
            Return New Mat3(Cos(rad), -Sin(rad), 0, _
                            Sin(rad), Cos(rad), 0, _
                            0, 0, 1)

        End Function

        Public Shared Function RotateAroundZ(ByVal a As Vec3, ByVal deg As Double) As Vec3
            Dim rad As Double = deg * Math.PI / 180
            Dim Az As Mat3 = MatAz(rad)
            Return Az * a
        End Function

        Public Function RotateAroundZ(ByVal deg As Double) As Vec3
            Dim rad As Double = deg * Math.PI / 180
            Dim Az As Mat3 = MatAz(rad)
            Return Az * Me
        End Function

#End Region

#Region "Axis Rotation"

        Private Shared Function MatRotate(ByVal ax As Vec3, ByVal rad As Double) As Mat3
            ax = ax.Normalization
            Dim C As Double = Cos(rad)
            Dim S As Double = Sin(rad)
            Dim jC As Double = 1 - C
            Dim jS As Double = 1 - S
            Dim _X As Double = ax.X
            Dim _Y As Double = ax.Y
            Dim _Z As Double = ax.Z
            Dim _X2 As Double = _X * _X
            Dim _Y2 As Double = _Y * _Y
            Dim _Z2 As Double = _Z * _Z
            'Return New Mat3(_X2 + (1 - _X2) * C, jC * _X * _Y - _Z * S, jC * _X * _Z + _Y * S, _
            '                jC * _X * _Y + _Z * S, _Y2 + (1 - _Y2) * C, jC * _Y * _Z - _X * S, _
            '                jC * _X * _Z - _Y * S, jC * _Y * _Z + _X * S, _Z2 + (1 - _Z2) * C)
            Return New Mat3(C + _X2 * jC, _X * _Y * jC - _Z * S, _X * _Z * jC + _Y * S, _
                            _X * _Y * jC + _Z * S, C + _Y2 * jC, _Y * _Z * jC - _X * S, _
                            _X * _Z * jC - _Y * S, _Y * _Z * jC + _X * S, C + _Z2 * jC)

        End Function

        Public Shared Function Rotate(ByVal a As Vec3, ByVal axis As Vec3, ByVal deg As Double) As Vec3
            Dim rad As Double = deg * Math.PI / 180
            Dim Arot As Mat3 = MatRotate(axis, rad)
            Return Arot * a
        End Function

        Public Function Rotate(ByVal axis As Vec3, ByVal deg As Double) As Vec3
            Dim rad As Double = deg * Math.PI / 180
            Dim Arot As Mat3 = MatRotate(axis, rad)
            Return Arot * Me
        End Function

#End Region

#Region "Cross product (&)"

        Public Shared Operator &(ByVal u As Vec3, ByVal v As Vec3) As Vec3
            Return New Vec3(u.Y * v.Z - u.Z * v.Y, _
                            u.Z * v.X - u.X * v.Z, _
                            u.X * v.Y - u.Y * v.X)
        End Operator

#End Region

#Region "Dot product (^)"

        Public Shared Operator ^(ByVal u As Vec3, ByVal v As Vec3) As Double
            Return (u.X * v.X + u.Y * v.Y + u.Z * v.Z)
        End Operator

#End Region

    End Structure

#End Region

    Sub TestVecCal()

        Dim C_Vec As New Vec3(1, 0, 3)

        Dim tc1 As Vec3 = 3 + C_Vec
        Dim tc2 As Vec3 = Vec3.RotateAroundX(C_Vec, 30)
        Dim tc3 As Vec3 = C_Vec.RotateAroundX(30)
        Dim tc4 As Vec3 = Vec3.RotateAroundY(C_Vec, 30)
        Dim tc5 As Vec3 = C_Vec.RotateAroundY(30)
        Dim tc6 As Vec3 = Vec3.RotateAroundZ(C_Vec, 30)
        Dim tc7 As Vec3 = C_Vec.RotateAroundZ(30)
        Dim tc8 As Vec3 = Vec3.Rotate(C_Vec, New Vec3(0, 0, 1), 30)
        Dim tc9 As Vec3 = C_Vec.Rotate(C_Vec.Normalization, 30)
        Dim a As Vec3 = New Vec3(1, 0, 0)
        Dim b As Vec3 = New Vec3(0, 1, 0)
        Dim c As Vec3 = a & b
        Dim j As Vec3 = c
        j = a
        j = tc1
    End Sub

End Class
