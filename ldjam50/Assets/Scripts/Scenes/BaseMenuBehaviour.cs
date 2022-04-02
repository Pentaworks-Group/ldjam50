using UnityEngine;
using Assets.Scripts.Base;

namespace Assets.Scripts.Scenes
{
    public class BaseMenuBehaviour : MonoBehaviour
    {
        public void Back()
        {
            Base.Core.Game.ChangeScene(SceneNames.MainMenu);
        }
    }
}
