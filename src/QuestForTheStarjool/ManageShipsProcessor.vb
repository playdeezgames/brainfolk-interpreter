Module ManageShipsProcessor
    Friend Sub Run(fellowship As Fellowship)
        Do
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Which Ship?[/]"}
            prompt.AddChoice(DoneText)
            Dim table = fellowship.Ships.ToDictionary(Function(x) x.UniqueName, Function(x) x)
            prompt.AddChoices(table.Keys)
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case DoneText
                    Exit Do
                Case Else
                    ManageShipProcessor.Run(table(answer))
            End Select
        Loop
    End Sub
End Module
