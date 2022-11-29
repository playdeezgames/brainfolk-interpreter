Public Class DruthersScriptInterpreter
    Private lines As New List(Of String)
    Private currentLine As Integer
    Private running As Boolean
    Public Sub New(filename As String)
        lines.AddRange(File.ReadAllLines(filename))
    End Sub
    Friend Sub Run()
        currentLine = 0
        running = True
        While running
            Dim line = lines(currentLine)
            Select Case line.First
                Case "#"c
                    'comment! just keep going!
                    currentLine += 1
                Case "."c
                    'command!
                    HandleCommandLine(line)
            End Select
        End While
    End Sub
    Private Sub HandleCommandLine(commandLine As String)
        Dim index = commandLine.IndexOf(" "c)
        Dim command = If(index = -1, commandLine, commandLine.Substring(0, index))
        Dim body = If(index = -1, String.Empty, commandLine.Substring(index + 1))
        DoCommand(command, body)
    End Sub
    Const ClearCommand = ".clear"
    Const WriteCommand = ".write"
    Const WriteLineCommand = ".write-line"
    Const StopCommand = ".stop"
    Const PauseCommand = ".pause"

    Private Sub DoCommand(command As String, body As String)
        Select Case command
            Case ClearCommand
                AnsiConsole.Clear()
                currentLine += 1
            Case WriteCommand
                AnsiConsole.Markup(body)
                currentLine += 1
            Case WriteLineCommand
                AnsiConsole.MarkupLine(body)
                currentLine += 1
            Case StopCommand
                running = False
            Case PauseCommand
                Console.ReadKey(True)
                currentLine += 1
        End Select
    End Sub
End Class
