Module ManageShipProcessor
    Friend Sub Run(ship As Ship)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine($"Serial#: {ship.Id}")
            AnsiConsole.MarkupLine($"Name: {ship.Name}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoice(DoneText)
            Select Case AnsiConsole.Prompt(prompt)
                Case DoneText
                    Exit Do
            End Select
        Loop
    End Sub
End Module
