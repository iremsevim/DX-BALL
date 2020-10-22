using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddingController : MonoBehaviour
{
    public static PaddingController instance;
    public float MaxValue;
    public float minValue;
    public float paddingspeed;
    public Transform ballpoint;
    public List<Transform> atespoints;
    public GameObject atesprefab;
    public float ateshızı;

   

    private void Awake()
    {
        instance = this;
    }


    public void Update()
    {
        PaddingMovement();
    }
    public void PaddingMovement()
    {

        float rightleft = Input.GetAxis("Horizontal");

        transform.position += new Vector3(rightleft*paddingspeed*Time.deltaTime, 0, 0);
        if(transform.position.x>=MaxValue)
        {
            transform.position = new Vector3(MaxValue, transform.position.y, transform.position.z);
        }
        else if(transform.position.x<=minValue)
        {
            transform.position = new Vector3(minValue, transform.position.y, transform.position.z);
        }
           
    }
    public IEnumerator AtesSpawn()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.2f);
            for (int j = 0; j <atespoints.Count; j++)
            {
                GameObject olusan = Instantiate(atesprefab.gameObject, atespoints[j].transform.position, Quaternion.identity);
                olusan.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1 * ateshızı);
            }
        }
       
    }
       
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="bonus")
        {

            if (BallController.instance.gelenbonus == Bonuslar.BonusType.ateş)
            {
                BallController.instance.TopYanma();

            }
            else if (BallController.instance.gelenbonus == Bonuslar.BonusType.direktyanma)
            {
                  BallController.instance.Yanma();


            }
            else if(BallController.instance.gelenbonus==Bonuslar.BonusType.padddingbüyüme)
            {
                transform.localScale += new Vector3(0.5f, 0, 0);
                StartCoroutine(AtesSpawn());
                
            }
            Destroy(collision.gameObject);
        }
        
    }
}
