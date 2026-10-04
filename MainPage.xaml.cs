namespace CalculateurAge;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    // Appelé quand on clique sur Calculer
    private async void OnCalculerClicked(object sender, EventArgs e)
    {
        // on refuse un nom vide
        if (string.IsNullOrWhiteSpace(entryNom.Text))
        {
            await DisplayAlertAsync("Erreur", "Entrez un nom", "OK");
            return;
        }

        // la date peut être nulle en .NET 10, on prend aujourd'hui par défaut
        DateTime d = pickerDate.Date ?? DateTime.Today;
        int age = DateTime.Today.Year - d.Year;

        // si l'anniversaire n'est pas encore passé cette année, on retire 1 an
        if (d.Date > DateTime.Today.AddYears(-age)) age--;

        lblResultat.Text = $"{entryNom.Text}, vous avez {age} ans";
        lblResultat.IsVisible = true;
    }
}