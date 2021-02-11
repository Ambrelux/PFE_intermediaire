using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using System.IO;
using Res.Scripts.Waves;
using UnityEditor;
using UnityEngine.Networking;


namespace Res.Scripts.API
{
    public class ApiRequest
    {

        public static IEnumerator CreateSound(List<GameObject> spheresList, int frequency)
        {
            var form = new WWWForm();
            var sphereForm = "[";
            form.AddField("emitted", DateTime.Now.ToString(CultureInfo.InvariantCulture));
            form.AddField("frequency", frequency.ToString());

            for (var i = 0; i < spheresList.Count; i++)
            {
                sphereForm = sphereForm + "{ \"key\":" + i + ", \"coordinates\":\"[";
                var waveCoordData = spheresList[i].GetComponent<Sphere>().WaveCoordData;
                for (int j = 0; j < waveCoordData.Count; j++)
                {
                    sphereForm = sphereForm + "[" + waveCoordData[j].x.ToString().Replace(",", ".") +
                                 "," + waveCoordData[j].y.ToString().Replace(",", ".") + "," +
                                 waveCoordData[j].z.ToString().Replace(",", ".") + "]";
                    if (j < waveCoordData.Count - 1)
                    {
                        sphereForm += ",";
                    }
                }

                sphereForm += "]}";

                if (i < spheresList.Count - 1)
                {
                    sphereForm += ",";
                }
            }

            sphereForm += "]";

            form.AddField("spheres", sphereForm);


            using (var www = UnityWebRequest.Post("http://localhost:3000/createSound", form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log("Form upload complete");
                }
            }
        }

        public static class JsonHelper
        {
            public static T[] FromJson<T>(string json)
            {
                Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
                return wrapper.Items;
            }

            public static string ToJson<T>(T[] array)
            {
                Wrapper<T> wrapper = new Wrapper<T>();
                wrapper.Items = array;
                return JsonUtility.ToJson(wrapper);
            }

            public static string ToJson<T>(T[] array, bool prettyPrint)
            {
                Wrapper<T> wrapper = new Wrapper<T>();
                wrapper.Items = array;
                return JsonUtility.ToJson(wrapper, prettyPrint);
            }

            [Serializable]
            private class Wrapper<T>
            {
                public T[] Items;
            }
        }

        public static IEnumerator FindSound()
        {
            using (UnityWebRequest www = UnityWebRequest.Get("http://localhost:3000/findSound"))
            {
                yield return www.Send();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    string result = www.downloadHandler.text;
                    //
                    //
                    // string path = "Assets/Res/Scripts/API/data.txt";
                    //
                    // //Write some text to the test.txt file
                    // StreamWriter writer = new StreamWriter(path, true);
                    // writer.WriteLine(result);
                    // writer.Close();
                    RootObject myObject = new RootObject();
                    myObject = JsonUtility.FromJson<RootObject>("{\"users\":" + result + "}");
                    
                    Debug.Log(myObject.myrootobject[0]._id);
                }
            }
        }

        [Serializable]
        public class RcJson
        {
            public string _id;
            public string emitted;
            public string frequency;
            public string spheres;
        }


        [Serializable]
        public class RootObject
        {
            public RcJson[] myrootobject;
        }
    }
}
