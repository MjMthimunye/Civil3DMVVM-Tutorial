using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jacobian.Civil3D.Tools.Civil3DMVVM.Model;
using System.Collections.ObjectModel;

namespace Jacobian.Civil3D.Tools.Civil3DMVVM.ViewModel
{
    /// <summary>
    /// ViewModel for the Civil3D MVVM tutorial panel.
    ///
    /// Follows the MVVM pattern:
    ///   - Properties decorated with [ObservableProperty] auto-generate
    ///     INotifyPropertyChanged plumbing via CommunityToolkit.Mvvm.
    ///   - Partial methods (OnXChanged) react to selection changes and cascade
    ///     data (corridor → baselines → feature lines) without any code-behind.
    ///   - [RelayCommand] auto-generates ICommand properties bound in XAML.
    ///   - The View never calls Civil 3D APIs directly — only this class does.
    /// </summary>
    public partial class Civil3DMVVMViewModel : ObservableObject
    {
        #region Fields and Properties
        private readonly Civil3DMVVMModel _model;

        // Corridor 
        [ObservableProperty]
        private ObservableCollection<Civil3DCorridor> _inputCorridors = new();

        [ObservableProperty]
        private string _corridorFilterString;

        [ObservableProperty]
        private Civil3DCorridor _selectedInputCorridor;

        // Baselines 
        [ObservableProperty]
        private ObservableCollection<Civil3DBaseline> _inputBaselines = new();

        [ObservableProperty]
        private Civil3DBaseline _selectedInputBaseline;
        
        // Feature Lines
        [ObservableProperty]
        private ObservableCollection<Civil3DBaselineFeatureLine> _inputBaselineFeatureLines = new();

        [ObservableProperty]
        private Civil3DBaselineFeatureLine _selectedInputBaselineFeatureLine;

        // Blocks
        [ObservableProperty]
        private ObservableCollection<Civil3DBlock> _blocks = new();

        [ObservableProperty]
        private string _blockSignageFilterString;

        [ObservableProperty]
        private Civil3DBlock _selectedBlock;

        // Alignments
        [ObservableProperty]
        private ObservableCollection<Civil3DAlignment> _alignments = new();

        [ObservableProperty]
        private Civil3DAlignment _selectedAlignment;

        [ObservableProperty]
        private string _alignmentFilterString;

        // Profiles (cascade from Alignment)
        [ObservableProperty]
        private ObservableCollection<Civil3DProfile> _profiles = new();

        [ObservableProperty]
        private Civil3DProfile _selectedProfile;

        // Alignment detail properties (shown when an alignment is selected)
        [ObservableProperty]
        private string _alignmentLength = "-";

        [ObservableProperty]
        private string _alignmentStartStation = "-";

        [ObservableProperty]
        private string _alignmentEndStation = "-";
        #endregion

        // Constructor
        public Civil3DMVVMViewModel(Civil3DMVVMModel model)
        {
            _model = model;

            // Pre-populate from the model
            model.Corridors.ForEach(c => InputCorridors.Add(c));
            model.Blocks.ForEach(b => Blocks.Add(b));
            model.Alignments.ForEach(a => Alignments.Add(a));
        }

        #region Partial methods reacting to selection changes
        partial void OnSelectedInputCorridorChanged(Civil3DCorridor value)
        {
            InputBaselines.Clear();
            InputBaselineFeatureLines.Clear();
            if (value != null)
            {
                var baselines = _model.GetBaselinesForCorridor(value.Id);
                baselines.ForEach(b => InputBaselines.Add(b));
            }
        }

        partial void OnSelectedInputBaselineChanged(Civil3DBaseline value)
        {
            InputBaselineFeatureLines.Clear();

            if (value.BaselineFeatureLines.FeatureLineCollectionMap == null)
                return;

            var foundCodes = new HashSet<string>(StringComparer.Ordinal);

            foreach (var collection in value.BaselineFeatureLines.FeatureLineCollectionMap)
            {
                if (collection == null) continue;

                foreach (var featureLine in collection)
                {
                    if (!foundCodes.Add(featureLine.CodeName)) continue;

                    InputBaselineFeatureLines.Add(new Civil3DBaselineFeatureLine(
                        featureLine.CodeName,
                        featureLine.CorridorId,
                        featureLine.StyleId,
                        featureLine.FeatureLinePoints.ToList()));
                }
            }
        }
         
        partial void OnSelectedAlignmentChanged(Civil3DAlignment value)
        {
            Profiles.Clear();
            SelectedProfile = null;

            if(value == null)
            {
                AlignmentLength = "-";
                AlignmentStartStation = "-";
                AlignmentEndStation = "-";
                return;
            }

            AlignmentLength = $"{value.Length:F2}";
            AlignmentStartStation = $"{value.StartStation:F2}";
            AlignmentEndStation = $"{value.EndStation:F2}";

            var profiles = _model.GetProfilesForAlignment(value.Id);
            profiles.ForEach(p => Profiles.Add(p));
        }

        partial void OnCorridorFilterStringChanged(string value)
        {
            InputCorridors.Clear();

            if (!string.IsNullOrEmpty(value))
            {
                _model.Corridors.Where(x => x.Name.ToLower().Contains(value.ToLower()))
                    .ToList()
                    .ForEach(c => InputCorridors.Add(c));
            }
            else
            {
                _model.Corridors.ForEach(c => InputCorridors.Add(c));
            }
        }

        partial void OnBlockSignageFilterStringChanged(string value)
        {
            Blocks.Clear();

            if (!string.IsNullOrEmpty(value))
            {
                _model.Blocks.Where(x => x.Name.ToLower().Contains(value.ToLower()))
                    .ToList()
                    .ForEach(b => Blocks.Add(b));
            }
            else
            {
                _model.Blocks.ForEach(b => Blocks.Add(b));
            }
        }
        
        partial void OnAlignmentFilterStringChanged(string value)
        {
            Alignments.Clear();

            if (!string.IsNullOrEmpty(value))
            {
                _model.Alignments.Where(x => x.Name.ToLower().Contains(value.ToLower()))
                    .ToList()
                    .ForEach(a => Alignments.Add(a));
            }
            else
            {
                _model.Alignments.ForEach(a => Alignments.Add(a));
            }
        }
        #endregion

        #region Commands
        [RelayCommand]
        private void PlaceBlock()
        {
            if (SelectedBlock == null)
            {
                System.Windows.MessageBox.Show("Please select a block to place.");
                return;
            }

            try
            {
                Database db = _model.civilDoc.Database;

                using DocumentLock docLock = _model.civilDoc.LockDocument();
                using Transaction tr = db.TransactionManager.StartTransaction();

                BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);

                if (!bt.Has(SelectedBlock.Name))
                {
                    System.Windows.MessageBox.Show($"Block '{SelectedBlock.Name}' not found in the drawing.");
                    return;
                }

                BlockTableRecord ms = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                Point3d insertPoint = Point3d.Origin;
                var br = new BlockReference(insertPoint, bt[SelectedBlock.Name]);

                ms.AppendEntity(br);
                tr.AddNewlyCreatedDBObject(br, true);
                tr.Commit();

                MessageBox.Show($"Block '{SelectedBlock.Name}' placed at {insertPoint}.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error placing block: {ex.Message}");
            }
        }
        #endregion

    }
}
