using Godot;
using System;
using System.Linq;
using System.Timers;

public class BallPhysics : RigidBody {

	public override void _Ready() {

		ContactMonitor = true;
		ContactsReported = 10;
		GetParent<Spatial>().GlobalTranslate(new Vector3(0.211f, 2.81f, 0));
	}

	public override void _PhysicsProcess(float delta) {
		GetCollidingBodies().Cast<Node>().Where(z => z.Name.StartsWith("Pin") && z.Name.EndsWith("Physics")).ToList().ForEach(

			z => {
				z.GetChild(0).GetChild<MeshInstance>(0).MaterialOverride = new SpatialMaterial {
					EmissionEnabled = true,
					EmissionEnergy = 0.8f,
					Emission = Color.FromHsv(GD.Randf(), 1, 1),
					Metallic = 0.5f,
					MetallicSpecular = 1,
					Roughness = 0.5f,
				};
			}
		);


		if (GetCollidingBodies().Cast<Node>().Any(z => z.Name.Equals("BottomPanePhysics"))) {
			var tempTrans = Transform;
			tempTrans.origin = (new Vector3(0, 0, 0));
			Transform = tempTrans;
		}
	}

}
