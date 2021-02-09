using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color emptyColor;
    public Color whiteColor;
    public Color resourceColor;
    public Color halfResourceColor;
    public Color quaterResourceColor;

    GameObject buildManager;
    
    
    public int resourceAmount = 0;
    //int times;







    private Renderer rend;

    public enum ResourceValue
    {
        None = 0,
        white = 1,
        resource = 16,
        halfresource = 8,
        quaterresource = 4,
    }

    public ResourceValue type;





    private void Start()
    {
        //times = Random.Range(3, 8);
        rend = GetComponent<Renderer>();
        buildManager = GameObject.FindGameObjectWithTag("BuildManager");
        //UpdateNode();
    }





    public void UpdateNode()
    {
        switch (type)
        {
            case ResourceValue.None:
                rend.material.color = emptyColor;
                break;

            case ResourceValue.white:
                rend.material.color = whiteColor;
                break;
            case ResourceValue.resource:
                rend.material.color = resourceColor;
                break;
            case ResourceValue.halfresource:
                rend.material.color = halfResourceColor;
                break;
            case ResourceValue.quaterresource:
                rend.material.color = quaterResourceColor;
                break;
        }
            
    }

    




    private void OnMouseDown()
    {
        int posInfo = 0;
        //Debug.Log("times: ");
        //Debug.Log(times);

        foreach (KeyValuePair<int, GameObject> item in buildManager.GetComponent<BuildManager>().tilesDict)
        {
            if (item.Value.transform == this.transform)
            {
                posInfo = item.Key;
            }
        }
        Debug.Log(posInfo.ToString());
        int PosY = Mathf.FloorToInt(posInfo / 32);

        int PosX = posInfo % 32;

        //int PosX = (int)Input.mousePosition.x;
        //int PosY = (int)Input.mousePosition.y;
        //Debug.Log(PosX);
        //Debug.Log(PosY);

        if (buildManager.GetComponent<BuildManager>().magnifyingGlass1 == true)
        {
            buildManager.GetComponent<BuildManager>().click--;
            

            if (buildManager.GetComponent<BuildManager>().click >= 0)
            {
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        if (PosY + y <= 31 && PosY + y >= 0 && PosX + x <= 31 && PosX + x >= 0)
                        {
                            buildManager.GetComponent<BuildManager>().tilesDict[((PosY + y) * 32 + PosX + x)].GetComponent<Node>().UpdateNode();
                        }

                    }
                }
            }
            else
            {
                Debug.Log("Can't Scan!!!");
            }
                

                
            
            

        }

        else
        {
            buildManager.GetComponent<BuildManager>().excavate--;
            if (buildManager.GetComponent<BuildManager>().excavate >= 0)
            {
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        if (PosY + y <= 31 && PosY + y >= 0 && PosX + x <= 31 && PosX + x >= 0)
                        {
                            //buildManager.GetComponent<BuildManager>().tilesDict[((PosY + y) * 32 + PosX + x)].GetComponent<Node>().UpdateNode();
                            buildManager.GetComponent<BuildManager>().resourceAmount += (int)buildManager.GetComponent<BuildManager>().tilesDict[((PosY + y) * 32 + PosX + x)].GetComponent<Node>().type * 5;
                            buildManager.GetComponent<BuildManager>().Down(buildManager.GetComponent<BuildManager>().tilesDict[((PosY + y) * 32 + PosX + x)]);
                            buildManager.GetComponent<BuildManager>().tilesDict[((PosY + y) * 32 + PosX + x)].GetComponent<Node>().UpdateNode();
                        }

                    }
                }

                buildManager.GetComponent<BuildManager>().tilesDict[((PosY) * 32 + PosX)].GetComponent<Node>().type = ResourceValue.None;
                buildManager.GetComponent<BuildManager>().tilesDict[((PosY) * 32 + PosX)].GetComponent<Node>().UpdateNode();
            }




            


        }

        
        
        
        
        
        //int posInfo = 0;
        //foreach (KeyValuePair<int, GameObject> item in buildManager.GetComponent<BuildManager>().tilesDict)
        //{
        //    if (item.Value.transform == this.transform)
        //    {
        //        posInfo = item.Key;
        //    }
        //}
        //Debug.Log(posInfo.ToString());
        //int PosY = Mathf.FloorToInt(posInfo / 32);

        //int PosX = posInfo % 32;  



        
        //for (int x = -1; x <= 1; x++)
        //{
        //    for (int y = -1; y <= 1; y++)
        //    {
        //       if(PosY+y<=31&&PosY+y>=0&& PosX + x <= 31 && PosX + x >= 0)
        //        {
        //            buildManager.GetComponent<BuildManager>().tilesDict[((PosY + y) * 32 + PosX + x)].GetComponent<Node>().UpdateNode();
        //        }
                


        //    }
        //}


        //RaycastHit hitInfo;
        //Ray ray;
        //ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //if (Physics.Raycast(ray, out hitInfo))
        //{
        //    Debug.Log("aaaa");
        //    if (sourcesDict.ContainsValue(hitInfo.transform.gameObject))
        //    {
        //        hitInfo.transform.GetComponent<Node>().UpdateNode();

        //    }




        //    //hit.transform.gameObject.GetComponent<SpriteRenderer>().color = scanColor;

        //}
    }





}
