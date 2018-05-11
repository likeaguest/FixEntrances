
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared.Math;

namespace DoorFix {

    public class DoorCreator {

        public int model;
        public float radius;
        public Vector3 location;
        public Vector3 rotation;
        private ColShape triggerColShape;

        public DoorCreator(int model, Vector3 location, Vector3 rotation, float radius = 1.0f) {
            this.model = model;
            this.location = location;
            this.rotation = rotation;
            this.radius = radius;
        }

        public void CreateColShape() {
            triggerColShape = API.shared.createSphereColShape(location, 10f);
            API.shared.createObject(model, location, rotation);
            triggerColShape.onEntityEnterColShape += (shape, entityHandle) => {
                Client player = API.shared.getPlayerFromHandle(entityHandle);
                if (player == null) {
                    return;
                }
                API.shared.delay(200, true, () => API.shared.sendNativeToPlayer(player, 0xF82D8F1926A02C3D, model, location.X, location.Y, location.Z, false, 0f, false));
            };
        }

        public void DeleteColShape() {
            if (triggerColShape != null) {
                API.shared.deleteColShape(triggerColShape);
            }
        }
    }
}
