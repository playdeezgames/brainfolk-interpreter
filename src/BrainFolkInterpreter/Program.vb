Module Program
    Sub Main(args As String())
        If Not args.Any Then
            Return
        End If
        Dim filename = args(0)
        Dim interpreter = New BrainFolkInterpreter(filename)
        interpreter.Run()
    End Sub
End Module
