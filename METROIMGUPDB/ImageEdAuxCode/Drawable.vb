'============================================================================
'
'    MetroImgUpDb
'    Copyright (C) 2013 - 2015 Visual Software Corporation
'
'    Author: ASV93
'    File: Drawable.vb
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

Imports System.Xml.Serialization

<Serializable()> _
Public MustInherit Class Drawable
    ' Drawing characteristics.
    <XmlIgnore()> Public ForeColor As Color
    <XmlIgnore()> Public FillColor As Color
    <XmlAttributeAttribute()> Public LineWidth As Integer = 0

    <XmlAttributeAttribute()> Public X1 As Integer
    <XmlAttributeAttribute()> Public Y1 As Integer
    <XmlAttributeAttribute()> Public X2 As Integer
    <XmlAttributeAttribute()> Public Y2 As Integer

    ' Indicates whether we should draw as selected.
    <XmlIgnore()> Public IsSelected As Boolean = False

    ' Constructors.
    Public Sub New()
        ForeColor = Color.Black
        FillColor = Color.White
    End Sub
    Public Sub New(ByVal fore_color As Color, ByVal fill_color As Color, Optional ByVal line_width As Integer = 0)
        LineWidth = line_width
        ForeColor = fore_color
        FillColor = fill_color
    End Sub

    ' Property procedures to serialize and
    ' deserialize ForeColor and FillColor.
    <XmlAttributeAttribute("ForeColor")> _
    Public Property ForeColorArgb() As Integer
        Get
            Return ForeColor.ToArgb()
        End Get
        Set(ByVal Value As Integer)
            ForeColor = Color.FromArgb(Value)
        End Set
    End Property
    <XmlAttributeAttribute("BackColor")> _
    Public Property FillColorArgb() As Integer
        Get
            Return FillColor.ToArgb()
        End Get
        Set(ByVal Value As Integer)
            FillColor = Color.FromArgb(Value)
        End Set
    End Property

#Region "Methods to override"
    ' Draw the object on this Graphics surface.
    Public MustOverride Sub Draw(ByVal gr As Graphics)

    ' Return the object's bounding rectangle.
    Public MustOverride Function GetBounds() As Rectangle

    ' Return True if this point is on the object.
    Public MustOverride Function IsAt(ByVal x As Integer, ByVal y As Integer) As Boolean

    ' The user is moving one of the object's points.
    Public MustOverride Sub NewPoint(ByVal x As Integer, ByVal y As Integer)

    ' Return True if the object is empty (e.g. a zero-length line).
    Public MustOverride Function IsEmpty() As Boolean

    ' Move the object a given distance.
    Public Sub MoveRelative(ByVal dx As Integer, ByVal dy As Integer)
        X1 += dx
        Y1 += dy
        X2 += dx
        Y2 += dy
    End Sub
#End Region

End Class
