using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BurgerHeroes.UI
{
    public class SettingsPanel : MonoBehaviour
    {
        public void OpenPrivacyPolicy()
        {
            Application.OpenURL("https://playducky.com/privacypolicy");
        }
    }
}
