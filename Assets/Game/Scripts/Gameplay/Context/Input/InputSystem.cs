using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace SampleGame
{
    public sealed class InputSystem : IEcsRunSystem
    {
        private readonly InputMap _inputMap;
        private readonly EcsSingletonInject<InputData> _inputData;

        public InputSystem(InputMap inputMap)
        {
            _inputMap = inputMap;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            InputData inputData = _inputData.Value;
            inputData.moveDirection = InputUseCase.GetMoveDirection(_inputMap);
            inputData.isFire = InputUseCase.IsFire(_inputMap);
        }
    }
}