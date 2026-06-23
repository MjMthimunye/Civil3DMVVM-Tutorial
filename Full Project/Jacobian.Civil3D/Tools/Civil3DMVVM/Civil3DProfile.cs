using Autodesk.AutoCAD.DatabaseServices;

namespace Jacobian.Civil3D.Tools.Civil3DMVVM;

/// <summary>
/// Lightweight wrapper around a Civil 3D Profile.
/// Stores display-friendly properties without requiring an open transaction.
/// </summary>
public class Civil3DProfile
{
    /// <summary>
    /// The name of the profile.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The unique identifier for the profile object.
    /// </summary>
    public ObjectId Id { get; set; }

    /// <summary>
    /// The profile type description, e.g. "Surface", "Layout", "Quick".
    /// </summary>
    public string ProfileType { get; set; }

    /// <summary>
    /// Minimum elevation along the profile.
    /// </summary>
    public double MinElevation { get; set; }

    /// <summary>
    /// Maximum elevation along the profile.
    /// </summary>
    public double MaxElevation { get; set; }

    public Civil3DProfile(string name, ObjectId id, string profileType, double minElevation, double maxElevation)
    {
        Name = name;
        Id = id;
        ProfileType = profileType;
        MinElevation = minElevation;
        MaxElevation = maxElevation;
    }
}