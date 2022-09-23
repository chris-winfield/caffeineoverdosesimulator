using System;

namespace CaffeineOverdoseSimulator;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("CAFFEINE OVERDOSE SIMULATOR");
        Console.WriteLine("Press any key to begin...");
        Console.ReadKey();
        int time = 0;
        int tiredness;
        int anxiety;
        int focus;
        int caffeine = 0;
        bool atHome = true;
        bool waitedForAlarm = false;
        bool showered = false;
        bool ateBreakfast = false;
        bool driving = false;
        bool atWork = false;
        bool wentToLunch = false;
        object[,] optionsOut;
        tiredness = StartTiredLevel();
        anxiety = StartAnxietyLevel();
        focus = StartFocusLevel();
        int[] statsArray = MakeStatsIntoArray(time, tiredness, anxiety, focus, caffeine);
        bool[] boolArray = MakeBoolsIntoArray(atHome, waitedForAlarm, showered, ateBreakfast);
        Console.WriteLine("Your fitful night of poor sleep must come to an end. Your front door slams in the distance...");
        Console.WriteLine($"That was your roommate leaving for work, meaning it's {ParseTime(time)}, time for you to get your day started.");
        Console.WriteLine("Your alarm goes off in ten minutes.");
        Console.WriteLine("You need to head out the door by 7:40am to make it to work on time at 8:00am.");
        Console.WriteLine(" ");
        Console.WriteLine("Here are the facts:");
        Console.WriteLine(ParseTiredness(tiredness));
        Console.WriteLine(ParseAnxiety(anxiety));
        Console.WriteLine(ParseFocus(focus));
        BuildOptionsList(ref statsArray, ref boolArray, out optionsOut);

    }

    //Allows all stats and all bools to be passed respectively as single refs:

    static int[] MakeStatsIntoArray(int time, int tiredness, int anxiety, int focus, int caffeine)
    {
        int[] statsArray = { time, tiredness, anxiety, focus, caffeine };
        return statsArray;
    }

    static bool[] MakeBoolsIntoArray(bool atHome, bool waitedForAlarm, bool showered, bool ateBreakfast)
    {
        bool[] boolArray = { atHome, waitedForAlarm, showered, ateBreakfast };
        return boolArray;
    }

    static void BuildOptionsList(ref int[] statsArray, ref bool[] boolsArray, out object[,] optionsOut, out int choiceOut)
    {
        object[,] optionsBuilder = new object[10, 10];
        int optionCounter = 0;
        List<string> options = new();
        if (boolsArray[0] == true)
        {
            if (statsArray[0] == 0)
            {
                optionsBuilder[optionCounter, 0] = $"{optionCounter + 1}. Sleep until your first alarm goes off";
                optionsBuilder[optionCounter, 1] = 1;
                options.Add((string)optionsBuilder[optionCounter, 0]);
                optionCounter++;
            }
            if (boolsArray[1] == true)
            {
                optionsBuilder[optionCounter, 0] = $"{optionCounter + 1}. Hit the snooze button";
                optionsBuilder[optionCounter, 1] = 2;
                options.Add((string)optionsBuilder[optionCounter, 0]);
                optionCounter++;
            }
            if (boolsArray[2] == false)
            {
                optionsBuilder[optionCounter, 0] = $"{optionCounter + 1}. Take a shower";
                optionsBuilder[optionCounter, 1] = 3;
                options.Add((string)optionsBuilder[optionCounter, 0]);
                optionCounter++;
            }
            if (boolsArray[3] == false)
            {
                optionsBuilder[optionCounter, 0] = $"{optionCounter + 1}. Have breakfast without caffeine. ";
                optionsBuilder[optionCounter, 1] = 4;
                options.Add((string)optionsBuilder[optionCounter, 0]);
                optionCounter++;
                optionsBuilder[optionCounter, 0] = $"{optionCounter + 1}. Have black tea with breakfast. ";
                optionsBuilder[optionCounter, 1] = 5;
                options.Add((string)optionsBuilder[optionCounter, 0]);
                optionCounter++;
                optionsBuilder[optionCounter, 0] = $"{optionCounter + 1}. Have energy drink with breakfast. ";
                optionsBuilder[optionCounter, 1] = 6;
                options.Add((string)optionsBuilder[optionCounter, 0]);
                optionCounter++;
                optionsBuilder[optionCounter, 0] = $"{optionCounter + 1}. Have caffeine tablet with breakfast. ";
                optionsBuilder[optionCounter, 1] = 7;
                options.Add((string)optionsBuilder[optionCounter, 0]);
                optionCounter++;
            }
            optionsBuilder[optionCounter, 0] = $"{optionCounter + 1}. Drive to work. ";
            optionsBuilder[optionCounter, 1] = 8;
            options.Add((string)optionsBuilder[optionCounter, 0]);
            optionCounter++;
        }

        optionsOut = optionsBuilder; 

        Console.WriteLine($"The time is {ParseTime(statsArray[0])}. Your options are {options}What do you want to do?");
        string userChoice = Console.ReadLine();
        if (Int32.TryParse(userChoice, out int choice))
        {
            choiceOut = choice;
        }
        else
        {
            Console.WriteLine("Please enter a numerical choice.");
        }
            



        
    }

    //Parse the choices:
    void ParseChoice(int choiceInt, ref object[,] optionsOut, ref int[] statsArray, ref bool[] boolsArray)
    {
        int choiceIndex = choiceInt - 1;
        int optionChoice = (int)optionsOut[choiceIndex, 1];
        switch (optionChoice)
        {
            case 1:
                WaitForAlarm(ref statsArray);
                break;
            case 2:
                HitTheSnooze(ref statsArray);
                break;
            case 3:
                TakeShower(ref statsArray);
                break;
            case 4:
                BreakfastWater(ref statsArray);
                break;
            case 5:
                BreakfastTea(ref statsArray);
                break;
            case 6:
                BreakfastCoffee(ref statsArray);
                break;
            case 7:
                BreakfastEnergyDrink(ref statsArray);
                break;
            case 8:
                BreakfastTablet(ref statsArray);
                break;
        }
    }

    //Random variables to start for these three stats
    static int StartTiredLevel()
    {
        Random rnd = new();
        return rnd.Next(39, 58);

    }
    static int StartAnxietyLevel()
    {
        Random rnd = new();
        return rnd.Next(19, 38);
    }
    static int StartFocusLevel()
    {
        Random rnd = new();
        return rnd.Next(29, 48);
    }

    //Stats output messages:
    
    static string ParseTiredness(int tiredness)
    {
        if (tiredness <= 9)
        {
            return "You feel fully awake!";
        }
        else if (tiredness <= 19)
        {
            return "You don't hardly feel tired at all.";
        }
        else if (tiredness <= 29)
        {
            return "You feel on the whole very awake.";
        }
        else if (tiredness <= 39)
        {
            return "You feel no more tired than you virtually always do.";
        }
        else if (tiredness <= 49)
        {
            return "Your thoughts are with sleep, but there's no sense that it has to come soon.";
        }
        else if (tiredness <= 59)
        {
            return "You find yourself yawning uncontrollably from every now and then.";
        }
        else if (tiredness <= 69)
        {
            return "Your eyes water from time to time.";
        }
        else if (tiredness <= 79)
        {
            return "You catch yourself closing your eyes involuntarily at inopportune moments.";
        }
        else if (tiredness <= 89)
        {
            return "Your head seems to be magnetically attracted to whatever is in front of you.";
        }
        else if (tiredness <= 99)
        {
            return "The line separating the real world and dream land is beginning to blur.";
        }
        else
        {
            return "You've passed out!";
        }

    }
    static string ParseTime(int time)
    {
        string ampm = "am";
        int hour = (time / 6) + 7;
        int minute = (time % 6) * 10;
        if (hour == 12)
        {
            ampm = "pm";
        }
        if (hour >= 13)
        {
            hour = hour - 12;
            ampm = "pm";
        }
        return hour + ":" + minute + "0" + ampm;

    }
    static string ParseAnxiety(int anxiety)
    {
        if (anxiety <= 9)
        {
            return "You haven't felt serenity like this since the summer after graduation!";
        }
        else if (anxiety <= 19)
        {
            return "You feel pretty calm.";
        }
        else if (anxiety <= 29)
        {
            return "You dread the tasks ahead of you, but aren't really bothered by them.";
        }
        else if (anxiety <= 39)
        {
            return "You feel jittery, but you doubt anyone else could tell.";
        }
        else if (anxiety <= 49)
        {
            return "You can't seem to stop yourself from bouncing your leg up and down.";
        }
        else if (anxiety <= 59)
        {
            return "Your occasionally feel your heart pick up speed for no apparently reason.";
        }
        else if (anxiety <= 69)
        {
            return "The pool of sweat underneath your shirt is unwarranted by cubicle work.";
        }
        else if (anxiety <= 79)
        {
            return "Your hands tremble to the point where you've lost some use of them.";
        }
        else if (anxiety <= 89)
        {
            return "Any sound other than the drone of the office HVAC system makes you jump in your seat.";
        }
        else if (anxiety <= 99)
        {
            return "Your mouth is dry. Your fingertips are numb. Your can't control your breathing.";
        }
        else
        {
            return "Oh no, you're having one of your indomitable panic attacks!";
        }
    }
    static string ParseFocus(int focus)
    {
        if (focus <= 9)
        {
            return "You cannot resist the compulsion to look at anything other than work!";
        }
        else if (focus <= 19)
        {
            return "You can't stay focused on anything for more than few seconds.";
        }
        else if (focus <= 29)
        {
            return "Your boredom has become a powerful distraction unto itself.";
        }
        else if (focus <= 39)
        {
            return "You're capable of completing sequences of actions, but you can't remember doing them after you've finished.";
        }
        else if (focus <= 49)
        {
            return "Your thinking feels slowed, but you're capable of deliberation.";
        }
        else if (focus <= 59)
        {
            return "With some effort, you're able to stay on task.";
        }
        else if (focus <= 69)
        {
            return "You feel fully capable of any task you might need to accomplish.";
        }
        else if (focus <= 79)
        {
            return "Your attention to detail is very minute and accurate.";
        }
        else if (focus <= 89)
        {
            return "You're reaching a flow state. Even the most mundane task gives you pleasure.";
        }
        else if (focus <= 99)
        {
            return "You're handling information in the world as if you were breathing it.";
        }
        else
        {
            return "You have perfect focus!";
        }
    }

    //Stat Modication Methods:
    static void PassTenMinutes(ref int[] statsArray) // passes 0.time 1.tiredness 2.anxiety 3.focus 4.caffeine
    {
        statsArray[0] = statsArray[0]++; //Increments time
        if (statsArray[0] % 6 == 0)
        {
            statsArray[1] = statsArray[1] + 3;
            Random rnd = new();
            int focusCoinFlip = rnd.Next(1, 2); //A 50-50 chance to gain or lose focus on a new hour.
            if (focusCoinFlip % 2 == 0)
            {
                statsArray[3] = statsArray[3] + 10;
            }
            else
            {
                statsArray[3] = statsArray[3] - 10;
            }
            statsArray[2] = (int)(statsArray[2] * Math.Pow(0.5, 0.17)); // Anxiety half-life of 1 hour decay
            statsArray[4] = (int)(statsArray[4] * Math.Pow(0.5, 0.042)); // Caffeine half life of 5 hour decay

        }
    }
    static void CalculateNewAnxiety(int newCaffeineDose, ref int[] statsArray) //Anxiety index 2, Tiredness Index 1
    {
        int addedAnxiety = (newCaffeineDose / 50) - (statsArray[1] / 10);
        statsArray[2] = statsArray[2] + addedAnxiety;
    }

    static void DirectlyChangeAnxiety(int directAnxiety, ref int[] statsArray) // Anxiety index 2
    {
        statsArray[2] = statsArray[2] + directAnxiety;
    }

    static void DirectlyChangeTiredness(int directTiredness, ref int[] statsArray) //Tiredness index 1
    {
        statsArray[1] = statsArray[1] + directTiredness;
    }

    static void DirectlyChangeFocus(int directFocus, ref int[] statsArray) // Focus index 3
    {
        statsArray[3] = statsArray[3] + directFocus;
    }

    static void DirectlyAddCaffeine(int dose, ref int[] statsArray) // Caffeine index 4
    {
        statsArray[4] = statsArray[4] + dose;
    }

    //Player Actions:

    static void WaitForAlarm(ref int[] statsArray)
    {
        Console.WriteLine("You let yourself drift back to sleep until your alarm wakes you up again. You consider hitting the snooze.");
        Random rnd = new();
        int tirednessChange = rnd.Next(-5, 0);
        DirectlyChangeTiredness(tirednessChange, ref statsArray);
        PassTenMinutes(ref statsArray);
    }

    static void HitTheSnooze (ref int[] statsArray)
    {
        Console.WriteLine("You hit the snooze button. Before you know it, your alarm is going off again even louder. You can't hit it again.");
        Random rnd = new();
        int tirednessChange = rnd.Next(-2, 0);
        DirectlyChangeTiredness(tirednessChange, ref statsArray);
        PassTenMinutes(ref statsArray);
    }

    static void TakeShower (ref int[] statsArray)
    {
        Console.WriteLine("You take a refreshing shower. It relaxes you.");
        Random rnd = new();
        int anxietyChange = rnd.Next(-10, -5);
        DirectlyChangeAnxiety(anxietyChange, ref statsArray);
        PassTenMinutes(ref statsArray);
    }

    static void BreakfastWater (ref int[] statsArray)
    {
        Console.WriteLine("You decide to experiment with not taking caffeine this morning. Best of luck!");
        DirectlyChangeFocus(10, ref statsArray);
        PassTenMinutes(ref statsArray);
    }

    static void BreakfastTea (ref int[] statsArray)
    {
        Console.WriteLine("You steep some tea with breakfast. It tastes full-bodied, yet refreshing.");
        DirectlyAddCaffeine(50, ref statsArray);
        DirectlyChangeFocus(10, ref statsArray);
        PassTenMinutes(ref statsArray);
    }
    static void BreakfastCoffee(ref int[] statsArray)
    {
        Console.WriteLine("You have a cup of coffee with breakfast. You really regret not being able to savor the incredible taste.");
        DirectlyAddCaffeine(100, ref statsArray);
        DirectlyChangeFocus(10, ref statsArray);
        PassTenMinutes(ref statsArray);
    }
    static void BreakfastEnergyDrink(ref int[] statsArray)
    {
        Console.WriteLine("You drink the energy drink with breakfast. You weren't drinking it for taste.");
        DirectlyAddCaffeine(200, ref statsArray);
        DirectlyChangeFocus(10, ref statsArray);
        PassTenMinutes(ref statsArray);
    }
    static void BreakfastTablet(ref int[] statsArray)
    {
        Console.WriteLine("You take a caffeine tablet after breakfast. There's something deeply unsatisfying about this method.");
        DirectlyAddCaffeine(400, ref statsArray);
        DirectlyChangeFocus(10, ref statsArray);
        PassTenMinutes(ref statsArray);
    }







}













