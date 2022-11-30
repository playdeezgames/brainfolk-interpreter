Public Class Fellowship
    Private _worldData As WorldData
    Private _id As Guid
    Sub New(worldData As WorldData, id As Guid)
        _worldData = worldData
        _id = id
    End Sub
    ReadOnly Property Name As String
        Get
            Return _worldData.Fellowships(_id).Name
        End Get
    End Property
    ReadOnly Property Ships As IEnumerable(Of Ship)
        Get
            Return _worldData.Ships.Where(Function(x) x.Value.FellowshipId = _id).Select(Function(x) New Ship(_worldData, x.Key))
        End Get
    End Property
End Class
