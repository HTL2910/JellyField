using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeInteract : MonoBehaviour
{
    public bool isMove ;
    private List<GameObject> list = new List<GameObject>();
    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag(this.gameObject.tag) == true && isMove == false && other.gameObject.GetComponent<CubeInteract>().isMove == false)
        {
            GameObject parent;
            GameObject otherParent;
            if (other.transform.parent != null)
            {
                otherParent = other.transform.parent.gameObject;
                if (otherParent.transform.childCount == 2)
                {
                    Vector3 center;
                    for(int i = 0; i < otherParent.transform.childCount; i++)
                    {
                        if (otherParent.transform.GetChild(i).gameObject != other.gameObject)
                        {
                            GameObject tmpObj = otherParent.transform.GetChild(i).gameObject;
                            center =GetCenterPosition(other.gameObject, tmpObj);
                            tmpObj.transform.position=center;
                            if (otherParent.transform.childCount == 1)
                            {
                                StartCoroutine(Destroytrigger(otherParent.gameObject, this.gameObject));

                            }
                            
                            if (tmpObj.transform.localScale.x <0.9f)
                            {
                                Vector3 newScale = tmpObj.transform.localScale;
                                newScale.x *= 2; // Tăng kích thước x gấp đôi
                                tmpObj.transform.localScale = newScale;
                            }
                            else if (tmpObj.transform.localScale.y <0.9f)
                            {
                                Vector3 newScale = tmpObj.transform.localScale;
                                newScale.y *= 2; // Tăng kích thước y gấp đôi
                                tmpObj.transform.localScale = newScale;
                            }
                        }
                    }
                }
                
            }
            else if(gameObject.transform.parent!=null)
            {
                parent=gameObject.transform.parent.gameObject;
                Vector3 center;
                for (int i = 0; i < parent.transform.childCount; i++)
                {
                    if (parent.transform.GetChild(i).gameObject != gameObject)
                    {
                        GameObject tmpObj = parent.transform.GetChild(i).gameObject;
                        center = GetCenterPosition(gameObject, tmpObj);
                        tmpObj.transform.position = center;
                        if (parent.transform.childCount == 1)
                        {
                            StartCoroutine(Destroytrigger(other.gameObject, parent.gameObject));

                        }
                     
                        if (tmpObj.transform.localScale.x <0.9f)
                        {
                            Vector3 newScale = tmpObj.transform.localScale;
                            newScale.x *= 2; // Tăng kích thước x gấp đôi
                            tmpObj.transform.localScale = newScale;
                        }
                        else if (tmpObj.transform.localScale.y < 0.9f)
                        {
                            Vector3 newScale = tmpObj.transform.localScale;
                            newScale.y *= 2; // Tăng kích thước y gấp đôi
                            tmpObj.transform.localScale = newScale;
                        }
                    }
                }
            }
            if(other.gameObject!=null && this.gameObject != null)
            {
                StartCoroutine(Destroytrigger(other.gameObject, this.gameObject));

            }


        }
    }
    IEnumerator Destroytrigger(GameObject obj1, GameObject obj2)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(obj1);
        Destroy(obj2);
    }
    private Vector3 GetCenterPosition(GameObject obj1, GameObject obj2)
    {
        Vector3 position1 = obj1.transform.position;
        Vector3 position2 = obj2.transform.position;

        Vector3 centerPosition = (position1 + position2) / 2.0f;

        return centerPosition;
    }

}
