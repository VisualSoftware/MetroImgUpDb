'============================================================================
'
'    MetroImgUpDb
'    Copyright (C) 2013 - 2015 Visual Software Corporation
'
'    Author: ASV93
'    File: GetDBToken.vb
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