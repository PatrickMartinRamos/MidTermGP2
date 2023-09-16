using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CircleScript : MonoBehaviour
{
    public float sppeed;
    // Start is called before the first frame update
    void Start()
    {
        
        transform.DORotate(new Vector3(0f,0f,360f), sppeed, RotateMode.LocalAxisAdd)
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear);
    }

    // Update is called once per frame
    void Update()
    {
  
    }

}
