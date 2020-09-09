using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusSwitcher : MonoBehaviour
{
    public string FocusedLayer = "Focused";

    private List<GameObject> currentlyFocused = new List<GameObject>(5);
    private int previousLayer = 0;

    public void SetFocused(List<GameObject> obj)
    {
        // enables this camera and the postProcessingVolume which is the child
        gameObject.SetActive(true);

        // if something else was focused before reset it
        if (currentlyFocused.Count > 0 && !currentlyFocused.Contains(null))
        {
            for (int i = 0; i < currentlyFocused.Count; i++)
            {
                currentlyFocused[i].layer = previousLayer;
            }
        }

        // store and focus the new object
        for (int i = 0; i < obj.Count; i++)
        {
            if (!currentlyFocused.Contains(obj[i]))
                currentlyFocused.Add(obj[i]);
        }

        if (currentlyFocused.Count > 0 && !currentlyFocused.Contains(null))
        {
            for (int i = 0; i < currentlyFocused.Count; i++)
            {

                previousLayer = currentlyFocused[i].layer;
                currentlyFocused[i].gameObject.layer = LayerMask.NameToLayer(FocusedLayer);
            }
        }
        else
        {
            // if no object is focused disable the FocusCamera
            // and PostProcessingVolume for not wasting rendering resources
            gameObject.SetActive(false);
        }
    }

    // On disable make sure to reset the current object
    private void OnDisable()
    {
        if (currentlyFocused.Count > 0 && !currentlyFocused.Contains(null))
        {
            for (int i = 0; i < currentlyFocused.Count; i++)
            {
                currentlyFocused[i].layer = previousLayer;
            }
        }
        for (int i = 0; i < currentlyFocused.Count; i++)
        {
            currentlyFocused[i] = null;

        }
    }
}
