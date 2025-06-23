using CommunityToolkit.Mvvm.ComponentModel;
using GestorNotas.Models;
using GestorNotas.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GestorNotas.ViewModels
{
    public partial class NotaViewModel : ObservableObject
    {
        private readonly NotaService _service = new();

        [ObservableProperty]
        private string titulo;

        [ObservableProperty]
        private string contenido;

        [ObservableProperty]
        private string textoBusqueda;

        [ObservableProperty]
        private Nota notaSeleccionada;

        public ObservableCollection<Nota> ListaNotas { get; set; } = new();

        public ICommand AgregarCommand { get; }
        public ICommand EliminarCommand { get; }
        public ICommand EditarCommand { get; }
        public ICommand CompartirCommand { get; }
        public ICommand BuscarCommand { get; }

        public NotaViewModel()
        {
            AgregarCommand = new Command(async () => await AgregarNota());
            EliminarCommand = new Command(async () => await EliminarNota());
            EditarCommand = new Command(async () => await EditarNota());
            CompartirCommand = new Command(async () => await CompartirNota());
            BuscarCommand = new Command(async () => await BuscarNotas());

            _ = CargarNotas();
        }

        public async Task CargarNotas()
        {
            ListaNotas.Clear();
            var notas = await _service.ObtenerNotas();
            foreach (var nota in notas)
                ListaNotas.Add(nota);
        }

        private async Task AgregarNota()
        {
            var nota = new Nota { Titulo = Titulo, Contenido = Contenido };
            await _service.AgregarNota(nota);
            await CargarNotas();
            Titulo = Contenido = string.Empty;
        }

        private async Task EditarNota()
        {
            if (NotaSeleccionada == null) return;

            NotaSeleccionada.Titulo = Titulo;
            NotaSeleccionada.Contenido = Contenido;
            await _service.ActualizarNota(NotaSeleccionada);
            await CargarNotas();
        }

        private async Task EliminarNota()
        {
            if (NotaSeleccionada == null) return;
            await _service.EliminarNota(NotaSeleccionada);
            await CargarNotas();
        }

        private async Task CompartirNota()
        {
            if (NotaSeleccionada == null) return;

            await Share.RequestAsync(new ShareTextRequest
            {
                Title = NotaSeleccionada.Titulo,
                Text = NotaSeleccionada.Contenido
            });
        }

        private async Task BuscarNotas()
        {
            var notas = await _service.ObtenerNotas();
            var filtradas = notas.Where(n =>
                (n.Titulo?.Contains(TextoBusqueda, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (n.Contenido?.Contains(TextoBusqueda, StringComparison.OrdinalIgnoreCase) ?? false)).ToList();

            ListaNotas.Clear();
            foreach (var nota in filtradas)
                ListaNotas.Add(nota);
        }
    }
}

