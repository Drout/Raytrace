Imports RayTraceHelper

Module RayTrace
    Const SpheresCount As Integer = 2
    Dim pp As Double
    'Dim px, py, pz As Double
    Dim p, d As Point3D
    'Dim dx, dy, dz As Double
    Dim x, y, z As Double
    Dim n, s, l, u, v As Double

    Dim aa, bb, dd, sc As Double
    Dim nn, nx, ny, nz As Double
    Dim r(SpheresCount) As Double
    Dim q(SpheresCount) As Double
    Dim c(SpheresCount, 2) As Double
    Dim image1 As Bitmap

    Dim point As Point3D
    Function Raytrace(SizeX As Integer, SizeY As Integer) As Bitmap
        r(1) = 0.2 : r(2) = 0.6
        c(1, 0) = 0.9 : c(1, 1) = -1.1 : c(1, 2) = 2 : c(2, 0) = -0.3 : c(2, 1) = -0.8 : c(2, 2) = 3
        image1 = New Bitmap(SizeX, SizeY)
        d = New Point3D()
        p = New Point3D()
        For k = 1 To SpheresCount
            q(k) = r(k) * r(k)
        Next k

        For i = 0 To SizeY - 1 : For j = 0 To SizeX - 1
                x = 0.3 : y = -0.5 : z = 0
                d.X = j - SizeX / 2 : d.Y = i - SizeY / 2 : d.Z = 475 : dd = d.X * d.X + d.Y * d.Y + d.Z * d.Z
                Hundred(j, i)
            Next j
        Next i
        Return image1
    End Function

    Sub Hundred(j As Integer, i As Integer)
100:
        n = y >= 0 Or d.Y <= 0 : If Not n Then s = -y / d.Y
        For k = 1 To SpheresCount
            p.X = c(k, 0) - x : p.Y = c(k, 1) - y : p.Z = c(k, 2) - z
            pp = p.X * p.X + p.Y * p.Y + p.Z * p.Z
            sc = p.X * d.X + p.Y * d.Y + p.Z * d.Z
            If sc <= 0 Then GoTo 200
            bb = sc * sc / dd
            aa = q(k) - pp + bb
            If aa <= 0 Then GoTo 200
            sc = (Math.Sqrt(bb) - Math.Sqrt(aa)) / Math.Sqrt(dd)
            If sc < s Or n < 0 Then n = k : s = sc
200:
        Next k
        If n < 0 Then
            Return
        End If
        d.X = d.X * s : d.Y = d.Y * s : d.Z = d.Z * s : dd = dd * s * s
        x = x + d.X : y = y + d.Y : z = z + d.Z
        If n = 0 Then GoTo 300
        nx = x - c(n, 0) : ny = y - c(n, 1) : nz = z - c(n, 2)
        nn = nx * nx + ny * ny + nz * nz
        l = 2 * (d.X * nx + d.Y * ny + d.Z * nz) / nn
        d.X = d.X - nx * l : d.Y = d.Y - ny * l : d.Z = d.Z - nz * l
        GoTo 100
300:
        For k = 1 To SpheresCount
            u = c(k, 0) - x : v = c(k, 2) - z : If u * u + v * v <= q(k) Then Return
        Next k
        If (x - Int(x) > 0.5) <> (z - Int(z) > 0.5) Then Draw(j, i)
        Return
    End Sub

    Sub Draw(x As Integer, y As Integer)
        image1.SetPixel(x, y, Color.Black())
    End Sub
End Module
