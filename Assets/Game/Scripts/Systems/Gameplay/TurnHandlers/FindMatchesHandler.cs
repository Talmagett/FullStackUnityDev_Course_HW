using System.Collections.Generic;
using Game.Gameplay.Items;
using UnityEngine;

public class FindMatchesHandler : BaseStepHandler
{
    public override void Execute(StepRequest request)
    {
        var matchedItems = new HashSet<Vector2Int>();
        request.MatchedPositions.Clear();
        
        var grid = request.Grid;
        if (grid == null)
        {
            Debug.LogError("Grid is null in FindMatchesCommand");
            return;
        }
        
        var pos1 = new Vector2Int(0,0);
        var pos2 = new Vector2Int(0,0);
        var pos3 = new Vector2Int(0,0);

        for (int x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Height - 2; y++)
            {
                pos1.Set(x, y);
                pos2.Set(x, y + 1);
                pos3.Set(x, y + 2);

                var item1 = grid.Get(pos1);
                var item2 = grid.Get(pos2);
                var item3 = grid.Get(pos3);

                if (AreItemsMatching(item1, item2, item3))
                {
                    matchedItems.UnionWith(new[] { pos1, pos2, pos3 });
                }
            }
        }

        for (int x = 0; x < grid.Width - 2; x++)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                pos1.Set(x, y);
                pos2.Set(x + 1, y);
                pos3.Set(x + 2, y);

                var item1 = grid.Get(pos1);
                var item2 = grid.Get(pos2);
                var item3 = grid.Get(pos3);
                if (AreItemsMatching(item1, item2, item3))
                {
                    matchedItems.UnionWith(new[] { pos1, pos2, pos3 });
                }
            }
        }

        if (matchedItems.Count > 0)
        {
            request.MatchedPositions.UnionWith(matchedItems);
            NextCommand?.Execute(request);
        }
    }

    private bool AreItemsMatching(ItemColor a, ItemColor b, ItemColor c)
    {
        return a == b && b == c;
    }
}