using Autodesk.AutoCAD.DatabaseServices;

namespace Jacobian.Civil3D.Tools.Civil3DMVVM;

/// <summary>
/// Lightweight wrapper around a Civil 3D Corridor.
/// Keeps only what the UI needs — name for display, ObjectId for API calls.
/// </summary>
public class Civil3DCorridor
{
    public string Name { get; set; }
    public ObjectId Id { get; set; }

    public Civil3DCorridor(string name, ObjectId id)
    {
        Name = name;
        Id = id; 
    }
}