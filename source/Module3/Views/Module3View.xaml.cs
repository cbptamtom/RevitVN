using Module3.ViewModels;

namespace Module3.Views;

public sealed partial class Module3View
{
    public Module3View(Module3ViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}