Imports System

Module Program
    Sub Main(args As String())
        Console.Title = "Quest for the Starjool!!"
        AnsiConsole.Clear()
        Dim figlet As New FigletText("Quest for the Starjool!!") With {.Alignment = Justify.Center, .Color = Color.Fuchsia}
        AnsiConsole.Write(figlet)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = String.Empty}
        prompt.AddChoice("Ok")
        AnsiConsole.Prompt(prompt)
    End Sub
End Module
