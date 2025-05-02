using UnityEngine;

namespace SampleGame
{
    public static class TeamViewUseCase
    {
        public static void SetTeam(in Renderer[] renderers, in TeamType teamType, in TeamViewConfig viewConfig)
        {
            TeamViewConfig.TeamInfo team = viewConfig.GetTeam(teamType);
            Material material = team.Material;
            for (int i = 0, count = renderers.Length; i < count; i++) 
                renderers[i].material = material;
        }
    }
}