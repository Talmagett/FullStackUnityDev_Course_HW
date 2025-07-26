
using System.Collections.Generic;
using Game.Gameplay.Items;
using Game.Gameplay.Match3;
using Game.Gameplay.Quests;
using UnityEngine;

public class BundleData
{
    public readonly LevelGrid Grid;
    public readonly Quest QuestTracker;
    public List<Item> Items { get; set; }

    public BundleData(LevelGrid grid, Quest questTracker)
    {
        Grid = grid;
        QuestTracker = questTracker;
    }
}