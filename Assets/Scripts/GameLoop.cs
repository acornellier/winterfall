using System.Collections;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    public bool loopShouldEnd;

    IEnumerator Run()
    {
        while (!loopShouldEnd)
        {
            // spawn enemies
            // spawn towers
            // move enemies
            // tick towers
            // apply effects
            // damage enemies
            // remove enemies
            // remove towers

            yield return null;
        }
    }
}