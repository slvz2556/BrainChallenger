using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainChallenger.SLVZ.Libraries;

public class OperatorsViewModel
{
    public int Level { get; set; } = 0;

    public List<QuestionsModel> Questions { get; set; } = new List<QuestionsModel>();

    public bool StrikeOne { get; set; } = false;
    public bool StrikeTwo { get; set; } = false;
    public bool StrikeThree { get; set; } = false;
}

public class ArithmeticViewModel
{
    public int Level { get; set; } = 0;

    public List<QuestionsModel> Questions { get; set; } = new List<QuestionsModel>();

    public bool StrikeOne { get; set; } = false;
    public bool StrikeTwo { get; set; } = false;
    public bool StrikeThree { get; set; } = false;
}

public class QuestionsModel
{
    public double Num1 { get; set; } = 0;
    public char Operator { get; set; } = '+';
    public double Num2 { get; set; } = 0;
    public double Result { get; set; } = 0;
}

//Intelligence
public class IntelligenceViewModel
{
    public List<LevelProperties> Levels = new List<LevelProperties>();

    public string ListStyle { get; set; } = "";

    public int CurrentLevel { get; set; } = 1;
}

public class LevelProperties
{
    public enum LevelStatus
    {
        Locked,
        NotLocked
    }

    public LevelStatus Status { get; set; } = LevelStatus.Locked;

    //public int Height { get; set; } = 0;
    //public int Width { get; set; } = 0;

    public int Xposition { get; set; } = 0;
    public int Yposition { get; set; } = 0;

    public int Level { get; set; } = 1;

    public string Style
    {
        get => $"height:30vw; width:30vw; left:{Xposition}vw; top:{Yposition}vw; animation-delay:{Level*150}ms;";
    }
}


public class IntelligenceGameViewModel
{
    public int Level { get; set; } = 0;

    public string ImageName { get => $"Level{Level}.svg"; }
}