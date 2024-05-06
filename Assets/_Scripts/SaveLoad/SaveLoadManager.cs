using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : NetworkBehaviour
{
    [SerializeField] private GameData _gameData;

    public SaveLoadManager _Instance;

    private IDataStore _dataStorage; // This is how we store the data

    #region Unity Methods

    private void Awake() 
    {
        if (_Instance == null)  // We make this a Singleton
        {
            _Instance = this;
            transform.SetParent(null);      // We set the parent of the this the gameObject that has this component to be its own parent, this will ensure that it will not be destroyed prematurely.
            DontDestroyOnLoad(gameObject);  // This will persist through scenes, but is not really needed in this instance but it is nice to have

        }
        else 
        {
            if (_Instance != this) 
            {
                Destroy(gameObject);
            }
        }

        _dataStorage = new FileWriteDataStore(new JsonSerialize());  // we instanciatite the _dataStorage with the JsonSerialize such we
    
    }

    #endregion

    private void SaveGame() 
    {
        _dataStorage.Save(_gameData);  
    }

    private void LoadGame(string gameName) 
    {
        _gameData = _dataStorage.Load(gameName); 
    }

}
