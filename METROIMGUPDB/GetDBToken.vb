Public Class GetDBToken

    Public authlink As String
    Public servicename As String

    Private Sub WebBrowser1_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs)
    End Sub

    Private Sub GetDBToken_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Clipboard.SetText(authlink)
        Try
            If servicename = "Dropbox" Then
                twitterpanel.Visible = False
                MetroButton2.Enabled = True
                Process.Start(authlink)
            ElseIf servicename = "Twitter" Then
                twitterpanel.Visible = True
                MetroButton2.Enabled = False
                TextBox1.Text = ""
                Process.Start(authlink)
            ElseIf servicename = "Test" Then

            Else

            End If
        Catch ex As Exception
            MessageBox.Show("Error: The authorization page can't be opened. Open your favorite browser and go to: " & authlink & " BEFORE clicking in OK. We also copied the link to your clipboard", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub MetroButton1_Click(sender As Object, e As EventArgs) Handles MetroButton1.Click
        Clipboard.SetText(authlink)
        MessageBox.Show("Copied!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub MetroButton2_Click(sender As Object, e As EventArgs) Handles MetroButton2.Click
        If servicename = "Twitter" Then
            DBUploader.verifier = TextBox1.Text
        Else

        End If
        Me.Close()
    End Sub

    Private Sub MetroButton3_Click(sender As Object, e As EventArgs)
        DBUploader.BackgroundWorker1.CancelAsync()
        DBUploader.BackgroundWorker2.CancelAsync()
        DBUploader.BackgroundWorker3.CancelAsync()
        Me.Close()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text = "" Then
            MetroButton2.Enabled = False
        Else
            MetroButton2.Enabled = True
        End If
    End Sub

    Private Sub MetroButton12_Click(sender As Object, e As EventArgs) Handles MetroButton12.Click
        TextBox1.Text = Clipboard.GetText()
    End Sub

    Private Sub GenError()
        Throw New NotImplementedException
    End Sub

End Class