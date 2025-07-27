using System.Collections.Generic;
using Game.Gameplay.Items;
using UnityEngine;

public class DestroyTilesHandler : BaseStepHandler
{
    public override void Execute(StepRequest bundle)
    {
        var questItems = new List<Vector2Int>();
        foreach (var position in bundle.DestroyingItems)
        {
            if (bundle.QuestTracker.IsQuestTarget(bundle.Grid.Get(position)))
                questItems.Add(position);
            bundle.Grid.RemoveItem(position);
        }
        bundle.QuestItems = questItems;

        NextCommand?.Execute(bundle);
    }
}