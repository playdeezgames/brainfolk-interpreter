Public Class World
    Private _worldData As WorldData
    Sub New()
        _worldData = New WorldData

        _worldData.PlayerFellowshipId = CreateFellowship("Yer Company").Id

        _worldData.Ships.Add(Guid.NewGuid, New ShipData With {.Name = "Yer Ship", .FellowshipId = PlayerFellowship.Id})

        _worldData.Ships.Add(Guid.NewGuid, New ShipData With {.Name = "Derelict Ship", .FellowshipId = Guid.Empty})
    End Sub
    Private Function CreateFellowship(name As String) As Fellowship
        Dim id = Guid.NewGuid
        _worldData.Fellowships.Add(id, New FellowshipData With {.Name = name})
        Return New Fellowship(_worldData, id)
    End Function
    Public ReadOnly Property PlayerFellowship As Fellowship
        Get
            If _worldData.PlayerFellowshipId.HasValue Then
                Return New Fellowship(_worldData, _worldData.PlayerFellowshipId.Value)
            End If
            Return Nothing
        End Get
    End Property
End Class
