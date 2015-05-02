Imports System.Drawing.Imaging
Public Class ImageEd

    ' Holds the picture we are drawing.
    Private m_Picture As New DrawablePicture(Me.BackColor)

    ' The current drawing attributes.
    Private m_CurrentLineWidth As Integer = 1
    Private m_CurrentForeColor As Color = Color.Black
    Private m_CurrentFillColor As Color = Color.White

    ' The object we are currently drawing.
    Private m_NewDrawable As Drawable

    ' The tool we have currently selected.
    Private m_SelectedToolButton As ToolBarButton

    ' The index of the first thickness image in its ImageList.
    Private m_FirstLineThicknessImage As Integer
    Private m_FirstLineColorImage As Integer
    Private m_FirstFillColorImage As Integer
    Dim LastAction As String
    Dim HotBarHoverColor As Color = Color.IndianRed
    Dim HotBarBGColor As Color = Color.Firebrick
    Dim HotBarClickedColor As Color = Color.DarkRed
    Dim SecureClose As Integer

    Private Sub ImageEd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SecureClose = 0
        m_NewDrawable = New DrawableImage(0, PicCanvas.BackgroundImage.Height, PicCanvas.BackgroundImage.Width, 0)
        m_Picture.Add(m_NewDrawable)
        Dim drawable_image As DrawableImage = DirectCast(m_NewDrawable, DrawableImage)
        drawable_image.Picture = PicCanvas.BackgroundImage
        Me.Refresh()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LastAction = "Pointer"
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Panel2.Visible = True
    End Sub

    Private Sub NewDrawable_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        ' No longer watch for MouseMove or MouseUp.
        RemoveHandler picCanvas.MouseMove, AddressOf NewDrawable_MouseMove
        RemoveHandler picCanvas.MouseUp, AddressOf NewDrawable_MouseUp

        ' See if the new object is empty (e.g. a zero-length line).
        If m_NewDrawable.IsEmpty() Then
            ' Discard this object.
            m_Picture.Remove(m_NewDrawable)
        Else
            ' If it's a new image, get the image file.
            If TypeOf (m_NewDrawable) Is DrawableImage Then
                Dim drawable_image As DrawableImage = DirectCast(m_NewDrawable, DrawableImage)
                drawable_image.Picture = New Bitmap(OpenFileDialog1.FileName)
            End If
        End If

        ' We're no longer working with the new object.
        m_NewDrawable = Nothing

        ' Redraw.
        picCanvas.Invalidate()
    End Sub

    Private Sub NewDrawable_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        ' Update the new line's coordinates.
        m_NewDrawable.NewPoint(e.X, e.Y)

        ' Redraw to show the new line.
        picCanvas.Invalidate()
    End Sub

    Private Sub picCanvas_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PicCanvas.MouseMove
        ' Only move if the left button is down.
        If e.Button = Windows.Forms.MouseButtons.Left Then
            ' Move it.
            m_Picture.MoveSelectedDrawableToMouse(e.X, e.Y)

            ' Redraw to show the new position.
            picCanvas.Invalidate()
        End If
    End Sub

    Private Sub picCanvas_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PicCanvas.MouseDown
        ' See which button was pressed.
        If e.Button = MouseButtons.Right Then
            ' Right button. See if we're drawing something.
            If m_NewDrawable Is Nothing Then
                ' We are not drawing. Ignore this button.
            Else
                ' We are drawing something. Cancel it.
                m_Picture.Remove(m_NewDrawable)

                m_NewDrawable = Nothing
                RemoveHandler picCanvas.MouseMove, AddressOf NewDrawable_MouseMove
                RemoveHandler picCanvas.MouseUp, AddressOf NewDrawable_MouseUp

                ' Redraw to erase the new object.
                picCanvas.Invalidate()
            End If
        Else
            ' Left button. See which tool is pushed.
            Select Case LastAction
                Case "Pointer"
                    ' Select an object.
                    m_Picture.SelectObjectAt(e.X, e.Y)
                Case "Line"
                    ' Start drawing a line.
                    m_NewDrawable = New DrawableLine(m_CurrentForeColor, m_CurrentLineWidth, e.X, e.Y, e.X, e.Y)
                    m_Picture.Add(m_NewDrawable)
                    AddHandler PicCanvas.MouseMove, AddressOf NewDrawable_MouseMove
                    AddHandler PicCanvas.MouseUp, AddressOf NewDrawable_MouseUp
                Case "Rectangle"
                    ' Start drawing a rectangle.
                    m_NewDrawable = New DrawableRectangle(m_CurrentForeColor, m_CurrentFillColor, m_CurrentLineWidth, e.X, e.Y, e.X, e.Y)
                    m_Picture.Add(m_NewDrawable)
                    AddHandler PicCanvas.MouseMove, AddressOf NewDrawable_MouseMove
                    AddHandler PicCanvas.MouseUp, AddressOf NewDrawable_MouseUp
                Case "Ellipse"
                    ' Start drawing an ellipse.
                    m_NewDrawable = New DrawableEllipse(m_CurrentForeColor, m_CurrentFillColor, m_CurrentLineWidth, e.X, e.Y, e.X, e.Y)
                    m_Picture.Add(m_NewDrawable)
                    AddHandler PicCanvas.MouseMove, AddressOf NewDrawable_MouseMove
                    AddHandler PicCanvas.MouseUp, AddressOf NewDrawable_MouseUp
                Case "Star"
                    ' Start drawing a star.
                    m_NewDrawable = New DrawableStar(m_CurrentForeColor, m_CurrentFillColor, m_CurrentLineWidth, e.X, e.Y, e.X, e.Y)
                    m_Picture.Add(m_NewDrawable)
                    AddHandler PicCanvas.MouseMove, AddressOf NewDrawable_MouseMove
                    AddHandler PicCanvas.MouseUp, AddressOf NewDrawable_MouseUp
                Case "Image"
                    ' Start drawing an image.
                    m_NewDrawable = New DrawableImage(e.X, e.Y, e.X, e.Y)
                    m_Picture.Add(m_NewDrawable)
                    AddHandler PicCanvas.MouseMove, AddressOf NewDrawable_MouseMove
                    AddHandler PicCanvas.MouseUp, AddressOf NewDrawable_MouseUp
            End Select

            ' Redraw.
            picCanvas.Invalidate()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        m_Picture = New DrawablePicture(Me.BackColor)
        m_Picture.Draw(PicCanvas.CreateGraphics())

        'Refresh Picture
        m_NewDrawable = New DrawableImage(0, PicCanvas.BackgroundImage.Height, PicCanvas.BackgroundImage.Width, 0)
        m_Picture.Add(m_NewDrawable)
        Dim drawable_image As DrawableImage = DirectCast(m_NewDrawable, DrawableImage)
        drawable_image.Picture = PicCanvas.BackgroundImage
        Me.Refresh()
    End Sub

    Private Sub PicCanvas_Paint(sender As Object, e As PaintEventArgs) Handles PicCanvas.Paint
        m_Picture.Draw(e.Graphics)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim picname As String
        picname = "Data\" & Now.Year & "-" & Now.Month & "-" & Now.Day & "-" & Now.Hour & "-" & Now.Minute & "-" & Now.Second & ".png"
        'SAVING
        Dim rect As Rectangle = m_Picture.GetBounds()
        Dim bm As New Bitmap(rect.Width, rect.Height)
        Using gr As Graphics = Graphics.FromImage(bm)
            m_Picture.Draw(gr)
        End Using

        ' Save the Bitmap.
        Dim filename As String = picname
        Dim ext As String = filename.Substring(filename.LastIndexOf("."))
        ext = ext.ToLower()
        Select Case (ext)
            Case ".bmp"
                bm.Save(filename, ImageFormat.Bmp)
            Case ".gif"
                bm.Save(filename, ImageFormat.Gif)
            Case ".jpg", ".jpeg"
                bm.Save(filename, ImageFormat.Jpeg)
            Case ".png"
                bm.Save(filename, ImageFormat.Png)
            Case ".tif", ".tiff"
                bm.Save(filename, ImageFormat.Tiff)
            Case Else
                MessageBox.Show("Unknown file extension '" & ext & "'", _
                    "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Select
        'END SAVING
        My.Forms.DBUploader.TextBox1.Text = My.Application.Info.DirectoryPath & "\" & picname
        Me.Close()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.FileName = "" Then

        Else
            LastAction = "Image"
        End If
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Panel2.Visible = False
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        LastAction = "Line"
    End Sub

    Private Sub Button6_Click_1(sender As Object, e As EventArgs) Handles Button6.Click
        LastAction = "Rectangle"
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        LastAction = "Ellipse"
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        LastAction = "Star"
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        m_Picture.Delete(m_Picture.SelectedDrawable)
        PicCanvas.Invalidate()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        ColorDialog1.ShowDialog()
        m_CurrentForeColor = ColorDialog1.Color
    End Sub

    Private Sub PictureBox10_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox10.MouseEnter
        PictureBox10.BackColor = HotBarHoverColor
    End Sub

    Private Sub PictureBox10_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox10.MouseLeave
        PictureBox10.BackColor = HotBarBGColor
    End Sub

    Private Sub PictureBox10_Click(sender As Object, e As EventArgs) Handles PictureBox10.Click
        PictureBox10.BackColor = HotBarClickedColor
        Dim newconfirm As Object
        newconfirm = MessageBox.Show("Are you sure you want to undo all changes made to the image?", "New", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If newconfirm = vbYes Then
            Button3_Click(sender, e)
        Else

        End If
    End Sub

    Private Sub PictureBox4_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox4.MouseEnter
        PictureBox4.BackColor = HotBarHoverColor
    End Sub

    Private Sub PictureBox4_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox4.MouseLeave
        PictureBox4.BackColor = HotBarBGColor
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        PictureBox4.BackColor = HotBarClickedColor
        PicCanvas.Cursor = Cursors.Arrow
        Button1_Click(sender, e)
    End Sub

    Private Sub PictureBox9_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox9.MouseEnter
        PictureBox9.BackColor = HotBarHoverColor
    End Sub

    Private Sub PictureBox9_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox9.MouseLeave
        PictureBox9.BackColor = HotBarBGColor
    End Sub

    Private Sub PictureBox9_Click(sender As Object, e As EventArgs) Handles PictureBox9.Click
        PictureBox9.BackColor = HotBarClickedColor
        If Not (m_Picture.SelectedDrawable Is Nothing) Then
            m_Picture.SelectedDrawable = Nothing
            PicCanvas.Invalidate()
        End If
        ContextMenuStrip1.Show(MousePosition)
    End Sub

    Private Sub PictureBox2_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox2.MouseEnter
        PictureBox2.BackColor = HotBarHoverColor
    End Sub

    Private Sub PictureBox2_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox2.MouseLeave
        PictureBox2.BackColor = HotBarBGColor
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        PictureBox2.BackColor = HotBarClickedColor
        ContextMenuStrip2.Show(MousePosition)
    End Sub

    Private Sub PictureBox11_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox11.MouseEnter
        PictureBox11.BackColor = HotBarHoverColor
    End Sub

    Private Sub PictureBox11_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox11.MouseLeave
        PictureBox11.BackColor = HotBarBGColor
    End Sub

    Private Sub PictureBox11_Click(sender As Object, e As EventArgs) Handles PictureBox11.Click
        SecureClose = 1
        PictureBox11.BackColor = HotBarClickedColor
        Button4_Click(sender, e)
    End Sub

    Private Sub LineToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LineToolStripMenuItem.Click
        Button7_Click(sender, e)
        PicCanvas.Cursor = Cursors.Cross
    End Sub

    Private Sub RectangleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RectangleToolStripMenuItem.Click
        Button6_Click_1(sender, e)
        PicCanvas.Cursor = Cursors.Cross
    End Sub

    Private Sub CircleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CircleToolStripMenuItem.Click
        Button8_Click(sender, e)
        PicCanvas.Cursor = Cursors.Cross
    End Sub

    Private Sub StarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StarToolStripMenuItem.Click
        Button9_Click(sender, e)
        PicCanvas.Cursor = Cursors.Cross
    End Sub

    Private Sub ImageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImageToolStripMenuItem.Click
        Button5_Click(sender, e)
        PicCanvas.Cursor = Cursors.Cross
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint
        If Not (m_Picture.SelectedDrawable Is Nothing) Then
            m_Picture.SelectedDrawable = Nothing
            PicCanvas.Invalidate()
        End If
        PicCanvas.Cursor = Cursors.Arrow
    End Sub

    Private Sub ForeColorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ForeColorToolStripMenuItem.Click
        ColorDialog1.ShowDialog()
        m_CurrentForeColor = ColorDialog1.Color
        ForeColorToolStripMenuItem.ForeColor = ColorDialog1.Color
        If Not (m_Picture.SelectedDrawable Is Nothing) Then
            m_Picture.SelectedDrawable.ForeColor = m_CurrentForeColor
            PicCanvas.Invalidate()
        End If
    End Sub

    Private Sub BackColorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackColorToolStripMenuItem.Click
        ColorDialog2.ShowDialog()
        m_CurrentFillColor = ColorDialog2.Color
        BackColorToolStripMenuItem.BackColor = ColorDialog2.Color
        If Not (m_Picture.SelectedDrawable Is Nothing) Then
            m_Picture.SelectedDrawable.FillColor = m_CurrentFillColor
            PicCanvas.Invalidate()
        End If
    End Sub

    Private Sub DeleteToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem1.Click
        Button11_Click(sender, e)
    End Sub

    Private Sub SendToBackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SendToBackToolStripMenuItem.Click
        m_Picture.SendToBack(m_Picture.SelectedDrawable)
        PicCanvas.Invalidate()
    End Sub

    Private Sub BringToFrontToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BringToFrontToolStripMenuItem.Click
        m_Picture.BringToFront(m_Picture.SelectedDrawable)
        PicCanvas.Invalidate()
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        PictureBox5.BackColor = HotBarClickedColor
        ContextMenuStrip3.Show(MousePosition)
    End Sub

    Private Sub SmallToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SmallToolStripMenuItem.Click
        ' Update the current pen.
        m_CurrentLineWidth = 1

        ' Update the selected object if there is one.
        If Not (m_Picture.SelectedDrawable Is Nothing) Then
            m_Picture.SelectedDrawable.LineWidth = m_CurrentLineWidth
            PicCanvas.Invalidate()
        End If
    End Sub

    Private Sub MediumToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MediumToolStripMenuItem.Click
        ' Update the current pen.
        m_CurrentLineWidth = 3

        ' Update the selected object if there is one.
        If Not (m_Picture.SelectedDrawable Is Nothing) Then
            m_Picture.SelectedDrawable.LineWidth = m_CurrentLineWidth
            PicCanvas.Invalidate()
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        PictureBox1.BackColor = HotBarClickedColor
        ContextMenuStrip4.Show(MousePosition)
    End Sub

    Private Sub PictureBox5_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox5.MouseEnter
        PictureBox5.BackColor = HotBarHoverColor
    End Sub

    Private Sub PictureBox5_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox5.MouseLeave
        PictureBox5.BackColor = HotBarBGColor
    End Sub

    Private Sub PictureBox1_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox1.MouseEnter
        PictureBox1.BackColor = HotBarHoverColor
    End Sub

    Private Sub PictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox1.MouseLeave
        PictureBox1.BackColor = HotBarBGColor
    End Sub

    Private Sub BigToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BigToolStripMenuItem.Click
        ' Update the current pen.
        m_CurrentLineWidth = 5

        ' Update the selected object if there is one.
        If Not (m_Picture.SelectedDrawable Is Nothing) Then
            m_Picture.SelectedDrawable.LineWidth = m_CurrentLineWidth
            PicCanvas.Invalidate()
        End If
    End Sub

    Private Sub PxToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PxToolStripMenuItem.Click
        ' Update the current pen.
        m_CurrentLineWidth = 8

        ' Update the selected object if there is one.
        If Not (m_Picture.SelectedDrawable Is Nothing) Then
            m_Picture.SelectedDrawable.LineWidth = m_CurrentLineWidth
            PicCanvas.Invalidate()
        End If
    End Sub

    Private Sub ImageBackgroundColorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImageBackgroundColorToolStripMenuItem.Click
        ColorDialog3.ShowDialog()
        ImageBackgroundColorToolStripMenuItem.BackColor = ColorDialog3.Color
        m_Picture.BackColor = ColorDialog3.Color
        PicCanvas.Invalidate()
    End Sub

    Private Sub ImageEd_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If SecureClose = 1 Then

        Else
            Dim newconfirm As Object
            newconfirm = MessageBox.Show("Are you sure you want to close the Image Editor?", "Image Editor", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If newconfirm = vbYes Then

            Else
                e.Cancel = True
            End If
        End If
    End Sub
End Class