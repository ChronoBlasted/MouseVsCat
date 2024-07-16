using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPopup : Popup
{
    [SerializeField] Navbar _navbar;
    public override void Init()
    {
        base.Init();

        _navbar.Init();
    }

    public override void OpenPopup()
    {
        base.OpenPopup();
    }

    public override void ClosePopup()
    {
        base.ClosePopup();
    }
}
