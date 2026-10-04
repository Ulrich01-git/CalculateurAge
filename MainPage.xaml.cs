using CalculateurAge.ViewModels;

namespace CalculateurAge;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        // Objet où tous les {Binding} de la page cherchent leurs valeurs
        BindingContext = new CalculateurViewModel();
    }
}