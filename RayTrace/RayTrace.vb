Imports RayTraceHelper
Module RayTrace
    Const SpheresCount As Integer = 2
    Dim pp As Double
    'Dim px, py, pz As Double
    Dim p, Ray As Point3D
    'Dim dx, dy, dz As Double
    'Dim PosX, PosY, PosZ As Double
    Dim Pos As Point3D
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
        Ray = New Point3D()
        p = New Point3D()
        For k = 1 To SpheresCount
            q(k) = r(k) * r(k)
        Next k

        For i = 0 To SizeY - 1 : For j = 0 To SizeX - 1
                Pos = New Point3D(0.3, -0.5, 0)
                'Pos.X = 0.3 : Pos.Y = -0.5 : Pos.Z = 0
                Ray.X = j - SizeX / 2 : Ray.Y = i - SizeY / 2 : Ray.Z = 475 : dd = Ray.X * Ray.X + Ray.Y * Ray.Y + Ray.Z * Ray.Z
                Hundred(j, i)
            Next j
        Next i
        Return image1
    End Function

    Sub Hundred(j As Integer, i As Integer)
100:
        n = Pos.Y >= 0 Or Ray.Y <= 0 : If Not n Then s = -Pos.Y / Ray.Y
        For k = 1 To SpheresCount
            p.X = c(k, 0) - Pos.X : p.Y = c(k, 1) - Pos.Y : p.Z = c(k, 2) - Pos.Z
            pp = p.X * p.X + p.Y * p.Y + p.Z * p.Z
            'ppp = p * p
            sc = p.X * Ray.X + p.Y * Ray.Y + p.Z * Ray.Z
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
        Ray.X = Ray.X * s : Ray.Y = Ray.Y * s : Ray.Z = Ray.Z * s : dd = dd * s * s
        Pos.X = Pos.X + Ray.X : Pos.Y = Pos.Y + Ray.Y : Pos.Z = Pos.Z + Ray.Z
        If n = 0 Then GoTo 300
        nx = Pos.X - c(n, 0) : ny = Pos.Y - c(n, 1) : nz = Pos.Z - c(n, 2)
        nn = nx * nx + ny * ny + nz * nz
        l = 2 * (Ray.X * nx + Ray.Y * ny + Ray.Z * nz) / nn
        Ray.X = Ray.X - nx * l : Ray.Y = Ray.Y - ny * l : Ray.Z = Ray.Z - nz * l
        GoTo 100
300:
        For k = 1 To SpheresCount
            u = c(k, 0) - Pos.X : v = c(k, 2) - Pos.Z : If u * u + v * v <= q(k) Then Return
        Next k
        If (Pos.X - Int(Pos.X) > 0.5) <> (Pos.Z - Int(Pos.Z) > 0.5) Then Draw(j, i)
        Return
    End Sub

    Sub Draw(x As Integer, y As Integer)
        image1.SetPixel(x, y, Color.Black())
    End Sub
End Module
