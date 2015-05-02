<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GetDBToken
    Inherits MetroFramework.Forms.MetroForm

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GetDBToken))
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.MetroLabel1 = New MetroFramework.Controls.MetroLabel()
        Me.MetroButton1 = New MetroFramework.Controls.MetroButton()
        Me.MetroButton2 = New MetroFramework.Controls.MetroButton()
        Me.twitterpanel = New MetroFramework.Controls.MetroPanel()
        Me.MetroButton12 = New MetroFramework.Controls.MetroButton()
        Me.TextBox1 = New MetroFramework.Controls.MetroTextBox()
        Me.MetroLabel2 = New MetroFramework.Controls.MetroLabel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.twitterpanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'WebBrowser1
        '
        Me.WebBrowser1.AllowWebBrowserDrop = False
        Me.WebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebBrowser1.Location = New System.Drawing.Point(20, 60)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(522, 153)
        Me.WebBrowser1.TabIndex = 6
        Me.WebBrowser1.Visible = False
        '
        'MetroLabel1
        '
        Me.MetroLabel1.AutoSize = True
        Me.MetroLabel1.Location = New System.Drawing.Point(23, 71)
        Me.MetroLabel1.Name = "MetroLabel1"
        Me.MetroLabel1.Size = New System.Drawing.Size(519, 95)
        Me.MetroLabel1.TabIndex = 7
        Me.MetroLabel1.Text = resources.GetString("MetroLabel1.Text")
        '
        'MetroButton1
        '
        Me.MetroButton1.Location = New System.Drawing.Point(104, 183)
        Me.MetroButton1.Name = "MetroButton1"
        Me.MetroButton1.Size = New System.Drawing.Size(186, 30)
        Me.MetroButton1.TabIndex = 8
        Me.MetroButton1.Text = "Copy Auth Link to the Clipboard"
        Me.MetroButton1.UseSelectable = True
        '
        'MetroButton2
        '
        Me.MetroButton2.Location = New System.Drawing.Point(296, 183)
        Me.MetroButton2.Name = "MetroButton2"
        Me.MetroButton2.Size = New System.Drawing.Size(186, 30)
        Me.MetroButton2.TabIndex = 9
        Me.MetroButton2.Text = "Continue"
        Me.MetroButton2.UseSelectable = True
        '
        'twitterpanel
        '
        Me.twitterpanel.Controls.Add(Me.MetroButton12)
        Me.twitterpanel.Controls.Add(Me.TextBox1)
        Me.twitterpanel.Controls.Add(Me.MetroLabel2)
        Me.twitterpanel.HorizontalScrollbarBarColor = True
        Me.twitterpanel.HorizontalScrollbarHighlightOnWheel = False
        Me.twitterpanel.HorizontalScrollbarSize = 10
        Me.twitterpanel.Location = New System.Drawing.Point(20, 60)
        Me.twitterpanel.Name = "twitterpanel"
        Me.twitterpanel.Size = New System.Drawing.Size(522, 117)
        Me.twitterpanel.TabIndex = 10
        Me.twitterpanel.VerticalScrollbarBarColor = True
        Me.twitterpanel.VerticalScrollbarHighlightOnWheel = False
        Me.twitterpanel.VerticalScrollbarSize = 10
        Me.twitterpanel.Visible = False
        '
        'MetroButton12
        '
        Me.MetroButton12.BackColor = System.Drawing.SystemColors.Control
        Me.MetroButton12.BackgroundImage = CType(resources.GetObject("MetroButton12.BackgroundImage"), System.Drawing.Image)
        Me.MetroButton12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.MetroButton12.Location = New System.Drawing.Point(347, 90)
        Me.MetroButton12.Name = "MetroButton12"
        Me.MetroButton12.Size = New System.Drawing.Size(28, 23)
        Me.MetroButton12.TabIndex = 18
        Me.ToolTip1.SetToolTip(Me.MetroButton12, "Paste")
        Me.MetroButton12.UseSelectable = True
        '
        'TextBox1
        '
        Me.TextBox1.AllowDrop = True
        Me.TextBox1.FontSize = MetroFramework.MetroTextBoxSize.Medium
        Me.TextBox1.FontWeight = MetroFramework.MetroTextBoxWeight.Bold
        Me.TextBox1.Location = New System.Drawing.Point(175, 90)
        Me.TextBox1.MaxLength = 32767
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.TextBox1.SelectedText = ""
        Me.TextBox1.Size = New System.Drawing.Size(166, 23)
        Me.TextBox1.TabIndex = 12
        Me.TextBox1.Text = "0000000"
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TextBox1.UseSelectable = True
        '
        'MetroLabel2
        '
        Me.MetroLabel2.AutoSize = True
        Me.MetroLabel2.Location = New System.Drawing.Point(2, 11)
        Me.MetroLabel2.Name = "MetroLabel2"
        Me.MetroLabel2.Size = New System.Drawing.Size(512, 76)
        Me.MetroLabel2.TabIndex = 8
        Me.MetroLabel2.Text = resources.GetString("MetroLabel2.Text")
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'GetDBToken
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(562, 233)
        Me.Controls.Add(Me.twitterpanel)
        Me.Controls.Add(Me.MetroButton2)
        Me.Controls.Add(Me.MetroButton1)
        Me.Controls.Add(Me.MetroLabel1)
        Me.Controls.Add(Me.WebBrowser1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "GetDBToken"
        Me.Resizable = False
        Me.ShadowType = MetroFramework.Forms.MetroFormShadowType.None
        Me.Style = MetroFramework.MetroColorStyle.Red
        Me.Text = "Authorize Metro Image Uploader for the Cloud"
        Me.twitterpanel.ResumeLayout(False)
        Me.twitterpanel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser
    Friend WithEvents MetroLabel1 As MetroFramework.Controls.MetroLabel
    Friend WithEvents MetroButton1 As MetroFramework.Controls.MetroButton
    Friend WithEvents MetroButton2 As MetroFramework.Controls.MetroButton
    Friend WithEvents twitterpanel As MetroFramework.Controls.MetroPanel
    Friend WithEvents MetroLabel2 As MetroFramework.Controls.MetroLabel
    Friend WithEvents TextBox1 As MetroFramework.Controls.MetroTextBox
    Friend WithEvents MetroButton12 As MetroFramework.Controls.MetroButton
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class
