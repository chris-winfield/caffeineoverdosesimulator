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
        int tasksComplete = 0;
        bool atHome = true;
        bool waitedForAlarm = false;
        bool hitSnooze = false;
        bool showered = false;
        bool ateBreakfast = false;
        bool driving = false;
        bool atWork = false;
        bool wentToLunch = false;
        bool passedOut = false;
        bool panicAttack = false;
        int choiceOut;
        object[,] optionsOut;
        tiredness = StartTiredLevel();
        anxiety = StartAnxietyLevel();
        focus = StartFocusLevel();
        int[] statsArray = MakeStatsIntoArray(time, tiredness, anxiety, focus, caffeine);
        bool[] boolArray = MakeBoolsIntoArray(atHome, waitedForAlarm, hitSnooze, showered, ateBreakfast, driving, atWork, passedOut, panicAttack, wentToLunch);
        Console.WriteLine("Your fitful night of poor sleep must come to an end. Your front door slams in the distance...");
        Console.WriteLine($"That was your roommate leaving for work, meaning it's {ParseTime(time)}, time for you to get your day started.");
        Console.WriteLine("Your alarm goes off in ten minutes.");
        Console.WriteLine("You need to head out the door by 7:40am to make it to work on time at 8:00am.");
        Console.WriteLine(" ");
        StateTheStats(statsArray);
        PrintStatsArray(statsArray);
        do
        {
            CheckForMaxValues(ref statsArray, ref boolArray);
            BuildOptionsList(ref statsArray, ref boolArray, out optionsOut, out choiceOut);
            Console.Clear();
            ParseChoice(choiceOut, ref optionsOut, ref statsArray, ref boolArray, ref tasksComplete);
            PrintStatsArray(statsArray); // For debug only
            Array.Clear(optionsOut);
        } while (time <= 60);
            



    }

    //Allows all stats and all bools to be passed respectively as single refs:

    static int[] MakeStatsIntoArray(int time, int tiredness, int anxiety, int focus, int caffeine)
    {
        int[] statsArray = { time, tiredness, anxiety, focus, caffeine };
        return statsArray;
    }

    static bool[] MakeBoolsIntoArray(bool atHome, bool waitedForAlarm, bool hitSnooze, bool showered, bool ateBreakfast, bool driving, bool atWork, bool passedOut, bool panicAttack, bool wentToLunch)
    {
        bool[] boolArray = { atHome, waitedForAlarm, hitSnooze, showered, ateBreakfast, driving, atWork, passedOut, panicAttack, wentToLunch };
        return boolArray;
    }

    //Debug method for arrays:
    static void PrintStatsArray(int [] statsArray)
    {
        Console.WriteLine($"Time: {statsArray[0]} *** Tiredness: {statsArray[1]} *** Anxiety: {statsArray[2]} *** Focus: {statsArray[3]} *** Caffeine: {statsArray[4]}");
    }

    // Options List builder works by checking for conditions of the events being met
    // If true, add their option text to the first dimension of a 2D array 
    // Then, a number representing an ID number of that option is stored in the second dimension
    // It then adds the option text to a list for easy printing:

    static void BuildOptionsList(ref int[] statsArray, ref bool[] boolsArray, out object[,] optionsOut, out int choiceOut)
    {
        object[,] optionsBuilder = new object[10, 10];
        int optionCounter = 0; // Allows the options to be numbered and keyed in
        List<string> options = new();

        optionsBuilder[optionCounter, 0] = $"{optionCounter + 1}. Check in with yourself about how tired, anxious, and focused you feel.";
        optionsBuilder[optionCounter, 1] = 1;
        options.Add((string)optionsBuilder[optionCounter, 0]);
        optionCounter++;

        if (boolsArray[0] == true)
        {
            if (statsArray[0] == 0)
            {
                optionsBuilder[optionCounter, 0] = $"{optionCounter + 1}. Sleep until your first alarm goes off";
                optionsBuilder[optionCounter, 1] = 2;
                options.Add((string)optionsBuilder[optionCounter, 0]);
                optionCounter++;
            }
            if (boolsArray[1] == true && boolsArray[2] == false)
            {
                optionsBuilder[optionCounter, 0] = $"{optionCounter + 1}. Hit the snooze button";
                optionsBuilder[optionCounter, 1] = 3;
                options.Add((string)optionsBuilder[optionCounter, 0]);
                optionCounter++;
            }
            if (boolsArray[3] == false)
            {
                optionsBuilder[optionCounter, 0] = $"{optionCounter + 1}. Take a shower";
                optionsBuilder[optionCounter, 1] = 4;
                options.Add((string)optionsBuilder[optionCounter, 0]);
                optionCounter++;
            }
            if (boolsArray[4] == false)
            {
                optionsBuilder[optionCounter, 0] = $"{optionCounter + 1}. Have breakfast without caffeine. ";
                optionsBuilder[optionCounter, 1] = 5;
                options.Add((string)optionsBuilder[optionCounter, 0]);
                optionCounter++;

                optionsBuilder[optionCounter, 0] = $"{optionCounter + 1}. Have black tea with breakfast. ";
                optionsBuilder[optionCounter, 1] = 6;
                options.Add((string)optionsBuilder[optionCounter, 0]);
                optionCounter++;

                optionsBuilder[optionCounter, 0] = $"{optionCounter + 1}. Have coffee with breakfast. ";
                optionsBuilder[optionCounter, 1] = 7;
                options.Add((string)optionsBuilder[optionCounter, 0]);
                optionCounter++;

                optionsBuilder[optionCounter, 0] = $"{optionCounter + 1}. Have energy drink with breakfast. ";
                optionsBuilder[optionCounter, 1] = 8;
                options.Add((string)optionsBuilder[optionCounter, 0]);
                optionCounter++;

                optionsBuilder[optionCounter, 0] = $"{optionCounter + 1}. Have caffeine tablet with breakfast. ";
                optionsBuilder[optionCounter, 1] = 9;
                options.Add((string)optionsBuilder[optionCounter, 0]);
                optionCounter++;
            }
            optionsBuilder[optionCounter, 0] = $"{optionCounter + 1}. Drive to work. ";
            optionsBuilder[optionCounter, 1] = 10;
            options.Add((string)optionsBuilder[optionCounter, 0]);
            optionCounter++;
        }

        if (boolsArray[5] == true)
        {
            optionsBuilder[optionCounter, 0] = $"{optionCounter + 1}. Drive straight to work. ";
            optionsBuilder[optionCounter, 1] = 11;
            options.Add((string)optionsBuilder[optionCounter, 0]);
            optionCounter++;

            optionsBuilder[optionCounter, 0] = $"{optionCounter + 1}. Drink an energy drink while driving to work. ";
            optionsBuilder[optionCounter, 1] = 12;
            options.Add((string)optionsBuilder[optionCounter, 0]);
            optionCounter++;

            optionsBuilder[optionCounter, 0] = $"{optionCounter + 1}. Stop to get coffee on the way to work. ";
            optionsBuilder[optionCounter, 1] = 13;
            options.Add((string)optionsBuilder[optionCounter, 0]);
            optionCounter++;
        }

        if (boolsArray[6] == true)
        {
            optionsBuilder[optionCounter, 0] = $"{optionCounter + 1}. Work on job tasks. ";
            optionsBuilder[optionCounter, 1] = 14;
            options.Add((string)optionsBuilder[optionCounter, 0]);
            optionCounter++;
        }

        optionsOut = optionsBuilder;

        Console.WriteLine($"The time is {ParseTime(statsArray[0])}. Your options are:");
        options.ForEach(Console.WriteLine);
        Console.WriteLine("What do you want to do?");
        GetInput: //A label which serves as a goto point for bad input
        string userChoice = Console.ReadLine();
        if (Int32.TryParse(userChoice, out int choice)) // First tries to see if the input is a number
        {
            if (optionsOut[choice - 1, 0] != null) // Then makes sure the number actually corresponds to an option
            {
                choiceOut = choice;
            }
            else
            {
                Console.WriteLine("Please enter one of the available options.");
                goto GetInput;
            }
        }
        else
        {
            Console.WriteLine("Please enter a numerical choice.");
            goto GetInput;
        }
            



        
    }

    //Parse the choices:
    static void ParseChoice(int choiceInt, ref object[,] optionsOut, ref int[] statsArray, ref bool[] boolsArray, ref int tasksComplete)
    {
        int choiceIndex = choiceInt - 1;
        int optionChoice = (int)optionsOut[choiceIndex, 1];
        switch (optionChoice)
        {
            case 1:
                StateTheStats(statsArray);
                break;
            case 2:
                WaitForAlarm(ref statsArray, ref boolsArray);
                break;
            case 3:
                HitTheSnooze(ref statsArray, ref boolsArray);
                break;
            case 4:
                TakeShower(ref statsArray, ref boolsArray);
                break;
            case 5:
                BreakfastWater(ref statsArray, ref boolsArray);
                break;
            case 6:
                BreakfastTea(ref statsArray, ref boolsArray);
                break;
            case 7:
                BreakfastCoffee(ref statsArray, ref boolsArray);
                break;
            case 8:
                BreakfastEnergyDrink(ref statsArray, ref boolsArray);
                break;
            case 9:
                BreakfastTablet(ref statsArray, ref boolsArray);
                break;
            case 10:
                LeaveForWork(ref statsArray, ref boolsArray);
                break;
            case 11:
                DriveStraightToWork(ref statsArray, ref boolsArray);
                break;
            case 12:
                DrinkEnergyDriving(ref statsArray, ref boolsArray);
                break;
            case 13:
                StopToGetCoffee(ref statsArray, ref boolsArray);
                break;
            case 14:
                WorkOnTasks(ref statsArray, ref boolsArray, ref tasksComplete);
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
    
    static void StateTheStats(int[] statsArray)
    {
        Console.WriteLine("Here are your impressions of about your present state:");
        Console.WriteLine($"{ParseTiredness(statsArray[1])} {ParseFocus(statsArray[3])} {ParseAnxiety(statsArray[2])}");
    }
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
        if (minute % 60 == 0)
        {
            return hour + ":" + minute + "0" + ampm;
        } 
        else
        {
            return hour + ":" + minute + ampm;
        }    
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

    //Methods which manage the max values of the gameplay variables:
    
    static void CheckForMaxValues(ref int[] statsArray, ref bool[] boolsArray)
        {
            if (statsArray[1] >= 100)
            {         
                boolsArray[7] = true;
                PassOut(ref statsArray, ref boolsArray);
            }    
            else if (statsArray[2] >= 100)
            {
                boolsArray[8] = true;
                PanicAttack(ref statsArray, ref boolsArray);
            } 
            else if (statsArray[4] >= 10000)
            {
                Console.WriteLine("You've suffered a caffeine overdose! You are dead!");
            }
        
        }

    static void PassOut(ref int[] statsArray, ref bool[] boolsArray)
        {
            do
            {
                Console.WriteLine("You've become so tired that you've passed out!");
                PassTenMinutes(ref statsArray);
            } while (statsArray[1] >= 100);
            Console.WriteLine("At last, you wake up from your involuntary nap. Back to working on tasks.");
            boolsArray[7] = false;
        }

    static void PanicAttack(ref int[] statsArray, ref bool[] boolsArray)
    {
        do
        {
            Console.WriteLine("Your anxiety has spiraled out of control into a panic attack!");
            PassTenMinutes(ref statsArray);
        } while (statsArray[2] >= 100);
        Console.WriteLine("Finally, your panic attack subsides. Better lay off the caffeine for an hour or two.");
        boolsArray[8] = false;
    }
            

    

    //Stat Modication Methods:
    static void PassTenMinutes(ref int[] statsArray) 
    {
        statsArray[0]++; //Increments time
        statsArray[2] = (int)(statsArray[2] * Math.Pow(0.5, 0.083)); // Anxiety half-life of 2 hour decay
        statsArray[4] = (int)(statsArray[4] * Math.Pow(0.5, 0.042)); // Caffeine half life of 5 hour decay
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
        }
    }
     static void CalculateCaffeineEffects(int newCaffeineDose, ref int[] statsArray)
    {
        int addedAnxiety = newCaffeineDose / 10;
        statsArray[2] = statsArray[2] + addedAnxiety;
        int removedTiredness = newCaffeineDose / 50;
        statsArray[1] = statsArray[1] - removedTiredness;
        int addedFocus = newCaffeineDose / 40;
        statsArray[3] = statsArray[3] + addedFocus;
        DirectlyAddCaffeine(newCaffeineDose, ref statsArray);
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

    static void WaitForAlarm(ref int[] statsArray, ref bool[] boolArray)
    {
        Console.WriteLine("You let yourself drift back to sleep until your alarm wakes you up again. You consider hitting the snooze.");
        Random rnd = new();
        int tirednessChange = rnd.Next(-5, 0);
        DirectlyChangeTiredness(tirednessChange, ref statsArray);
        boolArray[1] = true;
        PassTenMinutes(ref statsArray);
    }

    static void HitTheSnooze (ref int[] statsArray, ref bool[] boolsArray)
    {
        Console.WriteLine("You hit the snooze button. Before you know it, your alarm is going off again even louder. You can't hit it again.");
        Random rnd = new();
        int tirednessChange = rnd.Next(-2, 0);
        DirectlyChangeTiredness(tirednessChange, ref statsArray);
        boolsArray[2] = true;
        PassTenMinutes(ref statsArray);
    }

    static void TakeShower (ref int[] statsArray, ref bool[] boolsArray)
    {
        Console.WriteLine("You take a refreshing shower. It relaxes you.");
        Random rnd = new();
        int anxietyChange = rnd.Next(-10, -5);
        DirectlyChangeAnxiety(anxietyChange, ref statsArray);
        boolsArray[3] = true;
        PassTenMinutes(ref statsArray);
    }

    static void BreakfastWater (ref int[] statsArray, ref bool[] boolsArray)
    {
        Console.WriteLine("You decide to experiment with not taking caffeine this morning. Best of luck!");
        DirectlyChangeFocus(10, ref statsArray);
        boolsArray[4] = true;
        PassTenMinutes(ref statsArray);
    }

    static void BreakfastTea (ref int[] statsArray, ref bool[] boolsArray)
    {
        Console.WriteLine("You steep some tea with breakfast. It tastes full-bodied, yet refreshing.");
        DirectlyChangeFocus(10, ref statsArray);
        CalculateCaffeineEffects(50, ref statsArray);
        boolsArray[4] = true;
        PassTenMinutes(ref statsArray);
    }
    static void BreakfastCoffee(ref int[] statsArray, ref bool[] boolsArray)
    {
        Console.WriteLine("You have a cup of coffee with breakfast. You really regret not being able to savor the incredible taste.");
        DirectlyChangeFocus(10, ref statsArray);
        CalculateCaffeineEffects(100, ref statsArray);
        boolsArray[4] = true;
        PassTenMinutes(ref statsArray);
    }
    static void BreakfastEnergyDrink(ref int[] statsArray, ref bool[] boolsArray)
    {
        Console.WriteLine("You drink the energy drink with breakfast. You weren't drinking it for taste.");
        DirectlyChangeFocus(10, ref statsArray);
        CalculateCaffeineEffects(200, ref statsArray);
        boolsArray[4] = true;
        PassTenMinutes(ref statsArray);
    }
    static void BreakfastTablet(ref int[] statsArray, ref bool[] boolsArray)
    {
        Console.WriteLine("You take a caffeine tablet after breakfast. There's something deeply unsatisfying about this method.");
        DirectlyChangeFocus(10, ref statsArray);
        CalculateCaffeineEffects(400, ref statsArray);
        boolsArray[4] = true;
        PassTenMinutes(ref statsArray);
    }

    static void LeaveForWork(ref int[] statsArray, ref bool[] boolsArray)
    {
        string commuteEstimate;
        boolsArray[0] = false;
        boolsArray[5] = true;
        if (statsArray[0] <= 3)
        {
            commuteEstimate = "You've generously given yourself enough time to stop for a coffee, if you so choose.";
        }    
        else if (statsArray[0] == 4)
        {
            commuteEstimate = "You have just enough time to get yourself to work with no stops, but you are the pilot and this is your ship, so it's up to you.";
        } 
        else
        {
            commuteEstimate = "You will definitely be late for work at this point. Thankfully, you've recently become immune to the displeased looks of your coworkers.";
        }
            
        Console.WriteLine($"You leave for work at {ParseTime(statsArray[0])}. {commuteEstimate}");
        Console.WriteLine("Your stash of of energy drinks, slightly chilled by the crisp morning air, could also be tapped without making your commute longer.");
    }

    static void DriveStraightToWork(ref int[] statsArray, ref bool[] boolsArray)
    {
        Console.WriteLine("You drive straight to work. It's almost completely uneventful. The craziest thing you see on the road is a bunch of people doing the exact same thing: commuting.");
        boolsArray[5] = false;
        PassTenMinutes(ref statsArray);
        PassTenMinutes(ref statsArray);
        ArriveAtWork(ref statsArray, ref boolsArray);
    }

    static void DrinkEnergyDriving(ref int[] statsArray, ref bool[] boolsArray)
    {
        Console.WriteLine("Somehow, driving with one hand on the wheel and an energy drink in the other feels like something you were born to do.");
        boolsArray[5] = false;
        CalculateCaffeineEffects(200, ref statsArray);
        PassTenMinutes(ref statsArray);
        PassTenMinutes(ref statsArray);
        ArriveAtWork(ref statsArray, ref boolsArray);
    }

    static void StopToGetCoffee(ref int[] statsArray, ref bool[] boolsArray)
    {
        Console.WriteLine("Stopping to get coffee \"only\" adds ten minutes to your trip. You don't know that much about coffee, but you can tell this is the best you'll have all day.");
        boolsArray[5] = false;
        CalculateCaffeineEffects(100, ref statsArray);
        PassTenMinutes(ref statsArray);
        PassTenMinutes(ref statsArray);
        PassTenMinutes(ref statsArray);
        ArriveAtWork(ref statsArray, ref boolsArray);
    }

    static void ArriveAtWork(ref int[] statsArray, ref bool[] boolsArray)
    {
         boolsArray[6] = true;
        if (statsArray[0] < 6)
        {
            Console.WriteLine("You arrive at work early. This noticeably makes you feel better about work. Why can't you ever normally do this?");
            DirectlyChangeAnxiety(-10, ref statsArray);
        }
        else if (statsArray[0] == 6)
        {
            Console.WriteLine("You arrive to work right on time. Nobody can say anything to you, which is cool.");
            DirectlyChangeAnxiety(-1, ref statsArray);
        }
        else if (statsArray[0] == 7)
        {
            Console.WriteLine("You arrive to work slightly late. Not so late that anyone might actually care, but you did find yourself rushing up those stairs.");
            DirectlyChangeAnxiety(1, ref statsArray);
        }
        else if (statsArray[0] == 8)
        {
            Console.WriteLine("You arrive to work late, beyond the usual margin of error. You can hear the psychic scribbling of a mental note of your poor start to yet another workday.");
            DirectlyChangeAnxiety(5, ref statsArray);
        }
        else
        {
            Console.WriteLine("You are so late that you probably should have called ahead with an excuse prepared. But since it usually takes this long for all your functions to work, you'll just have to live with the shame.");
            DirectlyChangeAnxiety(10, ref statsArray);
        }
        Console.WriteLine("You have an email from your boss waiting for you. Everything's status quo. Work on your tasks until it's time to leave.");
        Console.WriteLine("Of course, you have your own ideas about how you should best spend your time.");
        Console.WriteLine("You like to work on tasks in twenty minute chunks. Of course, the number of tasks you complete will depend on your mental state...");
        Console.WriteLine("Which has demands of its own.");
       
    }

    static void WorkOnTasks(ref int[] statsArray, ref bool[] boolsArray, ref int tasksComplete)
    {
        int focusFactor = statsArray[3];
        int anxietyFactor = statsArray[2];
        int tiredFactor = statsArray[1];
        int successfulTasksThisChunk = 0;
        int taskRoll;
        int taskSuccessRate = focusFactor - (anxietyFactor / 5) - (tiredFactor / 10);
        Random rnd = new();
        for (int i = 0; i < 4; i++)
        {
            taskRoll = rnd.Next(0, 100);
            if (taskSuccessRate >= taskRoll)
            {
                successfulTasksThisChunk++;
            }
        }
        switch (successfulTasksThisChunk)
        {
            case 4:
                Console.WriteLine("You did some of your best work on those tasks!");
                break;
            case 3:
                Console.WriteLine("Those tasks went very well.");
                break;
            case 2:
                Console.WriteLine("Nobody can say you aren't doing your job.");
                break;
            case 1:
                Console.WriteLine("That wasn't your best work to be sure.");
                break;
            case 0:
                Console.WriteLine("When you look back on your work, it's an incomprehensible mess!");
                break;
        }
        tasksComplete += successfulTasksThisChunk;
        PassTenMinutes(ref statsArray);
        PassTenMinutes(ref statsArray);
    }

}












