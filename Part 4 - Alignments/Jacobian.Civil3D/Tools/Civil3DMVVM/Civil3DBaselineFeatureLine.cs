using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.Civil.DatabaseServices;

namespace Jacobian.Civil3D.Tools.Civil3DMVVM;

/// <summary>
/// Lightweight wrapper around a Civil 3D baseline feature line.
/// Stores the 3D point geometry (FeatureLinePoints) needed for
/// polyline creation and station-based geometry operations.
/// </summary>
public class Civil3DBaselineFeatureLine
{
    /// <summary>
    /// Gets or sets the name of the baseline feature line.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the associated corridor object.
    /// </summary>
    public ObjectId CorridorId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the style applied to this feature line.
    /// </summary>
    public ObjectId StyleId { get; set; }

    /// <summary>
    /// Gets or sets the collection of points that define the geometry of this feature line.
    /// </summary>
    public List<FeatureLinePoint> FeatureLinePoints { get; set; }

    public Civil3DBaselineFeatureLine(string name, ObjectId corridorId, ObjectId styleId, List<FeatureLinePoint> featureLinePoints)
    {
        Name = name;
        CorridorId = corridorId;
        StyleId = styleId;
        FeatureLinePoints = featureLinePoints; 
    }
}