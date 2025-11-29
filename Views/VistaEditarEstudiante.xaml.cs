using System.Net.Http;
using lgavidias6.Models;

namespace lgavidias6.Views;

public partial class VistaEditarEstudiante : ContentPage
{
    Estudiante _est;
    string URL = "http://10.2.3.103:8080/moviles/westudiante.php";

    public VistaEditarEstudiante(Estudiante est)
    {
        InitializeComponent();
        _est = est;

        txtCodigo.Text = est.codigo.ToString();
        txtNombre.Text = est.nombre;
        txtApellido.Text = est.apellido;
        txtEdad.Text = est.edad.ToString();
    }

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        string urlPut =
            $"{URL}?codigo={_est.codigo}&nombre={txtNombre.Text}&apellido={txtApellido.Text}&edad={txtEdad.Text}";

        var request = new HttpRequestMessage(HttpMethod.Put, urlPut);
        await new HttpClient().SendAsync(request);

        await DisplayAlert("Éxito", "Estudiante actualizado", "OK");
        await Navigation.PopAsync();
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Confirmar", "¿Eliminar estudiante?", "Sí", "No");
        if (!confirm) return;

        string urlDelete = $"{URL}?codigo={_est.codigo}";

        var request = new HttpRequestMessage(HttpMethod.Delete, urlDelete);
        await new HttpClient().SendAsync(request);

        await DisplayAlert("Éxito", "Estudiante eliminado", "OK");
        await Navigation.PopAsync();
    }
}
