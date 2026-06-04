using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.Civil.DatabaseServices;
using Jacobian.Civil3D.Utilities;

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

        public List<Civil3DCorridor> Corridors;

        public List<Civil3DBlock> Blocks;

        public Civil3DMVVMModel(Document doc)
        {
            civilDoc = doc;
            Corridors = Collectors.GetCorridors(doc);
            Blocks = Collectors.GetBlockDefinitions(doc);
        }

        /// <summary>
        /// Opens the corridor with the given ObjectId inside a transaction
        /// and returns its baselines wrapped as <see cref="Civil3DBaseline"/> objects.
        /// </summary>
        public List<Civil3DBaseline> GetBaselinesForCorridor(ObjectId corridorId)
        { 
        
            var baseLines = new List<Civil3DBaseline>();

            using(Transaction tr = civilDoc.TransactionManager.StartTransaction())
            {
                var corridor = tr.GetObject(corridorId, OpenMode.ForRead) as Corridor;

                if(corridor != null)
                {
                    foreach (Baseline bLine in corridor.Baselines)
                    {
                        baseLines.Add(new Civil3DBaseline(bLine.Name,
                            bLine.BaselineGuid, bLine.MainBaselineFeatureLines));
                    }
                }

                tr.Commit();
            }
            return baseLines;
        }
    }
}
