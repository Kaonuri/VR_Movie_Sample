using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeTriggerManager : MonoBehaviour
{
    private List<GazeTrigger> gazeTriggers = new List<GazeTrigger>();
    private float currentTime = 0;

    private void Awake()
    {
        foreach (var gazeTrigger in GetComponentsInChildren<GazeTrigger>())
        {
            gazeTriggers.Add(gazeTrigger);
            gazeTrigger.gameObject.SetActive(false);
        }        
    }

    private void Update()
    {
        currentTime = Time.time;
        for (int i = 0; i < gazeTriggers.Count; i++)
        {
            if (currentTime >= gazeTriggers[i].StartTime && currentTime <= gazeTriggers[i].FinishTime)
            {
                gazeTriggers[i].gameObject.SetActive(true);
            }
            else
            {
                gazeTriggers[i].gameObject.SetActive(false);
            }
        }
    }
}
