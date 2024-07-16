using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navbar : MonoBehaviour
{
    [SerializeField] Tab _firstTab;

    Tab _currentTab;

    public void Init()
    {
        ChangeTab(_firstTab);
    }

    public void ChangeTab(Tab newTab)
    {
        if (_currentTab == newTab) return;

        if (_currentTab != null) _currentTab.HandleOnReset();

        _currentTab = newTab;

        _currentTab.HandleOnPress();
    }
}
