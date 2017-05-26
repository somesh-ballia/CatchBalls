Public Class Menu

    Private Sub GameToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GameToolStripMenuItem.Click
        Dim objMain As New Main
        objMain.Show()

    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub Menu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim objMain As New Main
        Me.Hide()
        objMain.Show()


    End Sub
End Class