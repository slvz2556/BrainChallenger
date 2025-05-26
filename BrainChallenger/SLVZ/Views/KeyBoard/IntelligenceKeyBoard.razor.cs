using BrainChallenger.SLVZ.Solutions;
using BrainChallenger.SLVZ.Views.Intelligence;

namespace BrainChallenger.SLVZ.Views.KeyBoard;

public partial class IntelligenceKeyBoard
{
    string Answer = "";

    private enum ActionClickMode
    {
        Enter,
        BackSpace
    }

    private void KeyBoardClicked(string str)
    {
        if (Answer.Length < IntelligenceLevels.Answer(IntelligenceGame.Game.model.Level).Length)
            Answer += str;
    }

    private void KeyBoardActionClick(ActionClickMode mode)
    {
        if (mode == ActionClickMode.BackSpace)
        {
            if (Answer.Length > 0)
                Answer = Answer.Remove(Answer.Length - 1);
        }
        else
        {
            if (Answer == IntelligenceLevels.Answer(IntelligenceGame.Game.model.Level))
            {
                IntelligenceGame.Game.ShowStatus(IntelligenceGame.AnswerStatus.True);

                //Go to next level
                IntelligenceGame.Game.LevelUp();

                Answer = "";
                StateHasChanged();
            }
            else
                IntelligenceGame.Game.ShowStatus(IntelligenceGame.AnswerStatus.False);
        }
    }

}
