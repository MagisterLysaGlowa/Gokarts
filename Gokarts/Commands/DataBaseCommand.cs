namespace Gokarts.Commands;

public class DataBaseCommand : CommandBase
{
    private readonly Action action;
    public DataBaseCommand(Action action)
    {
        this.action = action;
    }
    public override void Execute(object? parameter)
    {
        action?.Invoke();
    }
}
