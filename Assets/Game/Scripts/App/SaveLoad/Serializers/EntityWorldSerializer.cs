using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Modules.Entities;
using UnityEngine;

namespace SampleGame.App
{
    public sealed class EntityWorldSerializer : GameSerializer<EntityWorld, EntityWorldData>
    {
        protected override EntityWorldData Serialize(EntityWorld world) =>
        new()
        {
            version = 1, // Increment this when the serialization format changes
            entities = world.GetAll().Select(entity =>
            {
                var transform = entity.transform;
                var entityData = new EntityData
                {
                    id = entity.Id,
                    name = entity.Name,
                    entityType = entity.Type,
                    position = new SerializableVector3(transform.position.x, transform.position.y, transform.position.z),
                    rotation = new SerializableVector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z),
                    components = entity.GetComponents<ISaveLoadData>()
                        .Select(c => new ComponentData
                        {
                            type = c.GetType().AssemblyQualifiedName,
                            properties = c.SaveData()
                        }).ToList()
                };
                return entityData;
            }).ToList()
        };


        protected override void Deserialize(EntityWorld world, EntityWorldData data)
        {
            if (data.version != 1)
                throw new System.Exception($"Unsupported version: {data.version}");
            world.DestroyAll();
            foreach (var ed in data.entities)
            {
                var pos = new Vector3(ed.position.x, ed.position.y, ed.position.z);
                var rot = Quaternion.Euler(ed.rotation.x, ed.rotation.y, ed.rotation.z);
                var entity = world.Spawn(ed.name, pos, rot, ed.id);

                entity.gameObject.name = ed.name;

                //Debug.Log($"[Serializer] Loading entity '{ed.name}' (ID: {ed.id}) with {ed.components.Count} components.");

                foreach (var compData in ed.components)
                {
                    // Get the system type
                    var type = System.Type.GetType(compData.type)
                               ?? System.AppDomain.CurrentDomain.GetAssemblies()
                                   .SelectMany(a => a.GetTypes())
                                   .FirstOrDefault(t => t.Name == compData.type.Split(',')[0]);

                    if (type == null)
                        throw new System.Exception($"Unknown component type: {compData.type}");

                    if (entity.TryGetComponent(type, out var compObj) && compObj is ISaveLoadData saveComp)
                    {
                        saveComp.LoadData(compData.properties);
                    }
                    else
                    {
                        Debug.LogWarning($"Component {type.Name} not found on entity {ed.name} (ID: {ed.id})");
                    }
                }
            }
        }
    }
}