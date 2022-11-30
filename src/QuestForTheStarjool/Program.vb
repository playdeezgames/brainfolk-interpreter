Imports System

Module Program
    Sub Main(args As String())
        Console.Title = Constants.GameTitle
        TitleProcessor.Run()
        MainMenuProcessor.Run()
    End Sub
End Module
