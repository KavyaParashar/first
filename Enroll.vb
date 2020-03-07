Imports System.Data.SqlClient
Imports System.IO
Public Class Enroll
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Dim da As New SqlDataAdapter

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim constring As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lenovo\source\repos\se\se\signup.mdf;Integrated Security=True"
        con = New SqlConnection(constring)
        con.Open()
        cmd.Connection = con
        cmd.CommandText = "INSERT INTO product (productid,category,product,username,startingbid,validity,productphoto,minincrement) VALUES (@productid, @category, @product, @username, @startingbid, @validity,@productphoto,@minincrement)"
        Dim parampid As New SqlParameter("@productid", SqlDbType.VarChar, 20)
        parampid.Value = TextBox1.Text
        Dim paramcat As New SqlParameter("@category", SqlDbType.NChar, 10)
        paramcat.Value = ComboBox1.SelectedItem
        Dim parampname As New SqlParameter("@product", SqlDbType.NChar, 20)
        parampname.Value = TextBox2.Text
        Dim paramusname As New SqlParameter("@username", SqlDbType.VarChar, 20)
        paramusname.Value = TextBox5.Text
        Dim paramsbid As New SqlParameter("@startingbid", SqlDbType.Decimal, 8)
        paramsbid.Value = TextBox3.Text
        Dim paramvalidity As New SqlParameter("@validity", SqlDbType.DateTime)
        paramvalidity.Value = DateTimePicker1.Value
        Dim paraminc As New SqlParameter("@minincrement", SqlDbType.Int)
        paraminc.Value = TextBox4.Text
        Dim ms As New MemoryStream
        PictureBox1.Image.Save(ms, PictureBox1.Image.RawFormat)
        Dim parampic As New SqlParameter("@productphoto", SqlDbType.VarBinary)
        parampic.Value = ms.ToArray()
        cmd.Parameters.Add(parampic)
        cmd.Parameters.Add(parampid)
        cmd.Parameters.Add(paramcat)
        cmd.Parameters.Add(parampname)
        cmd.Parameters.Add(paramusname)
        cmd.Parameters.Add(paramsbid)
        cmd.Parameters.Add(paraminc)
        cmd.Parameters.Add(paramvalidity)


        da.InsertCommand = cmd

        Try
            da.InsertCommand.ExecuteNonQuery()
            MsgBox("Inserted succesfully")
            dashboard.Show()
            Me.Close()
        Catch ex As Exception
            MsgBox("Something is wrong please Try Again !!")
            dashboard.Show()
            Me.Close()
        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim opf As New OpenFileDialog
        opf.Filter = "Choose Image(*.JPG;*.PNG;*.GIF)|*.jpg;*.png;*.gif"

        If opf.ShowDialog = Windows.Forms.DialogResult.OK Then

            PictureBox1.Image = Image.FromFile(opf.FileName)

        End If
    End Sub
    Private Sub Enroll_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.Items.Add("Paintings")
        ComboBox1.Items.Add("Antiques")
        ComboBox1.Items.Add("Handicrafts")
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Me.Close()
        dashboard.Show()
    End Sub
End Class