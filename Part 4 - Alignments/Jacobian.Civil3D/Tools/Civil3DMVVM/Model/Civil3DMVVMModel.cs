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
        /// <summary>
        /// The Civil 3D document.
        /// </summary>
        public Document civilDoc;

        /// <summary>
        /// All corridors collected at startup.
        /// </summary>
        public List<Civil3DCorridor> Corridors;

        /// <summary>
        /// All block definitions in the drawing collected at startup.
        /// </summary>
        public List<Civil3DBlock> Blocks;

        /// <summary>
        /// All alignments collected at startup.
        /// </summary>
        public List<Civil3DAlignment> Alignments;


        public Civil3DMVVMModel(Document doc)
        {
            civilDoc = doc;
            Corridors = Collectors.GetCorridors(doc);
            Blocks = Collectors.GetBlockDefinitions(doc);
            Alignments = Collectors.GetAlignments(doc);
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

        /// <summary>
        /// Opens the alignment with the given ObjectId inside a transaction
        /// and returns all its profiles wrapped as <see cref="Civil3DProfile"/> objects.
        ///
        /// Called by the ViewModel's OnSelectedAlignmentChanged partial method
        /// whenever the user picks a different alignment.
        /// </summary>
        public List<Civil3DProfile> GetProfilesForAlignment(ObjectId alignmentId)
        {
            var profiles = new List<Civil3DProfile>();

            using (Transaction tr = civilDoc.TransactionManager.StartTransaction())
            {
               var alignment = tr.GetObject(alignmentId, OpenMode.ForRead) as Alignment;

                if (alignment != null)
                {
                    foreach (ObjectId profileId in alignment.GetProfileIds())
                    {
                        var profile = tr.GetObject(profileId, OpenMode.ForRead) as Profile;

                        if (profile != null)
                        {
                            profiles.Add(new Civil3DProfile(
                                profile.Name,
                                profile.ObjectId,
                                profile.ProfileType.ToString(),
                                profile.ElevationMin,
                                profile.ElevationMax
                            ));
                        }
                    }
                }

                tr.Commit();

            }

            return profiles;
        }
    }
}
