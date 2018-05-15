
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared.Math;

namespace DoorFix {

    public class DoorFix : Script {

        ObjectRemover remover;
        DoorCreator door;
        DoorCreator gate;

        public DoorFix() {
            API.onResourceStart += FixDoors;
            API.onResourceStop += Reset;
        }

        private void FixDoors() {
            remover = new ObjectRemover(-844827165, new Vector3(-875.4845f, 18.12612f, 44.4434), 1f);
            door = new DoorCreator(724862427, new Vector3(-883.03f, 21.98f, 45.50705f), new Vector3(1.556294E-05, -3.07506E-05, -29.79311), 1f);
            gate = new DoorCreator(-2125423493, new Vector3(-875.4, 18.07, 44.45), new Vector3(0, 0, 149.9994), 1f);

            API.shared.createObject(door.model, door.location, door.rotation);
            API.shared.createObject(gate.model, gate.location, gate.rotation);

            remover.CreateColShape();
            door.CreateColShape();
            gate.CreateColShape();

            foreach (Client player in API.getAllPlayers()) {
                if (player.position.DistanceTo2D(remover.location) <= 25f) {
					API.shared.delay(200, true, () => API.shared.sendNativeToPlayer(player, 0xF82D8F1926A02C3D, model, location.X, location.Y, location.Z, false, 0f, false));
                    API.shared.deleteObject(player, remover.location, remover.model, remover.radius);
                }
            }
        }

        private void Reset() {
            remover.DeleteColShape();
            door.DeleteColShape();
            gate.DeleteColShape();
            foreach (Client player in API.getAllPlayers()) {
                API.shared.deleteObject(player, gate.location, gate.model, 5f);
                API.shared.deleteObject(player, door.location, door.model, 5f);
            }
        }
    }
}