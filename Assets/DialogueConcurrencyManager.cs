using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public static class DialogueConcurrencyManager 
{
    public static bool inDialogue = false;

    public static bool TryEnter()
    {
        if (inDialogue)
            return false;
        inDialogue = true;
        return true;
    }

    public static bool TryExit()
    {
        if (!inDialogue)
            return false;
        inDialogue = false;
        return true;
    }
}
