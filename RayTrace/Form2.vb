Public Class Form2

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        PictureBox1.Image = RayTrace.Raytrace(PictureBox1.Width, PictureBox1.Height)
    End Sub
End Class