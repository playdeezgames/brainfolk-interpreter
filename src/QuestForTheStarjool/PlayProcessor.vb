Module PlayProcessor
    Friend Sub Run(world As World)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine($"Fellowship: {world.PlayerFellowship.Name}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoice(AbandonGameText)
            Select Case AnsiConsole.Prompt(prompt)
                Case AbandonGameText
                    If Confirm("[red]Are you sure you want to abandon the game?[/]") Then
                        Exit Do
                    End If
            End Select
        Loop
    End Sub
End Module
