namespace Atelier6;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCalculerClicked(object sender, EventArgs e)
	{
		count++;

		if (string.IsNullOrWhiteSpace(NomEntry.Text))
		{
			DisplayAlert("Erreur", "Veuillez entrer votre nom.", "OK");
			return;
		}
		
		DateTime d = DateNaissancePicker.Date;
		int age = DateTime.Now.Year - d.Year;
		if (DateTime.Now.DayOfYear < d.DayOfYear)
		{
			age--;
		}

		lblResultat.Text = $"L'âge de {NomEntry.Text} est {age} ans.";
		lblResultat.IsVisible = true;

	}
}

