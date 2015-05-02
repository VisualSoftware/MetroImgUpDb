Imports DropNet
Imports MetroFramework
Imports System.IO
Imports TweetSharp
Imports System.Net

Public Class DBUploader
    'PUT YOUR DROPBOX/TWITTER API KEYS HERE:
    Dim client As New DropNetClient("YOURAPIKEYHERE", "YOURAPPSECRETHERE")
    Friend twitterclient As New TwitterService("YOURCONSUMERKEYHERE", "YOURCONSUMERSECRETHERE")
    Dim usertoken As String
    Dim usersecret As String
    Dim loggedin As Integer
    Dim twitterloggedin As Integer
    Dim twitterusertoken As String
    Dim twitterusersecret As String
    Dim errprc As String
    Dim errprc2 As String
    Dim errprc3 As String
    Dim filextension123 As String
    Dim newftpfilename As String
    Dim rootfolder As String = "/METROIMGUP/"
    Dim nolocalfile As Integer
    Public verifier As String
    Dim VSTools As VSSharedSource = New VSSharedSource
    Dim resUri As String

    Private Sub DBUploader_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            MetroLabel12.Text = ""
            If IO.Directory.Exists("Data") Then

            Else
                IO.Directory.CreateDirectory("Data")
            End If
            If My.Application.Info.CompanyName = "Visual Software" Then
                If Now.Year > 2013 Then
                    linklabel1.Text = My.Application.Info.AssemblyName & " © 2013-" & Now.Year & " " & My.Application.Info.CompanyName
                Else
                    linklabel1.Text = My.Application.Info.AssemblyName & " © 2013" & " " & My.Application.Info.CompanyName
                End If
            Else
                MessageBox.Show("Error, This application has been modified", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End If
            If IO.File.Exists("Data\metroimgupdb.log") = True Then
                Dim newentryline As String
                Dim logread As String
                newentryline = "################################ -= " & Now.Day & "/" & Now.Month & "/" & Now.Year & " =- ################################" & vbCrLf
                logread = IO.File.ReadAllText("Data\metroimgupdb.log")
                If logread.Contains(newentryline) Then

                Else
                    My.Computer.FileSystem.WriteAllText("Data\metroimgupdb.log", newentryline, True)
                End If
            Else
                MetroButton6.Enabled = False
            End If
            Me.Text = Me.Text & " - v" & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor
            label8.Text = "Version " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Build & "." & My.Application.Info.Version.Revision
            'PNG DELETION
            Dim myFile As String
            Dim mydir As String = My.Application.Info.DirectoryPath & "\Data\"
            For Each myFile In Directory.GetFiles(mydir, "*.png")
                File.Delete(myFile)
            Next
            'END OF DELETION
            If IO.File.Exists("Data\Temp.utk") Then
                MetroButton24.Enabled = True
                MetroButton23.Enabled = False
                usertoken = IO.File.ReadAllText("Data\Temp.utk")
                usersecret = IO.File.ReadAllText("Data\Temp.usc")
                loggedin = 1
                client.UserLogin = New DropNet.Models.UserLogin() With {
                    .Token = usertoken,
                    .Secret = usersecret
               }
                BackgroundWorker4.RunWorkerAsync()
            Else
                MetroButton24.Enabled = False
                MetroButton23.Enabled = True
            End If
            If IO.File.Exists("Data\Temp1.utk") Then
                MetroButton26.Enabled = True
                MetroButton25.Enabled = False
                twitterusertoken = IO.File.ReadAllText("Data\Temp1.utk")
                twitterusersecret = IO.File.ReadAllText("Data\Temp1.usc")
                twitterloggedin = 1
                twitterclient.AuthenticateWith(twitterusertoken, twitterusersecret)
                BackgroundWorker5.RunWorkerAsync()
            Else
                MetroButton26.Enabled = False
                MetroButton25.Enabled = True
            End If
            Try
                If IO.File.Exists(My.Application.Info.DirectoryPath & "\Setup.upd") = True Then
                    If IO.File.Exists(My.Application.Info.DirectoryPath & "\Setup.exe") = True Then
                        IO.File.Delete(My.Application.Info.DirectoryPath & "\Setup.exe")
                    Else

                    End If
                    My.Computer.FileSystem.RenameFile(My.Application.Info.DirectoryPath & "\Setup.upd", "Setup.exe")
                End If
            Catch ex As Exception

            End Try
        Catch ex As Exception
            MessageBox.Show("Critical Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MetroButton1_Click(sender As Object, e As EventArgs) Handles MetroButton1.Click
        nolocalfile = 0
        If TextBox1.Text = "" Then
            MessageBox.Show("Error: No file selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            TextBox1.ReadOnly = True
            MetroButton3.Enabled = False
            MetroButton22.Enabled = False
            MetroButton5.Enabled = False
            MetroButton7.Enabled = False
            MetroButton8.Enabled = False
            MetroButton9.Enabled = False
            MetroButton1.Visible = False
            MetroButton2.Visible = False
            MetroProgressSpinner1.Value = 0
            MetroProgressSpinner1.Visible = True
            MetroLabel3.Visible = True
            errprc = ""
            Timer1.Enabled = True
            TextBox1.AllowDrop = False
            If TextBox1.Text.Contains("http://") = True Then
                nolocalfile = 1
            ElseIf TextBox1.Text.Contains("https://") = True Then
                nolocalfile = 1
            Else

            End If
            If loggedin = 1 Then

            Else
                client.GetToken()
                GetDBToken.authlink = client.BuildAuthorizeUrl()
                GetDBToken.servicename = "Dropbox"
                GetDBToken.ShowDialog()
                client.GetAccessToken()
                IO.File.WriteAllText("Data\Temp.utk", client.UserLogin.Token)
                IO.File.WriteAllText("Data\Temp.usc", client.UserLogin.Secret)
                loggedin = 1
                MetroButton24.Enabled = True
                MetroButton23.Enabled = False
                BackgroundWorker4.RunWorkerAsync()
            End If
            BackgroundWorker1.RunWorkerAsync()
        End If
    End Sub

    Private Sub MetroButton2_Click(sender As Object, e As EventArgs) Handles MetroButton2.Click
        End
    End Sub

    Private Sub MetroButton3_Click(sender As Object, e As EventArgs) Handles MetroButton3.Click
        OpenFileDialog1.ShowDialog()

        If OpenFileDialog1.FileName = "" Then

        Else
            TextBox1.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub MetroButton4_Click(sender As Object, e As EventArgs) Handles MetroButton4.Click
        Clipboard.SetText(MetroTextBox1.Text)
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text = "" Then
            MetroButton1.Enabled = False
        Else
            MetroButton1.Enabled = True
        End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            If nolocalfile = 0 Then
                Dim FileSZ As FileInfo = New FileInfo(TextBox1.Text)
                filextension123 = FileSZ.Extension
                newftpfilename = MD5CalcFile(TextBox1.Text) & filextension123
                client.UploadFile(rootfolder, newftpfilename, IO.File.ReadAllBytes(TextBox1.Text))
            Else
                My.Computer.Network.DownloadFile(TextBox1.Text, "Data\TempImg_" & TextBox1.Text.Remove(0, TextBox1.Text.Length - 5), "", "", True, "10000000", True)
                Dim newinternetfile As String = My.Application.Info.DirectoryPath & "\Data\TempImg_" & TextBox1.Text.Remove(0, TextBox1.Text.Length - 5)
                Dim FileSZ As FileInfo = New FileInfo(newinternetfile)
                filextension123 = FileSZ.Extension
                newftpfilename = MD5CalcFile(newinternetfile) & filextension123
                client.UploadFile(rootfolder, newftpfilename, IO.File.ReadAllBytes(newinternetfile))
                IO.File.Delete(newinternetfile)
            End If
        Catch ex As Exception
            errprc = "1"
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MetroButton6_Click(sender As Object, e As EventArgs) Handles MetroButton6.Click
        If IO.File.Exists("Data\metroimgupdb.log") = True Then
            Process.Start("Data\metroimgupdb.log")
        Else
            MessageBox.Show("Error, the log file does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' specify the path to a file and this routine will calculate your hash
    Public Function MD5CalcFile(ByVal filepath As String) As String

        ' open file (as read-only)
        Using reader As New System.IO.FileStream(filepath, IO.FileMode.Open, IO.FileAccess.Read)
            Using md5 As New System.Security.Cryptography.MD5CryptoServiceProvider

                ' hash contents of this stream
                Dim hash() As Byte = md5.ComputeHash(reader)

                ' return formatted hash
                Return ByteArrayToString(hash)

            End Using
        End Using

    End Function

    ' utility function to convert a byte array into a hex string
    Private Function ByteArrayToString(ByVal arrInput() As Byte) As String

        Dim sb As New System.Text.StringBuilder(arrInput.Length * 2)

        For i As Integer = 0 To arrInput.Length - 1
            sb.Append(arrInput(i).ToString("X2"))
        Next

        Return sb.ToString().ToLower

    End Function

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        MetroTextBox2.Text = ""
        If errprc = "1" Then
            MetroLabel5.Text = "Upload Error"
            PictureBox2.Image = PictureBox4.Image
        Else
            MetroTextBox2.Text = newftpfilename
            MetroLabel5.Text = "Upload Successful"
            PictureBox2.Image = PictureBox3.Image
            MetroTextBox1.Text = client.GetShare(rootfolder & newftpfilename).Url
            Clipboard.SetText(MetroTextBox1.Text)
            Dim req As HttpWebRequest = DirectCast(HttpWebRequest.Create(MetroTextBox1.Text), HttpWebRequest)
            Dim Res As HttpWebResponse = req.GetResponse()
            Dim response As HttpWebResponse
            response = req.GetResponse
            resUri = response.ResponseUri.AbsoluteUri
            My.Computer.FileSystem.WriteAllText("Data\metroimgupdb.log", MetroTextBox1.Text & vbCrLf, True)
            MetroButton6.Enabled = True
            TextBox1.Text = ""
        End If
        PictureBox2.Visible = True
        MetroLabel5.Visible = True
        TextBox1.ReadOnly = False
        MetroButton3.Enabled = True
        MetroButton22.Enabled = True
        MetroButton5.Enabled = True
        MetroButton7.Enabled = True
        MetroButton8.Enabled = True
        MetroButton9.Enabled = True
        MetroButton1.Visible = True
        MetroButton2.Visible = True
        MetroProgressSpinner1.Visible = False
        MetroLabel3.Visible = False
        Timer1.Enabled = False
        TextBox1.AllowDrop = True
        Timer2.Enabled = True
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If MetroProgressSpinner1.Value = "100" Then
            MetroProgressSpinner1.Value = "0"
            MetroProgressSpinner1.Value = MetroProgressSpinner1.Value + 1
        Else
            MetroProgressSpinner1.Value = MetroProgressSpinner1.Value + 1
        End If
        If MetroProgressSpinner2.Value = "100" Then
            MetroProgressSpinner2.Value = "0"
            MetroProgressSpinner2.Value = MetroProgressSpinner2.Value + 1
        Else
            MetroProgressSpinner2.Value = MetroProgressSpinner2.Value + 1
        End If
    End Sub

    Private Sub MetroButton5_Click(sender As Object, e As EventArgs) Handles MetroButton5.Click
        MetroButton5.Enabled = False
        ImageClipboard.Show()
    End Sub

    Private Sub MetroTextBox1_TextChanged(sender As Object, e As EventArgs) Handles MetroTextBox1.TextChanged
        If MetroTextBox1.Text = "" Then
            MetroButton4.Enabled = False
            MetroButton21.Enabled = False
        Else
            MetroButton4.Enabled = True
            MetroButton21.Enabled = True
        End If
    End Sub

    Private Sub MetroButton7_Click(sender As Object, e As EventArgs) Handles MetroButton7.Click
        ImageClipboard.Button4_Click(sender, e)
    End Sub

    Private Sub MetroButton8_Click(sender As Object, e As EventArgs) Handles MetroButton8.Click
        ImageClipboard.Button1_Click(sender, e)
    End Sub

    Private Sub MetroButton9_Click(sender As Object, e As EventArgs) Handles MetroButton9.Click
        ImageClipboard.Button4_Click(sender, e)
    End Sub

    Private Sub linklabel1_Click(sender As Object, e As EventArgs) Handles linklabel1.Click
        Process.Start("http://visualsoftware.wordpress.com")
    End Sub

    Private Sub TextBox1_DragEnter(sender As Object, e As DragEventArgs) Handles TextBox1.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        End If
    End Sub

    Private Sub TextBox1_DragDrop(sender As Object, e As DragEventArgs) Handles TextBox1.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim MyFiles() As String
            Dim i As Integer
            ' Assign the files to an array.
            MyFiles = e.Data.GetData(DataFormats.FileDrop)
            ' Loop through the array and add the files to the list.
            For i = 0 To MyFiles.Length - 1
                TextBox1.Text = (MyFiles(i))
            Next
        End If
    End Sub

    Private Sub MetroCheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles MetroCheckBox1.CheckedChanged
        If MetroTextBox2.Text = "" Then
            MetroButton10.Enabled = False
        Else
            If MetroCheckBox1.Checked = True Then
                MetroButton10.Enabled = True
            Else
                MetroButton10.Enabled = False
            End If
        End If
    End Sub

    Private Sub MetroTextBox2_TextChanged(sender As Object, e As EventArgs) Handles MetroTextBox2.TextChanged
        If MetroTextBox2.Text = "" Then
            MetroButton10.Enabled = False
        Else
            If MetroCheckBox1.Checked = True Then
                MetroButton10.Enabled = True
            Else
                MetroButton10.Enabled = False
            End If
        End If
    End Sub

    Private Sub MetroButton11_Click(sender As Object, e As EventArgs) Handles MetroButton11.Click
        End
    End Sub

    Private Sub MetroButton10_Click(sender As Object, e As EventArgs) Handles MetroButton10.Click
        MetroTextBox2.ReadOnly = True
        MetroButton10.Enabled = False
        MetroCheckBox1.Enabled = False
        MetroButton12.Enabled = False
        errprc2 = "0"
        If loggedin = 1 Then

        Else
            client.GetToken()
            GetDBToken.authlink = client.BuildAuthorizeUrl()
            GetDBToken.servicename = "Dropbox"
            GetDBToken.ShowDialog()
            client.GetAccessToken()
            IO.File.WriteAllText("Data\Temp.utk", client.UserLogin.Token)
            IO.File.WriteAllText("Data\Temp.usc", client.UserLogin.Secret)
            loggedin = 1
            MetroButton24.Enabled = True
            BackgroundWorker4.RunWorkerAsync()
        End If
        BackgroundWorker2.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorker2_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Try
            client.Delete(rootfolder & MetroTextBox2.Text)
        Catch ex As Exception
            errprc2 = "1"
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BackgroundWorker2_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker2.RunWorkerCompleted
        MetroTextBox2.ReadOnly = False
        MetroButton10.Enabled = True
        MetroButton12.Enabled = True
        MetroCheckBox1.Enabled = True
        If errprc2 = "1" Then
            MetroLabel7.Text = "Deletion Error"
            PictureBox6.Image = PictureBox4.Image
        Else
            MetroLabel7.Text = "File Successfully deleted"
            PictureBox6.Image = PictureBox5.Image
            MetroTextBox2.Text = ""
            MetroCheckBox1.Checked = False
        End If
        PictureBox6.Visible = True
        MetroLabel7.Visible = True
        Timer2.Enabled = True
    End Sub

    Private Sub MetroButton12_Click(sender As Object, e As EventArgs) Handles MetroButton12.Click
        MetroTextBox2.Text = Clipboard.GetText()
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        PictureBox2.Visible = False
        MetroLabel5.Visible = False
        PictureBox6.Visible = False
        MetroLabel7.Visible = False
        PictureBox7.Visible = False
        MetroLabel8.Visible = False
        Timer2.Enabled = False
    End Sub

    Private Sub MetroButton14_Click(sender As Object, e As EventArgs) Handles MetroButton14.Click
        MetroPanel1.Visible = True
    End Sub

    Private Sub MetroButton17_Click(sender As Object, e As EventArgs) Handles MetroButton17.Click
        MetroPanel1.Visible = False
    End Sub

    Private Sub MetroButton18_Click(sender As Object, e As EventArgs) Handles MetroButton18.Click
        OpenFileDialog2.ShowDialog()
        If OpenFileDialog2.FileName = "" Then

        Else
            ListBox1.Items.AddRange(OpenFileDialog2.FileNames)
        End If
    End Sub

    Private Sub MetroButton15_Click(sender As Object, e As EventArgs) Handles MetroButton15.Click
        ListBox1.Items.Remove(ListBox1.SelectedItem)
        If ListBox1.Items.Count > 0 Then
            ListBox1.SelectedIndex = "0"
        Else

        End If
    End Sub

    Private Sub MetroButton19_Click(sender As Object, e As EventArgs) Handles MetroButton19.Click
        If ListBox1.SelectedIndex > 0 Then
            Dim I = ListBox1.SelectedIndex - 1
            ListBox1.Items.Insert(I, ListBox1.SelectedItem)
            ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
            ListBox1.SelectedIndex = I
        End If
    End Sub

    Private Sub MetroButton20_Click(sender As Object, e As EventArgs) Handles MetroButton20.Click
        'Make sure our item is not the last one on the list.
        If ListBox1.SelectedIndex < ListBox1.Items.Count - 1 Then
            'Insert places items above the index you supply, since we want
            'to move it down the list we have to do + 2
            Dim I = ListBox1.SelectedIndex + 2
            ListBox1.Items.Insert(I, ListBox1.SelectedItem)
            ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
            ListBox1.SelectedIndex = I - 1
        End If
    End Sub

    Private Sub ListBox1_DragDrop(sender As Object, e As DragEventArgs) Handles ListBox1.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim MyFiles() As String
            Dim i As Integer
            ' Assign the files to an array.
            MyFiles = e.Data.GetData(DataFormats.FileDrop)
            ' Loop through the array and add the files to the list.
            For i = 0 To MyFiles.Length - 1
                ListBox1.Items.Add((MyFiles(i)))
            Next
        End If
    End Sub

    Private Sub ListBox1_DragEnter(sender As Object, e As DragEventArgs) Handles ListBox1.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        End If
    End Sub

    Private Sub MetroButton16_Click(sender As Object, e As EventArgs) Handles MetroButton16.Click
        If ListBox1.Items.Count = 0 Then
            MessageBox.Show("Error: No files to upload", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            ListBox1.Enabled = False
            ListBox1.AllowDrop = False
            MetroButton16.Visible = False
            MetroButton17.Visible = False
            MetroButton18.Enabled = False
            MetroButton15.Enabled = False
            MetroButton19.Enabled = False
            MetroButton20.Enabled = False
            MetroProgressSpinner2.Value = 0
            MetroProgressSpinner2.Visible = True
            MetroLabel10.Visible = True
            multilog.Text = ""
            errprc3 = "0"
            Timer1.Enabled = True
            If loggedin = 1 Then

            Else
                client.GetToken()
                GetDBToken.authlink = client.BuildAuthorizeUrl()
                GetDBToken.servicename = "Dropbox"
                GetDBToken.ShowDialog()
                client.GetAccessToken()
                IO.File.WriteAllText("Data\Temp.utk", client.UserLogin.Token)
                IO.File.WriteAllText("Data\Temp.usc", client.UserLogin.Secret)
                loggedin = 1
                MetroButton24.Enabled = True
                BackgroundWorker4.RunWorkerAsync()
            End If
            Control.CheckForIllegalCrossThreadCalls = False
            BackgroundWorker3.RunWorkerAsync()
        End If
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        If ListBox1.SelectedItem = "" Then
            MetroButton15.Enabled = False
        Else
            MetroButton15.Enabled = True
            If ListBox1.SelectedIndex > 0 Then
                MetroButton19.Enabled = True
            Else
                MetroButton19.Enabled = False
            End If
            If ListBox1.SelectedIndex < ListBox1.Items.Count - 1 Then
                MetroButton20.Enabled = True
            Else
                MetroButton20.Enabled = False
            End If
        End If
        If ListBox1.Items.Count = 0 Then
            MetroButton16.Enabled = False
        Else
            MetroButton16.Enabled = True
        End If
    End Sub

    Private Sub BackgroundWorker3_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker3.DoWork
        Do While ListBox1.Items.Count > 0
            'CALCULATE CRC32
            Dim actfile As String
            Try
                actfile = ListBox1.Items(0)
                Dim FileSZ As FileInfo = New FileInfo(actfile)
                filextension123 = FileSZ.Extension
                newftpfilename = MD5CalcFile(actfile) & filextension123
                'UPLOAD FILE
                client.UploadFile(rootfolder, newftpfilename, IO.File.ReadAllBytes(actfile))
                'WRITE TO LOG
                multilog.Text = multilog.Text & client.GetShare(rootfolder & newftpfilename).Url & vbCrLf
                'DELETE ITEM
                ListBox1.Items.RemoveAt(0)
            Catch ex As Exception
                errprc3 = "1"
                MessageBox.Show(ex.Message)
                Exit Do
            End Try
        Loop
    End Sub

    Private Sub BackgroundWorker3_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker3.RunWorkerCompleted
        My.Computer.FileSystem.WriteAllText("Data\metroimgupdb.log", multilog.Text & vbCrLf, True)
        MetroButton6.Enabled = True
        Clipboard.SetText(multilog.Text)
        If errprc3 = "1" Then
            MetroLabel8.Text = "Upload Error"
            PictureBox7.Image = PictureBox4.Image
        Else
            MetroLabel8.Text = "Upload Successful"
            PictureBox7.Image = PictureBox3.Image
        End If
        ListBox1.Enabled = True
        ListBox1.AllowDrop = True
        MetroLabel8.Visible = True
        PictureBox7.Visible = True
        MetroButton16.Visible = True
        MetroButton17.Visible = True
        MetroButton18.Enabled = True
        MetroButton15.Enabled = True
        MetroButton19.Enabled = True
        MetroButton20.Enabled = True
        MetroProgressSpinner2.Visible = False
        MetroLabel10.Visible = False
        Timer1.Enabled = False
        Timer2.Enabled = True
    End Sub

    Private Sub BackgroundWorker4_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker4.DoWork
        Control.CheckForIllegalCrossThreadCalls = False
        Dim maxquota As String
        maxquota = Val(client.AccountInfo.quota_info.quota) \ 1024 \ 1024
        Dim currentquota As String
        currentquota = Val(Val(client.AccountInfo.quota_info.normal) + Val(client.AccountInfo.quota_info.shared)) \ 1024 \ 1024
        MetroLabel4.Text = "Logged-In as: " & client.AccountInfo.display_name & " (" & client.AccountInfo.email & ")"
        If currentquota > 999 Then
            If maxquota > 999 Then
                MetroLabel12.Text = Math.Round(Val(currentquota) / 1024, 2) & " GB / " & Math.Round(Val(maxquota) / 1024, 2) & " GB"
            Else
                MetroLabel12.Text = Math.Round(Val(currentquota) / 1024, 2) & " GB / " & maxquota & " MB"
            End If
        Else
            If maxquota > 999 Then
                MetroLabel12.Text = currentquota & " MB / " & Math.Round(Val(maxquota) / 1024, 2) & " GB"
            Else
                MetroLabel12.Text = currentquota & " MB / " & maxquota & " MB"
            End If
        End If
    End Sub

    Private Sub MetroButton22_Click(sender As Object, e As EventArgs) Handles MetroButton22.Click
        TextBox1.Text = Clipboard.GetText()
    End Sub

    Private Sub MetroButton21_Click(sender As Object, e As EventArgs) Handles MetroButton21.Click
        If twitterloggedin = 1 Then

        Else
            Dim requestToken As OAuthRequestToken = twitterclient.GetRequestToken()
            GetDBToken.authlink = twitterclient.GetAuthorizationUri(requestToken).ToString()
            GetDBToken.servicename = "Twitter"
            GetDBToken.ShowDialog()
            Dim access As OAuthAccessToken = twitterclient.GetAccessToken(requestToken, verifier)
            twitterclient.AuthenticateWith(access.Token, access.TokenSecret)
            IO.File.WriteAllText("Data\Temp1.utk", access.Token)
            IO.File.WriteAllText("Data\Temp1.usc", access.TokenSecret)
            twitterloggedin = 1
            MetroButton25.Enabled = False
            MetroButton26.Enabled = True
            BackgroundWorker5.RunWorkerAsync()
        End If
        SendTweet.MetroTextBox1.Text = MetroTextBox1.Text
        MetroButton21.Enabled = False
        SendTweet.Show()
    End Sub

    Private Sub BackgroundWorker5_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker5.DoWork
        Control.CheckForIllegalCrossThreadCalls = False
        Dim twtuser As New TwitterUser
        Dim twtcred As New VerifyCredentialsOptions
        twtuser = twitterclient.VerifyCredentials(twtcred)
        MetroLabel13.Text = "Twitter Account: @" & twtuser.ScreenName
        If twtuser.IsProtected = True Then
            PictureBox8.Visible = True
        Else
            PictureBox8.Visible = False
        End If
        If twtuser.IsTranslator = True Then
            PictureBox9.Visible = True
        Else
            PictureBox9.Visible = False
        End If
        If twtuser.IsVerified = True Then
            PictureBox10.Visible = True
        Else
            PictureBox10.Visible = False
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        GetDBToken.servicename = "Test"
        GetDBToken.ShowDialog()
    End Sub

    Private Sub MetroButton24_Click(sender As Object, e As EventArgs) Handles MetroButton24.Click
        If IO.File.Exists("Data\Temp.utk") = True Then
            IO.File.Delete("Data\Temp.utk")
            IO.File.Delete("Data\Temp.usc")
        Else

        End If
        MessageBox.Show("Successfully removed Dropbox stored login details!.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        MetroButton24.Enabled = False
        MetroButton23.Enabled = True
        loggedin = 0
        MetroLabel4.Text = "Not Logged-In"
        MetroLabel12.Text = ""
    End Sub

    Private Sub MetroButton26_Click(sender As Object, e As EventArgs) Handles MetroButton26.Click
        If IO.File.Exists("Data\Temp1.utk") = True Then
            IO.File.Delete("Data\Temp1.utk")
            IO.File.Delete("Data\Temp1.usc")
        Else

        End If
        MessageBox.Show("Successfully removed Twitter stored login details!.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        twitterloggedin = 0
        MetroLabel13.Text = "Not connected to Twitter"
        PictureBox8.Visible = False
        PictureBox9.Visible = False
        PictureBox10.Visible = False
        MetroButton26.Enabled = False
        MetroButton25.Enabled = True
    End Sub

    Private Sub MetroButton23_Click(sender As Object, e As EventArgs) Handles MetroButton23.Click
        Try
            client.GetToken()
            GetDBToken.authlink = client.BuildAuthorizeUrl()
            GetDBToken.servicename = "Dropbox"
            GetDBToken.ShowDialog()
            client.GetAccessToken()
            IO.File.WriteAllText("Data\Temp.utk", client.UserLogin.Token)
            IO.File.WriteAllText("Data\Temp.usc", client.UserLogin.Secret)
            loggedin = 1
            MetroButton24.Enabled = True
            MetroButton23.Enabled = False
            BackgroundWorker4.RunWorkerAsync()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MetroButton25_Click(sender As Object, e As EventArgs) Handles MetroButton25.Click
        Try
            Dim requestToken As OAuthRequestToken = twitterclient.GetRequestToken()
            GetDBToken.authlink = twitterclient.GetAuthorizationUri(requestToken).ToString()
            GetDBToken.servicename = "Twitter"
            GetDBToken.ShowDialog()
            Dim access As OAuthAccessToken = twitterclient.GetAccessToken(requestToken, verifier)
            twitterclient.AuthenticateWith(access.Token, access.TokenSecret)
            IO.File.WriteAllText("Data\Temp1.utk", access.Token)
            IO.File.WriteAllText("Data\Temp1.usc", access.TokenSecret)
            twitterloggedin = 1
            MetroButton26.Enabled = True
            MetroButton25.Enabled = False
            BackgroundWorker5.RunWorkerAsync()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Timer4_Tick(sender As Object, e As EventArgs) Handles Timer4.Tick
        If BackgroundWorker6.IsBusy = True Then

        Else
            BackgroundWorker6.RunWorkerAsync()
        End If
    End Sub

    Private Sub BackgroundWorker6_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker6.DoWork

    End Sub

    Private Sub PictureBox13_Click(sender As Object, e As EventArgs) Handles PictureBox13.Click
        Process.Start("https://www.twitter.com/VisualSoftCorp")
    End Sub

    Private Sub PictureBox14_Click(sender As Object, e As EventArgs) Handles PictureBox14.Click
        VSTools.OpenDonationPage()
    End Sub

    Private Sub TakeAFullScreenshotOfAllScreensToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TakeAFullScreenshotOfAllScreensToolStripMenuItem.Click
        ImageClipboard.Button2_Click(sender, e)
    End Sub

    Private Sub CopyDirectLinkToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyDirectLinkToolStripMenuItem.Click
        Clipboard.SetText(resUri & "?dl=1")
    End Sub

    Private Sub CopyBBCodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyBBCodeToolStripMenuItem.Click
        Clipboard.SetText("[IMG]" & resUri & "?dl=1[/IMG]")
    End Sub

    Private Sub MetroButton13_Click(sender As Object, e As EventArgs) Handles MetroButton13.Click

    End Sub
End Class
