using Microsoft.JSInterop;

namespace BrainChallenger.SLVZ.Views.Shared;

public partial class QuiteDialog
{
    public static QuiteDialog Dialog;

    string visibility = "display:none;", ClassName = "";

    Action CallBack;

    public QuiteDialog()
    {
        Dialog = this;
    }

    public async void ShowDialog(Action action, string classname = ".div-body")
    {
        CallBack = action;

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

    private void YesClicked()
    {
        CallBack();
        HideDialog();
    }

}
