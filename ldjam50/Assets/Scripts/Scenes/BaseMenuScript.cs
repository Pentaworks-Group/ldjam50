using UnityEngine;
using Assets.Scripts.Base;

namespace Assets.Scripts.Scenes
{
    public class BaseMenuScript : MonoBehaviour
    {
        public void Back()
        {
            Base.Core.Game.ChangeScene(SceneNames.MainMenu);
        }
    }
}
