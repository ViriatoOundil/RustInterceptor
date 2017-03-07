using System.Collections.Generic;
using UnityEngine;

namespace Rust_Interceptor.Data {

    public class Entity {
        public static List<EntityUpdate> GetPositions(Packet p) {
            List<EntityUpdate> updates = new List<EntityUpdate>();
            /* EntityPosition packets may contain multiple positions */
            while (p.unread >= 28L /* Uint32 = 4bytes, Float = 4bytes. Uint32 + (Float * 6) = 28 */) {
                EntityUpdate update = new EntityUpdate();
                /* Entity UID */
                update.uid = p.UInt32();
                /* Read 2 Vector3, Position and Rotation */
                update.position = p.Vector3();
                update.rotation = p.Vector3();
                updates.Add(update);
            }
            return updates;
        }

        public static uint GetEntity(Packet p, out ProtoBuf.Entity entity) {
            /* Entity Number/Order */
            var num = p.UInt32();
            entity = ProtoBuf.Entity.Deserialize(p);
            return num;
        }

        public class EntityUpdate {
            internal uint uid;
            public uint UID { get { return uid; } }
            internal Vector3 position;
            public Vector3 Position { get { return position; } }
            internal Vector3 rotation;
            public Vector3 Rotation { get { return rotation; } }
        }


    }
}
