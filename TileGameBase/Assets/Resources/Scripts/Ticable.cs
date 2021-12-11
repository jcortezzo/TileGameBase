using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ticable : MonoBehaviour
{
    public bool CanTic { get; set; }

    /// <summary>
    /// Tics self.
    /// </summary>
    /// <returns>Whether a Tic went through or not.</returns>
    public virtual bool Tic()
    {
        // Only allow each Ticable to Tic once per turn by default
        if (!CanTic)
        {
            return false;
        }
        else
        {
            CanTic = false;
            return true;
        }
    }
}
