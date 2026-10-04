using System.Windows.Input;

namespace CalculateurAge.ViewModels;

// Transforme une méthode en objet liable à un Button
public class RelayCommand : ICommand
{
    private readonly Action _executer;          // quoi faire
    private readonly Func<bool>? _peutExecuter; // si c'est possible

    public RelayCommand(Action executer, Func<bool>? peutExecuter = null)
    {
        _executer = executer;
        _peutExecuter = peutExecuter;
    }

    // Le bouton se grise si ça renvoie false
    public bool CanExecute(object? p) => _peutExecuter?.Invoke() ?? true;

    public void Execute(object? p) => _executer();

    public event EventHandler? CanExecuteChanged;

    // À appeler pour que le bouton repose sa question
    public void Rafraichir()
        => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}