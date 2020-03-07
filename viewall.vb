Imports System.Data.SqlClient
Imports System.IO
Public Class viewall
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand

    Private Sub viewall_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Panel2.Visible = False
        con.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lenovo\source\repos\se\se\signup.mdf;Integrated Security=True"
        con.Open()
        cmd.Connection = con
        cmd.CommandText = "select product from product"
        Dim dr1 As SqlDataReader
        dr1 = cmd.ExecuteReader
        Do While dr1.Read
            ComboBox1.Items.Add(dr1("product"))
        Loop
        dr1.Close()
        con.Close()
    End Sub
    Public Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        con.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lenovo\source\repos\se\se\signup.mdf;Integrated Security=True"
        con.Open()
        Panel2.Visible = True
        Dim dr2 As SqlDataReader
        Dim cmd2 As New SqlCommand
        Dim imgByte() As Byte
        cmd2.Connection = con
        cmd2.CommandText = "select startingbid,currentbid,validity,buyer,username,productphoto from product where product=" & "(@product)"
        Dim paramproduct As New SqlParameter("@product", SqlDbType.NChar, 20)
        paramproduct.Direction = ParameterDirection.Input
        paramproduct.Value = ComboBox1.SelectedItem
        cmd2.Parameters.Add(paramproduct)
        dr2 = cmd2.ExecuteReader
        Do While dr2.Read
            Label14.Text = dr2("startingbid")
            Try
                Label15.Text = dr2("currentbid")
            Catch ex As Exception
                Label15.Text = dr2("startingbid")
            End Try

            Label3.Text = dr2("Buyer")
            Label4.Text = dr2("Username")
            DateTimePicker2.Value = dr2("validity")

            imgByte = dr2("productphoto")
            Dim ms As New MemoryStream(imgByte, True)
            PictureBox2.Image = Bitmap.FromStream(ms)

        Loop
        dr2.Close()
        con.Close()

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Me.Close()
        Form1.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        con.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lenovo\source\repos\se\se\signup.mdf;Integrated Security=True"
        con.Open()
        Dim cmd2 As New SqlCommand
        Dim da As New SqlDataAdapter
        cmd2.Connection = con
        cmd2.CommandText = "Delete from product where product=" & "(@product)"
        Dim paramproduct As New SqlParameter("@product", SqlDbType.NChar, 20)
        paramproduct.Direction = ParameterDirection.Input
        paramproduct.Value = ComboBox1.SelectedItem
        cmd2.Parameters.Add(paramproduct)
        da.DeleteCommand = cmd2

        Dim i As Integer = da.DeleteCommand.ExecuteNonQuery()
        If (i > 0) Then
            MsgBox("Record is successfully deleted")
            Me.Close()
        Else
            MsgBox("Record is not deleted")
        End If
        con.Close()


    End Sub
End Class