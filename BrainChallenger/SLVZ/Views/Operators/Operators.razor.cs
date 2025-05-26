using BrainChallenger.SLVZ.Libraries;
using BrainChallenger.SLVZ.Solutions;
using BrainChallenger.SLVZ.Views.KeyBoard;
using BrainChallenger.SLVZ.Views.Shared;
using BrainChallenger.SLVZ.Views.Tutorial;

namespace BrainChallenger.SLVZ.Views.Operators;

public partial class Operators
{
    OperatorsViewModel model = new OperatorsViewModel();

    LevelModel LastScore = new LevelModel();

    double num1, num2, result;
    char operate, userAnswar = ' ';
    int index = 0;

    public Operators()
    {
        RouteLayout.OnBack = () => 
        {
            if (OperatorsTutorial.Tutorial.HideTutorial()) { }
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

        OperatorsKeyBoard.OperatorAction = (e) =>
        {
            userAnswar = e;
            StateHasChanged();
        };
        OperatorsKeyBoard.CallAction = CallAction;
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            if (!OperatorsManager.IsRecordRelated)
            {
                OperatorsTutorial.Tutorial.ShowTutorial();

                model.Questions.Clear();
                model.Questions = GenerateQuestions.OperatorsQuestions(model.Level);
                num1 = model.Questions[index].Num1;
                num2 = model.Questions[index].Num2;
                result = model.Questions[index].Result;
                operate = model.Questions[index].Operator;

                StateHasChanged();
            }
            else
            {
                LastScore = OperatorsManager.GetLevel();

                if (LastScore.Level == 0)
                {
                    model.Questions.Clear();
                    model.Questions = GenerateQuestions.OperatorsQuestions(model.Level);
                    num1 = model.Questions[index].Num1;
                    num2 = model.Questions[index].Num2;
                    result = model.Questions[index].Result;
                    operate = model.Questions[index].Operator;

                    StateHasChanged();
                }
                else                
                    StartOverDialog.Dialog.ShowDialog(StartOver, Continue, LastScore.Level.ToString());
                //Ask user if it want to contuine
            }
        }            
    }

    private void Continue()
    {
        model.Level = LastScore.Level;
        model.StrikeOne = LastScore.StrikeOne;
        model.StrikeTwo = LastScore.StrikeTwo;
        model.StrikeThree = LastScore.StrikeThree;

        model.Questions.Clear();
        model.Questions = GenerateQuestions.OperatorsQuestions(model.Level);
        num1 = model.Questions[index].Num1;
        num2 = model.Questions[index].Num2;
        result = model.Questions[index].Result;
        operate = model.Questions[index].Operator;

        StateHasChanged();
    }
    private void StartOver()
    {
        index = 0;
        model.Level = 0;

        userAnswar = ' ';

        model.Questions.Clear();
        model.Questions = GenerateQuestions.OperatorsQuestions(model.Level);
        num1 = model.Questions[index].Num1;
        num2 = model.Questions[index].Num2;
        result = model.Questions[index].Result;
        operate = model.Questions[index].Operator;        

        OperatorsManager.SetLevel(new LevelModel
        {
            Level = model.Level,
            StrikeOne = false,
            StrikeTwo = false,
            StrikeThree = false,
            TopLevel = model.Level > LastScore.TopLevel ? model.Level : LastScore.TopLevel
        });

        StateHasChanged();
    }

    private void SetNextQuestion()
    {
        if (index == model.Questions.Count())
        {
            if (model.Level < 1000)
                model.Level++;
            else
            {
                if (OperatorsManager.GetLevel().TopLevel < 1000)
                {
                    CelebrationDialog.Dialog.ShowDialog();
                    OperatorsManager.SetLevel(new LevelModel
                    {
                        TopLevel = 1000,
                        Level = 1000,
                        StrikeOne = model.StrikeOne,
                        StrikeTwo = model.StrikeTwo,
                        StrikeThree = model.StrikeThree
                    });
                }
            }

            model.Questions.Clear();
            model.Questions = GenerateQuestions.OperatorsQuestions(model.Level);
            index = 0;

            OperatorsManager.SetLevel(new LevelModel
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
        result = model.Questions[index].Result;
        operate = model.Questions[index].Operator;

        StateHasChanged();
    }

    private void CallAction(OperatorsKeyBoard.ActionType type)
    {
        if (type == OperatorsKeyBoard.ActionType.BackSpace)
        {
            userAnswar = ' ';
            StateHasChanged();
        }
        else
        {
            if (userAnswar != ' ')
            {
                switch (userAnswar)
                {
                    case '+':
                        if(result == (num1 + num2))
                        {
                            index++;
                            userAnswar = ' ';
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
                        break;
                    case '/':
                        if (result == Math.Round(num1 / num2, 2)) 
                        {
                            index++;
                            userAnswar = ' ';
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
                        break;
                    case '*':
                        if (result == (num1 * num2))
                        {
                            index++;
                            userAnswar = ' ';
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
                        break;
                    case '-':
                        if (result == (num1 - num2))
                        {
                            index++;
                            userAnswar = ' ';
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
                        break;
                }
            }
        }
    }
    
    private void GoBackToHome()
    {
        OperatorsManager.SetLevel(new LevelModel
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
