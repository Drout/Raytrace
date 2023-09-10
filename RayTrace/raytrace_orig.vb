Const spheres As Integer = 2
Dim pp, px, py, pz As Double
Dim dx, dy, dz As Double
Dim x, y, z As Double
Dim n, s, l, u, v As Double
Dim aa, bb, dd, sc As Double
Dim nn, nx, ny, nz As Double
Dim r(spheres) As Double
Dim q(spheres) As Double
Dim c(spheres, 2) As Double
Dim image1 As Bitmap
Sub Raytrace()
Dim SizeX = PictureBox1.Width
Dim SizeY = PictureBox1.Height
r(1) = 0.2 : r(2) = 0.6
c(1, 0) = 0.9 : c(1, 1) = -1.1 : c(1, 2) = 2 : c(2, 0) = -0.3 : c(2, 1) = -0.8 : c(2, 2) = 3
image1 = New Bitmap(SizeX, SizeY)
For k = 1 To spheres
q(k) = r(k) * r(k)
Next k
For i = 0 To SizeY - 1 : For j = 0 To SizeX - 1
x = 0.3 : y = -0.5 : z = 0
dx = j - SizeX / 2 : dy = i - SizeY / 2 : dz = 475 : dd = dx * dx + dy * dy + dz * dz
Hundred(j, i)
Next j
Next i
End Sub
Sub Hundred(j As Integer, i As Integer)
100:
n = y >= 0 Or dy <= 0 : If Not n Then s = -y / dy
For k = 1 To spheres
px = c(k, 0) - x : py = c(k, 1) - y : pz = c(k, 2) - z
pp = px * px + py * py + pz * pz
sc = px * dx + py * dy + pz * dz
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
dx = dx * s : dy = dy * s : dz = dz * s : dd = dd * s * s
x = x + dx : y = y + dy : z = z + dz
If n = 0 Then GoTo 300
nx = x - c(n, 0) : ny = y - c(n, 1) : nz = z - c(n, 2)
nn = nx * nx + ny * ny + nz * nz
l = 2 * (dx * nx + dy * ny + dz * nz) / nn
dx = dx - nx * l : dy = dy - ny * l : dz = dz - nz * l
GoTo 100
300:
For k = 1 To spheres
u = c(k, 0) - x : v = c(k, 2) - z : If u * u + v * v <= q(k) Then Return
Next k
If (x - Int(x) > 0.5) <> (z - Int(z) > 0.5) Then Draw(j, i)
Return
End Sub
Sub Draw(x As Integer, y As Integer)
image1.SetPixel(x, y, Color.Black())
PictureBox1.Image = image1
End Sub
