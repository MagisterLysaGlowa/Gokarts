using Gokarts.Commands;
using Gokarts.Controllers;
using Gokarts.Models;
using Gokarts.Stores;
using System.Windows.Input;

namespace Gokarts.ViewModels;

public class AddOrEditTournamentViewModel : ViewModelBase
{
    private readonly Tournament _tournament;
    private string _name;
    private string _description;
    private string _type;

    public Action? Callback { get; }
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }
    public string Description
    {
        get => _description;
        set
        {
            _description = value;
            OnPropertyChanged(nameof(Description));
        }
    }
    public string Type
    {
        get => _type;
        set
        {
            _type = value;
            OnPropertyChanged(nameof(Type));
        }
    }
    public bool IsEditing { get; private set; }
    public string ButtonText { get; private set; }

    public ICommand AddOrSaveCommand { get; }
    public ICommand NavigateTournamentListCommand { get; } 

    public AddOrEditTournamentViewModel(NavigationStore navigationStore, Tournament? tournament, Action? callback)
    {
        Callback = callback;
        NavigateTournamentListCommand = new NavigateCommand<TournamentListViewModel>(navigationStore, () => new TournamentListViewModel(navigationStore));
        IsEditing = tournament != null;
        ButtonText = IsEditing ? "Zapisz" : "Dodaj";
        _tournament = tournament ?? new Tournament();
        _name = _tournament.Name;
        _description = _tournament.Description;
        _type = _tournament.Type;
        AddOrSaveCommand = new DataBaseCommand(() =>
        {
            _tournament.Name = Name;
            _tournament.Description = Description;
            _tournament.Type = Type;
            if (IsEditing)
            {
                DataBaseController.UpdateTournament(_tournament);
            }
            else
            {
                DataBaseController.InsertTournament(_tournament);
            }
            Callback?.Invoke();
            NavigateTournamentListCommand.Execute(null);
        });
    }
}
