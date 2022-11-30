Public Class WorldData
    Public Property PlayerFellowshipId As Guid?
    Public Property Fellowships As New Dictionary(Of Guid, FellowshipData)
    Public Property Ships As New Dictionary(Of Guid, ShipData)
End Class
