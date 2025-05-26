using BrainChallenger.SLVZ.Libraries;
using BrainChallenger.SLVZ.Solutions;

namespace BrainChallenger.SLVZ.Views.Intelligence;

public partial class IntelligenceLevel
{

    IntelligenceViewModel model = new IntelligenceViewModel();

    public IntelligenceLevel()
    {
        RouteLayout.OnBack = () => RouteLayout.Router.NavigationTo("Home");
    }

    protected override void OnInitialized()
    {
        if (!IntelligenceManager.IsRecordRelated)
            model.CurrentLevel = 1;
        else model.CurrentLevel = IntelligenceManager.GetLevel();

        model.Levels.Clear();

        CreateList();
    }

    //Create list
    private void CreateList()
    {
        Thread Create = new Thread(async () =>
        {
            int Column = 0, Row = 0;

            for(int i = 1; i <= 30; i++)
            {
                LevelProperties level = new LevelProperties();

                level.Level = i;

                if (i <= model.CurrentLevel)
                    level.Status = LevelProperties.LevelStatus.NotLocked;
                else
                    level.Status = LevelProperties.LevelStatus.Locked;

                level.Xposition = (Column * 23);
                level.Yposition = (Row * 32);

                if ((Column % 2) == 0)
                    level.Yposition += 16;

                Column++;
                
                if(Column==4)
                {
                    Column = 0;
                    Row++;
                }
                model.Levels.Add(level);
            }
            model.ListStyle = $"height:{((Row + 1) * 32) + 16}vw;";
            await this.InvokeAsync(StateHasChanged);

        });
        Create.Start();
    } 

    private void OnLevelClick(int level)
    {
        if (model.Levels.Find(x => x.Level == level).Status == LevelProperties.LevelStatus.NotLocked)
            RouteLayout.Router.NavigationTo("IntelligenceGame", level);
    }

}//End