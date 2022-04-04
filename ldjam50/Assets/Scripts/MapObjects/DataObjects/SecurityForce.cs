using System;
using System.Collections.Generic;

public class SecurityForce : CoreUnit
{
    public CoreMapBase Base { get; set; }
    public bool IsSelected { get; internal set; }
    public GameFrame.Core.Media.Color SelectedColor { get; set; }
    public GameFrame.Core.Media.Color Color { get; set; }
    public List<String> MarchSounds { get; set; }
    public Int32? AssignedKey { get; set; }
}
