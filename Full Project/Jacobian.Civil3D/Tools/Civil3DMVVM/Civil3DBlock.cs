using Autodesk.AutoCAD.DatabaseServices;

namespace Jacobian.Civil3D.Tools.Civil3DMVVM;

/// <summary>
/// Represents a Civil 3D Block object with its associated metadata.
/// This class serves as a data model for MVVM binding, encapsulating the block's name and ObjectId reference.
/// </summary>
public class Civil3DBlock
{
    /// <summary>
    /// The name of the Civil 3D block.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The object ID of the Civil 3D block.
    /// </summary>
    public ObjectId Id { get; set; }

    public Civil3DBlock(string name, ObjectId id)
    {
        Name = name;
        Id = id;
    }
}