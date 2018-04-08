using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster
{
    public enum Action { Reset, EndGame, Tidy, EndTurn };

    public Action Bowl(int pins)
    {
        if (pins < 0 || pins > 10)
        {
            throw new UnityException("pins out of range: " + pins);
        }
        if (pins == 10)
        {
            return Action.EndTurn;
        }
        throw new UnityException("Not sure what action to return for pins: " + pins);
    }
}
