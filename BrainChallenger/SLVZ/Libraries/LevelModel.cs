using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainChallenger.SLVZ.Libraries;

public class LevelModel
{
    public int Level { get; set; } = 0;

    public bool StrikeOne { get; set; } = false;
    public bool StrikeTwo { get; set; } = false;
    public bool StrikeThree { get; set; } = false;

    public int TopLevel { get; set; } = 0;
}
