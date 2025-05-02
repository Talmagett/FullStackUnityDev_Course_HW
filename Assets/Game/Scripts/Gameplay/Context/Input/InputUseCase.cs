using Unity.Mathematics;
using UnityEngine;

namespace SampleGame
{
    public static class InputUseCase
    {
        public static float3 GetMoveDirection(in InputMap inputMap)
        {
            float3 result = float3.zero;

            if (Input.GetKey(inputMap.forward))
                result.z = 1;
            else if (Input.GetKey(inputMap.back)) 
                result.z = -1;
            
            if (Input.GetKey(inputMap.right))
                result.x = 1;
            else if (Input.GetKey(inputMap.left)) 
                result.x = -1;

            return result;
        }

        public static bool IsFire(in InputMap inputMap)
        {
            return Input.GetKeyDown(inputMap.fire);
        }
    }
}