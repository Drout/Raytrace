Imports System.Numerics
Imports RayTraceHelper
'Imports System.Windows.Media.Media3D
Module RayTrace
    Const SpheresCount As Integer = 2
    Dim pp As Double
    'Dim px, py, pz As Double
    Dim p, Ray As Vector3
    'Dim dx, dy, dz As Double
    'Dim PosX, PosY, PosZ As Double
    Dim Pos As Vector3
    Dim n, s, l, u, v As Double

    Dim aa, bb, dd, sc As Double
    'Dim nn, nx, ny, nz As Double
    Dim normv As Vector3
    'Dim r(SpheresCount) As Double
    'Dim q(SpheresCount) As Double
    'Dim c(SpheresCount, 2) As Double
    Dim Spheres(2) As Sphere
    Dim image1 As Bitmap

    'Dim point As Point3D
    Function Raytrace(SizeX As Integer, SizeY As Integer) As Bitmap
        Spheres(1) = New Sphere(New Vector3(0.9, -1.1, 2), 0.2)
        Spheres(2) = New Sphere(New Vector3(-0.3, -0.8, 3), 0.6)

        image1 = New Bitmap(SizeX, SizeY)

        Ray = New Vector3()
        p = New Vector3()

        For i = 0 To SizeY - 1 : For j = 0 To SizeX - 1
                Pos = New Vector3(0.3, -0.5, 0)

                Ray.X = j - SizeX / 2 : Ray.Y = i - SizeY / 2 : Ray.Z = SizeX
                'dd = Ray.X * Ray.X + Ray.Y * Ray.Y + Ray.Z * Ray.Z
                dd = Vector3.Dot(Ray, Ray) 'dd = Ray.Length * Ray.Length
                Hundred(j, i)
            Next j
        Next i
        Return image1
    End Function

    Sub Hundred(j As Integer, i As Integer)
100:
        n = Pos.Y >= 0 Or Ray.Y <= 0 : If Not n Then s = -Pos.Y / Ray.Y
        For k = 1 To SpheresCount
            'p.X = Spheres(k).Center.X - Pos.X : p.Y = Spheres(k).Center.Y - Pos.Y : p.Z = Spheres(k).Center.Z - Pos.Z
            p = Spheres(k).Center - Pos
            'pp = p.X * p.X + p.Y * p.Y + p.Z * p.Z
            pp = Vector3.Dot(p, p) 'p.LengthSquared 
            'sc = p.X * Ray.X + p.Y * Ray.Y + p.Z * Ray.Z
            sc = Vector3.Dot(p, Ray)

            If sc > 0 Then
                bb = sc * sc / dd
                aa = Spheres(k).Q - pp + bb
                If aa > 0 Then
                    sc = (Math.Sqrt(bb) - Math.Sqrt(aa)) / Math.Sqrt(dd)
                    If sc < s Or n < 0 Then n = k : s = sc
                End If

            End If
        Next k
        If n < 0 Then ' we hit nothing (so it's the sky)
            Draw(j, i, Pos.Z - Int(Pos.Z), Pos.X - Int(Pos.X))
            Return
        End If
        ' we hit something
        '        Ray.X = Ray.X * s : Ray.Y = Ray.Y * s : Ray.Z = Ray.Z * s : dd = dd * s * s ' set the ray to the correct length
        Ray = Ray * s
        dd = dd * s * s
        ' go where the ray hit
        'Pos.X = Pos.X + Ray.X : Pos.Y = Pos.Y + Ray.Y : Pos.Z = Pos.Z + Ray.Z 
        Pos = Pos + Ray
        If n = 0 Then GoTo 300         ' we hit the floor - finally! 
        ' hit a sphere
        ' calculate normal vector
        'nx = Pos.X - Spheres(n).Center.X : ny = Pos.Y - Spheres(n).Center.Y : nz = Pos.Z - Spheres(n).Center.Z
        normv = Pos - Spheres(n).Center
        'nn = nx * nx + ny * ny + nz * nz
        'nn = Vector3.Dot(normv, normv)
        'l = 2 * (Ray.X * nx + Ray.Y * ny + Ray.Z * nz) / nn
        l = 2 * Vector3.Dot(Ray, normv) / Vector3.Dot(normv, normv)


        'Ray.X = Ray.X - nx * l : Ray.Y = Ray.Y - ny * l : Ray.Z = Ray.Z - nz * l
        Ray = Ray - normv * l
        GoTo 100
300:
        ' we hit the floor - finally!            
        ' check the shadows
        For k = 1 To SpheresCount
            u = Spheres(k).Center.X - Pos.X : v = Spheres(k).Center.Z - Pos.Z
            If u * u + v * v <= Spheres(k).Q Then
                ' we are in the shadow
                Draw(j, i, Pos.X - Int(Pos.X), Pos.Z - Int(Pos.Z), (u * u + v * v) / Spheres(k).Q * 255)
                Return
            End If
        Next k
        ' If (Pos.X - Int(Pos.X) > 0.5) <> (Pos.Z - Int(Pos.Z) > 0.5) Then
        Draw(j, i, Pos.X - Int(Pos.X), Pos.Z - Int(Pos.Z))
        'End If
        Return
    End Sub

    Sub Draw(x As Integer, y As Integer)
        image1.SetPixel(x, y, Color.Black())
    End Sub

    Sub Draw(x As Integer, y As Integer, XX As Single, YY As Single, Optional a As Single = 255)
        image1.SetPixel(x, y, Color.FromArgb(a / 2 * (XX + YY), a * XX, a * YY))

    End Sub
End Module
