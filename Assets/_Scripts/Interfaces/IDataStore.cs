public interface IDataStore
{
    void Save(GameData data); // We will save data with the GameData Object
    GameData Load(string fileName); // We will load data by passing in a string
    void DeleteSave(string fileName); // this can delete a save with the file name

}
