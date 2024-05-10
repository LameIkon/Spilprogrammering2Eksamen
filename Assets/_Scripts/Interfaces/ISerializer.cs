// This interface will ensure that we implement the Serialize and Delerialize methods, also we assume that all data will be saved as a .json file

public interface ISerializer
{
    string Serialize<T>(T obj); // This is generic as we will want to pass in different different objects 
    T Deserialize<T>(string json); // The same is true for this
}
