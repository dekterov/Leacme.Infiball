using Godot;
using System;

public class Table : Spatial {

	private RigidBody flipperL;
	private RigidBody flipperR;
	private const int IMPULSE = 100;

	public override void _Ready() {

		flipperL = GetNode<RigidBody>("FlipperLPhysics");
		flipperR = GetNode<RigidBody>("FlipperRPhysics");

		flipperL.Mass = flipperR.Mass = IMPULSE;

		flipperL.AxisLockLinearX = flipperR.AxisLockLinearX = true;
		flipperL.AxisLockLinearY = flipperR.AxisLockLinearY = true;
		flipperL.AxisLockLinearZ = flipperR.AxisLockLinearZ = true;

		flipperL.AxisLockAngularY = flipperR.AxisLockAngularY = true;
		flipperL.AxisLockAngularZ = flipperR.AxisLockAngularZ = true;

	}

	public override void _Input(InputEvent @event) {

		if (@event is InputEventScreenTouch ld && ld.Pressed && ld.Position.x < GetViewport().GetVisibleRect().Size.x / 2) {
			flipperL.ApplyTorqueImpulse(new Vector3(IMPULSE, 0, 0));
		} else if (@event is InputEventScreenTouch lu && !lu.Pressed && lu.Position.x < GetViewport().GetVisibleRect().Size.x / 2) {
			flipperL.ApplyTorqueImpulse(new Vector3(-IMPULSE, 0, 0));
		}

		if (@event is InputEventScreenTouch rd && rd.Pressed && rd.Position.x > GetViewport().GetVisibleRect().Size.x / 2) {
			flipperR.ApplyTorqueImpulse(new Vector3(-IMPULSE, 0, 0));
		} else if (@event is InputEventScreenTouch ru && !ru.Pressed && ru.Position.x > GetViewport().GetVisibleRect().Size.x / 2) {
			flipperR.ApplyTorqueImpulse(new Vector3(IMPULSE, 0, 0));
		}

	}

	public override void _PhysicsProcess(float delta) {

		if (flipperL.RotationDegrees.y > 40) {
			flipperL.RotationDegrees = new Vector3(0, 40, 0);
			flipperL.AngularVelocity = new Vector3(0, 0, 0);
			flipperL.ApplyTorqueImpulse(new Vector3(-IMPULSE, 0, 0));
		} else if (flipperL.RotationDegrees.y < 0) {
			flipperL.RotationDegrees = new Vector3(0, 0, 0);
			flipperL.AngularVelocity = new Vector3(0, 0, 0);
		}

		if (flipperR.RotationDegrees.y < -40) {
			flipperR.RotationDegrees = new Vector3(0, -40, 0);
			flipperR.AngularVelocity = new Vector3(0, 0, 0);
			flipperR.ApplyTorqueImpulse(new Vector3(IMPULSE, 0, 0));
		} else if (flipperR.RotationDegrees.y > 0) {
			flipperR.RotationDegrees = new Vector3(0, 0, 0);
			flipperR.AngularVelocity = new Vector3(0, 0, 0);
		}
	}

}
