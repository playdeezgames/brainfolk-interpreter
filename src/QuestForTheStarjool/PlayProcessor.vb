Module PlayProcessor
    Friend Sub Run(world As World)
        Do
            AnsiConsole.Clear()
            ShowStatus(world.PlayerFellowship)
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoices(ManageFellowshipText, ManageShipsText, AbandonGameText)
            Select Case AnsiConsole.Prompt(prompt)
                Case AbandonGameText
                    If Confirm("[red]Are you sure you want to abandon the game?[/]") Then
                        Exit Do
                    End If
                Case ManageFellowshipText
                    ManageFellowshipProcessor.Run(world.PlayerFellowship)
                Case ManageShipsText
                    ManageShipsProcessor.Run(world.PlayerFellowship)
                Case ManageShipsText
            End Select
        Loop
    End Sub

    Private Sub ShowStatus(fellowship As Fellowship)
        AnsiConsole.MarkupLine($"Fellowship: {fellowship.Name}")
        AnsiConsole.MarkupLine($"Ships: {String.Join(", ", fellowship.Ships.Select(Function(x) x.Name).ToArray)}")
    End Sub
End Module
