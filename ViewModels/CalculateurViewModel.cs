namespace CalculateurAge.ViewModels;

public class CalculateurViewModel : BaseViewModel
{
    private string _nom = "";

    private DateTime _dateNaissance = DateTime.Today.AddYears(-20);

    private string _resultat = "";

    private bool _isResultatVisible ;

    public string Nom
    {
        get => _nom;
        set
        {
            if (SetField(ref _nom, value))
            {
                CalculerCommand.RafraichirCanExecute();
            }
        }
    }

    public DateTime DateNaissance
    {
        get => _dateNaissance;
        set
        {
            if (SetField(ref _dateNaissance, value))
            {
                CalculerCommand.RafraichirCanExecute();
            }
        }
    }

    public string Resultat
    {
        get => _resultat;
        set => SetField(ref _resultat, value);
    }

    public bool IsResultatVisible
    {
        get => _isResultatVisible;
        set => SetField(ref _isResultatVisible, value);
    }

    public RelayCommand CalculerCommand { get; }

    public CalculateurViewModel()
    {
        CalculerCommand = new RelayCommand(Calculer,
            _ => !string.IsNullOrWhiteSpace(Nom));
    }

    private void Calculer(object? parameter)
    {
        int age = DateTime.Today.Year - DateNaissance.Year;

        if (DateNaissance > DateTime.Today.AddYears(-age))
            age--;

        Resultat = $"Bonjour {Nom}, vous avez {age} ans.";
        IsResultatVisible = true;
    }
}