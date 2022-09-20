using System;

namespace Actions


public class Action
{
    public int ActionID { get; set; }
    public int TimePassed { get; set; }
    public int DirectTiredness { get; set; }
    public int DirectAnxiety { get; set; }
    public int DirectFocus { get; set; }
    public int Dose { get; set; }

    public Action(int actionID, int timePassed, int directTiredness, int directAnxiety, int directFocus, int dose)
    {
        ActionID = actionID;
        TimePassed = timePassed;
        DirectTiredness = directTiredness;
        DirectAnxiety = directAnxiety;
        DirectFocus = directFocus;
        Dose = dose;

    }

}