using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using Res.Scripts.Waves;
using UnityEditor;
using UnityEngine.Networking;
using Random = UnityEngine.Random;
using Res.Scripts.UserInterface;
namespace Res.Scripts.API
{
    public class ApiRequest
    {
        public const string URL = "http://localhost:3000/";

        private static string ParseSoundToJson(List<GameObject> spheresList, int frequency)
        {
            var spheresCoord = new List<string>();
            for (var i = 0; i < spheresList.Count; i++)
            {

                SphereCoords sphereCoords = new SphereCoords(spheresList[i].GetComponent<Sphere>().WaveCoordData);
                spheresCoord.Add(JsonUtility.ToJson(sphereCoords));
            }

            var newSound = new Sound(250, spheresCoord);
            var json = JsonUtility.ToJson(newSound);
            Debug.Log(json);
            return json;
        }
        
        public static IEnumerator InsertSound(List<GameObject> spheresList, int frequency)
        {
            var json = ParseSoundToJson(spheresList, frequency);

            var request = new UnityWebRequest ("http://localhost:3000/createSound", "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();

            if (request.error != null)
            {
                Debug.Log("Error: " + request.error);
            }
            else
            {
                Debug.Log("All OK");
                Debug.Log("Status Code: " + request.responseCode);
            }
            
            // string path = "Assets/Res/Scripts/API/datainsert.txt";
            //         
            // //Write some text to the test.txt file
            // StreamWriter writer = new StreamWriter(path, true);
            // writer.WriteLine(json);
            // writer.Close();
            //
            // yield return null;
        }

        public static IEnumerator FindSound()
        {
            using (UnityWebRequest www = UnityWebRequest.Get("http://localhost:3000/findSound"))
            {
                yield return www.SendWebRequest();
        
                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    string result = www.downloadHandler.text;
                    UiSounds.sounds = JsonHelper.getJsonArray<Sound>(result);
                    // Sound[] sounds = JsonHelper.getJsonArray<Sound> (result);
                    // Debug.Log(sounds[0].spheres[0]);
                    // Debug.Log(sounds[1]._id);
                    // Debug.Log(result);
                    // string path = "Assets/Res/Scripts/API/datafind.txt";
                    //
                    // //Write some text to the test.txt file
                    // StreamWriter writer = new StreamWriter(path, true);
                    // writer.WriteLine(result);
                    // writer.Close();
                }
            }
        }
    }
}

public class JsonHelper
{
    public static T[] getJsonArray<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>> (newJson);
        return wrapper.array;
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}



/*
 *
 *
 *
 *
 *
 * 
*/

[Serializable]
public class Sound
{
    public int _id;
    public string date;
    public int frequency;
    public List<string> spheres;

    public Sound(int _frequency, List<string> _spheres)
    {
        _id = Random.Range(0,999999);
        date = DateTime.Now.ToString(CultureInfo.InvariantCulture);
        frequency = _frequency;
        spheres = _spheres;
    }
}

[Serializable]
public class SphereCoords
{
    public List<Vector3> sphereCoords;

    public SphereCoords(List<Vector3> _coords)
    {
        sphereCoords = _coords;
    }
}
// public class Sound
// {
//     public int id;
//     public string date;
//     public int frequency;
//     public List<Vector3> spheres;
//
//     public Sound(int _frequency, List<Vector3> _spheres)
//     {
//         id = Random.Range(0,999999);
//         date = DateTime.Now.ToString(CultureInfo.InvariantCulture);
//         frequency = _frequency;
//         spheres = _spheres;
//     }
// }
// public class Sound
// {
//     public int id;
//     public string date;
//     public int frequency;
//     public List<List<Vector3>> spheres;
//
//     public Sound(int _frequency, List<List<Vector3>> _spheres)
//     {
//         id = Random.Range(0,999999);
//         date = DateTime.Now.ToString(CultureInfo.InvariantCulture);
//         frequency = _frequency;
//         spheres = _spheres;
//     }
// }