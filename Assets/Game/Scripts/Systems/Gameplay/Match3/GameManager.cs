using System;
using System.Collections.Generic;
using Game.Gameplay.Items;
using Game.Gameplay.Match3;
using Game.Gameplay.Quests;
using UnityEngine;

public class GameManager
{
    public event Action OnTurnFinished;
    public event Action OnGameStarted;
    public event Action OnGameFinished;
    public bool IsRunning => _isRunning;
    private bool _isRunning;
    private readonly ItemGrid _grid;
    private readonly Quest _questTracker;
    private List<IStepHandler> _pipeline;
    public GameManager(ItemGrid grid, Quest questTracker)
    {
        _grid = grid;
        _questTracker = questTracker;
        GeneratePipeline();
    }

    private void GeneratePipeline()
    {
        var swapTilesCommand = new SwapTilesCommand();
        var findMatchesCommand = new FindMatchesHandler();
        var destroyTilesCommand = new DestroyTilesHandler();
        var fallCommand = new FallTilesHandler();
        var generateTilesCommand = new GenerateTilesHandler();

        swapTilesCommand.SetNext(null);
        
        findMatchesCommand.SetNext(destroyTilesCommand);
        destroyTilesCommand.SetNext(fallCommand);
        fallCommand.SetNext(generateTilesCommand);
        generateTilesCommand.SetNext(findMatchesCommand);
        _pipeline = new List<IStepHandler>
        {
            swapTilesCommand,
            findMatchesCommand,
            destroyTilesCommand,
            fallCommand,
            generateTilesCommand
        };
    }

    public void TrySwap(Vector2Int pos1, Vector2Int pos2)
    {
        var request = new StepRequest(_grid, _questTracker, pos1, pos2);
        _pipeline[0].Execute(request);
    }

    public void StartGame()
    {
        _isRunning = true;
        OnGameStarted?.Invoke();
    }
}