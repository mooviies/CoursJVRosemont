using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastUtil
{
    public static bool DebugMode { get; set; }
    public static bool VerboseMode { get; set; }
    public static bool TesterCollision2D(Vector2 centre, Vector2 direction, float distance, LayerMask layer, float ajustement = 0.2f)
    {
        float distanceTotal = distance + ajustement;
        
        RaycastHit2D raycastHit = Physics2D.Raycast(centre, direction, 
            distanceTotal, layer);

        if (DebugMode)
        {
            Color rayColor;
            if (raycastHit.collider != null)
            {
                rayColor = Color.green;
            }
            else
            {
                rayColor = Color.red;
            }

            Debug.DrawRay(centre, direction * distanceTotal, rayColor);
            
            if(VerboseMode)
                Debug.Log(raycastHit.collider);
        }

        return raycastHit.collider != null;
    }
}
