using CommunityToolkit.Mvvm.ComponentModel;
using Jacobian.Civil3D.Tools.Civil3DMVVM.Model;

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
        private readonly Civil3DMVVMModel _model;

        // Constructor
        public Civil3DMVVMViewModel(Civil3DMVVMModel model)
        {
            _model = model;
        }
    }
}
