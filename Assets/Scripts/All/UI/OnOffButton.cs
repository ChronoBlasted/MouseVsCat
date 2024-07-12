using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chrono.UI
{
    public class OnOffButton : CustomButton
    {
        [SerializeField] Image _ico;
        [SerializeField] Sprite _onSprite, _offSprite;

        public void SetOn()
        {
            _ico.sprite = _onSprite;
        }

        public void SetOff()
        {
            _ico.sprite = _offSprite;
        }
    }
}


