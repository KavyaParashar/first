Imports System.Data.SqlClient
Public Class Form1
    Dim username As String
    Dim password As String
    Dim pwdb As String
    Dim con As New SqlConnection
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        signup.Show()
        Me.Hide()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        con.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lenovo\source\repos\se\se\signup.mdf;Integrated Security=True"
        con.Open()
        Dim cmd As New SqlCommand("Select * from users where Username=@username and Password=@password", con)
        cmd.Connection = con
        cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = TextBox1.Text
        cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = TextBox2.Text
        Dim adapter As New SqlDataAdapter(cmd)
        Dim table As New DataTable()
        adapter.Fill(table)
        If table.Rows.Count() <= 0 Then
            MessageBox.Show("Invalid username or password")
        Else
            MessageBox.Show("Login Successful")
            dashboard.Show()
            Me.Hide()
        End If

        con.Close()
    End Sub


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = " "
        TextBox2.Text = " "
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        con.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lenovo\source\repos\se\se\signup.mdf;Integrated Security=True"
        con.Open()
        Dim cmd As New SqlCommand("Select * from users where Username=@username and Password=@password", con)
        cmd.Connection = con
        cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = TextBox1.Text
        cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = TextBox2.Text
        Dim adapter As New SqlDataAdapter(cmd)
        Dim table As New DataTable()
        adapter.Fill(table)
        If table.Rows.Count() <= 0 Then
            MessageBox.Show("Invalid username or password")
        Else
            MessageBox.Show("Login Successful")
            Enroll.Show()
            Me.Hide()
        End If

        con.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        con.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lenovo\source\repos\se\se\signup.mdf;Integrated Security=True"
        con.Open()
        Dim cmd As New SqlCommand("Select * from users where Username=@username and Password=@password", con)
        cmd.Connection = con
        cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = TextBox1.Text
        cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = TextBox2.Text
        Dim adapter As New SqlDataAdapter(cmd)
        Dim table As New DataTable()
        adapter.Fill(table)
        If table.Rows.Count <= 0 Then
            MessageBox.Show("Invalid username or password")
        Else
            If TextBox1.Text = "kpadmin" Then

                MessageBox.Show("Login Successful")
                viewall.Show()
                Me.Hide()

            ElseIf TextBox1.Text = "bkadmin" Then

                MessageBox.Show("Login Successful")
                viewall.Show()
                Me.Hide()
            Else
                MsgBox("Try logging in as buyer or seller. You are not admin!")
            End If
        End If
        con.Close()
    End Sub
End Class
