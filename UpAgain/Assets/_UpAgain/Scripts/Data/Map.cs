using System;
using UnityEngine;

public class Map
{
    private static int _currentMap;

    public static int CurrentMap
    {
        get
        {
            return _currentMap;
        }
        set
        {
            _currentMap = value;
            PlayerPrefs.SetInt("CurrentMap", value);
            MapChanging?.Invoke();
        }
    }

    public static event Action MapChanging;
}