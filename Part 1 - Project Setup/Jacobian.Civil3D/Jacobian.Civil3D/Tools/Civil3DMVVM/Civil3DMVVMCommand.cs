using Autodesk.AutoCAD.ApplicationServices;
using Jacobian.Civil3D.Tools.Civil3DMVVM.Model;
using Jacobian.Civil3D.Tools.Civil3DMVVM.View;
using Jacobian.Civil3D.Tools.Civil3DMVVM.ViewModel;

namespace Jacobian.Civil3D.Tools.Civil3DMVVM
{
    /// <summary>
    /// ICommand implementation registered as the ribbon button's CommandHandler.
    ///
    /// When the button is clicked, this:
    ///   1. Captures the active document.
    ///   2. Constructs the Model (data access layer).
    ///   3. Constructs the ViewModel, passing in the Model.
    ///   4. Constructs the View, sets its DataContext to the ViewModel.
    ///   5. Shows the WPF window modelessly so Civil 3D remains interactive.
    /// </summary>
    public class Civil3DMVVMCommandHandler : System.Windows.Input.ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument;

            // MVVM wiring 
            var model = new Civil3DMVVMModel(doc);
            var viewModel = new Civil3DMVVMViewModel(model);
            var view = new Civil3DMVVMView { DataContext = viewModel };

            // ShowModelessWindow keeps Civil 3D usable while the panel is open
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModelessWindow(view);
        }
    }
}
