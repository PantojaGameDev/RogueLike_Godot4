using Godot;
using System;

public partial class jogador : CharacterBody3D
{
	private float m_velocidadeJogador = 5.0f;
	private float m_velocidadePulo = 4.5f;
	private const float m_GRAVIDADE = 9.8f;
	private float m_sensibilidadeMouse = 0.003f;
	private bool m_jogadorPodeMover = true;

	

	private Node3D n_rostoJogador;
	private Camera3D n_cameraJogador;
	[Export] private PackedScene n_cenaMagia;


	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
        n_rostoJogador = GetNode<Node3D>("Rosto");
		n_cameraJogador = GetNode<Camera3D>("Rosto/Camera");	
	}

	public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseMotion)
		{
			InputEventMouseMotion movimentoMouse = @event as InputEventMouseMotion;
			n_rostoJogador.RotateY(-movimentoMouse.Relative.X * m_sensibilidadeMouse);
			n_cameraJogador.RotateX(-movimentoMouse.Relative.Y * m_sensibilidadeMouse);
			Vector3 rotCamera = n_cameraJogador.Rotation;
			rotCamera.X = Mathf.Clamp(rotCamera.X, Mathf.DegToRad(-80.0f), Mathf.DegToRad(80.0f));
			n_cameraJogador.Rotation = rotCamera;
		}

		if(Input.IsActionJustPressed("correr"))
		{
			m_velocidadeJogador = 10.0f;
		}
		if(Input.IsActionJustPressed("atirar"))
		{
			DispararMagia();
		}
		
    }

	public override void _PhysicsProcess(double delta)
	{
		if(m_jogadorPodeMover)
		{
			MoverJogador((float)delta);
		}
	}

	private void DispararMagia()
	{
		var magia = (Node3D)n_cenaMagia.Instantiate();
		magia.GlobalTransform = GetNode<Marker3D>("Rosto/Camera/posInicialMagia").GlobalTransform;
		Owner.AddChild(magia);
	}
	private void MoverJogador(float delta)
	{
		Vector3 velocity = Velocity;

		
		if (!IsOnFloor())
			velocity.Y -= m_GRAVIDADE * (float)delta;

		
		if (Input.IsActionJustPressed("pular") && IsOnFloor())
			velocity.Y = m_velocidadePulo;

		
		Vector2 inputDir = Input.GetVector("mover_esquerda", "mover_direita", "mover_frente", "mover_tras");
		Vector3 direction = (n_rostoJogador.Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * m_velocidadeJogador;
			velocity.Z = direction.Z * m_velocidadeJogador;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, m_velocidadeJogador);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, m_velocidadeJogador);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
