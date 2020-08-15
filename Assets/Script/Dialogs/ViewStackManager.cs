using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class viewStackManagement
{
    private List<ViewObject> viewStack = new List<ViewObject>();

    public void pushToStack(ViewObject view)
    {
        removeNulls();
        if (this.getLastView() != view)
        {
            viewStack.Add(view);
        }
    }
    public ViewObject popFromStack()
    {
        removeNulls();
        if (viewStack.Count > 0)
        {
            var last = viewStack.Last();
            viewStack.Remove(last);
            return last;
        }
        return null;
    }
    public ViewObject getLastView()
    {
        removeNulls();
        if (viewStack.Count > 0)
        {
            return viewStack.Last();
        }
        return null;
    }
    public void removeView(ViewObject view){
        if(viewStack.Contains(view)){
            viewStack.Remove(view);
        }
    }

    private void removeNulls()
    {
        for (int i = 0; i < viewStack.Count; i++)
        {
            if (viewStack[i] == null)
            {
                viewStack.RemoveAt(i);
            }
        }
    }
}
