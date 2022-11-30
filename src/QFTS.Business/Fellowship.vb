Public Class Fellowship
    Private _worldData As WorldData
    Public ReadOnly Property Id As Guid
    Sub New(worldData As WorldData, id As Guid)
        _worldData = worldData
        Me.Id = id
    End Sub
    Property Name As String
        Get
            Return _worldData.Fellowships(Id).Name
        End Get
        Set(value As String)
            _worldData.Fellowships(Id).Name = value
        End Set
    End Property
    ReadOnly Property Ships As IEnumerable(Of Ship)
        Get
            Return _worldData.Ships.Where(Function(x) x.Value.FellowshipId = Id).Select(Function(x) New Ship(_worldData, x.Key))
        End Get
    End Property
End Class
