using BrainChallenger.SLVZ.Libraries;
using Java.Util.Logging;
using static Android.Graphics.ColorSpace;

namespace BrainChallenger.SLVZ.Solutions;

public class GenerateQuestions
{
    public static List<QuestionsModel> OperatorsQuestions(int level)
    {
        List<QuestionsModel> list = new List<QuestionsModel>();

        level *= 10;

        Random random = new Random();

        for (int i = 0; i < (level < 20 ? 5 : 10); i++)
        {
            QuestionsModel model = new QuestionsModel();

            model.Num1 = random.Next((level == 0 ? -10 : -level), (level == 0 ? 10 : level));
            do
            {
                model.Num2 = random.Next((level == 0 ? -10 : -level), (level == 0 ? 10 : level));
            }
            while (model.Num1 == 0 && model.Num2 == 0);

            model.Operator = random.Next(0, 4) switch
            {
                0 => '+',
                1 => '/',
                2 => '*',
                3 => '-',
            };

            if (model.Operator == '/')
            {
                if(model.Num2>model.Num1)
                {
                    double c = model.Num2;
                    model.Num2 = model.Num1;
                    model.Num1 = c;
                }
            }


            switch (model.Operator)
            {
                case '+':
                    model.Result = model.Num1 + model.Num2;
                    break;
                case '-':
                    model.Result = model.Num1 - model.Num2;
                    break;
                case '*':
                    model.Result = model.Num1 * model.Num2;
                    break;
                case '/':
                    model.Result = Math.Round(model.Num1 / model.Num2, 2);
                    break;
            }

            model.Result = model.Result == -0 ? 0 : model.Result;


            list.Add(model);
        }

        return list;
    }

    public static List<QuestionsModel> ArithmeticQuestions(int level)
    {
        List<QuestionsModel> list = new List<QuestionsModel>();

        int count = level switch
        {
            0 => 10,
            1 => 15,
            _ => 20
        };

        Random re = new Random();
        int i = 0;

        while (i < count)
        {

            int starter;
            if (level == 1 || level == 0) starter = 1;
            else starter = level * 10;

            int maxValue = level < 1 ? 20 : level * 20;

            QuestionsModel question = new QuestionsModel();

            char elemet = re.Next(0, 9) switch
            {
                8 => '*',
                7 => '/',
                6 => '-',
                _ => '+'
            };

            int num1, num2;

            switch (elemet)
            {
                case '+':
                    {
                        num1 = re.Next(starter, maxValue);
                        num2 = re.Next(starter, maxValue);
                        question = new QuestionsModel
                        {
                            Result = num1 + num2,
                            Num1 = num1,
                            Operator = '+',
                            Num2 = num2
                        };
                    }
                    break;
                case '-':
                    {
                        while (true)
                        {
                            num1 = re.Next(starter, maxValue);
                            num2 = re.Next(starter, maxValue);

                            if (num1 - num2 >= 0)
                            {
                                question = new QuestionsModel
                                {
                                    Result = num1 - num2,
                                    Num1 = num1,
                                    Operator = '-',
                                    Num2 = num2
                                };
                                break;
                            }
                        }
                    }
                    break;
                case '*':
                    {
                        num1 = re.Next(starter, maxValue);
                        num2 = re.Next(starter, maxValue);
                        question = new QuestionsModel
                        {
                            Result = num1 * num2,
                            Num1 = num1,
                            Operator = '*',
                            Num2 = num2
                        };
                        break;
                    }
                case '/':
                    {
                        while (true)
                        {
                            num1 = re.Next(starter, maxValue);
                            num2 = re.Next(starter, maxValue);
                            if (num1 % num2 == 0)
                            {
                                question = new QuestionsModel
                                {
                                    Result = num1 / num2,
                                    Num1 = num1,
                                    Operator = '/',
                                    Num2 = num2
                                };
                                break;
                            }
                        }
                    }
                    break;
            }

            bool bl = false;
            foreach (QuestionsModel q in list)
                if ((question.Num1 == q.Num1) && (question.Num2 == q.Num2) && (question.Operator == q.Operator)) bl = true;

            if (!bl)
            {
                list.Add(question);
                i++;
            }
        }

        return list;
    }
}
