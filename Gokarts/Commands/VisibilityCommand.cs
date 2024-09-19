namespace Gokarts.Commands;

internal class VisibilityCommand : CommandBase
{
    private readonly Action _changeVisibility;

    public VisibilityCommand(Action func) 
    {
        _changeVisibility = func;
    }
    public override void Execute(object? parameter)
    {
        _changeVisibility?.Invoke();
    }
}
