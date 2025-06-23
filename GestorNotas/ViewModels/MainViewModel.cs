

using CommunityToolkit.Mvvm.ComponentModel;
using GestorNotas.Models;
using GestorNotas.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;

public partial class NotaViewModel : ObservableObject
{
    public ObservableCollection<Nota> ListaNotas { get; set; } = new();
    private NotaService notaService = new();

    private string textoBusqueda;
    public string TextoBusqueda
    {
        get => textoBusqueda;
        set
        {
            textoBusqueda = value;
            OnPropertyChanged();
            BuscarNotas();
        }
    }

    public ICommand CompartirCommand { get; }
    public ICommand EliminarCommand { get; }
    public ICommand AgregarCommand { get; }

    public MainViewModel()
    {
        CompartirCommand = new Command<Nota>(CompartirNota);
        EliminarCommand = new Command<Nota>(EliminarNota);
        AgregarCommand = new Command(AgregarNota);
        BuscarCommand = new Command(async () => await BuscarNotas());

        _ = CargarNotas();
    }

    private void CargarNotas()
    {
        ListaNotas.Clear();
        var notas = notaService.ObtenerNotas();
        foreach (var nota in notas)
            ListaNotas.Add(nota);
    }

    private async Task BuscarNotas()
    {
        ListaNotas.Clear();
        var notas = await notaService.ObtenerNotas();

        var filtradas = string.IsNullOrWhiteSpace(TextoBusqueda)
            ? notas
            : notas.Where(n =>
                (n.Titulo?.Contains(TextoBusqueda, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (n.Contenido?.Contains(TextoBusqueda, StringComparison.OrdinalIgnoreCase) ?? false));

        foreach (var nota in filtradas)
            ListaNotas.Add(nota);
    }


    private async void CompartirNota(Nota nota)
    {
        if (nota is not null)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Title = nota.Titulo ?? "Nota",
                Text = nota.Contenido ?? ""
            });
        }
    }

    private void EliminarNota(Nota nota)
    {
        if (nota is not null && ListaNotas.Contains(nota))
            ListaNotas.Remove(nota);
    }

    private void AgregarNota()
    {
        ListaNotas.Add(new Nota { Titulo = "Nueva Nota", Contenido = "" });
    }

}

