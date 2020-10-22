using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform boxparent;
    public RunTime runtime;

    private void Awake()
    {
        instance = this;
       ;
    }
    public void GameComplated()
    {
        if(boxparent.childCount <= 0)
        {
            Debug.Log("Game Complated");
        }
    }

    [System.Serializable]
    public class RunTime
    {
        public int cansayısı;
        public GameObject topyanmapartkül;
        public GameObject patlama;
       
    }
        

}
