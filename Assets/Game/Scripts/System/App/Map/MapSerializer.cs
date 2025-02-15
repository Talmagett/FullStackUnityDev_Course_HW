using SampleGame.App;

namespace Game.Scripts.System.App.Map
{
    public class MapSerializer: GameSerializer<Game.System.App.Map.Map, MapData>
    {
        protected override MapData Serialize(Game.System.App.Map.Map service)=>
            new (){MaxLevel = service.MaxLevel};

        protected override void Deserialize(Game.System.App.Map.Map service, MapData data) => 
            service.SetMaxLevel(data.MaxLevel);
    }
}