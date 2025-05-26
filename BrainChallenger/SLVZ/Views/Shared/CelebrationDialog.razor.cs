using Microsoft.JSInterop;

namespace BrainChallenger.SLVZ.Views.Shared;

public partial class CelebrationDialog
{
    string visibility = "display:none;", ClassName = "";

    public static CelebrationDialog Dialog;

    public CelebrationDialog()
    {
        Dialog = this;
    }


    public async void ShowDialog(string classname = ".div-body")
    {
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
            RouteLayout.Router.NavigationTo("Home");
            return true;
        }
        else
            return false;
    }

}
