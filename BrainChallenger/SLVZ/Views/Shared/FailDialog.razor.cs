using Microsoft.JSInterop;

namespace BrainChallenger.SLVZ.Views.Shared;

public partial class FailDialog
{
    public static FailDialog Dialog;

    string visibility = "display:none;", ClassName = "";

    Action TryAgain, Home;

    public FailDialog()
    {
        Dialog = this;
    }

    public async void ShowDialog(Action TryAgain, Action Home, string classname = ".div-body")
    {
        this.TryAgain = TryAgain;
        this.Home = Home;

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

    private void TryAgainClicked()
    {
        TryAgain();
        HideDialog();
    }
    private void HomeClicked()
    {
        Home();
        HideDialog();
    }

}
