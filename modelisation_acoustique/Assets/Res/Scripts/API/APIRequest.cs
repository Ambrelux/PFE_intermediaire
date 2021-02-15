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


        // public static IEnumerator FindSound()
        // {
        //     using (UnityWebRequest www = UnityWebRequest.Get("http://localhost:3000/findSound"))
        //     {
        //         yield return www.SendWebRequest();
        //
        //         if (www.isNetworkError || www.isHttpError)
        //         {
        //             Debug.Log(www.error);
        //         }
        //         else
        //         {
        //             string result = www.downloadHandler.
        //             Player[] player = JsonHelper.FromJson<Player>(result);
        //             Debug.Log(player[0]._id);
        //             // Debug.Log(result);
        //             //
        //             //
        //             // string path = "Assets/Res/Scripts/API/data.txt";
        //             //
        //             // //Write some text to the test.txt file
        //             // StreamWriter writer = new StreamWriter(path, true);
        //             // writer.WriteLine(result);
        //             // writer.Close();
        //         }
        //     }
        // }
    }
}