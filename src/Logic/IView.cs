using UnoNoMercy.Models;

namespace UnoNoMercy.Controllers;

public interface IView
{
    void Update();
    void SetModel(GameModel model);
}