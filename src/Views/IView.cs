using UnoNoMercy.Models;

namespace UnoNoMercy.Views;

public interface IView
{
    void Update();
    void SetModel(GameModel model);
}