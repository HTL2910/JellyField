using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeInteract : MonoBehaviour
{
    public bool isMove ;
    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag(this.gameObject.tag) == true && isMove==false && other.gameObject.GetComponent<CubeInteract>().isMove==false)
        {

            StartCoroutine(Destroytrigger(other.gameObject, this.gameObject));
        }
    }
    IEnumerator Destroytrigger(GameObject obj1, GameObject obj2)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(obj1);
        Destroy(obj2);
    }
  
}
