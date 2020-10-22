using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public static BoxSpawner boxSpawner;
    public List<BoxProfil> boxprofil;
    public Transform startpoint;
    public Vector3 sonrskikonum;


    private void Awake()
    {
        CreateBox();
    }
    public void CreateBox()
    {
        Vector3 pos = startpoint.transform.position;
        foreach (var item in boxprofil)
        {
            for (int i = 0; i < item.count; i++)
            {
                GameObject olusan = Instantiate(item.box, pos, Quaternion.identity);
                olusan.transform.SetParent(GameManager.instance.boxparent);
                pos.x += 2f;
            }
            pos.y -= 0.75f;
            pos.x = startpoint.position.x;
        }
    }
    

       




    
    [System.Serializable]
    public class BoxProfil
    {
        public BoxType boxtype;
        public int count;
        public GameObject box;
    }

    public enum BoxType
    {
        kırmızı=0,mavi=1,sarı=2,yeşil=3,gri=4,mor=5
    }
}
