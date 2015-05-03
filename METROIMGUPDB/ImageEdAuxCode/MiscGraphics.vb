'============================================================================
'
'    MetroImgUpDb
'    Copyright (C) 2013 - 2015 Visual Software Corporation
'
'    Author: ASV93
'    File: MiscGraphics.vb
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

Module MiscGraphics

    ' Width of grab rectangles. Should be odd.
    Public GrabHandleWidth As Integer = 5
    Public GrabHandleHalfWidth As Integer = GrabHandleWidth \ 2

    Public Sub DrawGrabHandle(ByVal gr As Graphics, ByVal x As Integer, ByVal y As Integer)
        ' Fill a white rectangle.
        gr.FillRectangle(Brushes.White, _
            x - GrabHandleHalfWidth, _
            y - GrabHandleHalfWidth, _
            GrabHandleWidth, _
            GrabHandleWidth)

        ' Outline it in black.
        gr.DrawRectangle(Pens.Black, _
            x - GrabHandleHalfWidth, _
            y - GrabHandleHalfWidth, _
            GrabHandleWidth, _
            GrabHandleWidth)
    End Sub
End Module
