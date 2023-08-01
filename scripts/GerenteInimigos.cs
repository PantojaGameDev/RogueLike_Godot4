using Godot;

public partial class GerenteInimigos : Node3D
{
	[Export] private PackedScene n_cenaInimigos;
	private Marker3D n_PosInicialInimigos;
	private int m_numeroMaxInimigos = 5;
	private int m_numeroInimigos = 0;

	private Vector3 v_posIni = Vector3.Zero;

	public override void _Ready()
	{
		n_PosInicialInimigos = GetTree().GetFirstNodeInGroup("posInicialInimigos") as Marker3D;
	}

	public override void _Process(double delta)
	{
		if (m_numeroInimigos < m_numeroMaxInimigos)
		{
			v_posIni.Z += 1;
			var inimigos = (Node3D)n_cenaInimigos.Instantiate();
			inimigos.Position = n_PosInicialInimigos.GlobalPosition + v_posIni;
			Owner.AddChild(inimigos);
			m_numeroInimigos += 1;
			//GD.Print(m_numeroInimigos);
		}
	}

	private void TimerNovosInimigos()
	{
		m_numeroInimigos = 0;
	}
}