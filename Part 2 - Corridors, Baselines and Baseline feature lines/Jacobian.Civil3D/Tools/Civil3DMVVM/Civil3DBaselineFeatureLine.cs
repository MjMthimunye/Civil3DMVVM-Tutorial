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
    public string Name { get; set; }

    public ObjectId CorridorId { get; set; }

    public ObjectId StyleId { get; set; }

    public List<FeatureLinePoint> FeatureLinePoints { get; set; }

    public Civil3DBaselineFeatureLine(string name, ObjectId corridorId, ObjectId styleId, List<FeatureLinePoint> featureLinePoints)
    {
        Name = name;
        CorridorId = corridorId;
        StyleId = styleId;
        FeatureLinePoints = featureLinePoints; 
    }
}