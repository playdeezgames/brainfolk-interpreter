Public Class Ship
    Private _worldData As WorldData
    Public ReadOnly Id As Guid
    Sub New(worldData As WorldData, id As Guid)
        _worldData = worldData
        Me.Id = id
    End Sub
    Property Name As String
        Get
            Return _worldData.Ships(Id).Name
        End Get
        Set(value As String)
            _worldData.Ships(Id).Name = value
        End Set
    End Property
    ReadOnly Property UniqueName As String
        Get
            Return $"{Name} (#{Id})"
        End Get
    End Property
    ReadOnly Property XYZ As (Double, Double, Double)
        Get
            Return _worldData.Ships(Id).XYZ
        End Get
    End Property
End Class
