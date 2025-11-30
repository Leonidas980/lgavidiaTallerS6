using System.Net.Http;
using System.Collections.Generic;

namespace lgavidias6.Views;

public partial class VistaAgregarEstudiante : ContentPage
{
    private const string URL = "http://localhost:8080/moviles/wsestudiante.php";
    private readonly HttpClient cliente = new HttpClient();

    public VistaAgregarEstudiante()
    {
        InitializeComponent();
    }

    private async void btnGuardar_Clicked(object sender, EventArgs e)
    {
        var datos = new Dictionary<string, string>
        {
            { "nombre", txtNombre.Text },
            { "apellido", txtApellido.Text },
            { "edad", txtEdad.Text }
        };

        var contenido = new FormUrlEncodedContent(datos);
        var respuesta = await cliente.PostAsync(URL, contenido);

        if (respuesta.IsSuccessStatusCode)
        {
            await DisplayAlert("Éxito", "Estudiante agregado correctamente", "OK");
            await Navigation.PopAsync(); // volver a la lista
        }
        else
        {
            await DisplayAlert("Error", "No se pudo agregar", "OK");
        }
    }
}

