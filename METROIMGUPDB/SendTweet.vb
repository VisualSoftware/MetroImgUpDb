'============================================================================
'
'    MetroImgUpDb
'    Copyright (C) 2013 - 2015 Visual Software Corporation
'
'    Author: ASV93
'    File: SendTweet.vb
'
'    This program is free software; you can redistribute it and/or modify
'    it under the terms of the GNU General Public License as published by
'    the Free Software Foundation; either version 2 of the License, or
'    (at your option) any later version.
'
'    This program is distributed in the hope that it will be useful,
'    but WITHOUT ANY WARRANTY; without even the implied warranty of
'    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'    GNU General Public License for more details.
'
'    You should have received a copy of the GNU General Public License along
'    with this program; if not, write to the Free Software Foundation, Inc.,
'    51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
'
'============================================================================

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