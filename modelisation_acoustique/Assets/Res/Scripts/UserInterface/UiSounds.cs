using System;
using System.Collections.Generic;
using UnityEngine;
using Res.Scripts.API;
using UnityEngine.UI;

namespace Res.Scripts.UserInterface
{
    public class UiSounds : MonoBehaviour
    {
        private List<GameObject> _uiSounds = new List<GameObject>();
        public static Sound[] sounds;
        private void Awake()
        {
            _uiSounds.AddRange(GameObject.FindGameObjectsWithTag("UI_Sound"));    
        }

        public void UpdateUISound()
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                Text id = _uiSounds[i].transform.Find("no_text").GetComponent<Text>();
                Text date = _uiSounds[i].transform.Find("date_text").GetComponent<Text>();
                Text frequency = _uiSounds[i].transform.Find("frequency_text").GetComponent<Text>();

                id.text = sounds[i]._id.ToString();
                date.text = sounds[i].date;
                frequency.text = sounds[i].frequency.ToString();
            }
        }
    }
}