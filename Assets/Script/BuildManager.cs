using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public GameObject NodePrefab;

    

    public Sprite shovel;
    public Sprite magnifyingGlass;

    bool shovel1 = false;
    public bool magnifyingGlass1 = true;

    public Button button;
    public Text text;
    public int resourceAmount;
    public int click = 6;
    public int excavate = 3;
    public Text clickNum;
    public Text excavateNum;
    
    


    //public Dictionary<int[,], GameObject> tilesDict = new Dictionary<int[,], GameObject>();
    public Dictionary<int, GameObject> tilesDict = new Dictionary<int, GameObject>();
    //public Dictionary<int, GameObject> sourcesDict = new Dictionary<int, GameObject>();

    //public Dictionary<int, Dictionary<int, GameObject>> tilesDictDestroy = new Dictionary<int, Dictionary<int, GameObject>>();




    private void Start()
    {
        
        CreateNode();
        DestroyNode();



    }

    private void Update()
    {
        text.text = resourceAmount.ToString();
        if(click >= 0)
        {
            clickNum.text = click.ToString();
        }
        else
        {
            clickNum.text = "No more Time";
        }

        if (excavate >= 0)
        {
            excavateNum.text = excavate.ToString();
        }
        else
        {
            excavateNum.text = "No more Time";
        }
        
    }




    //private void OnMouseDown()
    //{
    //    RaycastHit hit;
    //    Ray ray;
    //    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    if (Physics.Raycast(ray, out hit))
    //    {
    //        hit.transform.gameObject.GetComponent<SpriteRenderer>().color = scanColor;
    //        foreach (Dictionary item in tilesDict.Values)
    //        {

    //        }
    //    }
    //}


    void CreateNode()
    {
        //tilesDict = new Dictionary<int, GameObject>();
        for (int x = 0; x < 32; x++)
        {
            
            for (int y = 0; y < 32; y++)
            {
                Vector2 CreateNodePos = new Vector2(x, y);
                GameObject Nodes = GameObject.Instantiate(NodePrefab, CreateNodePos, NodePrefab.transform.rotation);
                tilesDict.Add(y*32+x, Nodes);
                Nodes.GetComponent<Node>().type = Node.ResourceValue.white;

            }
            
        }
    }

    void DestroyNode()
    {
        int destroyPosX = Random.Range(2, 30);
        //int destroyPosX = 2;
        int destroyPosY = Random.Range(2, 30);
        //int destroyPosY = 2;
        Debug.Log(destroyPosY);
        Debug.Log(destroyPosX);
        Debug.Log(tilesDict.Count);
        
        for (int x = -2; x <= 2; x++)
        {
            for (int y = -2; y <= 2; y++)
            {
                
                Destroy(tilesDict[(destroyPosY+y)*32+destroyPosX+x]);
                tilesDict.Remove((destroyPosY + y) * 32 + destroyPosX + x);
            }
        }

        CreateResource(destroyPosX, destroyPosY);
    }

    void CreateResource(int destroyPosX, int destroyPosY)
    {
        for (int x = -2; x <= 2; x++)
        {

            for (int y = -2; y <= 2; y++)
            {
                int resourcePosy = destroyPosY;
                //GameObject Resource = Instantiate(tilesDict[destroyPosX + x - 2][destroyPosY + y - 2]);
                GameObject Resource = GameObject.Instantiate(NodePrefab, new Vector2(destroyPosX +x, destroyPosY + y), NodePrefab.transform.rotation);
                tilesDict.Add((destroyPosY+y) * 32 + destroyPosX + x, Resource);
                Resource.GetComponent<Node>().type = Node.ResourceValue.resource;
                if (x == -2|| x == 2 || y == -2 || y == 2)
                {
                    Resource.GetComponent<Node>().type = Node.ResourceValue.quaterresource;
                }

                else if (x == -1 || x == 1 || y == -1 || y == 1)
                {
                    Resource.GetComponent<Node>().type = Node.ResourceValue.halfresource;
                }

            }
        }

    }

    public void ChangeButton()
    {
        
        if(magnifyingGlass1 == true)
        {
            shovel1 = true;
            magnifyingGlass1 = false;
            button.image.sprite = shovel;
        }
        else
        {
            shovel1 = false;
            magnifyingGlass1 = true;
            button.image.sprite = magnifyingGlass;
        }
        
    }

    public void Down(GameObject node)
    {
        switch (node.GetComponent<Node>().type)
        {
            case Node.ResourceValue.white:
                node.GetComponent<Node>().type = Node.ResourceValue.None;
                break;
            case Node.ResourceValue.resource:
                node.GetComponent<Node>().type = Node.ResourceValue.halfresource;
                break;
            case Node.ResourceValue.halfresource:
                node.GetComponent<Node>().type = Node.ResourceValue.quaterresource;
                break;
            case Node.ResourceValue.quaterresource:
                node.GetComponent<Node>().type = Node.ResourceValue.white;
                break;
            default:
                break;
        }
    }



}
