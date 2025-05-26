using BrainChallenger.SLVZ.Libraries;
using BrainChallenger.SLVZ.Solutions;
using BrainChallenger.SLVZ.Views.Shared;
using BrainChallenger.SLVZ.Views.Tutorial;

namespace BrainChallenger.SLVZ.Views.Intelligence;

public partial class IntelligenceGame
{
    public IntelligenceGameViewModel model = new IntelligenceGameViewModel();

    public static IntelligenceGame Game;

    public enum AnswerStatus
    {
        True,
        False,
        None
    };
    private AnswerStatus status = AnswerStatus.None;

    public IntelligenceGame()
    {
        Game = this;

        RouteLayout.OnBack = () =>
        {
            if (IntelligenceTutorial.Tutorial.HideTutorial()) { }
            else if (CelebrationDialog.Dialog.HideDialog()) { }
            else
                RouteLayout.Router.NavigationTo("Intelligence");
        };
    }

    protected override void OnInitialized()
    {
        model.Level = Convert.ToInt16(RouteLayout.Router.Data.ToString());

        StateHasChanged();
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            if (!IntelligenceManager.IsRecordRelated)
            {
                IntelligenceManager.SetLevel(1);
                IntelligenceTutorial.Tutorial.ShowTutorial();
                StateHasChanged();
            }
        }
    }

    public void LevelUp()
    {
        if(model.Level < IntelligenceLevels.TopLevel)
        {
            model.Level++;

            IntelligenceManager.SetLevel(model.Level);

            StateHasChanged();
        }
        else
        {
            if (IntelligenceManager.GetLevel() == IntelligenceLevels.TopLevel)
                RouteLayout.Router.NavigationTo("Intelligence");
            else            
                CelebrationDialog.Dialog.ShowDialog();

            IntelligenceManager.SetLevel(IntelligenceLevels.TopLevel);
        }
    }

    public async void ShowStatus(AnswerStatus s)
    {
        status = AnswerStatus.None;
        StateHasChanged();

        await Task.Delay(100);

        status = s;
        StateHasChanged();
    }

}
