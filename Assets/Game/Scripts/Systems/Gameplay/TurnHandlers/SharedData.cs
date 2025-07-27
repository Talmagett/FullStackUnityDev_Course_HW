
using System.Collections.Generic;
using Game.Gameplay.Items;
using Game.Gameplay.Match3;
using Game.Gameplay.Quests;
using UnityEngine;

public class StepRequest
{
    public readonly ItemGrid Grid;
    public readonly Quest QuestTracker;
    public Vector2Int Position1 { get; }
    public Vector2Int Position2 { get; }
    public HashSet<Vector2Int> MatchedPositions { get; set; }
    public List<Vector2Int> DestroyingItems { get; set; }
    public List<Vector2Int> QuestItems { get; set; }
    public StepRequest(ItemGrid grid, Quest questTracker, Vector2Int pos1, Vector2Int pos2)
    {
        Grid = grid;
        QuestTracker = questTracker;
        Position1 = pos1;
        Position2 = pos2;
        MatchedPositions = new HashSet<Vector2Int>();
        DestroyingItems = new List<Vector2Int>();
        QuestItems = new List<Vector2Int>();
    }
    //     public ItemGrid Grid;                     // поле
    // public List<Item> ToDrop = new();         // что падает
    // public List<BonusCreationInfo> Bonuses = new();
    // public List<GameStep> Steps = new();      // лог истории, для view

    // public bool IsLevelCompleted;
}