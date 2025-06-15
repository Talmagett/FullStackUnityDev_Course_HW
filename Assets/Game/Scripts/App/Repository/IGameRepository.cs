using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace SampleGame.App
{
    public interface IGameRepository
    {
        UniTask SetState(Dictionary<string, string> gameState);
        UniTask<Dictionary<string, string>> GetState();
    }
}