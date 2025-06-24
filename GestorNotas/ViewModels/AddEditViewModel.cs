using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestorNotas.Models;
using GestorNotas.Services;



namespace GestorNotas.ViewModels
{
    public partial class AddEditViewModel : ObservableObject
    {
        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private string titulo;

        [ObservableProperty]
        private string contenido;



        private readonly NotasServices _service;

        public AddEditViewModel()
        {
            _service = new NotasServices();
        }

        public AddEditViewModel(Notas Notas)
        {
            _service = new NotasServices();
            Id = Notas.Id;
            Titulo = Notas.Titulo;
            Contenido = Notas.Contenido;

        }

        private void Alerta(string Titulo, string Mensaje)
        {
            MainThread.BeginInvokeOnMainThread(async () => await App.Current!.MainPage!.DisplayAlert(Titulo, Mensaje, "Aceptar"));
        }

        [RelayCommand]
        private async Task AddUpdate()
        {
            try
            {
                

                Notas Notas = new Notas()
                {
                    Id = Id,
                    Titulo = Titulo,
                    Contenido = Contenido,

                };

                if (Notas.Titulo == null || Notas.Titulo == "")
                {
                    Alerta("ADVERTENCIA", "Nota en blanco.");
                }
                else
                {
                    if (Id == 0)
                    {
                        _service.Insert(Notas);
                    }
                    else
                    {
                        _service.Update(Notas);
                    }

                    await App.Current!.MainPage!.Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                Alerta("ERROR", ex.Message);
            }
        }
    }
}
