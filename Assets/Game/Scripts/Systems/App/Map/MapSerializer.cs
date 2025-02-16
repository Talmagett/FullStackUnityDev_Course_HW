using SampleGame.App;

namespace Game.App.Map
{
    public class MapSerializer: GameSerializer<Map, MapData>
    {
        protected override MapData Serialize(Map service)=>
            new (){MaxLevel = service.MaxLevel};

        protected override void Deserialize(Map service, MapData data) => 
            service.SetMaxLevel(data.MaxLevel);
    }
}