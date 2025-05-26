namespace BrainChallenger.SLVZ.Views.KeyBoard;

public partial class ArithmeticKeyBoard
{
    public enum ActionType
    {
        BackSpace,
        Enter
    }

    public static Action<string> NumClicked;
    public static Action<ActionType> ActionClicked;
}
