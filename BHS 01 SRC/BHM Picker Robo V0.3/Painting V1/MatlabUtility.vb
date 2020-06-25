Public Class MatlabUtility

    Public Shared Sub ClearTempCSV(ByVal filepath As String)
        Try
            System.IO.File.Delete(filepath)
        Catch ex As Exception

        End Try
    End Sub

    Public Shared Sub AppendToTempCSV(ByVal filepath As String, ByVal x As Double, ByVal y As Double, ByVal z As Double, ByVal a As Double, ByVal b As Double)
        System.IO.File.AppendAllText(filepath, x.ToString & "," & y.ToString & "," & z.ToString & "," & a.ToString & "," & b.ToString & Environment.NewLine, System.Text.Encoding.Default)
    End Sub

End Class
