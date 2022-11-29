Public Class BrainFolkInterpreter
    Private tape As New List(Of (BrainFolkToken, Integer))
    Public Sub New(filename As String)
        Dim text = File.ReadAllText(filename)
        Dim loopStack As New Stack(Of Integer)
        For Each character In text
            Select Case character
                Case "."c
                    AddTokenToTape(BrainFolkToken.Output)
                Case ","c
                    AddTokenToTape(BrainFolkToken.Input)
                Case "+"c
                    AddTokenToTape(BrainFolkToken.Increment)
                Case "-"c
                    AddTokenToTape(BrainFolkToken.Decrement)
                Case ">"c
                    AddTokenToTape(BrainFolkToken.MoveNext)
                Case "<"c
                    AddTokenToTape(BrainFolkToken.MovePrevious)
                Case "["c
                    loopStack.Push(tape.Count)
                    tape.Add((BrainFolkToken.StartLoop, 0))
                Case "]"c
                    Dim start = loopStack.Pop
                    tape.Add((BrainFolkToken.EndLoop, start + 1))
                    tape(start) = (BrainFolkToken.StartLoop, tape.Count)
            End Select
        Next
    End Sub

    Private Sub AddTokenToTape(token As BrainFolkToken)
        If Not tape.Any OrElse tape.Last.Item1 <> token Then
            tape.Add((token, 1))
        Else
            tape(tape.Count - 1) = (token, tape.Last.Item2 + 1)
        End If
    End Sub

    Friend Sub Run()
        Dim position As Integer = 0
        Dim memoryPointer As Integer = 0
        Dim memory As New Dictionary(Of Integer, Byte)
        While position < tape.Count
            Dim token = tape(position)
            position += 1
            Select Case token.Item1
                Case BrainFolkToken.MoveNext
                    memoryPointer += token.Item2
                Case BrainFolkToken.MovePrevious
                    memoryPointer -= token.Item2
                Case BrainFolkToken.Increment
                    If memory.ContainsKey(memoryPointer) Then
                        memory(memoryPointer) = CByte(memory(memoryPointer) + token.Item2)
                    Else
                        memory(memoryPointer) = CByte(token.Item2)
                    End If
                Case BrainFolkToken.Decrement
                    If memory.ContainsKey(memoryPointer) Then
                        memory(memoryPointer) = CByte(memory(memoryPointer) - token.Item2)
                    Else
                        memory(memoryPointer) = CByte(-token.Item2)
                    End If
                Case BrainFolkToken.Output
                    For index = 1 To token.Item2
                        Console.Write(Chr(memory(memoryPointer)))
                    Next
                Case BrainFolkToken.Input
                    For index = 1 To token.Item2
                        memory(memoryPointer) = CByte(AscW(Console.ReadKey.KeyChar))
                    Next
                Case BrainFolkToken.StartLoop
                    If Not memory.ContainsKey(memoryPointer) OrElse memory(memoryPointer) = 0 Then
                        position = token.Item2
                    End If
                Case BrainFolkToken.EndLoop
                    If memory.ContainsKey(memoryPointer) AndAlso memory(memoryPointer) <> 0 Then
                        position = token.Item2
                    End If
            End Select
        End While
    End Sub
End Class
