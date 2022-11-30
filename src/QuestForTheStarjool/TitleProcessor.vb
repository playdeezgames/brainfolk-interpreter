Module TitleProcessor
    Friend Sub Run()
        AnsiConsole.Clear()
        Dim figlet As New FigletText(Constants.GameTitle) With
            {
                .Alignment = Justify.Center,
                .Color = Color.Fuchsia
            }
        AnsiConsole.Write(figlet)
        Utility.OkPrompt()
    End Sub
End Module
