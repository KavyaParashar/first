Imports System.Data.SqlClient
Imports System.IO

Public Class paintings
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Dim item As String

    Public Sub paintings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Panel2.Visible = False
        con.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lenovo\source\repos\se\se\signup.mdf;Integrated Security=True"
        con.Open()
        cmd.Connection = con
        cmd.CommandText = "select product from product where category='Paintings'"
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
        cmd2.CommandText = "select startingbid,currentbid,validity,minincrement,productphoto from product where product=" & "(@product)"
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

            Label6.Text = dr2("minincrement")
            DateTimePicker2.Value = dr2("validity")

            imgByte = dr2("productphoto")
            Dim ms As New MemoryStream(imgByte, True)
            PictureBox2.Image = Bitmap.FromStream(ms)

        Loop
        dr2.Close()
        con.Close()

    End Sub

    Public Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        con.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lenovo\source\repos\se\se\signup.mdf;Integrated Security=True"
        con.Open()
        cmd.Connection = con
        cmd.CommandText = "update product set currentbid=@currentbid, buyer=@username where Product=" & "(@product)"
        Dim paramproduct As New SqlParameter("@product", SqlDbType.NChar, 20)
        paramproduct.Direction = ParameterDirection.Input
        paramproduct.Value = ComboBox1.SelectedItem
        cmd.Parameters.Add(paramproduct)
        Dim paramcbid As New SqlParameter("@currentbid", SqlDbType.Decimal, 8)
        paramcbid.Value = TextBox1.Text
        cmd.Parameters.Add(paramcbid)
        Dim paramusname As New SqlParameter("@username", SqlDbType.VarChar, 20)
        paramusname.Value = TextBox2.Text
        cmd.Parameters.Add(paramusname)
        Dim da As New SqlDataAdapter
        If DateTimePicker2.Value > Date.Now() Then
            da.UpdateCommand = cmd
            Try
                da.UpdateCommand.ExecuteNonQuery()

            Catch ex As Exception
                MsgBox("Invalid Username!")
                Me.Close()
            End Try
            If TextBox1.Text < (Label14.Text + Label6.Text) Then
                MsgBox("Bid value should be greater")
                con.Close()
                MsgBox("Please close and open again")
            Else

                MsgBox("Bade succesfully")
                con.Close()
                payment.Show()

            End If

        Else
            MsgBox("Product Expired")
            con.Close()
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Panel1.Enabled = True
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Me.Close()
        Form1.Show()
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        dashboard.Show()
        Me.Close()
    End Sub
End Class