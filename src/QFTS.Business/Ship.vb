Public Class Ship
    Private _worldData As WorldData
    Private _id As Guid
    Sub New(worldData As WorldData, id As Guid)
        _worldData = worldData
        _id = id
    End Sub
    ReadOnly Property Name As String
        Get
            Return _worldData.Ships(_id).Name
        End Get
    End Property
End Class
