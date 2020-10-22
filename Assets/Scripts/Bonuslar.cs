using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonuslar : MonoBehaviour
{
    public static Bonuslar instance;
    public List<BonusProfil> bonuslar;

    private void Awake()
    {
        instance = this;
    }


    public BonusType BonusÜret(BonusType type,Vector3 konum)
    {

       BonusProfil bulunanbonus= bonuslar.Find(x => x.bonusID == type);
       GameObject bonusuoluştur = Instantiate(bulunanbonus.bonusprefab, konum, Quaternion.identity);
        return type;
        
    }
    


    [System.Serializable]

    public class BonusProfil
    {
        public string bonusname;
        public GameObject bonusprefab;
        public BonusType bonusID;
       
    }
    public enum BonusType
    {
        none = 0, ateş = 1, padddingbüyüme = 2, direktyanma = 3
    }




}
