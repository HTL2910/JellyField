using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeCubeChild : MonoBehaviour
{
    public List<GameObject> listCurrentChild;
    public List<GameObject> listNewChild;

    private void Start()
    {
        listCurrentChild = AddList();
    }

    private List<GameObject> AddList()
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            list.Add(transform.GetChild(i).gameObject);
        }
        return list;
    }

    private void Update()
    {
        listNewChild = AddList();
        ResizeChild();
        // Cập nhật lại listCurrentChild nếu có sự thay đổi
        if (listCurrentChild.Count != listNewChild.Count)
        {
            listCurrentChild = new List<GameObject>(listNewChild);
        }
    }

    private void ResizeChild()
    {
        int count = transform.childCount;
        if (count < listCurrentChild.Count)
        {
            int index = CheckIndex();
            if (index >= 0 && index < listCurrentChild.Count)
            {
                // Lấy child tương ứng và kiểm tra index
                Transform childToResize = null;
                if (index % 2 == 0)
                {
                    childToResize = transform.GetChild(index + 1);
                }
                else
                {
                    childToResize = transform.GetChild(index - 1);

                }
               
                if (childToResize != null) // Nếu child không null
                {
                    Vector3 newScale = childToResize.localScale;
                    newScale.y *= 2; // Tăng kích thước y gấp đôi
                    childToResize.localScale = newScale;
                }
                
            }
        }
    }

    private int CheckIndex()
    {
        for (int i = 0; i < listCurrentChild.Count; i++)
        {
            if (!listNewChild.Contains(listCurrentChild[i]))
            {
                return i;
            }
        }
        return -1;
    }
}
