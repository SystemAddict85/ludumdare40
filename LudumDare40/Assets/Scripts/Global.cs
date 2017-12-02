using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class Global
{
    public static bool Paused = false;

    public static void CallDialog(string text)
    {
        GameManager.Instance.UI.dialogBox.CallDialogBox(text);
    }
    
}

