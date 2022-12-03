Friend Module CommandHandlers
    Friend Sub OnClear(interpreter As DruthersScriptInterpreter, body As String)
        AnsiConsole.Clear()
        interpreter.NextLine()
    End Sub
    Friend Sub OnWrite(interpreter As DruthersScriptInterpreter, body As String)
        AnsiConsole.Markup(body)
        interpreter.NextLine()
    End Sub
    Friend Sub OnWriteLine(interpreter As DruthersScriptInterpreter, body As String)
        AnsiConsole.MarkupLine(body)
        interpreter.NextLine()
    End Sub
    Friend Sub OnStop(interpreter As DruthersScriptInterpreter, body As String)
        interpreter.Stop()
    End Sub
    Friend Sub OnPause(interpreter As DruthersScriptInterpreter, body As String)
        Console.ReadKey(True)
        interpreter.NextLine()
    End Sub
    Friend Sub OnGoTo(interpreter As DruthersScriptInterpreter, body As String)
        interpreter.GoToLabel(body)
    End Sub
    Friend Sub OnConfirm(interpreter As DruthersScriptInterpreter, body As String)
        If interpreter.DoConfirm() Then
            interpreter.GoToLabel(body)
        Else
            interpreter.NextLine()
        End If
    End Sub
    Friend Sub OnSetFlag(interpreter As DruthersScriptInterpreter, body As String)
        interpreter.Flags(body) = True
        interpreter.NextLine()
    End Sub
    Friend Sub OnClearFlag(interpreter As DruthersScriptInterpreter, body As String)
        interpreter.Flags(body) = False
        interpreter.NextLine()
    End Sub
    Friend Sub OnToggleFlag(interpreter As DruthersScriptInterpreter, body As String)
        interpreter.Flags(body) = Not interpreter.Flags(body)
        interpreter.NextLine()
    End Sub
    Friend Sub OnOnFlagGoTo(interpreter As DruthersScriptInterpreter, body As String)
        Dim tokens = body.Split(" "c, StringSplitOptions.RemoveEmptyEntries Or StringSplitOptions.TrimEntries)
        If interpreter.Flags(tokens(0)) Then
            interpreter.GoToLabel(tokens(1))
        Else
            If tokens.Count > 2 Then
                interpreter.GoToLabel(tokens(2))
            Else
                interpreter.NextLine()
            End If
        End If
    End Sub
    Friend Sub OnCopyFlag(interpreter As DruthersScriptInterpreter, body As String)
        Dim tokens = body.Split(" "c, StringSplitOptions.RemoveEmptyEntries Or StringSplitOptions.TrimEntries)
        interpreter.Flags(tokens(0)) = interpreter.Flags(tokens(1))
        interpreter.NextLine()
    End Sub
End Module
