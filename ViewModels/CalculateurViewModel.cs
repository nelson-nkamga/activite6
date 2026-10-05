namespace Atelier6.ViewModels;

public class CalculateurViewModel : BaseViewModel
{
    private string _nom = string.Empty;
    private DateTime _dateNaissance = DateTime.Today.AddYears(-20);
    private string _resultat = string.Empty;
    private bool _isResultatVisible;
    private string _messageErreur = string.Empty;
    private int _ageAnnees;
    private int _ageMois;
    private int _ageJours;

    public string Nom
    {
        get => _nom;
        set
        {
            if (SetField(ref _nom, value))
            {
                CalculerCommand.RafraichirCanExecute();
                OnPropertyChanged(nameof(IsErrorVisible));
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

    public string MessageErreur
    {
        get => _messageErreur;
        set => SetField(ref _messageErreur, value);
    }

    public bool IsErrorVisible => !string.IsNullOrWhiteSpace(MessageErreur);

    public int AgeAnnees
    {
        get => _ageAnnees;
        set => SetField(ref _ageAnnees, value);
    }

    public int AgeMois
    {
        get => _ageMois;
        set => SetField(ref _ageMois, value);
    }

    public int AgeJours
    {
        get => _ageJours;
        set => SetField(ref _ageJours, value);
    }

    public RelayCommand CalculerCommand { get; }
    public RelayCommand ResetCommand { get; }

    public CalculateurViewModel()
    {
        CalculerCommand = new RelayCommand(_ => Calculer(), _ => !string.IsNullOrWhiteSpace(Nom));
        ResetCommand = new RelayCommand(_ => Reinitialiser());
    }

    private void Calculer()
    {
        if (string.IsNullOrWhiteSpace(Nom))
        {
            MessageErreur = "Veuillez entrer un nom valide.";
            IsResultatVisible = false;
            Resultat = string.Empty;
            return;
        }

        if (DateNaissance > DateTime.Today)
        {
            MessageErreur = "La date de naissance ne peut pas être dans le futur.";
            IsResultatVisible = false;
            Resultat = string.Empty;
            return;
        }

        MessageErreur = string.Empty;

        var today = DateTime.Today;
        var ageYears = today.Year - DateNaissance.Year;

        if (DateNaissance.Date > today.AddYears(-ageYears))
            ageYears--;

        var birthdayThisYear = new DateTime(today.Year, DateNaissance.Month, DateNaissance.Day);
        if (birthdayThisYear > today)
            birthdayThisYear = birthdayThisYear.AddYears(-1);

        var months = (today.Year - birthdayThisYear.Year) * 12 + today.Month - birthdayThisYear.Month;
        if (today.Day < DateNaissance.Day)
            months--;

        var totalDays = (today - DateNaissance.Date).Days;

        AgeAnnees = ageYears;
        AgeMois = Math.Max(0, months);
        AgeJours = Math.Max(0, totalDays - (AgeAnnees * 365 + AgeMois * 30));

        Resultat = $"Bonjour {Nom}, vous avez {AgeAnnees} ans, {AgeMois} mois et {AgeJours} jours.";
        IsResultatVisible = true;
    }

    private void Reinitialiser()
    {
        Nom = string.Empty;
        DateNaissance = DateTime.Today.AddYears(-20);
        Resultat = string.Empty;
        MessageErreur = string.Empty;
        IsResultatVisible = false;
        AgeAnnees = 0;
        AgeMois = 0;
        AgeJours = 0;
    }
}