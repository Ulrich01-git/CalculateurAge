using System.Collections.ObjectModel;

namespace CalculateurAge.ViewModels;

public class CalculateurViewModel : BaseViewModel
{
    // Champs privés : la vraie donnée
    private string _nom = "";
    private DateTime _dateNaissance = DateTime.Today.AddYears(-20);
    private string _resultat = "";
    private bool _resultatVisible;
    private string _message = "";
    private string _joursRestants = "";

    // Propriétés publiques : ce que le XAML voit
    public string Nom
    {
        get => _nom;
        set
        {
            if (SetField(ref _nom, value))
                CalculerCommand.Rafraichir();
        }
    }

    public DateTime DateNaissance
    {
        get => _dateNaissance;
        set => SetField(ref _dateNaissance, value);
    }

    public string Resultat
    {
        get => _resultat;
        set => SetField(ref _resultat, value);
    }

    public bool ResultatVisible
    {
        get => _resultatVisible;
        set => SetField(ref _resultatVisible, value);
    }

    // Majeur ou Mineur
    public string Message
    {
        get => _message;
        set => SetField(ref _message, value);
    }

    // Jours avant le prochain anniversaire
    public string JoursRestants
    {
        get => _joursRestants;
        set => SetField(ref _joursRestants, value);
    }

    // Historique des calculs
    public ObservableCollection<string> Historique { get; } = new();

    // Lus par les boutons dans le XAML
    public RelayCommand CalculerCommand { get; }
    public RelayCommand EffacerCommand { get; }

    public CalculateurViewModel()
    {
        CalculerCommand = new RelayCommand(
            Calculer,
            () => !string.IsNullOrWhiteSpace(Nom));

        EffacerCommand = new RelayCommand(Effacer);
    }

    // La logique métier : aucun contrôle d'interface ici
    private void Calculer()
    {
        int age = DateTime.Today.Year - DateNaissance.Year;
        if (DateNaissance.Date > DateTime.Today.AddYears(-age)) age--;

        Resultat = $"{Nom}, vous avez {age} ans";
        Message = age >= 18 ? "Majeur" : "Mineur";

        int jours = (ProchainAnniversaire() - DateTime.Today).Days;
        JoursRestants = jours == 0
            ? "Joyeux anniversaire !"
            : $"Prochain anniversaire dans {jours} jour(s)";

        Historique.Insert(0, $"{Nom} : {age} ans");
        ResultatVisible = true;
    }

    private DateTime ProchainAnniversaire()
    {
        int annee = DateTime.Today.Year;
        DateTime date = AnniversaireEn(annee);
        if (date < DateTime.Today) date = AnniversaireEn(annee + 1);
        return date;
    }

    // Gère le 29 février les années non bissextiles
    private DateTime AnniversaireEn(int annee)
    {
        int jour = DateNaissance.Day;
        if (DateNaissance.Month == 2 && jour == 29
            && !DateTime.IsLeapYear(annee)) jour = 28;
        return new DateTime(annee, DateNaissance.Month, jour);
    }

    // Remet tous les champs à zéro
    private void Effacer()
    {
        Nom = "";
        DateNaissance = DateTime.Today.AddYears(-20);
        Resultat = "";
        Message = "";
        JoursRestants = "";
        ResultatVisible = false;
    }
}