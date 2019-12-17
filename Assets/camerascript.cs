using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerascript : MonoBehaviour
{
    public GameObject nodeObject;
    public UnityEngine.UI.InputField inputField;
    public UnityEngine.UI.InputField inputF;
    private bool turnOnSpace;
    // Start is called before the first frame update
    void Start()
    {
        LeftX = 2.0f;
        x = 4;
        turnOnSpace = true;
    }
    private float x;
    private float difference = 2.0f;
    private float LeftX, RightX;
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Return)&&inputField.gameObject.activeInHierarchy)
        {
            CreateTape();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
            Dismiss();
        x = this.transform.position.x;
        var y = this.transform.position.y;
        var z = this.transform.position.z;
        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //    LeftCamera();
        //else if (Input.GetKeyDown(KeyCode.RightArrow))
        //    RightCamera();
        RaycastHit hitObj;
        var vect1 = this.transform.position;
        var vect2 = new Vector3(this.transform.position.x, -1.182054f, -4.211586f);
        if (Physics.Raycast(vect1, vect2 - vect1, out hitObj))
        {
            if(Input.GetKeyDown(KeyCode.Space)&&turnOnSpace)
            {
                var c1=hitObj.transform.Find("GameObject").GetComponent<TextMesh>().text[0];
                List<char> list = GetToRead(c1);
                if(list!=null && list.Count==3)
                {
                    state = list[0];
                    hitObj.transform.Find("GameObject").GetComponent<TextMesh>().text = list[1].ToString();
                    char H = list[2];
                    if (H == 'R')
                        RightCamera();
                    else if (H == 'L')
                        LeftCamera();
                    if (state == 'I')
                    {
                        print("Accepted!");
                        turnOnSpace = false;
                    }
                }
                else
                {
                    print("Rejected!");
                    turnOnSpace = false;
                }
            }
        }
        this.gameObject.transform.position = new Vector3(x, y, z);
        inputF.text = "Current State = "+state;
    }
  
    private char state = 'A';
    private List<char> GetToRead(char c1)
    {
        char s = '\0';
        char c2 = '\0';
        char H = '\0';
        if (state == 'A')
        {
            if (c1 == '0')
            {
                s = 'B';
                c2 = ' ';
                H = 'R';
            }
            else if (c1 == '1')
            {
                s = 'H';
                c2 = ' ';
                H = 'R';
            }
            else if (c1 == ' ')
            {
                s = 'I';
                c2 = ' ';
                H = 'S';
            }
        }
        else if (state == 'B')
        {
            if (c1 == '0')
            {
                s = 'C';
                c2 = '0';
                H = 'R';
            }
            else if (c1 == '1')
            {
                s = 'C';
                c2 = '1';
                H = 'R';
            }
            else if (c1 == ' ')
            {
                s = 'I';
                c2 = ' ';
                H = 'S';
            }
        }
        else if (state == 'C')
        {
            if (c1 == '0')
            {
                s = 'C';
                c2 = '0';
                H = 'R';
            }
            else if (c1 == '1')
            {
                s = 'C';
                c2 = '1';
                H = 'R';
            }
            else if (c1 == ' ')
            {
                s = 'D';
                c2 = ' ';
                H = 'L';
            }
        }
        else if (state == 'D')
        {
            if (c1 == '0')
            {
                s = 'E';
                c2 = ' ';
                H = 'L';
            }
        }
        else if (state == 'E')
        {
            if (c1 == '0')
            {
                s = 'E';
                c2 = '0';
                H = 'L';
            }
            else if (c1 == '1')
            {
                s = 'E';
                c2 = '1';
                H = 'L';
            }
            else if (c1 == ' ')
            {
                s = 'A';
                c2 = ' ';
                H = 'R';
            }
        }
        else if (state == 'F')
        {
            if (c1 == '1')
            {
                s = 'E';
                c2 = ' ';
                H = 'L';
            }
        }
        else if (state == 'G')
        {
            if (c1 == '0')
            {
                s = 'G';
                c2 = '0';
                H = 'R';
            }
            else if (c1 == '1')
            {
                s = 'G';
                c2 = '1';
                H = 'R';
            }
            else if (c1 == ' ')
            {
                s = 'F';
                c2 = ' ';
                H = 'L';
            }
        }
        else if (state == 'H')
        {
            if (c1 == '0')
            {
                s = 'G';
                c2 = '0';
                H = 'R';
            }
            else if (c1 == '1')
            {
                s = 'G';
                c2 = '1';
                H = 'R';
            }
            else if (c1 == ' ')
            {
                s = 'I';
                c2 = ' ';
                H = 'S';
            }
        }
        if (s != '\0' && c2 != '\0' && H != '\0')
        {
            return new List<char>() { s, c2, H };
        }
        return null;

    }
    public void LeftCamera()
    {
        x = x - difference;
        if (x == LeftX)
        {
            LeftX -= difference;
            CreateObject(LeftX," ");
        }
    }
    public void RightCamera()
    {
        x = x + difference;
        if (x == RightX)
        {
            RightX += difference;
            CreateObject(RightX, " ");
        }
    }
    public void CreateTape()
    {
        string str = inputField.text;
        CreateObject(LeftX, " ");
        x = LeftX;
        for(int i=0;i<str.Length;i++)
        {
            x = x + difference;
            CreateObject(x,str[i].ToString());
        }
        RightX = x + difference;
        CreateObject(RightX," ");
        x = LeftX + difference;
        inputField.gameObject.SetActive(false);
    }
    public void CreateObject(float x,string text)
    {
        nodeObject.transform.position = new Vector3(x, nodeObject.transform.position.y, nodeObject.transform.position.z);
        nodeObject.transform.Find("GameObject").GetComponent<TextMesh>().text = text;
        Instantiate(nodeObject);
    }
    public void Dismiss()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}

 






























