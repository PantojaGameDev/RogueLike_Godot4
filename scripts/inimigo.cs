using Godot;
using System;

public partial class inimigo : CharacterBody3D
{
	private const float m_velocidadeInimigo = 5.5f;
	private const float m_gravidade = 9.8f;
	private float  m_vidaInimigo = 100.0f;

	private NavigationAgent3D n_agenteNav;
	private jogador n_controleJogador;

    public override void _Ready()
    {
        n_agenteNav = GetNode<NavigationAgent3D>("NavigationAgent3D");
		n_controleJogador = GetTree().GetFirstNodeInGroup("Jogador") as jogador;
    }


    public override void _Process(double delta)
    {
        if(m_vidaInimigo <= 0)
		{
			QueueFree();
		}
    }

    

	public override void _PhysicsProcess(double delta)
	{
		var novaVelocidade = Vector3.Zero;

		if(!IsOnFloor())
		{
			novaVelocidade.Y -= m_gravidade;
		}
		n_agenteNav.TargetPosition = n_controleJogador.GlobalPosition;
		var proximaPos = n_agenteNav.GetNextPathPosition();
		var posAtual = GlobalPosition;
		novaVelocidade = (proximaPos - posAtual).Normalized() * m_velocidadeInimigo;

		n_agenteNav.SetVelocity(novaVelocidade);
		Velocity = novaVelocidade;
		MoveAndSlide();
	}

	private void MagiaAcertou(Area3D area)
	{
		GD.Print(m_vidaInimigo);
		m_vidaInimigo -= 10.0f;
	}
}
