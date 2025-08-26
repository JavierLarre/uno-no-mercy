using Godot;

namespace UnoNoMercy.scripts;

public partial class UnoNoMercyGame : Control
{
    public override void _Ready()
    {
        GD.Print("mish");
        GD.Print(OS.GetDataDir());
    }
}