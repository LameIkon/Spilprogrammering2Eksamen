#pragma warning disable IDE1006 // Naming Styles
using System;

[Serializable]
public class GameData
{
    private string _fileName;
    private PlayerData _playerData;

    public string _FileName 
    {
        get { return _fileName; }
        set { _fileName = value; }
    }
    public PlayerData _PlayerData 
    {
        get { return _playerData; }
        set { _playerData = value; }
    }
}
