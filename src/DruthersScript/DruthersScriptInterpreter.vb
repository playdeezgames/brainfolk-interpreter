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
    Const ClearFlagCommand = ".clear-flag"
    Const CopyFlagCommand = ".copy-flag"
    Const ConfirmCommand = ".confirm"
    Const GoToCommand = ".go-to"
    Const OnFlagGoToCommand = ".on-flag-go-to"
    Const PauseCommand = ".pause"
    Const SetFlagCommand = ".set-flag"
    Const StopCommand = ".stop"
    Const ToggleFlagCommand = ".toggle-flag"
    Const WriteCommand = ".write"
    Const WriteLineCommand = ".write-line"

    Friend Sub NextLine()
        _currentLine += 1
    End Sub

    Friend Sub GoToLabel(body As String)
        _currentLine = _labels(body.Trim)
    End Sub
    Friend Sub [Stop]()
        _running = False
    End Sub

    Private Shared ReadOnly _commandTable As IReadOnlyDictionary(Of String, Action(Of DruthersScriptInterpreter, String)) =
        New Dictionary(Of String, Action(Of DruthersScriptInterpreter, String)) From
        {
            {ClearCommand, AddressOf OnClear},
            {WriteCommand, AddressOf OnWrite},
            {WriteLineCommand, AddressOf OnWriteLine},
            {StopCommand, AddressOf OnStop},
            {PauseCommand, AddressOf OnPause},
            {GoToCommand, AddressOf OnGoTo},
            {ConfirmCommand, AddressOf OnConfirm},
            {SetFlagCommand, AddressOf OnSetFlag},
            {ClearFlagCommand, AddressOf OnClearFlag},
            {ToggleFlagCommand, AddressOf OnToggleFlag},
            {OnFlagGoToCommand, AddressOf OnOnFlagGoTo},
            {CopyFlagCommand, AddressOf OnCopyFlag}
        }

    Private Sub DoCommand(command As String, body As String)
        _commandTable(command)(Me, body)
    End Sub

    Friend Function DoConfirm() As Boolean
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
