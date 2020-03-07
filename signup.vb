Imports System.Data.SqlClient

Public Class signup
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Dim var As Integer = 0
    Private Sub signup_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ComboBox1.Items.Add("Buyer")
        ComboBox1.Items.Add("Seller")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Label14.Visible = False

        con.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lenovo\source\repos\se\se\signup.mdf;Integrated Security=True"
        con.Open()
        cmd.Connection = con
        cmd.CommandText = "INSERT INTO users(fname, mname, lname, usertype,email, phoneno, username, password) VALUES (@fname, @mname, @lname, @usertype,@email, @phoneno, @username, @password)"
        Dim paramfname As New SqlParameter("@fname", SqlDbType.Char, 20)
        paramfname.Value = TextBox1.Text
        Dim parammname As New SqlParameter("@mname", SqlDbType.Char, 20)
        parammname.Value = TextBox2.Text
        Dim paramlname As New SqlParameter("@lname", SqlDbType.Char, 20)
        paramlname.Value = TextBox3.Text
        Dim paramusertype As New SqlParameter("@usertype", SqlDbType.Char, 10)
        paramusertype.Value = ComboBox1.SelectedItem
        Dim paramemailid As New SqlParameter("@email", SqlDbType.VarChar, 50)
        paramemailid.Value = TextBox4.Text
        Dim paramphoneno As New SqlParameter("@phoneno", SqlDbType.NVarChar, 50)
        paramphoneno.Value = TextBox5.Text
        Dim paramusername As New SqlParameter("@username", SqlDbType.VarChar, 20)
        paramusername.Value = TextBox7.Text
        Dim parampassword As New SqlParameter("@password", SqlDbType.VarChar, 12)
        parampassword.Value = TextBox8.Text

        cmd.Parameters.Add(paramfname)
        cmd.Parameters.Add(parammname)
        cmd.Parameters.Add(paramlname)
        cmd.Parameters.Add(paramusertype)
        cmd.Parameters.Add(paramemailid)
        cmd.Parameters.Add(paramphoneno)
        cmd.Parameters.Add(paramusername)
        cmd.Parameters.Add(parampassword)

        If TextBox9.Text <> TextBox8.Text Then
            MessageBox.Show("Unsuccessful signup! Passwords not matching")
            var = 1
            Label14.Visible = True
        End If

        Dim da As New SqlDataAdapter
        da.InsertCommand = cmd
        Try
            da.InsertCommand.ExecuteNonQuery()
            MsgBox("Signed Up succesfully")
        Catch ex As Exception
            MsgBox("Invalid Username!")
        End Try
        Me.Close()

        con.Close()

        If var = 0 Then
            Form1.Show()
            Form1.Top = True
        End If

    End Sub
End Class