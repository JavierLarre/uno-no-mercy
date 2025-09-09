using Godot;
using UnoNoMercy.Controllers;
using UnoNoMercy.Models;

namespace UnoNoMercy.Views;

public partial class Ui : Control, IView
{
    private CardBox _cardBox;

    public override void _Ready()
    {
        _cardBox = GetChild<CardBox>(0);
    }

    public void SetModel(GameModel model)
    {
        _cardBox.SetModel(model);
    }

    public void Update()
    {
        _cardBox.Update();
    }
}