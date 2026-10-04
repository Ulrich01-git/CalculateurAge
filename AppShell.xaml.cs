using CalculateurAge.Views;

namespace CalculateurAge;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Déclare la route, sinon GoToAsync lève une exception
        Routing.RegisterRoute(nameof(ResultatPage), typeof(ResultatPage));
    }
}