using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using System.Collections.Generic;
using System.Collections;
using Res.Scripts.Objects;
using Res.Scripts.Waves;
using UnityEngine.Networking;


namespace Res.Scripts.API
{
    public class APIRequest
    {


        public IEnumerator CreateSound(List<GameObject> spheresList, int frequency)
        {
            WWWForm form = new WWWForm();
            List<Vector3> waveCoordData;
            String sphereForm = "[";
            form.AddField("emitted",DateTime.Now.ToString());
            form.AddField("frequency", frequency.ToString());

            for (int i = 0; i < spheresList.Count; i++)
            {
                sphereForm = sphereForm + "{ \"key\":" + i + ", \"coordinates\":\"[";
                waveCoordData = spheresList[i].GetComponent<Sphere>().WaveCoordData;
                for (int j = 0; j < waveCoordData.Count; j++)
                {
                    sphereForm = sphereForm + "[" + waveCoordData[j].x.ToString().Replace(",",".") +
                                 "," + waveCoordData[j].y.ToString().Replace(",",".")  + "," +
                                 waveCoordData[j].z.ToString().Replace(",",".")  + "]";
                    if (j < waveCoordData.Count - 1)
                    {
                        sphereForm = sphereForm + ",";
                    }
                }

                sphereForm = sphereForm + "]}";
            
                if (i < spheresList.Count - 1)
                {
                    sphereForm = sphereForm + ",";
                }
            }

            sphereForm = sphereForm + "]";
            
            form.AddField("spheres",sphereForm);


            using (UnityWebRequest wwww = UnityWebRequest.Post("http://localhost:3000/createSound", form))
            {
                yield return wwww.SendWebRequest();

                if (wwww.isNetworkError || wwww.isHttpError)
                {
                    Debug.Log(wwww.error);
                }
                else
                {
                    Debug.Log("Form upload complete");
                }
            }
        }
    }
}