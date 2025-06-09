using Bytagramm.ViewModels.Community;

namespace Bytagramm.Views.Community;

public partial class CommunityDetailsPage : ContentPage
{
    public CommunityDetailsPage()
    {
        InitializeComponent();
        var viewModel = MauiProgram.Current.GetService<CommunityDetailsViewModel>();
        BindingContext = viewModel;
    }
}