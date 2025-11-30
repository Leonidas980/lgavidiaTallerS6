using System.Net.Http;
using lgavidias6.Models;

namespace lgavidias6.Views;

public partial class VistaEditarEstudiante : ContentPage
{
    private Estudiante estudiante;
    private const string URL = "http://localhost:8080/moviles/wsestudiante.php";

    public VistaEditarEstudiante(Estudiante est)
    {
        InitializeComponent();
        estudiante = est;

        txtCodigo.Text = est.codigo.ToString();
        txtNombre.Text = est.nombre;
        txtApellido.Text = est.apellido;
        txtEdad.Text = est.edad.ToString();
    }

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        string urlPut =
            $"{URL}?codigo={estudiante.codigo}&nombre={txtNombre.Text}&apellido={txtApellido.Text}&edad={txtEdad.Text}";

        var request = new HttpRequestMessage(HttpMethod.Put, urlPut);
        await new HttpClient().SendAsync(request);

        await DisplayAlert("Éxito", "Estudiante actualizado", "OK");
        await Navigation.PopAsync();
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Confirmar", "¿Eliminar estudiante?", "Sí", "No");
        if (!confirm) return;

        string urlDelete = $"{URL}?codigo={estudiante.codigo}";

        var request = new HttpRequestMessage(HttpMethod.Delete, urlDelete);
        await new HttpClient().SendAsync(request);

        await DisplayAlert("Éxito", "Estudiante eliminado", "OK");
        await Navigation.PopAsync();
    }
}

