namespace BrainChallenger.SLVZ.Views;

public partial class RouteLayout
{
    public static Action OnBack;

    public static RouteLayout Router;
    
    public Object Data { get; set; }

    string route = "Home";

    public RouteLayout()
    {
        Router = this;
        MainActivity.BackClicked = () => OnBack();
    }

    public void NavigationTo(string route)
    {
        this.route = route;

        StateHasChanged();
    }

    public void NavigationTo(string route, Object data)
    {
        this.route = route;
        Data = data;

        StateHasChanged();
    }

}
