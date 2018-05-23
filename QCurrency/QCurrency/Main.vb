Public Class Main
    Dim KeyName(3) As String
    Dim KeyValues(3) As String
    Dim coinList() As String = {"bitcoin", "litecoin", "ethereum", "ripple"}

    Private Sub Main_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load
        Key()
        For i As Integer = 0 To coinList.Length - 1 Step 1
            ReadINI(Application.StartupPath + "\QCurrency.ini", coinList(i), KeyName, KeyValues)
            If KeyValues(1) = "True" Then
                newCoin(coinList(i), KeyValues(2), KeyValues(3))
            End If
        Next

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            newCoin("bitcoin", 100, 100)
            WriteINI(Application.StartupPath + "\QCurrency.ini", "bitcoin", "Enabled", True)
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            newCoin("litecoin", 100, 100)
            WriteINI(Application.StartupPath + "\QCurrency.ini", "litecoin", "Enabled", True)
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked Then
            newCoin("ethereum", 100, 100)
            WriteINI(Application.StartupPath + "\QCurrency.ini", "ethereum", "Enabled", True)
        End If
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked Then
            newCoin("ripple", 100, 100)
            WriteINI(Application.StartupPath + "\QCurrency.ini", "ripple", "Enabled", True)
        End If
    End Sub

    Public Sub newCoin(coin As String, x As Integer, y As Integer)
        Dim newCoin As New Coin
        newCoin.coin = coin
        newCoin.Show()
        newCoin.Location = New Point(x, y)
    End Sub

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.FormClosed
        Me.ShowInTaskbar = False
        Me.Hide()
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Me.Visible = True
        Me.WindowState = FormWindowState.Normal
    End Sub
    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        Me.WindowState = FormWindowState.Minimized
        Me.Visible = False
        e.Cancel = True
        NotifyIcon1.Visible = True
    End Sub

#Region "INI"
    Private Declare Unicode Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringW" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Int32, ByVal lpFileName As String) As Int32
    Private Declare Unicode Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringW" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Int32

    Public Sub Key()
        KeyName(1) = "Enabled"
        KeyName(2) = "X"
        KeyName(3) = "Y"
    End Sub

    Public Sub KeyVClear()
        KeyValues(1) = ""
        KeyValues(2) = ""
        KeyValues(3) = ""
    End Sub

    Public Overloads Sub ReadINI(ByVal INIPath As String, ByVal Section As String, ByVal KeyName() As String, ByVal KeyValue() As String)
        Dim length As Integer
        Dim strData As String
        strData = Space$(100)
        For i As Integer = 1 To KeyName.Length - 1 Step 1
            If KeyName(i) <> "" Then
                length = GetPrivateProfileString(Section, KeyName(i), KeyValue(i), strData, strData.Length, LTrim(RTrim(INIPath)))
                If length > 0 Then
                    KeyValue(i) = strData.Substring(0, length)
                Else
                    KeyValue(i) = ""
                End If
            End If
        Next
    End Sub

    Public Sub WriteINI(ByVal INIPath As String, ByVal Section As String, ByVal KeyName As String, ByVal KeyValue As String)
        Dim Result As Integer
        Result = WritePrivateProfileString(Section, KeyName, KeyValue, INIPath)
    End Sub
#End Region
End Class