using Autodesk.AutoCAD.DatabaseServices;

namespace Jacobian.Civil3D.Tools.Civil3DMVVM;

/// <summary>
/// Lightweight wrapper around a Civil 3D Alignment.
/// Stores the key properties the UI needs — name, length, station range —
/// without requiring an open transaction.
/// </summary>
public class Civil3DAlignment
{
    /// <summary>
    /// The name of the alignment.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The unique object identifier of the alignment in the drawing.
    /// </summary>
    public ObjectId Id { get; set; }

    /// <summary>
    /// Total length of the alignment in drawing units.
    /// </summary>
    public double Length { get; set; }

    /// <summary>
    /// Starting station value.
    /// </summary>
    public double StartStation { get; set; }

    /// <summary>
    /// Ending station value.
    /// </summary>
    public double EndStation { get; set; }

    public Civil3DAlignment(string name, ObjectId id, double length, double startStation, double endStation)
    {
        Name = name;
        Id = id;
        Length = length;
        StartStation = startStation;
        EndStation = endStation;
    }
}