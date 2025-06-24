using GestorNotas.Models;
using GestorNotas.ViewModels;

namespace GestorNotas.Views;

public partial class AddEditViews : ContentPage
{
    private AddEditViewModel viewModel;
    public AddEditViews()
    {
        InitializeComponent();
        viewModel = new AddEditViewModel();
        this.BindingContext = viewModel;
    }

    public AddEditViews(Notas notas)
    {
        InitializeComponent();
        viewModel = new AddEditViewModel(notas);
        this.BindingContext = viewModel;
    }
}