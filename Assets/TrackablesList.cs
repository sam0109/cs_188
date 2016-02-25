using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Vuforia;

public class TrackablesList : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Get the StateManager
        StateManager sm = TrackerManager.Instance.GetStateManager();

        // Query the StateManager to retrieve the list of
        // currently 'active' trackables 
        //(i.e. the ones currently being tracked by Vuforia)
        IEnumerable<TrackableBehaviour> activeTrackables = sm.GetActiveTrackableBehaviours();

        foreach (TrackableBehaviour tb in activeTrackables)
        {
            
        }
    }
}