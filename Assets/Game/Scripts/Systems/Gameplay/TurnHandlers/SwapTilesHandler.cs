using UnityEngine;
using Zenject;

public class SwapTilesCommand : BaseStepHandler
{
    [Inject] private SignalBus _signalBus;

    public override void Execute(StepRequest request)
    {
        var grid = request.Grid;
        if (grid == null)
        {
            Debug.LogError("Grid is null in SwapTilesCommand");
            return;
        }
        var pos1 = request.Position1;
        var pos2 = request.Position2;
        var item1 = grid.Get(pos1);
        var item2 = grid.Get(pos2);
        
        grid.Set(pos1, item2);
        grid.Set(pos2, item1);

        _signalBus.Fire(new SwapTilesEvent(pos1, pos2));
        NextCommand?.Execute(request);
    }
}