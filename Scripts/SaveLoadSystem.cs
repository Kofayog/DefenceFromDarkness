using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoadSystem
{
    public static event System.Action SaveInitiated;

    public static string RootFolder { get; private set; } = Path.Combine(Application.persistentDataPath, "Saves");
    private const string fileExtension = ".save"; 

    public static void Save<T>(T objectToSave, string uniqueKey)
    {
        if (!File.Exists(RootFolder))
            Directory.CreateDirectory(RootFolder);

        string dataPath = Path.Combine(RootFolder, uniqueKey + fileExtension);

        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream stream = new FileStream(dataPath, FileMode.Create))
        {
            formatter.Serialize(stream, objectToSave);
        }
    }
    public static void Save<T>(T objectToSave, string uniqueKey, string subFolder)
    {
        string folderPath = Path.Combine(RootFolder, subFolder);

        if (!File.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        string dataPath = Path.Combine(folderPath, uniqueKey + fileExtension);

        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream stream = new FileStream(dataPath, FileMode.Create))
        {
            formatter.Serialize(stream, objectToSave);
        }
    }

    public static T Load<T>(string uniqueKey)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        T returnValue = default(T);
        try
        {
            // Getting info about the root folder
            DirectoryInfo directoryInfo = new DirectoryInfo(RootFolder);
            // Finding file that matching specified searchPattern
            FileInfo[] filesInfo = directoryInfo.GetFiles(uniqueKey + fileExtension, SearchOption.AllDirectories);

            string dataPath = Path.Combine(filesInfo[0].Directory.FullName, filesInfo[0].FullName);

            using (FileStream stream = new FileStream(dataPath, FileMode.Open))
            {
                returnValue = (T)formatter.Deserialize(stream);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"File not found: {e.ToString()}");
        }

        return returnValue;
    }

    public static bool SaveExist(string uniqueKey)
    {
        var file = Directory.GetFiles(RootFolder, uniqueKey + fileExtension, SearchOption.AllDirectories);
        return file.Length > 0 ? true : false;
    }
    public static void OnSaveInitiated()
    {
        SaveInitiated?.Invoke();
    }
}
