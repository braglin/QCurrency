Public Class Coin
    Public coin = ""
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PictureBox1.ImageLocation = Application.StartupPath + "/img/" + coin + ".png"
        Update()
    End Sub

    Public Sub Update()
        Try
            Dim sourceString As String = New System.Net.WebClient().DownloadString("https://api.coinmarketcap.com/v1/ticker/" + coin)
            Dim data() As String = sourceString.Replace(Chr(13), "").Split(Chr(10))

            currentLbl.Text = Mid(data(6).Replace("""", ""), 20, data(6).Replace("""", "").Length - 21)
            onehourLbl.Text = Mid(data(13).Replace("""", ""), 28, data(13).Replace("""", "").Length - 29)
            onedayLbl.Text = Mid(data(14).Replace("""", ""), 29, data(14).Replace("""", "").Length - 30)

            onedayLbl.ForeColor = Color.YellowGreen
            onehourLbl.ForeColor = Color.YellowGreen
            If onedayLbl.Text.Contains("-") Then onedayLbl.ForeColor = Color.Red
            If onehourLbl.Text.Contains("-") Then onehourLbl.ForeColor = Color.Red

            'For Each ctrl As Control In Me.Controls
            '    If ctrl.Name = "onehourLbl" Or ctrl.Name = "onedayLbl" Then
            '        If ctrl.Text.Contains("-") Then
            '            ctrl.ForeColor = Color.Red
            '        Else
            '            ctrl.ForeColor = Color.YellowGreen
            '        End If
            '    End If
            'Next
        Catch ex As Exception
        End Try
    End Sub

#Region "Move Form and Resize (form and font)"
    Public MoveForm As Boolean
    Public MoveForm_MousePosition As Point
    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown, currentLbl.MouseDown, Label3.MouseDown, PictureBox1.MouseDown
        If e.Button = MouseButtons.Left Then
            MoveForm = True
            Me.Cursor = Cursors.NoMove2D
            MoveForm_MousePosition = e.Location
        End If
    End Sub
    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove, currentLbl.MouseMove, Label3.MouseMove, PictureBox1.MouseMove
        If MoveForm Then
            Me.Location = Me.Location + (e.Location - MoveForm_MousePosition)
        End If
    End Sub
    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp, currentLbl.MouseUp, Label3.MouseUp, PictureBox1.MouseUp
        If e.Button = MouseButtons.Left Then
            MoveForm = False
            Me.Cursor = Cursors.Default
            WriteINI(Application.StartupPath + "\QCurrency.ini", coin, "X", Me.Location.X)
            WriteINI(Application.StartupPath + "\QCurrency.ini", coin, "Y", Me.Location.Y)
        End If
    End Sub
#End Region

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        currentLbl.Text = "..."
        Update()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        WriteINI(Application.StartupPath + "\QCurrency.ini", coin, "Enabled", False)
    End Sub


    Private Declare Unicode Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringW" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Int32

    Public Sub WriteINI(ByVal INIPath As String, ByVal Section As String, ByVal KeyName As String, ByVal KeyValue As String)
        Dim Result As Integer
        Result = WritePrivateProfileString(Section, KeyName, KeyValue, INIPath)
    End Sub

End Class
