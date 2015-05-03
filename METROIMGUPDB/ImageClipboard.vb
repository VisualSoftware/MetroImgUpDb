'============================================================================
'
'    MetroImgUpDb
'    Copyright (C) 2013 - 2015 Visual Software Corporation
'
'    Author: ASV93
'    File: ImageClipboard.vb
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

Imports System.Runtime.InteropServices

Public Class ImageClipboard

    Private IsFormBeingDragged As Boolean = False
    Private MouseDownX As Integer
    Private MouseDownY As Integer

    Public Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Clipboard.ContainsImage = True Then
            Dim picname As String
            picname = "Data\" & Now.Year & "-" & Now.Month & "-" & Now.Day & "-" & Now.Hour & "-" & Now.Minute & "-" & Now.Second & ".png"
            PictureBox1.BackgroundImage = Clipboard.GetImage()
            PictureBox1.BackgroundImage.Save(picname, System.Drawing.Imaging.ImageFormat.Png)
            My.Forms.DBUploader.TextBox1.Text = My.Application.Info.DirectoryPath & "\" & picname
        Else
            MessageBox.Show("Error, The clipboard does not contain any image", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Public Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim picname As String
        picname = "Data\" & Now.Year & "-" & Now.Month & "-" & Now.Day & "-" & Now.Hour & "-" & Now.Minute & "-" & Now.Second & ".png"
        PictureBox1.BackgroundImage = GetScreenCapture(True) 'TakeImage()
        PictureBox1.BackgroundImage.Save(picname, System.Drawing.Imaging.ImageFormat.Png)
        My.Forms.DBUploader.TextBox1.Text = My.Application.Info.DirectoryPath & "\" & picname
    End Sub

    Private Function TakeImage()
        Return TakeImage(0, 0, Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height)
    End Function
    Private Function TakeImage2()
        Return TakeImage2(Me.Location.X, Me.Location.Y, Me.Width, Me.Height)
    End Function
    Private Function TakeImage3()
        Return TakeImage2(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height)
    End Function
    Private Function TakeImage(ByVal X As Integer, ByVal Y As Integer, ByVal Width As Integer, ByVal Height As Integer)

        Dim Opacity As Double = Me.Opacity
        Me.Opacity = 0
        Dim Opacity1 As Double = My.Forms.DBUploader.Opacity
        My.Forms.DBUploader.Opacity = 0
        Dim Img As New Bitmap(Width, Height)
        Dim g As Graphics = Graphics.FromImage(Img)
        g.CopyFromScreen(X, Y, 0, 0, Img.Size)
        g.Dispose()

        Me.Opacity = Opacity
        My.Forms.DBUploader.Opacity = Opacity1
        Return Img
    End Function

    Private Function TakeImage2(ByVal X As Integer, ByVal Y As Integer, ByVal Width As Integer, ByVal Height As Integer)
        Dim Opacity As Double = Me.Opacity
        Me.Opacity = 0
        Dim Img As New Bitmap(Width, Height)
        Dim g As Graphics = Graphics.FromImage(Img)
        g.CopyFromScreen(X, Y, 0, 0, Img.Size)
        g.Dispose()
        Return Img
        Me.Opacity = Opacity
    End Function

    Public Function GetScreenCapture( _
       Optional ByVal FullScreen As Boolean = False) As Image
        ' Captures the current screen and returns as an Image
        ' object
        If FullScreen = True Then
            ' Print Screen pressed twice here as some systems
            ' grab active window "accidentally" on first run
            SendKeys.SendWait("{PRTSC 2}")
        Else
            SendKeys.SendWait("%{PRTSC}")
        End If
        Dim objData As IDataObject = Clipboard.GetDataObject()
        Return objData.GetData(DataFormats.Bitmap)
    End Function

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim picname As String
        picname = "Data\" & Now.Year & "-" & Now.Month & "-" & Now.Day & "-" & Now.Hour & "-" & Now.Minute & "-" & Now.Second & ".png"
        PictureBox1.BackgroundImage = TakeImage2()
        PictureBox1.BackgroundImage.Save(picname, System.Drawing.Imaging.ImageFormat.Png)
        My.Forms.DBUploader.TextBox1.Text = My.Application.Info.DirectoryPath & "\" & picname
    End Sub

    Public Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim picname As String
        picname = "Data\" & Now.Year & "-" & Now.Month & "-" & Now.Day & "-" & Now.Hour & "-" & Now.Minute & "-" & Now.Second & ".png"
        PictureBox1.BackgroundImage = TakeImage3()
        PictureBox1.BackgroundImage.Save(picname, System.Drawing.Imaging.ImageFormat.Png)
        My.Forms.DBUploader.TextBox1.Text = My.Application.Info.DirectoryPath & "\" & picname
    End Sub

    Private Sub ImageClipboard_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        My.Forms.DBUploader.MetroButton5.Enabled = True
    End Sub

    Private Sub ButtonItem2_Click(sender As Object, e As EventArgs)
        Button3_Click(sender, e)
        Me.Close()
    End Sub

    Private Sub ImageClipboard_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left And Me.WindowState <> FormWindowState.Maximized Then
            ResizeForm(resizeDir)
        End If
    End Sub

    Private Sub ImageClipboard_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp

    End Sub

    Private Sub ImageClipboard_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
        'Calculate which direction to resize based on mouse position

        If e.Location.X < BorderWidth And e.Location.Y < BorderWidth Then
            resizeDir = ResizeDirection.TopLeft

        ElseIf e.Location.X < BorderWidth And e.Location.Y > Me.Height - BorderWidth Then
            resizeDir = ResizeDirection.BottomLeft

        ElseIf e.Location.X > Me.Width - BorderWidth And e.Location.Y > Me.Height - BorderWidth Then
            resizeDir = ResizeDirection.BottomRight

        ElseIf e.Location.X > Me.Width - BorderWidth And e.Location.Y < BorderWidth Then
            resizeDir = ResizeDirection.TopRight

        ElseIf e.Location.X < BorderWidth Then
            resizeDir = ResizeDirection.Left

        ElseIf e.Location.X > Me.Width - BorderWidth Then
            resizeDir = ResizeDirection.Right

        ElseIf e.Location.Y < BorderWidth Then
            resizeDir = ResizeDirection.Top

        ElseIf e.Location.Y > Me.Height - BorderWidth Then
            resizeDir = ResizeDirection.Bottom

        Else
            resizeDir = ResizeDirection.None
        End If
    End Sub

    Private Sub ResizeForm(ByVal direction As ResizeDirection)
        Dim dir As Integer = -1
        Select Case direction
            Case ResizeDirection.Left
                dir = HTLEFT
            Case ResizeDirection.TopLeft
                dir = HTTOPLEFT
            Case ResizeDirection.Top
                dir = HTTOP
            Case ResizeDirection.TopRight
                dir = HTTOPRIGHT
            Case ResizeDirection.Right
                dir = HTRIGHT
            Case ResizeDirection.BottomRight
                dir = HTBOTTOMRIGHT
            Case ResizeDirection.Bottom
                dir = HTBOTTOM
            Case ResizeDirection.BottomLeft
                dir = HTBOTTOMLEFT
        End Select

        If dir <> -1 Then
            ReleaseCapture()
            SendMessage(Me.Handle, WM_NCLBUTTONDOWN, dir, 0)
        End If
    End Sub

#Region " Functions and Constants "

    <DllImport("user32.dll")> _
    Public Shared Function ReleaseCapture() As Boolean
    End Function

    <DllImport("user32.dll")> _
    Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    End Function

    Private Const WM_NCLBUTTONDOWN As Integer = &HA1
    Private Const HTBORDER As Integer = 18
    Private Const HTBOTTOM As Integer = 15
    Private Const HTBOTTOMLEFT As Integer = 16
    Private Const HTBOTTOMRIGHT As Integer = 17
    Private Const HTCAPTION As Integer = 2
    Private Const HTLEFT As Integer = 10
    Private Const HTRIGHT As Integer = 11
    Private Const HTTOP As Integer = 12
    Private Const HTTOPLEFT As Integer = 13
    Private Const HTTOPRIGHT As Integer = 14

#End Region

    'Width of the 'resizable border', the area where you can resize.
    Private Const BorderWidth As Integer = 6
    Private _resizeDir As ResizeDirection = ResizeDirection.None

    Public Enum ResizeDirection
        None = 0
        Left = 1
        TopLeft = 2
        Top = 3
        TopRight = 4
        Right = 5
        BottomRight = 6
        Bottom = 7
        BottomLeft = 8
    End Enum

    Public Property resizeDir() As ResizeDirection
        Get
            Return _resizeDir
        End Get
        Set(ByVal value As ResizeDirection)
            _resizeDir = value

            'Change cursor
            Select Case value
                Case ResizeDirection.Left
                    Me.Cursor = Cursors.SizeWE

                Case ResizeDirection.Right
                    Me.Cursor = Cursors.SizeWE

                Case ResizeDirection.Top
                    Me.Cursor = Cursors.SizeNS

                Case ResizeDirection.Bottom
                    Me.Cursor = Cursors.SizeNS

                Case ResizeDirection.BottomLeft
                    Me.Cursor = Cursors.SizeNESW

                Case ResizeDirection.TopRight
                    Me.Cursor = Cursors.SizeNESW

                Case ResizeDirection.BottomRight
                    Me.Cursor = Cursors.SizeNWSE

                Case ResizeDirection.TopLeft
                    Me.Cursor = Cursors.SizeNWSE

                Case Else
                    Me.Cursor = Cursors.Default
            End Select
        End Set
    End Property

    Private Sub ButtonItem1_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub Bar1_MouseUp(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            IsFormBeingDragged = False
        End If
    End Sub

    Private Sub Bar1_MouseMove(sender As Object, e As MouseEventArgs)
        If IsFormBeingDragged Then
            Dim temp As Point = New Point()
            temp.X = Me.Location.X + (e.X - MouseDownX)
            temp.Y = Me.Location.Y + (e.Y - MouseDownY)
            Me.Location = temp
            temp = Nothing
        End If
    End Sub

    Private Sub Bar1_MouseDown(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            IsFormBeingDragged = True
            MouseDownX = e.X
            MouseDownY = e.Y
        End If
    End Sub

    Private Sub ImageClipboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub MetroTile1_Click(sender As Object, e As EventArgs) Handles MetroTile1.Click
        Button3_Click(sender, e)
        Me.Close()
    End Sub

    Private Sub MetroTile2_Click(sender As Object, e As EventArgs) Handles MetroTile2.Click
        Me.Close()
    End Sub

    Private Sub MetroTile3_Click(sender As Object, e As EventArgs) Handles MetroTile3.Click
        ImageEd.PicCanvas.BackgroundImage = TakeImage2()
        ImageEd.Show()
        Me.Close()
    End Sub
End Class