using Microsoft.JSInterop;

namespace BrainChallenger.SLVZ.Views.Tutorial;

public partial class OperatorsTutorial
{
    public static OperatorsTutorial Tutorial;

    string visibility = "display:none;";

    string lanuage = "english";

    public OperatorsTutorial()
    {
        Tutorial = this;
    }

    public async void ShowTutorial()
    {
        visibility = "display:block;";

        await Js.InvokeVoidAsync("SetBlur", ".div-body", 4);

        StateHasChanged();
    }

    public bool HideTutorial()
    {
        if (visibility == "display:block;")
        {
            Js.InvokeVoidAsync("SetBlur", ".div-body", 0);
            visibility = "display:none;";
            StateHasChanged();
            return true;
        }
        else return false;
    }

    private void English()
    {
        lanuage = "english";
        StateHasChanged();
    }
    private void Persian()
    {
        lanuage = "persian";
        StateHasChanged();
    }
}
