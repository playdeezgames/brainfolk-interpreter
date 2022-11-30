Public Class Ship
    Private _worldData As WorldData
    Public ReadOnly Id As Guid
    Sub New(worldData As WorldData, id As Guid)
        _worldData = worldData
        Me.Id = id
    End Sub
    ReadOnly Property Name As String
        Get
            Return _worldData.Ships(Id).Name
        End Get
    End Property
End Class
