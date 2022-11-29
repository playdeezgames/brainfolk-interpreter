Public Class DruthersScriptInterpreter
    Private lines As New List(Of String)
    Private currentLine As Integer
    Private labels As New Dictionary(Of String, Integer)
    Private flags As New HashSet(Of String)
    Private running As Boolean
    Public Sub New(filename As String)
        lines.AddRange(File.ReadAllLines(filename))
        lines = lines.Where(Function(x) Not String.IsNullOrWhiteSpace(x)).ToList
        For index = 0 To lines.Count - 1
            If lines(index).First = "@"c Then
                labels(lines(index).Trim) = index
            End If
        Next
    End Sub
    Friend Sub Run()
        currentLine = 0
        running = True
        While running
            Dim line = lines(currentLine)
            Select Case line.First
                Case "#"c, "@"c
                    'just keep going!
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
    Const GoToCommand = ".go-to"
    Const ConfirmCommand = ".confirm"
    Const SetFlagCommand = ".set-flag"
    Const ClearFlagCommand = ".clear-flag"
    Const OnFlagGotoCommand = ".on-flag-go-to"

    Private Sub NextLine()
        currentLine += 1
    End Sub

    Private Sub GoToLabel(body As String)
        currentLine = labels(body.Trim)
    End Sub

    Private Sub DoCommand(command As String, body As String)
        Select Case command
            Case ClearCommand
                AnsiConsole.Clear()
                NextLine()
            Case WriteCommand
                AnsiConsole.Markup(body)
                NextLine()
            Case WriteLineCommand
                AnsiConsole.MarkupLine(body)
                NextLine()
            Case StopCommand
                running = False
            Case PauseCommand
                Console.ReadKey(True)
                NextLine()
            Case GoToCommand
                GoToLabel(body)
            Case ConfirmCommand
                If DoConfirm() Then
                    GoToLabel(body)
                Else
                    NextLine()
                End If
            Case SetFlagCommand
                flags.Add(body.Trim)
                NextLine()
            Case ClearFlagCommand
                flags.Remove(body.Trim)
                NextLine()
            Case OnFlagGotoCommand
                Dim tokens = body.Split(" "c, StringSplitOptions.RemoveEmptyEntries Or StringSplitOptions.TrimEntries)
                If flags.Contains(tokens(1)) Then
                    GoToLabel(tokens(2))
                Else
                    If tokens.Count > 3 Then
                        GoToLabel(tokens(3))
                    Else
                        NextLine()
                    End If
                End If
            Case Else
                Throw New NotImplementedException(command)
        End Select
    End Sub

    Private Function DoConfirm() As Boolean
        Dim prompt As New SelectionPrompt(Of String)
        prompt.AddChoices("No", "Yes")
        Return AnsiConsole.Prompt(prompt) = "Yes"
    End Function
End Class
