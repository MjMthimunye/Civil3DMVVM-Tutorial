using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.Civil.ApplicationServices;
using Autodesk.Civil.DatabaseServices;
using Jacobian.Civil3D.Tools.Civil3DMVVM;

namespace Jacobian.Civil3D.Utilities;

/// <summary>
/// Static helpers for collecting Civil 3D and AutoCAD objects.
///
/// Every method receives the active <see cref="Document"/> as a parameter
/// rather than caching it in a static field. This avoids the
/// <c>AccessViolationException</c> that occurs when static readonly fields
/// are initialised before Civil 3D's document context is fully ready
/// (e.g. on first plugin launch after AutoCAD startup).
/// </summary>
public static class Collectors
{
    /// <summary>
    /// Returns all corridors in the active Civil 3D document,
    /// wrapped as lightweight <see cref="Civil3DCorridor"/> objects.
    /// </summary>
    public static List<Civil3DCorridor> GetCorridors(Document doc)
    {
        var results = new List<Civil3DCorridor>();
        var civilDoc = CivilApplication.ActiveDocument;
        var db = doc.Database;

        using (Transaction tr = db.TransactionManager.StartTransaction())
        {
            foreach(ObjectId corridorId in civilDoc.CorridorCollection)
            {
                var corridor = tr.GetObject(corridorId, OpenMode.ForRead) as Corridor;
                if (corridor != null)
                {
                    results.Add(new Civil3DCorridor(corridor.Name, corridor.ObjectId)); 
                }
            }

            tr.Commit();
        }

        return results;
    }


    /// <summary>
    /// Returns all block definitions in the active document,
    /// wrapped as lightweight <see cref="Civil3DBlock"/> objects.
    /// Excludes anonymous blocks, layouts, and external references.
    /// </summary>
    public static List<Civil3DBlock> GetBlockDefinitions(Document doc) 
    {
        var results = new List<Civil3DBlock>();
        var db = doc.Database;

        using(Transaction tr = db.TransactionManager.StartTransaction())
        {
            var bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);

            foreach(ObjectId blockId in bt)
            {
                var block = tr.GetObject(blockId, OpenMode.ForRead) as BlockTableRecord;

                if (block.IsAnonymous || block.IsLayout)
                    continue;

                if(string.IsNullOrEmpty(block.Name) || block.Name.StartsWith("*", StringComparison.Ordinal))
                    continue;

                if (block.IsFromExternalReference)
                    continue;

                results.Add(new Civil3DBlock(block.Name, block.ObjectId));
            }

            tr.Commit();
        }

        results.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase));
        return results;
    }
}