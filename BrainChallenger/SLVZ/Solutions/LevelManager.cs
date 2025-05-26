using BrainChallenger.SLVZ.Libraries;

namespace BrainChallenger.SLVZ.Solutions;

public class IntelligenceManager
{
    public static bool IsRecordRelated
    {
        get => File.Exists(Path.Combine(FileSystem.AppDataDirectory, "Intelligence.slvz"));
    }

    
    public static int GetLevel()
    {
        int level = 0;

        if (IsRecordRelated)
        {
            using (FileStream file = new FileStream(Path.Combine(FileSystem.AppDataDirectory, "Intelligence.slvz"), FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(file))
            {
                //Read file extention
                reader.ReadString();

                //Read level
                level = Convert.ToInt16(reader.ReadString());


                reader.Close();
                file.Close();

                
            }
        }
        return level;
    }

    public static void SetLevel(int level)
    {
        int lastlevel = 0;
        if (IsRecordRelated)
        {
            lastlevel = GetLevel();
            File.Delete(Path.Combine(FileSystem.AppDataDirectory, "Intelligence.slvz"));            
        }

        if (lastlevel <= level)
        {
            using (FileStream file = new FileStream(Path.Combine(FileSystem.AppDataDirectory, "Intelligence.slvz"), FileMode.Create, FileAccess.Write))
            using (BinaryWriter writer = new BinaryWriter(file))
            {
                //Read file extention
                writer.Write("SLVZ");

                //set level
                writer.Write(level.ToString());


                writer.Close();
                file.Close();
            }
        }
    }

}

public class OperatorsManager
{
    public static bool IsRecordRelated
    {
        get => File.Exists(Path.Combine(FileSystem.AppDataDirectory, "Operators.slvz"));
    }


    public static LevelModel GetLevel()
    {
        LevelModel model = new LevelModel();

        if (IsRecordRelated)
        {
            using (FileStream file = new FileStream(Path.Combine(FileSystem.AppDataDirectory, "Operators.slvz"), FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(file))
            {
                //Read file extention
                reader.ReadString();

                //Read level
                model.Level = Convert.ToInt16(reader.ReadString());

                //Read top level
                model.TopLevel = Convert.ToInt16(reader.ReadString());

                //Read stikes
                model.StrikeOne = reader.ReadString().ToLower() == "true" ? true : false;
                model.StrikeTwo = reader.ReadString().ToLower() == "true" ? true : false;
                model.StrikeThree = reader.ReadString().ToLower() == "true" ? true : false;

                reader.Close();
                file.Close();


            }
        }
        return model;
    }

    public static void SetLevel(LevelModel model)
    {
        if (IsRecordRelated)
            File.Delete(Path.Combine(FileSystem.AppDataDirectory, "Operators.slvz"));


        using (FileStream file = new FileStream(Path.Combine(FileSystem.AppDataDirectory, "Operators.slvz"), FileMode.Create, FileAccess.Write))
        using (BinaryWriter writer = new BinaryWriter(file))
        {
            //Read file extention
            writer.Write("SLVZ");

            //set level
            writer.Write(model.Level.ToString());

            //set top level
            writer.Write(model.TopLevel.ToString());

            //set strikes
            writer.Write(model.StrikeOne ? "true" : "false");
            writer.Write(model.StrikeTwo ? "true" : "false");
            writer.Write(model.StrikeThree ? "true" : "false");

            writer.Close();
            file.Close();
        }
    }

}

public class ArithmeticManager
{
    public static bool IsRecordRelated
    {
        get => File.Exists(Path.Combine(FileSystem.AppDataDirectory, "Arithmetic.slvz"));
    }


    public static LevelModel GetLevel()
    {
        LevelModel model = new LevelModel();

        if (IsRecordRelated)
        {
            using (FileStream file = new FileStream(Path.Combine(FileSystem.AppDataDirectory, "Arithmetic.slvz"), FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(file))
            {
                //Read file extention
                reader.ReadString();

                //Read level
                model.Level = Convert.ToInt16(reader.ReadString());

                //Read top level
                model.TopLevel = Convert.ToInt16(reader.ReadString());

                //Read stikes
                model.StrikeOne = reader.ReadString().ToLower() == "true" ? true : false;
                model.StrikeTwo = reader.ReadString().ToLower() == "true" ? true : false;
                model.StrikeThree = reader.ReadString().ToLower() == "true" ? true : false;

                reader.Close();
                file.Close();


            }
        }
        return model;
    }

    public static void SetLevel(LevelModel model)
    {
        if (IsRecordRelated)
            File.Delete(Path.Combine(FileSystem.AppDataDirectory, "Arithmetic.slvz"));


        using (FileStream file = new FileStream(Path.Combine(FileSystem.AppDataDirectory, "Arithmetic.slvz"), FileMode.Create, FileAccess.Write))
        using (BinaryWriter writer = new BinaryWriter(file))
        {
            //Read file extention
            writer.Write("SLVZ");

            //set level
            writer.Write(model.Level.ToString());

            //set top level
            writer.Write(model.TopLevel.ToString());

            //set strikes
            writer.Write(model.StrikeOne ? "true" : "false");
            writer.Write(model.StrikeTwo ? "true" : "false");
            writer.Write(model.StrikeThree ? "true" : "false");

            writer.Close();
            file.Close();
        }
    }

}