using UnityEngine;

public class BackgroundControl : MonoBehaviour
{
    public int backgroundNum;
    public Sprite[] Layer_Sprites;
    private GameObject[] Layer_Object = new GameObject[6];
    private int max_backgroundNum = 19;

    public void Initiailize()
    {
        for (int i = 0; i < Layer_Object.Length; i++)
        {
            Layer_Object[i] = GetComponent<ParallaxController>().Layer_Objects[i];
        }        
        ChangeSprite();
    }

    public void ChangeSprite()
    {
        backgroundNum = Manager.Stage.Chapter % max_backgroundNum;
        Layer_Object[0].GetComponent<SpriteRenderer>().sprite = Layer_Sprites[backgroundNum*6];
        for (int i = 1; i < Layer_Object.Length; i++){
            Sprite changeSprite = Layer_Sprites[backgroundNum*6 + i];
            Layer_Object[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = changeSprite;
            Layer_Object[i].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = changeSprite;
        }
    }
}
