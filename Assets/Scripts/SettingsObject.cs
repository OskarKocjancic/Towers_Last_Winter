using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsObject
{
    public SettingsObject() { }
    public SettingsObject(bool _fullscreen, int _selectedResolution, float _volume)
    {
        fullscreen = _fullscreen;
        volume = _volume;
        selectedResolution = _selectedResolution;
    }
    public bool fullscreen;
    public int selectedResolution;
    public float volume;
}