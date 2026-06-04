using Autodesk.Civil.DatabaseServices;

namespace Jacobian.Civil3D.Tools.Civil3DMVVM;

/// <summary>
/// Lightweight wrapper around a Civil 3D Baseline.
/// Stores the raw BaselineFeatureLines reference so feature line data
/// can be iterated without re-opening a transaction.
/// </summary>
public class Civil3DBaseline
{
    public string Name { get; set; }

    public Guid GUID { get; set; }

    public BaselineFeatureLines BaselineFeatureLines { get; set; }

    public Civil3DBaseline(string name, Guid guid, BaselineFeatureLines baselineFeatureLines)
    {
        Name = name;
        GUID = guid;
        BaselineFeatureLines = baselineFeatureLines; 
    }
}