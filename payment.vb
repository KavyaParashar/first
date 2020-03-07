Imports System.Data.SqlClient
Public Class payment
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Dim mode As String

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        If RadioButton1.Checked = True Then
            mode = "Cash"
        ElseIf RadioButton2.Checked = True Then
            mode = "UPI/IP"
        ElseIf RadioButton3.Checked = True Then
            mode = "Debit/Credit card"
        Else
            MessageBox.Show("Select a mode of payment")
        End If
        Dim constring As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lenovo\source\repos\se\se\signup.mdf;Integrated Security=True"
        con = New SqlConnection(constring)
        con.Open()
        cmd.Connection = con
        cmd.CommandText = "INSERT INTO Payment(username,mode) values(@username,@mode)"
        Dim paramusname As New SqlParameter("@username", SqlDbType.VarChar, 20)
        paramusname.Value = TextBox1.Text
        Dim parammode As New SqlParameter("@mode", SqlDbType.VarChar, 10)
        parammode.Value = mode
        cmd.Parameters.Add(paramusname)
        cmd.Parameters.Add(parammode)
        Dim da As New SqlDataAdapter
        da.InsertCommand = cmd
        Try
            da.InsertCommand.ExecuteNonQuery()
            MsgBox("Success!")
        Catch ex As Exception
            MsgBox("Invalid Username!")
        End Try

        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class