using Microsoft.JSInterop;

namespace BrainChallenger.SLVZ.Views.Shared;

public partial class StartOverDialog
{
    public static StartOverDialog Dialog;

    string visibility = "display:none;", ClassName = "", Level = "";

    Action StartOver, Continue;

    public StartOverDialog()
    {
        Dialog = this;
    }

    public async void ShowDialog(Action StartOver,Action Continue, string level, string classname = ".div-body")
    {
        this.StartOver = StartOver;
        this.Continue = Continue;

        Level = level;

        visibility = "display:block;";

        ClassName = classname;

        await Js.InvokeVoidAsync("SetBlur", classname, 4);

        StateHasChanged();
    }

    public bool HideDialog()
    {
        if (visibility == "display:block;")
        {
            Js.InvokeVoidAsync("SetBlur", ClassName, 0);
            visibility = "display:none;";
            StateHasChanged();
            return true;
        }
        else return false;
    }

    private void StartOverClicked()
    {
        StartOver();
        HideDialog();
    }
    private void ContinueClicked()
    {
        Continue();
        HideDialog();
    }

}
