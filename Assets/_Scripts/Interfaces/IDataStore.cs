public interface IDataStore
{
    void Save(GameData data); // We will save data with the GameData Object
    GameData Load(string name); // We will load data by passing in a string

}
