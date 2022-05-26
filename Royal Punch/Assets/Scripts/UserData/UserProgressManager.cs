using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class UserProgressManager
{
    public static string Path { get; } = Application.persistentDataPath + "/UserData.bin";
    public static UserData UserData { get; set; }

    public static UserData LoadUserData(string path)
    {
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        try
        {
            UserData = (UserData)new BinaryFormatter().Deserialize(stream);
            return UserData;
        }
        catch (System.Exception)
        {
            return null;
        }
        finally
        {
            stream.Close();
        }
    }

    public static UserData LoadUserData()
    {
        FileStream stream = new FileStream(Path, FileMode.OpenOrCreate);
        try
        {
            UserData = (UserData)new BinaryFormatter().Deserialize(stream);
            return UserData;
        }
        catch (System.Exception)
        {
            return null;
        }
        finally
        {
            stream.Close();
        }
    }

    public static void SaveUserData(string path, UserData data)
    {
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        new BinaryFormatter().Serialize(stream, data);
        stream.Close();
        Debug.Log("data saved");
    }

    public static void SaveUserData(UserData data)
    {
        FileStream stream = new FileStream(Path, FileMode.OpenOrCreate);
        new BinaryFormatter().Serialize(stream, data);
        stream.Close();
        Debug.Log("data saved");
    }
}
