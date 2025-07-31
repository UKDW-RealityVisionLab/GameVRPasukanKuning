using System;
using System.Collections.Generic;

[Serializable]
public class NPCDialogEntry
{
    public string[] explanation;
    public string[] offer;
    public string[] questions;
    public string[] answers;
    public string[] randomChat;
}

[Serializable]
public class NPCDialogWrapper : Dictionary<string, NPCDialogEntry> { }
