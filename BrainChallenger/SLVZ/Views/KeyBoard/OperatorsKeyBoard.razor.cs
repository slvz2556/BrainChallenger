namespace BrainChallenger.SLVZ.Views.KeyBoard;

public partial class OperatorsKeyBoard
{
    public enum ActionType{
        BackSpace,
        Enter
    }

    public static Action<char> OperatorAction;
    public static Action<ActionType> CallAction;
}
