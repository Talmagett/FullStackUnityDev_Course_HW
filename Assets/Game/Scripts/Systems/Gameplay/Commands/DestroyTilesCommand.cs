using System.Collections.Generic;
using Game.Gameplay.Items;
using UnityEngine;

public class DestroyTilesCommand : IChainCommand
{
    private IChainCommand _nextCommand;

    public void Execute(BundleData bundle)
    {
        var questItems = new List<Item>();
        foreach (var item in bundle.Items)
        {
            if (bundle.QuestTracker.IsQuestTarget(item.ItemType))
                questItems.Add(item);
            bundle.Grid.RemoveItem(item.GridPosition);
        }
        bundle.Items= questItems;
    }
    
    public IChainCommand Next()
    {
        return _nextCommand;
    }
}