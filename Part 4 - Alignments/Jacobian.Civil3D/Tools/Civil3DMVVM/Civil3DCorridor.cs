using Autodesk.AutoCAD.DatabaseServices;

namespace Jacobian.Civil3D.Tools.Civil3DMVVM;

/// <summary>
/// Lightweight wrapper around a Civil 3D Corridor.
/// Keeps only what the UI needs — name for display, ObjectId for API calls.
/// </summary>
public class Civil3DCorridor
{
    /// <summary>
    /// Gets or sets the name of the corridor.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the Civil 3D ObjectId associated with this corridor.
    /// </summary>
    public ObjectId Id { get; set; }

    public Civil3DCorridor(string name, ObjectId id)
    {
        Name = name;
        Id = id; 
    }
}