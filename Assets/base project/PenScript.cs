using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public Material currentMat;
    public GameObject paint;
    
        // Update is called once per frame
        void Update()
    {
        if (Input.GetMouseButton(0))
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.tag=="modeldraw")
                {
                    var blob = Instantiate(paint, new Vector3(hit.point.x, hit.point.y, hit.point.z),transform.rotation);
                    blob.GetComponent<MeshRenderer>().material = currentMat;
                    blob.transform.parent = GameObject.Find(hit.collider.name).transform;
                     

                }
                if(hit.collider.tag=="board")
                {
                    var blob = Instantiate(paint, new Vector3(hit.point.x, hit.point.y, hit.point.z), transform.rotation);
                    blob.GetComponent<MeshRenderer>().material = currentMat;
                    blob.transform.parent = GameObject.Find(hit.collider.name).transform;
                  //  blob.tag = "boardpaint";
                }
                    
            }
        }
    }
}
