using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class BallController : MonoBehaviour
{
    public static BallController instance;
    public Rigidbody2D rb;
    public Vector3 lastspeed;
    public Bonuslar.BonusType gelenbonus;
    public bool Moving = false;
    public bool IsFire;
    public Canvas canvas;
    public Text scoretext;
    public int score = 0;
    
    
 

    
   
   

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
      
     

    }
    public void TopYanma()
    {
        IsFire = true;
        GameObject olusanpartikül = Instantiate(GameManager.instance.runtime.topyanmapartkül, transform.position, Quaternion.identity);
        olusanpartikül.transform.SetParent(transform);
        
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) &&!Moving)
        {
            rb.velocity = new Vector2(1, 3) * 2;
            Moving = true;
            transform.SetParent(null);
        }
        lastspeed = rb.velocity;
    }
    public void ScoreArtısı()
    {
        score++;
        scoretext.text = "Score :"+score.ToString();
    }
    public void ArtıBirÜret(GameObject creationpoint)
    {

      Text artıbir=  canvas.transform.GetChild(1).GetComponent<Text>();
        GameObject olusantext = Instantiate(artıbir.gameObject,creationpoint.transform.position, Quaternion.identity,canvas.transform);
      
        olusantext.SetActive(true);
      
        ArtıBirTası(olusantext.GetComponent<Text>());
    }
    public void ArtıBirTası(Text text)
    {
        text.GetComponent<RectTransform>().DOMove(scoretext.transform.position, 1f).OnComplete(()=>
        {
            Destroy(text.gameObject);
            ScoreArtısı();
        }
        );
        
     
    }
    public void Yanma()
    {
        GameManager.instance.runtime.cansayısı -= 1;
        GameManager.instance.runtime.cansayısı = Mathf.Max(GameManager.instance.runtime.cansayısı, 0);
        rb.velocity = Vector2.zero;
        transform.position = PaddingController.instance.ballpoint.position;
        transform.SetParent(PaddingController.instance.ballpoint);
        Moving = false;
    }
    public IEnumerator OrderedExplosion(List<Vector3> explosionboxpiont)
    {
        Debug.Log(explosionboxpiont.Count);
        foreach (var item in explosionboxpiont)
        {
            Debug.Log("Partikül oluştu");
            GameObject olusanpartikül = Instantiate(GameManager.instance.runtime.patlama,item, Quaternion.identity);
            Destroy(olusanpartikül, 1f);
        }

        yield return null;
         
    }


    public void OnCollisionEnter2D(Collision2D other)
    {
        
        var speed = lastspeed.magnitude;

        Vector3 direction = Vector3.Reflect(lastspeed.normalized, other.contacts[0].normal);

        rb.velocity = direction * Mathf.Max(speed, 0f);
        

        if(other.gameObject.tag=="box")
        {
          

            if(IsFire)
            {
                Collider2D[] hittedbox= Physics2D.OverlapCircleAll(other.contacts[0].point, 1.5f);
                List<Collider2D> yeni = new List<Collider2D>();
                for (int i = 0; i < hittedbox.Length; i++)
                {
                    if(hittedbox[i].transform.gameObject.tag=="box")
                    {
                        ArtıBirÜret(hittedbox[i].gameObject);
                            
                        yeni.Add(hittedbox[i]);
                    }
                }

                if (Random.Range(1, 100) > 50)
                {
                    gelenbonus = Bonuslar.instance.BonusÜret((Bonuslar.BonusType)Random.Range(1, 3), other.gameObject.transform.position);

                }

                List<Vector3> hitboxposition=  yeni.ConvertAll(x => x.transform.position);
               
                StartCoroutine(OrderedExplosion(hitboxposition));

                foreach (var item in yeni)
                {

                    Destroy(item.gameObject);
                }

                SFX.instance.PlaySound("ballhitexplosion");
            }
            else
            {
                SFX.instance.PlaySound("ballhit");
                ArtıBirÜret(other.gameObject);

                if (Random.Range(1, 100) > 50)
                {
                    gelenbonus = Bonuslar.instance.BonusÜret((Bonuslar.BonusType)Random.Range(1, 3), other.gameObject.transform.position);

                }
                this.GetComponent<AnimasyonScale>().PlayAnim();
                GameManager.instance.GameComplated();
                Destroy(other.gameObject);
            }
           


        }
        else if(other.gameObject.tag=="engel")

        {
            SFX.instance.PlaySound("ballhitwall");
        }
        else if(other.gameObject.tag=="Padding")
        {
            SFX.instance.PlaySound("ballhitwall");

        }
        else if(other.gameObject.tag=="zemin")
        {
            Yanma();

        }

    }
  
}
