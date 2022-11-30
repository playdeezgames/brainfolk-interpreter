Public Class World
    Private _worldData As WorldData
    Sub New()
        _worldData = New WorldData
        Dim playerFellowshipId = Guid.NewGuid
        _worldData.PlayerFellowshipId = playerFellowshipId
        _worldData.Fellowships.Add(playerFellowshipId, New FellowshipData With {.Name = "Yer Company"})
        _worldData.Ships.Add(Guid.NewGuid, New ShipData With {.Name = "Yer Ship", .FellowshipId = playerFellowshipId})
        _worldData.Ships.Add(Guid.NewGuid, New ShipData With {.Name = "Derelict Ship", .FellowshipId = Guid.Empty})
    End Sub
    Public ReadOnly Property PlayerFellowship As Fellowship
        Get
            If _worldData.PlayerFellowshipId.HasValue Then
                Return New Fellowship(_worldData, _worldData.PlayerFellowshipId.Value)
            End If
            Return Nothing
        End Get
    End Property
End Class
