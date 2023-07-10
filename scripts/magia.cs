using Godot;
using System;

public partial class magia : Area3D
{
	
	public override void _Ready()
	{
	}

	
	public override void _Process(double delta)
	{
		Position += -GlobalTransform.Basis.Z * 5.0f *(float)delta;
	}
}
