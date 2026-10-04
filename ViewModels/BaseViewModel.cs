using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CalculateurAge.ViewModels;

// Classe mère de tous les ViewModels
public class BaseViewModel : INotifyPropertyChanged
{
    // L'événement auquel le binding s'abonne
    public event PropertyChangedEventHandler? PropertyChanged;

    // Prévient la vue qu'une propriété a changé
    protected void OnPropertyChanged([CallerMemberName] string? nom = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nom));

    // Affecte la valeur ET notifie. Renvoie true si la valeur a changé.
    protected bool SetField<T>(ref T champ, T valeur,
        [CallerMemberName] string? nom = null)
    {
        // évite les notifications inutiles et les boucles en TwoWay
        if (EqualityComparer<T>.Default.Equals(champ, valeur))
            return false;

        champ = valeur;
        OnPropertyChanged(nom);
        return true;
    }
}