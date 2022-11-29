Public Class DruthersScriptInterpreter
    Private _lines As New List(Of String)
    Private _currentLine As Integer
    Private _labels As New Dictionary(Of String, Integer)
    Private _flags As New HashSet(Of String)
    Private _running As Boolean
    Public Sub New(filename As String)
        _lines.AddRange(File.ReadAllLines(filename))
        _lines = _lines.Where(Function(x) Not String.IsNullOrWhiteSpace(x)).ToList
        For index = 0 To _lines.Count - 1
            If _lines(index).First = "@"c Then
                _labels(_lines(index).Trim) = index
            End If
        Next
    End Sub
    Friend Sub Run()
        _currentLine = 0
        _running = True
        While _running
            Dim line = _lines(_currentLine)
            Select Case line.First
                Case "#"c, "@"c
                    'just keep going!
                    _currentLine += 1
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
    Const ToggleFlagCommand = ".toggle-flag"
    Const OnFlagGotoCommand = ".on-flag-go-to"

    Private Sub NextLine()
        _currentLine += 1
    End Sub

    Private Sub GoToLabel(body As String)
        _currentLine = _labels(body.Trim)
    End Sub

    Private ReadOnly _commandTable As IReadOnlyDictionary(Of String, Action(Of String)) =
        New Dictionary(Of String, Action(Of String)) From
        {
            {ClearCommand, Sub(body)
                               AnsiConsole.Clear()
                               NextLine()
                           End Sub},
            {WriteCommand, Sub(body)
                               AnsiConsole.Markup(body)
                               NextLine()
                           End Sub},
            {WriteLineCommand, Sub(body)
                                   AnsiConsole.MarkupLine(body)
                                   NextLine()
                               End Sub},
            {StopCommand, Sub(body)
                              _running = False
                          End Sub},
            {PauseCommand, Sub(body)
                               Console.ReadKey(True)
                               NextLine()
                           End Sub},
            {GoToCommand, Sub(body)
                              GoToLabel(body)
                          End Sub},
            {ConfirmCommand, Sub(body)
                                 If DoConfirm() Then
                                     GoToLabel(body)
                                 Else
                                     NextLine()
                                 End If
                             End Sub},
            {SetFlagCommand, Sub(body)
                                 Flags(body) = True
                                 NextLine()
                             End Sub},
            {ClearFlagCommand, Sub(body)
                                   Flags(body) = False
                                   NextLine()
                               End Sub},
            {ToggleFlagCommand, Sub(body)
                                    Flags(body) = Not Flags(body)
                                    NextLine()
                                End Sub},
            {OnFlagGotoCommand, Sub(body)
                                    Dim tokens = body.Split(" "c, StringSplitOptions.RemoveEmptyEntries Or StringSplitOptions.TrimEntries)
                                    If _flags.Contains(tokens(0)) Then
                                        GoToLabel(tokens(1))
                                    Else
                                        If tokens.Count > 2 Then
                                            GoToLabel(tokens(2))
                                        Else
                                            NextLine()
                                        End If
                                    End If
                                End Sub}
        }

    Private Sub DoCommand(command As String, body As String)
        _commandTable(command)(body)
    End Sub

    Private Function DoConfirm() As Boolean
        Dim prompt As New SelectionPrompt(Of String)
        prompt.AddChoices("No", "Yes")
        Return AnsiConsole.Prompt(prompt) = "Yes"
    End Function

    Property Flags(flag As String) As Boolean
        Get
            Return _flags.Contains(flag.Trim)
        End Get
        Set(value As Boolean)
            If value Then
                _flags.Add(flag.Trim)
            Else
                _flags.Remove(flag.Trim)
            End If
        End Set
    End Property
End Class
