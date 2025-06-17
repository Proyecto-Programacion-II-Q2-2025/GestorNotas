

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestorNotas.Models;
using GestorNotas.Services;

namespace GestorNotas.ViewModels
{
    public partial class AddEditViewModel: ObservableObject
    {
        [ObservableProperty]
        private int id;
        [ObservableProperty]
        private string? titulo;
        [ObservableProperty]
        private string? contenido;

        private NotaService _service;

        public AddEditViewModel()
        {
            _service = new NotaService();
        }

        public AddEditViewModel(Nota Nota)
        {
            _service = new NotaService();
            id = Nota.Id;
            titulo = Nota.Titulo;
            contenido = Nota.Contenido;
        }

        private void Alerta(string Titulo, string Mensaje)
        {
            MainThread.BeginInvokeOnMainThread(async () => await App.Current!.MainPage!.DisplayAlert(Titulo, Mensaje, "Aceptar"));
        }

        private bool Validar(Nota Nota)
        {
            if (Nota.Titulo is null || Nota.Titulo == "")
            {
                Alerta("ADVERTENCIA", "Ingrese el titulo de la nota");
                return false;
            }
            else if (Nota.Contenido is null || Nota.Contenido == "")
            {
                Alerta("ADVERTENCIA", "Ingrese el contenido de la nota");
                return false;
            }

            return true;
        }

        [RelayCommand]
        private async Task AddUpdate()
        {
            try
            {
                Nota Nota = new Nota()
                {
                    Id = id,
                    Titulo = titulo,
                    Contenido = contenido
                };

                if (Validar(Nota))
                {
                    if (id == 0)
                    {
                        _service.Insert(Nota);
                    }
                    else
                    {
                        _service.Update(Nota);
                    }

                    await App.Current!.MainPage!.Navigation.PopAsync();

                }

            }
            catch (Exception ex)
            {
                Alerta("ERROR", ex.Message);
                throw;
            }
        }


    }
}

    
