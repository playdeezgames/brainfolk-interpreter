Module ManageShipProcessor
    Friend Sub Run(ship As Ship)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine($"Serial#: {ship.Id}")
            AnsiConsole.MarkupLine($"Name: {ship.Name}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoices(DoneText, ChangeNameText)
            Select Case AnsiConsole.Prompt(prompt)
                Case ChangeNameText
                    HandleChangeName(ship)
                Case DoneText
                    Exit Do
            End Select
        Loop
    End Sub
    Private Sub HandleChangeName(ship As Ship)
        Dim newName = Utility.SanitizedStringAsk("[olive]New Name?[/]")
        If String.IsNullOrEmpty(newName) Then
            Return
        End If
        ship.Name = newName
    End Sub
End Module
