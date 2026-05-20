using Autodesk.AutoCAD.ApplicationServices;

namespace Jacobian.Civil3D.Tools.Civil3DMVVM.Model
{
    /// <summary>
    /// The Model in the MVVM pattern.
    ///
    /// Responsibilities:
    ///   - Owns all Civil 3D / AutoCAD data access.
    ///   - Exposes pre-collected lists so the ViewModel never opens transactions itself.
    ///   - Provides on-demand queries (e.g. baselines for a corridor, profiles for an alignment).
    ///
    /// The ViewModel depends on this class; the View never touches it directly.
    /// </summary>
    public class Civil3DMVVMModel
    {
        public Document civilDoc;

        public Civil3DMVVMModel(Document doc)
        {
            civilDoc = doc;
        }
    }
}
