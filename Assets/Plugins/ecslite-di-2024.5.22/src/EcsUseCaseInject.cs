using System.Reflection;

namespace Leopotam.EcsLite.Di
{
    public struct EcsUseCaseInject<T> : IEcsDataInject, IEcsCustomDataInject where T : struct
    {
        public T Value;

        // EcsWorldInject, EcsFilterInject, EcsPoolInject, EcsSharedInject.
        void IEcsDataInject.Fill(IEcsSystems systems)
        {
            FieldInfo[] fields = this.Value.GetType().GetFields(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance
            );

            foreach (var f in fields)
                if (!f.IsStatic)
                    InjectBuiltIns(f, ref this.Value, systems);
        }

        // IEcsCustomDataInject derivatives (EcsCustomInject, etc).
        void IEcsCustomDataInject.Fill(object[] injects)
        {
            FieldInfo[] fields = this.Value.GetType().GetFields(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance
            );

            foreach (var f in fields)
                if (!f.IsStatic)
                    InjectCustoms(f, ref this.Value, injects);
        }


        private static void InjectBuiltIns(FieldInfo fieldInfo, ref T s, IEcsSystems systems)
        {
            if (!typeof(IEcsDataInject).IsAssignableFrom(fieldInfo.FieldType))
                return;

            IEcsDataInject instance = (IEcsDataInject) fieldInfo.GetValue(s);
            instance.Fill(systems);
            fieldInfo.SetValueDirect(__makeref(s), instance);
        }

        private static void InjectCustoms(FieldInfo fieldInfo, ref T s, object[] injects)
        {
            if (!typeof(IEcsCustomDataInject).IsAssignableFrom(fieldInfo.FieldType))
                return;

            IEcsCustomDataInject instance = (IEcsCustomDataInject) fieldInfo.GetValue(s);
            instance.Fill(injects);
            fieldInfo.SetValueDirect(__makeref(s), instance);
        }
    }
}