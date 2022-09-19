namespace CaffeineOverdoseSimulator
{
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
            bool driving = false;
            bool atWork = false;
            bool dressed = false;
            bool ateBreakfast = false;
            bool wentToLunch = false;
            tiredness = StartTiredLevel();
            anxiety = StartAnxietyLevel();
            focus = StartFocusLevel();
            Console.WriteLine("Your fitful night of poor sleep must come to an end. Your front door slams in the distance...");
            Console.WriteLine($"That was your roommate leaving for work, meaning it's {ParseTime(time)}, time for you to get your day started.");
            Console.WriteLine("You need to head out the door by 7:40am to make it to work on time at 8:00am.");
            Console.WriteLine(ParseTiredness(tiredness));
            Console.WriteLine(ParseAnxiety(anxiety);

            
            
           
        }

        static public int StartTiredLevel()
        {
            Random rnd = new();
            return rnd.Next(40, 58);
            
        }
        static public int StartAnxietyLevel()
        {
            Random rnd = new();
            return rnd.Next(20, 38);
        }
        static public int StartFocusLevel()
        {
            Random rnd = new();
            return rnd.Next(30, 49);
        }

        static public string ParseTiredness(int level)
        {
            if (level <= 9)
            {
                return "You feel fully awake!";
            }
            else if (level <= 19)
            {
                return "You don't hardly feel tired at all.";
            }
            else if (level <= 29)
            {
                return "You feel on the whole very awake.";
            }
            else if (level <= 39)
            {
                return "You feel no more tired than you virtually always do.";
            }
            else if (level <= 49)
            {
                return "Your thoughts are with sleep, but there's no sense that it has to come soon.";
            }
            else if (level <= 59)
            {
                return "You find yourself yawning uncontrollably from every now and then.";
            }
            else if (level <= 69)
            {
                return "Your eyes water from time to time.";
            }
            else if (level <= 79)
            {
                return "You catch yourself closing your eyes involuntarily at inopportune moments.";
            }
            else if (level <= 89)
            {
                return "Your head seems to be magnetically attracted to whatever is in front of you.";
            }
            else if (level <= 99)
            {
                return "The line separating the real world and dream land is beginning to blur.";
            }
            else
            {
                return "You've passed out!";
            } 
                
        }
        static public string ParseTime(int time)
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
            }
            return hour + ":" + minute + "0" + ampm;
                 
        }
        static public string ParseAnxiety(int level)
        {
            if (level <= 9)
            {
                return "You haven't felt serenity like this since the summer after graduation!";
            }
            else if (level <= 19)
            {
                return "You feel pretty calm.";
            }
            else if (level <= 29)
            {
                return "You dread the tasks ahead of you, but aren't really bothered by them.";
            }
            else if (level <= 39)
            {
                return "You feel jittery, but you doubt anyone else could tell.";
            }
            else if (level <= 49)
            {
                return "You can't seem to stop yourself from bouncing your leg up and down.";
            }
            else if (level <= 59)
            {
                return "Your occasionally feel your heart pick up speed for no apparently reason.";
            }
            else if (level <= 69)
            {
                return "The pool of sweat underneath your shirt is unwarranted by cubicle work.";
            }
            else if (level <= 79)
            {
                return "Your hands tremble to the point where you've lost some use of them.";
            }
            else if (level <= 89)
            {
                return "Any sound other than the drone of the office HVAC system makes you jump in your seat.";
            }
            else if (level <= 99)
            {
                return "Your mouth is dry. Your fingertips are numb. Your can't control your breathing.";
            }
            else
            {
                return "Oh no, you're having one of your indomitable panic attacks!";
            }
        }
        static public string ParseFocus(int level)
        {
            if (level <= 9)
            {
                return "You cannot resist the compulsion to look at anything other than work!";
            }
            else if (level <= 19)
            {
                return "You can't stay focused on anything for more than few seconds.";
            }
            else if (level <= 29)
            {
                return "Your boredom has become a powerful distraction unto itself.";
            }
            else if (level <= 39)
            {
                return "You're capable of completing sequences of actions, but you can't remember doing them after you've finished.";
            }
            else if (level <= 49)
            {
                return "Your thinking feels slowed, but capable of deliberation.";
            }
            else if (level <= 59)
            {
                return "With some effort, you're able to stay on task.";
            }
            else if (level <= 69)
            {
                return "Your eyes water from time to time.";
            }
            else if (level <= 79)
            {
                return "You catch yourself closing your eyes involuntarily at inopportune moments.";
            }
            else if (level <= 89)
            {
                return "Your head seems to be magnetically attracted to whatever is in front of you.";
            }
            else if (level <= 99)
            {
                return "The line separating the real world and dream land is beginning to blur.";
            }
            else
            {
                return "You've passed out!";
            }
            static public void PassTenMinutes(int time, int tiredness, int focus, int anxiety, int caffeine)
        {
            time = time++;
            if (time % 6 == 0)
            {
                tiredness = tiredness + 3;
                Random rnd = new();
                int focusCoinFlip = rnd.Next(1, 2);
                if (focusCoinFlip % 2 == 0)
                {
                    focus = focus + 10;
                } else
                {
                    focus = focus - 10;
                }
                anxiety = (int)(anxiety * Math.Pow(0.5, 0.17));
                caffeine = (int)(caffeine * Math.Pow(0.5, 0.042));

            }
        }
        static public int CalculateNewAnxiety(int caffeine, int tiredness, int anxiety)
        {
            int addedAnxiety = (caffeine / 50) - (tiredness / 100);
            return anxiety + addedAnxiety;
        }

        static public int DirectlyChangeAnxiety(int directAnxiety, int anxiety)
        {
            return anxiety + directAnxiety;
        }

        static public int DirectlyChangeTiredness(int directTiredness, int tiredness)
        {
            return tiredness + directTiredness;
        }


    }
}













