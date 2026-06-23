using Autodesk.Civil.DatabaseServices;

namespace Jacobian.Civil3D.Tools.Civil3DMVVM;

/// <summary>
/// Lightweight wrapper around a Civil 3D Baseline.
/// Stores the raw BaselineFeatureLines reference so feature line data
/// can be iterated without re-opening a transaction.
/// </summary>
public class Civil3DBaseline
{
    /// <summary>
    /// Gets or sets the name of the baseline.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the baseline.
    /// </summary>
    public Guid GUID { get; set; }

    /// <summary>
    /// Gets or sets the feature lines associated with the baseline.
    /// </summary>
    public BaselineFeatureLines BaselineFeatureLines { get; set; }

    public Civil3DBaseline(string name, Guid guid, BaselineFeatureLines baselineFeatureLines)
    {
        Name = name;
        GUID = guid;
        BaselineFeatureLines = baselineFeatureLines; 
    }
}