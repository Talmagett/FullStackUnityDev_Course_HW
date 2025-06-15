using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace SampleGame.App
{
    public interface IGameRepository
    {
        UniTask SetState(Dictionary<string, string> gameState);
        UniTask<Dictionary<string, string>> GetVersionedState(int version);
        UniTask<Dictionary<string, string>> GetLastState();

    }
}