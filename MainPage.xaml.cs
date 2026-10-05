namespace Atelier6;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnCalculerClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NomEntry.Text))
        {
            await DisplayAlert("Erreur", "Veuillez entrer un nom valide.", "OK");
            return;
        }

        var dateNaissance = DateNaissancePicker.Date;
        var age = DateTime.Today.Year - dateNaissance.Year;

        if (dateNaissance.Date > DateTime.Today.AddYears(-age))
            age--;

        lblResultat.Text = $"Bonjour {NomEntry.Text}, vous avez {age} ans.";
        lblResultat.IsVisible = true;
    }
}