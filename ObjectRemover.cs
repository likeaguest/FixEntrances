
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared.Math;

namespace DoorFix {

    public class ObjectRemover {

        public int model;
        public float radius;
        public Vector3 location;
        private ColShape deletionColShape;

        public ObjectRemover(int model, Vector3 location, float radius = 1.0f) {
            this.model = model;
            this.location = location;
            this.radius = radius;
        }

        public void CreateColShape() {
            deletionColShape = API.shared.createSphereColShape(location, 25f);
            deletionColShape.onEntityEnterColShape += (shape, entityHandle) => {
                Client player = API.shared.getPlayerFromHandle(entityHandle);
                if (player == null) {
                    return;
                }
                API.shared.delay(200, true, () => API.shared.deleteObject(player, location, model, radius));
            };
        }

        public void DeleteColShape() {
            if (deletionColShape != null) {
                API.shared.deleteColShape(deletionColShape);
            }
        }
    }
}