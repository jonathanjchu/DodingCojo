using System;
using System.Collections.Generic;

[Serializable]
public class LevelData
{
    public string name;
    public List<List<string>> map;
    public List<UnitData> enemies;
    public List<UnitData> NPCs;
    public string nextLevel;
}