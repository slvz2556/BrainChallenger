using BrainChallenger.SLVZ.Solutions;

namespace BrainChallenger.SLVZ.Views.Home;

public partial class Home
{
    string IntelligenceLevel, ArithmeticTopLevel = "None", OperatorsTopLevel = "None", ArithmeticLevel, OperatorsLevel;

    public Home()
    {
#if ANDROID
        RouteLayout.OnBack = () => MainActivity.Main.Finish();
#endif
    }

    protected override void OnInitialized()
    {
        if (!IntelligenceManager.IsRecordRelated) IntelligenceLevel = "Nothing yet";
        else
            IntelligenceLevel = IntelligenceManager.GetLevel().ToString();

        if (!ArithmeticManager.IsRecordRelated) ArithmeticLevel = "Nothing yet";
        else
        {
            var model = ArithmeticManager.GetLevel();

            ArithmeticLevel = model.Level.ToString();
            ArithmeticTopLevel = string.IsNullOrEmpty(model.TopLevel.ToString()) ? "None" : model.TopLevel.ToString();
        }


        if (!OperatorsManager.IsRecordRelated) OperatorsLevel = "Nothing yet";
        else 
        {
            var model = OperatorsManager.GetLevel();
            OperatorsLevel = model.Level.ToString();
            OperatorsTopLevel = string.IsNullOrEmpty(model.TopLevel.ToString()) ? "None" : model.TopLevel.ToString();
        }

        StateHasChanged();
    }

    //Website
    private async void WebSiteClicked()
    {
        await Browser.OpenAsync("https://slvz.dev");
    }

    //Share app
    private async void ShareClicked()
    {
        /*
        //Bazar
        await Share.RequestAsync(new ShareTextRequest
        (
            "سلام دوست خوبم\n" +
            "این برنامه، درواقع یه بازی فکریه که به تقویت حافظه و قدرت ذهن کمک میکنه\n" +
            "میتونی از بازار نصبش کنی\n\n" +
            "https://cafebazaar.ir/app/?id=com.slvz.brainchallenger"
            ));
        */
         
        /*
        //Myket
         await Share.RequestAsync(new ShareTextRequest
         (
             "سلام دوست خوبم\n" +
             "این برنامه، درواقع یه بازی فکریه که به تقویت حافظه و قدرت ذهن کمک میکنه\n" +
             "میتونی از مایکت نصبش کنی\n\n" +
             "https://myket.ir/app/com.slvz.brainchallenger"
             ));
        */
        
        //Google play
        await Share.RequestAsync(new ShareTextRequest
        (
            "Hi my dear friend\n" +
            "This program is actually a mental game that helps to strengthen memory and mind power\n" +
            "You can install it from GooglePlay\n\n" +
            "https://play.google.com/store/apps/details?id=com.slvz.brainchallenger"
            ));
        
    }
    
    //Rate app
    private void RateClicked()
    {
        Android.Content.Intent intent = new Android.Content.Intent(Android.Content.Intent.ActionView);
        intent.SetData(Android.Net.Uri.Parse("market://details?id=com.slvz.brainchallenger"));
        MainActivity.Main.StartActivity(intent);
    }

    //Donate
    private async void DonateClicked()
    {
        await Browser.OpenAsync("https://www.coffeete.ir/slvz");
    }

}
