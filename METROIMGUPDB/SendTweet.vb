Public Class SendTweet

    Dim errmsg As Integer

    Private Sub MetroTextBox1_TextChanged(sender As Object, e As EventArgs) Handles MetroTextBox1.TextChanged
        If MetroTextBox1.Text = "" Then
            MetroButton1.Enabled = False
        Else
            MetroButton1.Enabled = True
        End If
        MetroLabel1.Text = 140 - MetroTextBox1.Text.Length & " characters left"
        If MetroTextBox1.Text.Length > 140 Then
            MetroLabel1.ForeColor = Color.Red
        Else
            MetroLabel1.ForeColor = Color.Black
        End If
    End Sub

    Private Sub MetroButton1_Click(sender As Object, e As EventArgs) Handles MetroButton1.Click
        Control.CheckForIllegalCrossThreadCalls = False
        If MetroTextBox1.Text.Length > 140 Then
            MessageBox.Show("Error: Your tweet is too long", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Dim tweetsender As New TweetSharp.SendTweetOptions
            tweetsender.Status = MetroTextBox1.Text
            DBUploader.twitterclient.SendTweet(tweetsender)
            Me.Close()
        End If
    End Sub

    Private Sub MetroButton2_Click(sender As Object, e As EventArgs) Handles MetroButton2.Click
        Me.Close()
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            Dim tweetsender As New TweetSharp.SendTweetOptions
            tweetsender.Status = MetroTextBox1.Text
            DBUploader.twitterclient.SendTweet(tweetsender)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            errmsg = 1
        End Try
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        MetroButton1.Enabled = True
        MetroButton2.Enabled = True
        MetroTextBox1.ReadOnly = False
        If errmsg = 1 Then
            errmsg = 0
        Else
            Me.Close()
        End If
    End Sub

    Private Sub SendTweet_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DBUploader.MetroButton21.Enabled = True
    End Sub
End Class