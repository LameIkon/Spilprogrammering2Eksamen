using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : NetworkBehaviour
{
    [SerializeField] public GameData _gameData;

    private IDataStore _dataStore; // This is how we store the data

    #region Unity Methods

    private void Awake() 
    {
        _dataStore = new FileWriteDataStore(new JsonSerialize()); // we instanciate the _dataStorage as a type FileWriter with the JsonSerialize, we also need to do it here in the Awake method, as the persistantDataPath cannot be instanciatied outside of Awake() and Strat()
    }

    #endregion

    public void SaveGame() 
    {
        _dataStore.Save(_gameData);  
    }

    public void LoadGame(string gameName) 
    {
        _gameData = _dataStore.Load(gameName); 
    }

}
