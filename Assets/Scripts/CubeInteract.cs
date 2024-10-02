using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeInteract : MonoBehaviour
{
    public bool isMove ;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag(this.gameObject.tag) == true && isMove==false)
        {
            Debug.Log(other.gameObject.name);

            StartCoroutine(Destroytrigger(other.gameObject, this.gameObject));
        }
    }
    IEnumerator Destroytrigger(GameObject obj1, GameObject obj2)
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(obj1);
        Destroy(obj2);
    }
}
