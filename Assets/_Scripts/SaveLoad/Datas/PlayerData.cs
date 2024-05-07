#pragma warning disable IDE1006 // Naming Styles
using System;

[Serializable]
public class PlayerData
{
    private int _id;
    private string _name;
    private int _score;

    public int _Id
    {
        get { return _id; }
        set { _id = value; }
    }
    public string _Name 
    {
        get{ return _name; }
        set { _name = value; }
    }
    public int _Score 
    {
        get { return _score; }
        set { _score = value; }
    }

}
