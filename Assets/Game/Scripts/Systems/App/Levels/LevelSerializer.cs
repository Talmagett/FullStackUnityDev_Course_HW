using SampleGame.App;

namespace Game.App.Levels
{
    public class LevelSerializer: GameSerializer<LevelService, LevelData>
    {
        protected override LevelData Serialize(LevelService service)=>
            new (){MaxLevel = service.MaxLevel};

        protected override void Deserialize(LevelService service, LevelData data) => 
            service.SetMaxLevel(data.MaxLevel);
    }
}