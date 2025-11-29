using System.Net.Http;
using System.Net.Http.Headers;

namespace lgavidias6.Views;

public partial class VistaAgregarEstudiante : ContentPage
{
    // ? URL COMPLETA (sin espacios) y apuntando al script que inserta (post.php)
  private const string URL = "http://localhost:8080/moviles/post.php";

    private readonly HttpClient cliente = new HttpClient();

    public VistaAgregarEstudiante()
    {
        InitializeComponent();
    }

    private async void btnGuardar_Clicked(object sender, EventArgs e)
    {
        // Datos que tu PHP espera: nombre, apellido, edad
        var datos = new Dictionary<string, string>
        {
            { "nombre",   txtNombre.Text },
            { "apellido", txtApellido.Text },
            { "edad",     txtEdad.Text }
        };

        var contenido = new FormUrlEncodedContent(datos);

        try
        {
            var respuesta = await cliente.PostAsync(URL, contenido);

            if (respuesta.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "Estudiante agregado correctamente", "OK");
                await Navigation.PopAsync();   // volver a la lista
            }
            else
            {
                // Leemos el cuerpo para ver qué devolvió el PHP (por si hay error 500, etc.)
                var body = await respuesta.Content.ReadAsStringAsync();
                await DisplayAlert(
                    "Error",
                    $"No se pudo agregar.\nCódigo: {(int)respuesta.StatusCode}\nRespuesta: {body}",
                    "OK"
                );
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error de conexión", ex.Message, "OK");
        }
    }
}
