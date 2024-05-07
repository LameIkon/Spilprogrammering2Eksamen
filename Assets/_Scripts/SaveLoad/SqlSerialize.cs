using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;

public class SqlSerialize : ISerialize
{
    public string Serialize<T>(T obj)
    {
        throw new System.NotImplementedException();
    }

    public T Deserialize<T>(string json)
    {
        throw new System.NotImplementedException();
    }

}
