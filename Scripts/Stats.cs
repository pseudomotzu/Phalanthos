using Godot;
using System;
using System.Collections.Generic;
public class Stats : Resource
{
    [Export]
    public string Name { get; set; }

    [Export]
    public Dictionary<string, Dictionary<string, float>> Skills = new Dictionary<string, Dictionary<string, float>>()
    {
        {"Rowing", new Dictionary<string, float> { { "Base Value", default }, { "Adjusted Value", default }, { "Modifier", default } } },
        {"Piloting", new Dictionary<string, float> { { "Base Value", default }, { "Adjusted Value", default }, { "Modifier", default } } },
        {"Sailing", new Dictionary<string, float> { { "Base Value", default }, { "Adjusted Value", default }, { "Modifier", default } } },
        {"Rigging", new Dictionary<string, float> { { "Base Value", default }, { "Adjusted Value", default }, { "Modifier", default } } },
        {"Navigating", new Dictionary<string, float> { { "Base Value", default }, { "Adjusted Value", default }, { "Modifier", default } } },
        {"Swimming", new Dictionary<string, float> { { "Base Value", default }, { "Adjusted Value", default }, { "Modifier", default } } },
        {"Fishing", new Dictionary<string, float> { { "Base Value", default }, { "Adjusted Value", default }, { "Modifier", default } } },
        {"Foraging", new Dictionary<string, float> { { "Base Value", default }, { "Adjusted Value", default }, { "Modifier", default } } },
        {"Hunting", new Dictionary<string, float> { { "Base Value", default }, { "Adjusted Value", default }, { "Modifier", default } } },
        {"Firecrafting", new Dictionary<string, float> { { "Base Value", default }, { "Adjusted Value", default }, { "Modifier", default } } },
        {"Tracking", new Dictionary<string, float> { { "Base Value", default }, { "Adjusted Value", default }, { "Modifier", default } } },
        {"Farming", new Dictionary<string, float> { { "Base Value", default }, { "Adjusted Value", default }, { "Modifier", default } } },
        {"Animal Handling", new Dictionary<string, float> { { "Base Value", default }, { "Adjusted Value", default }, { "Modifier", default } } },
        {"Animal Husbandry", new Dictionary<string, float> { { "Base Value", default }, { "Adjusted Value", default }, { "Modifier", default } } },
        {"Riding", new Dictionary<string, float> { { "Base Value", default }, { "Adjusted Value", default }, { "Modifier", default } } },
        {"Cooking", new Dictionary<string, float> { { "Base Value", default }, { "Adjusted Value", default }, { "Modifier", default } } },
        {"Blacksmithing", new Dictionary<string, float> { { "Base Value", default }, { "Adjusted Value", default }, { "Modifier", default } } },
        {"Sculpting", new Dictionary<string, float> { { "Base Value", default }, { "Adjusted Value", default }, { "Modifier", default } } },
        {"Toolcrafting", new Dictionary<string, float> { { "Base Value", default }, { "Adjusted Value", default }, { "Modifier", default } } },
        {"Carpenting", new Dictionary<string, float> { { "Base Value", default }, { "Adjusted Value", default }, { "Modifier", default } } },
        {"Masoning", new Dictionary<string, float> { { "Base Value", default }, { "Adjusted Value", default }, { "Modifier", default } } },
        {"Shipbuilding", new Dictionary<string, float> { { "Base Value", default }, { "Adjusted Value", default }, { "Modifier", default } } },
        {"Healing", new Dictionary<string, float> { { "Base Value", default }, { "Adjusted Value", default }, { "Modifier", default } } },
        {"Intimidating", new Dictionary<string, float> { { "Base Value", default }, { "Adjusted Value", default }, { "Modifier", default } } },
        {"Orating", new Dictionary<string, float> { { "Base Value", default }, { "Adjusted Value", default }, { "Modifier", default } } },
        {"Commanding", new Dictionary<string, float> { { "Base Value", default }, { "Adjusted Value", default }, { "Modifier", default } } },
        {"Strategizing", new Dictionary<string, float> { { "Base Value", default }, { "Adjusted Value", default }, { "Modifier", default } } },
        {"Scouting", new Dictionary<string, float> { { "Base Value", default }, { "Adjusted Value", default }, { "Modifier", default } } },
        {"Sneaking", new Dictionary<string, float> { { "Base Value", default }, { "Adjusted Value", default }, { "Modifier", default } } },
    };

    public Dictionary<ValueTuple<string, string>, ValueTuple<float, float>> Modifiers { get; set; }

    public void RemoveModifiers()
    {
        foreach (var modifier in Modifiers)
        {
            if (GameManager.time >= modifier.Value.Item2)
            {
                Modifiers.Remove(modifier.Key);
            }
        }
    }

    public void ApplyModifiers()
    {
        foreach (var skill in Skills)
        {
            skill.Value["Modifier"] = 0;
        }

        foreach (var modifier in Modifiers)
        {
            Skills[modifier.Key.Item2]["Modifier"] += modifier.Value.Item1;
        }

        foreach (var skill in Skills)
        {
            skill.Value["Adjusted Value"] = skill.Value["Base Value"] + skill.Value["Modifier"];
        }
    }

    public void AddModifier(string name, string skill, float value, float length, bool stacks)
    {
        ValueTuple<string, string> names = (name, skill);

        if (stacks && Modifiers.ContainsKey(names))
        {
            Modifiers[names] = new ValueTuple<float, float>(Modifiers[names].Item1 + value, Modifiers[names].Item2 + length);

            Skills[skill]["Modifier"] += value;
            Skills[skill]["Adjusted Value"] += value;
        }
        else
        {
            if (Modifiers.ContainsKey(names))
            {
                Skills[skill]["Modifier"] -= Modifiers[names].Item1;
                Skills[skill]["Adjusted Value"] -= Modifiers[names].Item1;
                Modifiers.Remove(names);
            }

            Modifiers.Add(names, new ValueTuple<float, float>(value, GameManager.time + length));

            Skills[skill]["Modifier"] += value;
            Skills[skill]["Adjusted Value"] += value;
        }
    }
}
