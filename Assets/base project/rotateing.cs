using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean;
public class rotateing : MonoBehaviour
{
    public bool sflag;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
          sflag = GetComponent<Lean.Touch.LeanSelectable>().IsSelected;
if(sflag)
        {
            if (Input.GetKey(KeyCode.R))
            {
                transform.Rotate(0f, 1f, 0.0f, Space.Self);
            }
        }

    }
}
