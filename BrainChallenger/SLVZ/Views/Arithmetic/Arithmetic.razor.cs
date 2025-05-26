using BrainChallenger.SLVZ.Libraries;
using BrainChallenger.SLVZ.Solutions;
using BrainChallenger.SLVZ.Views.KeyBoard;
using BrainChallenger.SLVZ.Views.Shared;
using BrainChallenger.SLVZ.Views.Tutorial;
using Microsoft.Extensions.Logging;

namespace BrainChallenger.SLVZ.Views.Arithmetic;

public partial class Arithmetic
{
    ArithmeticViewModel model = new ArithmeticViewModel();

    LevelModel LastScore = new LevelModel();

    double num1 = 0, num2 = 0;
    char operate = '+';
    string Result = "", Answer = "";
    int index = 0;

    public Arithmetic()
    {
        RouteLayout.OnBack = () =>
        {
            if (ArithmeticTutorial.Tutorial.HideTutorial()) { }
            else if (QuiteDialog.Dialog.HideDialog()) { }
            else if (StartOverDialog.Dialog.HideDialog()) { StartOver(); }
            else if (CelebrationDialog.Dialog.HideDialog()) { }
            else if (FailDialog.Dialog.HideDialog())
            {
                model.Level = 0;
                model.StrikeOne = false;
                model.StrikeThree = false;
                model.StrikeTwo = false;
                GoBackToHome();
            }
            else
                OnBackClick();            
        };

        ArithmeticKeyBoard.NumClicked = (e)=>
        {
            if (Answer.Length < Result.Length)
            {
                Answer += e;
                StateHasChanged();
            }
        };
        ArithmeticKeyBoard.ActionClicked = KeyBoardActionClicked;
    }

    private void KeyBoardActionClicked(ArithmeticKeyBoard.ActionType type)
    {
        if (type == ArithmeticKeyBoard.ActionType.BackSpace)
        {
            if (Answer.Length > 0)
            {
                Answer = Answer.Remove(Answer.Length - 1);
                StateHasChanged();
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(Answer))
            {
                if (Answer == Result)
                {
                    index++;
                    SetNextQuestion();
                }
                else
                {
                    if (!model.StrikeOne) model.StrikeOne = true;
                    else if (!model.StrikeTwo) model.StrikeTwo = true;
                    else if (!model.StrikeThree) model.StrikeThree = true;
                    else
                    {
                        LastScore.TopLevel = model.Level > LastScore.TopLevel ? model.Level : LastScore.TopLevel;
                        model.Level = 0;
                        model.StrikeOne = false;
                        model.StrikeThree = false;
                        model.StrikeTwo = false;

                        FailDialog.Dialog.ShowDialog(StartOver, GoBackToHome);

                        StateHasChanged();
                    }
                    StateHasChanged();
                }
            }
        }
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            if (!ArithmeticManager.IsRecordRelated)
            {
                ArithmeticTutorial.Tutorial.ShowTutorial();

                model.Questions.Clear();
                model.Questions = GenerateQuestions.ArithmeticQuestions(model.Level);
                num1 = model.Questions[index].Num1;
                num2 = model.Questions[index].Num2;
                Result = model.Questions[index].Result.ToString();
                operate = model.Questions[index].Operator;

                StateHasChanged();
            }
            else
            {
                LastScore = ArithmeticManager.GetLevel();

                if (LastScore.Level == 0)
                {
                    model.Questions.Clear();
                    model.Questions = GenerateQuestions.ArithmeticQuestions(model.Level);
                    num1 = model.Questions[index].Num1;
                    num2 = model.Questions[index].Num2;
                    Result = model.Questions[index].Result.ToString();
                    operate = model.Questions[index].Operator;

                    StateHasChanged();
                }
                else
                    StartOverDialog.Dialog.ShowDialog(StartOver, Continue, LastScore.Level.ToString());
                //Ask user if it want to contuine
            }
        }
    }

    private void StartOver()
    {
        index = 0;
        model.Level = 0;

        Answer = "";

        model.StrikeOne = false;
        model.StrikeTwo = false;
        model.StrikeThree = false;

        model.Questions.Clear();
        model.Questions = GenerateQuestions.ArithmeticQuestions(model.Level);
        num1 = model.Questions[index].Num1;
        num2 = model.Questions[index].Num2;
        Result = model.Questions[index].Result.ToString();
        operate = model.Questions[index].Operator;

        ArithmeticManager.SetLevel(new LevelModel
        {
            Level = 0,
            StrikeOne = false,
            StrikeThree = false,
            StrikeTwo = false,
            TopLevel = LastScore.TopLevel
        });

        StateHasChanged();        
    }

    private void Continue()
    {
        model.Level = LastScore.Level;
        model.StrikeOne = LastScore.StrikeOne;
        model.StrikeTwo = LastScore.StrikeTwo;
        model.StrikeThree = LastScore.StrikeThree;

        model.Questions.Clear();
        model.Questions = GenerateQuestions.ArithmeticQuestions(model.Level);
        num1 = model.Questions[index].Num1;
        num2 = model.Questions[index].Num2;
        Result = model.Questions[index].Result.ToString();
        operate = model.Questions[index].Operator;

        StateHasChanged();
    }


    private void SetNextQuestion()
    {
        if (index == model.Questions.Count())
        {
            if (model.Level < 200)
                model.Level++;
            else
            {
                if(ArithmeticManager.GetLevel().TopLevel < 200)
                {
                    CelebrationDialog.Dialog.ShowDialog();
                    ArithmeticManager.SetLevel(new LevelModel
                    {
                        TopLevel = 200,
                        Level = 200,
                        StrikeOne = model.StrikeOne,
                        StrikeTwo = model.StrikeTwo,
                        StrikeThree = model.StrikeThree
                    });
                }
            }

            model.Questions.Clear();
            model.Questions = GenerateQuestions.ArithmeticQuestions(model.Level);
            index = 0;

            ArithmeticManager.SetLevel(new LevelModel
            {
                Level = model.Level,
                StrikeOne = model.StrikeOne,
                StrikeTwo = model.StrikeTwo,
                StrikeThree = model.StrikeThree,
                TopLevel = model.Level > LastScore.TopLevel ? model.Level : LastScore.TopLevel
            });
        }

        num1 = model.Questions[index].Num1;
        num2 = model.Questions[index].Num2;
        Result = model.Questions[index].Result.ToString();
        operate = model.Questions[index].Operator;

        Answer = "";

        StateHasChanged();
    }


    private void GoBackToHome()
    {
        ArithmeticManager.SetLevel(new LevelModel
        {
            Level = model.Level,
            StrikeOne = model.StrikeOne,
            StrikeTwo = model.StrikeTwo,
            StrikeThree = model.StrikeThree,
            TopLevel = model.Level > LastScore.TopLevel ? model.Level : LastScore.TopLevel
        });

        RouteLayout.Router.NavigationTo("Home");
    }

    private void OnBackClick()
    {
        if (model.Level < 1)
            GoBackToHome();
        else
        {
            QuiteDialog.Dialog.ShowDialog(GoBackToHome);
        }
    }

}
