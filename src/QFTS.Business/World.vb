Public Class World
    Private _worldData As WorldData
    Sub New()
        _worldData = New WorldData

        _worldData.PlayerFellowshipId = CreateFellowship("Yer Company").Id

        CreateShip("Yer Ship", PlayerFellowship)
        CreateShip("Derelict Ship", Nothing)
    End Sub
    Private Function CreateFellowship(name As String) As Fellowship
        Dim id = Guid.NewGuid
        _worldData.Fellowships.Add(id, New FellowshipData With {.Name = name})
        Return New Fellowship(_worldData, id)
    End Function
    Private Function CreateShip(name As String, owner As Fellowship) As Ship
        Dim id = Guid.NewGuid
        _worldData.Ships.Add(id, New ShipData With {.Name = name, .FellowshipId = If(owner Is Nothing, Guid.Empty, owner.Id)})
        Return New Ship(_worldData, id)
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
