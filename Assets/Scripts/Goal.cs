using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[System.Serializable]
public class BlankGoal
{
    public int numberCollected;
    public Sprite goalSprite;
    public string matchTag;
}
public class Goal : MonoBehaviour
{
    public static Goal Instance;
    public BlankGoal[] arrayGoals;


    public GameObject itemPrefabs;
    public Transform goalTransform;

    int countGoal=0;
    public GameObject winPanel;
    //public GameObject losePanel;
    private List<GameObject> itemList=new List<GameObject>();
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI levelText;
    public int coin=0;
    public int level = 1;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        winPanel.SetActive(false);
        CreateTarget();
        coinText.text=coin.ToString();
    }

    void CreateTarget()
    {
        for(int i = 0; i < arrayGoals.Length; i++)
        {
            GameObject itemTmp=Instantiate(itemPrefabs);
            itemTmp.transform.GetChild(0).GetComponent<Image>().sprite = arrayGoals[i].goalSprite;
            itemTmp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = arrayGoals[i].numberCollected.ToString();
            itemTmp.transform.SetParent(goalTransform);
            itemTmp.transform.localScale = Vector3.one; 
            itemTmp.transform.localPosition = Vector3.zero;
            itemList.Add(itemTmp);
        }
    }
 
    public void UpdateText()
    {
        countGoal = 0;
        for (int i = 0; i < arrayGoals.Length; i++)
        {
            if (arrayGoals[i].numberCollected > 0)
            {
                itemList[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = arrayGoals[i].numberCollected.ToString();
               
            }
            else
            {
                countGoal++;
                itemList[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "V";
                itemList[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.green;
            }
            
      
        }

        if (countGoal == arrayGoals.Length)
        {
            winPanel.SetActive(true);
        }
    }

    public void Get()
    {
        coinText.text = coin.ToString();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
   
}
