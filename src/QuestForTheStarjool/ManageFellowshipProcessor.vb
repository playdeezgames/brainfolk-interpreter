Module ManageFellowshipProcessor
    Friend Sub Run(fellowship As Fellowship)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine($"Name: {fellowship.Name}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoices(DoneText, ChangeNameText)
            Select Case AnsiConsole.Prompt(prompt)
                Case ChangeNameText
                    HandleChangeName(fellowship)
                Case DoneText
                    Exit Do
            End Select
        Loop
    End Sub

    Private Sub HandleChangeName(fellowship As Fellowship)
        Dim newName = SanitizedStringAsk("[olive]New Name?[/]")
        If String.IsNullOrWhiteSpace(newName) Then
            Return
        End If
        fellowship.Name = newName
    End Sub
End Module
