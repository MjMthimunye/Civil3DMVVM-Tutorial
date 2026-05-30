using CommunityToolkit.Mvvm.ComponentModel;
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

        [ObservableProperty]
        private ObservableCollection<Civil3DCorridor> _inputCorridors = new();

        [ObservableProperty]
        private string _corridorFilterString;

        [ObservableProperty]
        private Civil3DCorridor _selectedInputCorridor;

        [ObservableProperty]
        private ObservableCollection<Civil3DBaseline> _inputBaselines = new();

        [ObservableProperty]
        private Civil3DBaseline _selectedInputBaseline;

        [ObservableProperty]
        private ObservableCollection<Civil3DBaselineFeatureLine> _inputBaselineFeatureLines = new();

        [ObservableProperty]
        private Civil3DBaselineFeatureLine _selectedInputBaselineFeatureLine;
        #endregion

        // Constructor
        public Civil3DMVVMViewModel(Civil3DMVVMModel model)
        {
            _model = model;
            model.Corridors.ForEach(c => InputCorridors.Add(c));
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
        #endregion
    }
}
