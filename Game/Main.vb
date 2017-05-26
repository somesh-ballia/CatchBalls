Imports System.Drawing.Drawing2D

Public Class Main

    Dim objDGDesign As DGDesign
    Dim FinalScore As Integer = 0
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            PointCreate.Graphics = Me.pbRectangle.CreateGraphics()
            lblScore.Text = "00"
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        lblScore.Text = "00"
        Label5.Text = "00"
        Label7.Text = "00"
        If Not objDGDesign Is Nothing Then
            Me.tmrControl.Enabled = False
        End If
        lblLevelInd.Text = " Level : " & 1
        tmrControl.Enabled = False
        objDGDesign = New DGDesign(6)
        FinalScore = 0
        objDGDesign.DrawBalls(Me.pbRectangle.CreateGraphics(), Me.pbRectangle.BackColor)
        tmrControl.Enabled = True
        AddHandler pbRectangle.MouseDown, AddressOf Balls


    End Sub

    Private Sub Balls(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim count As Integer = objDGDesign.CMouseEnter(New Point(e.X, e.Y))
        If count <> 1 And count <> 0 Then
            FinalScore += 10 * count
        End If

        lblScore.Text = FinalScore
        objDGDesign.DrawBalls(Me.pbRectangle.CreateGraphics(), Me.pbRectangle.BackColor)
    End Sub

    Private Sub tmrControl_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrControl.Tick
        objDGDesign.NewBalls()
        objDGDesign.DrawBalls(Me.pbRectangle.CreateGraphics(), Me.pbRectangle.BackColor)
        If FinalScore > 1000 And FinalScore < 2000 Then
            lblLevelInd.Text = " Level : " & 2
            tmrControl.Interval = 4000
        ElseIf (FinalScore > 2000 And FinalScore < 3000) Then
            lblLevelInd.Text = " Level : " & 3
            tmrControl.Interval = 3000

        ElseIf (FinalScore > 3000 And FinalScore < 4000) Then
            lblLevelInd.Text = " Level : " & 4
            tmrControl.Interval = 2000

        ElseIf (FinalScore > 4000 And FinalScore < 5000) Then
            lblLevelInd.Text = " Level : " & 5
            tmrControl.Interval = 1000

        ElseIf (FinalScore > 5000) Then
            lblLevelInd.Text = " Level : " & 6
            tmrControl.Interval = 500
        End If

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Main_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Application.Exit()

    End Sub

    Private Sub pbRectangle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbRectangle.Click

        Label5.Text = Integer.Parse(Label5.Text) + 1
        If Integer.Parse(lblScore.Text) > 0 Then
            Label7.Text = (Integer.Parse(lblScore.Text)) / (Integer.Parse(Label5.Text))
        End If

    End Sub
End Class




Public Class PointCreate
    Private Shared GrpValue As Graphics
    Private Shared GridHeight As Integer
    Public Shared Property Graphics() As Graphics
        Get
            Return GrpValue
        End Get
        Set(ByVal Value As Graphics)
            GrpValue = Value
            GridHeight = CInt(GrpValue.VisibleClipBounds.Height())
        End Set
    End Property
    Public Shared Function PlacedToBottom(ByVal topleft As Point) As Point
        Dim newPoint As Point
        newPoint.X = topleft.X
        newPoint.Y = GridHeight - topleft.Y
        Return newPoint
    End Function
    Public Shared Function PlacedToTop(ByVal bottomleft As Point) As Point
        Dim newPoint As Point
        newPoint.X = bottomleft.X
        newPoint.Y = GridHeight - bottomleft.Y
        Return newPoint
    End Function
End Class



Public Class Balls
    Public Const BallsSize As Integer = 25
    Private clrVal As Color
    Private delVal As Boolean = False
    Private Shared rand As New Random

    Public Property Color() As Color
        Get
            Return clrVal
        End Get
        Set(ByVal Value As Color)
            clrVal = Value
        End Set
    End Property

    Public Property DelBall() As Boolean
        Get
            Return delVal
        End Get
        Set(ByVal Value As Boolean)
            delVal = Value
        End Set
    End Property

    Public Sub New(ByVal newColor As Color)
        clrVal = newColor
    End Sub

    Public Sub New(ByVal colors() As Color)
        Dim ncolors As Integer = colors.Length
        Dim pickedColor As Integer
        pickedColor = rand.Next(0, ncolors)
        clrVal = colors(pickedColor)
    End Sub

    Public Sub DrwBlls(ByVal graphics As Graphics, ByVal point As Point)
        Dim ptrnsLctn As Point = PointCreate.PlacedToBottom(point)
        Dim pbrshpt1 As Point = ptrnsLctn
        Dim pbrshpt2 As New Point(ptrnsLctn.X + Balls.BallsSize + 4, ptrnsLctn.Y - BallsSize - 4)
        Dim Newbrush As New Drawing2D.LinearGradientBrush(pbrshpt1, pbrshpt2, Me.Color, System.Drawing.Color.White)
        Dim brush As System.Drawing.Drawing2D.LinearGradientBrush = Newbrush
        Dim ptopleft As Point = point
        Dim pbottomright As Point = New Point(point.X + BallsSize, point.Y + BallsSize)
        Dim ptransTopLeft As Point = PointCreate.PlacedToBottom(ptopleft)
        Dim ptransBottomRight As Point = PointCreate.PlacedToBottom(pbottomright)
        Dim itranswidth As Integer = ptransBottomRight.X - ptransTopLeft.X
        Dim itransheight As Integer = ptransBottomRight.Y - ptransTopLeft.Y
        graphics.FillEllipse(brush, New Rectangle(ptransTopLeft, New Size(itranswidth, itransheight)))
    End Sub



    Private Function CreateTheBrush(ByVal location As Point) As Drawing2D.LinearGradientBrush
        Dim ptrnsLctn As Point = PointCreate.PlacedToBottom(location)
        Dim pbrshpt1 As Point = ptrnsLctn
        Dim pbrshpt2 As New Point(ptrnsLctn.X + Balls.BallsSize + 4, ptrnsLctn.Y - BallsSize - 4)
        Dim Newbrush As New Drawing2D.LinearGradientBrush(pbrshpt1, pbrshpt2, Me.Color, System.Drawing.Color.White)
        Return Newbrush
    End Function
End Class




Public Class DGDesign

    Dim objBalls(13, 9) As Balls
    Public Sub New(ByVal NoOfrowsToDraw As Integer)
        If NoOfrowsToDraw > objBalls.GetLength(0) Then
            Exit Sub


        End If
        Dim NoOfrow As Integer
        Dim NoOfcolumn As Integer
        For NoOfrow = 0 To NoOfrowsToDraw - 1
            For NoOfcolumn = 0 To objBalls.GetLength(1) - 1
                objBalls(NoOfrow, NoOfcolumn) = New Balls(New Color() {Color.Red, Color.Black, Color.Green})
            Next
        Next
        For NoOfrow = NoOfrowsToDraw To objBalls.GetLength(0) - 1


            For NoOfcolumn = 0 To objBalls.GetLength(1) - 1
                objBalls(NoOfrow, NoOfcolumn) = Nothing
            Next
        Next
    End Sub


    Public Sub NewBalls()
        Dim cols As Integer

        For cols = 0 To objBalls.GetLength(1) - 1
            Dim newBlock As New Balls(New Color() {Color.Red, Color.Brown, Color.Green, Color.Cyan})
            For row As Integer = objBalls.GetLength(0) - 1 To 1 Step -1
                objBalls(row, cols) = objBalls(row - 1, cols)


            Next
            objBalls(0, cols) = newBlock
        Next
    End Sub

    Public Sub DrawBalls(ByVal graphics As Graphics, ByVal backColor As Color)
        graphics.Clear(backColor)
        Dim row As Integer
        Dim column As Integer
        Dim theBlock As Balls
        For row = 0 To objBalls.GetLength(0) - 1
            For column = 0 To objBalls.GetLength(1) - 1
                theBlock = objBalls(row, column)
                If Not theBlock Is Nothing Then
                    Dim pointA As New Point(column * Balls.BallsSize, row * Balls.BallsSize)
                    objBalls(row, column).DrwBlls(graphics, pointA)
                    'If row = 13 Then
                    '    If MessageBox.Show("Game Over,Do you want Continue", "Muhil", MessageBoxButtons.YesNo) = DialogResult.Yes Then

                    '    Else

                    '    End If
                    'End If
                End If
            Next
        Next
    End Sub


    Public Function CMouseEnter(ByVal point As Point) As Integer
        Dim Fndtotal As Integer = 0
        Dim total As Integer = 0
        Try

            Dim transPt As Point = PointCreate.PlacedToTop(point)
            Dim selectedRow As Integer = transPt.Y \ Balls.BallsSize
            Dim selectedColumn As Integer = transPt.X \ Balls.BallsSize
            Dim selectedBlock As Balls = objBalls(selectedRow, selectedColumn)

            If Not selectedBlock Is Nothing Then
                selectedBlock.DelBall = True
                FndSmeClrBalls(selectedRow, selectedColumn)

                Dim FndScorerow As Integer
                Dim FndScoreColumn As Integer

                For FndScoreColumn = objBalls.GetLength(1) - 1 To 0 Step -1

                    For FndScorerow = 0 To objBalls.GetLength(0) - 1
                        If Not objBalls(FndScorerow, FndScoreColumn) Is Nothing Then
                            If objBalls(FndScorerow, FndScoreColumn).DelBall Then
                                Fndtotal += 1
                            End If
                        End If
                    Next
                Next

                If Fndtotal > 1 Then
                    Me.ReBuildBalls()
                Else

                    Dim row As Integer
                    Dim column As Integer
                    For column = objBalls.GetLength(1) - 1 To 0 Step -1
                        For row = 0 To objBalls.GetLength(0) - 1
                            If Not objBalls(row, column) Is Nothing Then
                                objBalls(row, column).DelBall = False
                            End If
                        Next
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
        Return Fndtotal
    End Function

    Public Sub ReBuildBalls()
        Dim theBlock As Balls
        Dim column As Integer
        Dim row As Integer
        Dim aRow As Integer

        For column = 0 To objBalls.GetLength(1) - 1
            For row = objBalls.GetLength(0) - 1 To 0 Step -1
                theBlock = objBalls(row, column)
                If (Not theBlock Is Nothing) Then
                    If theBlock.DelBall Then
                        For aRow = row To objBalls.GetLength(0) - 2
                            objBalls(aRow, column) = objBalls(aRow + 1, column)
                        Next
                        objBalls(objBalls.GetLength(0) - 1, column) = Nothing
                    End If
                End If
            Next
        Next
        For row = 0 To objBalls.GetLength(0) - 1
            For column = 0 To objBalls.GetLength(1) - 1
                theBlock = objBalls(row, column)
                If Not theBlock Is Nothing Then
                    theBlock.DelBall = False
                End If
            Next
        Next
        Dim irow As Integer
        Dim icolumn As Integer
        For icolumn = objBalls.GetLength(1) - 1 To 0 Step -1

            Dim noBlocks As Boolean = True
            For irow = 0 To objBalls.GetLength(0) - 1
                If Not objBalls(irow, icolumn) Is Nothing Then
                    noBlocks = False
                End If
            Next

            If noBlocks Then
                Dim newcol As Integer
                For newcol = icolumn To objBalls.GetLength(1) - 2
                    For irow = 0 To objBalls.GetLength(0) - 1
                        objBalls(irow, newcol) = objBalls(irow, newcol + 1)
                    Next
                Next
                newcol = objBalls.GetLength(1) - 1
                For irow = 0 To objBalls.GetLength(0) - 1
                    objBalls(irow, newcol) = Nothing
                Next
            End If
        Next
    End Sub

    Default Public Property Item(ByVal row As Integer, ByVal column As Integer) As Balls
        Get
            Return objBalls(row, column)
        End Get
        Set(ByVal Value As Balls)
            objBalls(row, column) = Value
        End Set
    End Property


    Private ArrLstBalls As ArrayList

    Private Sub FndSmeClrBalls(ByVal row As Integer, ByVal column As Integer)
        Dim color As Color = objBalls(row, column).Color
        ArrLstBalls = New ArrayList
        ArrLstBalls.Add(New Point(row, column))
        objBalls(row, column).DelBall = True

        While ArrLstBalls.Count > 0
            Dim location As Point = CType(ArrLstBalls(0), Point)
            Dim currentBlock As Balls = objBalls(location.X, location.Y)
            Dim rows As Integer = location.X
            Dim columns As Integer = location.Y
            ArrLstBalls.RemoveAt(0)

            Dim nextRow As Integer
            Dim nextCol As Integer
            Dim selected As Balls


            If rows < objBalls.GetLength(0) - 1 Then
                nextRow = rows + 1
                selected = objBalls(nextRow, columns)
                If Not selected Is Nothing Then
                    If selected.Color.Equals(color) Then
                        If Not selected.DelBall Then
                            selected.DelBall = True
                            ArrLstBalls.Add(New Point(row, column))
                        End If
                    End If
                End If
            End If


            If rows > 0 Then
                nextRow = rows - 1
                selected = objBalls(nextRow, columns)

                If Not selected Is Nothing Then
                    If selected.Color.Equals(color) Then
                        If Not selected.DelBall Then
                            selected.DelBall = True
                            ArrLstBalls.Add(New Point(row, column))
                        End If
                    End If
                End If
            End If


            If columns > 0 Then
                nextCol = columns - 1
                selected = objBalls(rows, nextCol)

                If Not selected Is Nothing Then
                    If selected.Color.Equals(color) Then
                        If Not selected.DelBall Then
                            selected.DelBall = True
                            ArrLstBalls.Add(New Point(row, column))
                        End If
                    End If
                End If
            End If


            If columns < objBalls.GetLength(1) - 1 Then
                nextCol = columns + 1
                selected = objBalls(rows, nextCol)

                If Not selected Is Nothing Then
                    If selected.Color.Equals(color) Then
                        If Not selected.DelBall Then
                            selected.DelBall = True
                            ArrLstBalls.Add(New Point(row, column))
                        End If
                    End If
                End If
            End If
        End While
    End Sub

End Class
