namespace BrainChallenger.SLVZ.Solutions;

public class IntelligenceLevels
{
    public static string Answer(int level)
    {
        return level switch
        {
            1 => "2",
            2 => "5",
            3 => "25",
            4 => "20",
            5 => "64",
            6 => "12",
            7 => "15",
            8 => "32",
            9 => "28",
            10 => "5",
            11 => "46",
            12 => "13",
            13 => "1",
            14 => "13",
            15 => "79",
            16 => "23",
            17 => "63",
            18 => "41",
            19 => "27",
            20 => "44",
            21 => "17",
            22 => "61",
            23 => "7",
            24 => "16",
            25 => "135",
            26 => "111",
            27 => "55",
            28 => "35",
            29 => "53",
            30 => "4",
            _ => ""
        };
    }

    public static int TopLevel
    {
        get => 30;
    }
}
