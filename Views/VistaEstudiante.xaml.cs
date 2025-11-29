using System.Net.Http;
using lgavidias6.Models;
using Newtonsoft.Json;

namespace lgavidias6.Views;

public partial class VistaEstudiante : ContentPage
{
    private const string URL = "http://localhost:8080/moviles/wsestudiante.php";
    private readonly HttpClient cliente = new HttpClient();

    public VistaEstudiante()
    {
        InitializeComponent();
        // No esperes en el constructor, solo dispara la carga
        _ = Mostrar();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = Mostrar();
    }

    private async Task Mostrar()
    {
        try
        {
            var json = await cliente.GetStringAsync(URL);
            var lista = JsonConvert.DeserializeObject<List<Estudiante>>(json);

            lvEstudiantes.ItemsSource = lista;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo cargar los estudiantes:\n{ex.Message}", "OK");
        }
    }

    private async void btnAgregar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new VistaAgregarEstudiante());
    }

    private async void lvEstudiantes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is Estudiante est)
        {
            await Navigation.PushAsync(new VistaEditarEstudiante(est));
        }
    }
}
