using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
public class RandomChestItem : MonoBehaviour {

    GameObject playerObj;
    bool gaveItem = false;
    public Text text;

    public bool canContainCoins = true;
    public bool canContainItems = true;
    public bool canContainHealth = true;
    public bool canContainCharges = true;
    public AudioSource sound;
    public ParticleSystem particles;
    public GameObject sprite;

    bool opened = true;
    IEnumerator ChestAnimation()
    {

        yield return new WaitForSeconds(.1f);
        ScreenShake.instance.shake(.3f);
        yield return new WaitForSeconds(.1f);
        ScreenShake.instance.shake(.2f);
        yield return new WaitForSeconds(.1f);
        ScreenShake.instance.shake(.1f);
        yield return new WaitForSeconds(2);
        sprite.SetActive(false);
        particles.Stop();
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!opened)
            return;

        if (other.tag == "Player")
        {
            playerObj = other.gameObject;
            OpenChest();
            StartCoroutine("ChestAnimation");
        }
    }

    void OpenChest()
    {
        opened = false;
        ScreenShake.instance.shake(.5f);
        sound.Play();
        particles.Play();
        string itemDisplayString = "";
        while (!gaveItem)
        {
            int randomInt = Random.Range(0, 4);
            switch (randomInt)
            {
                case 0:
                    if(canContainItems)
                    {
                        itemDisplayString = GetItem();
                    }
                    break;
                case 1:
                    if(canContainHealth)
                    {
                        itemDisplayString = GetHealth();
                    }
                    break;
                case 2:
                    if(canContainCharges)
                    {
                        itemDisplayString = GetCharges();
                    }
                    break;
                case 3:
                    if(canContainCoins)
                    {
                        itemDisplayString = GetGold();
                    }
                    break;
                case 4:
                    itemDisplayString = GetItem();
                    break;
                default:
                    itemDisplayString = "Oh no an empty chest";
                    break;
            }
            Debug.Log("itemDisplayString " + itemDisplayString);
            DisplayString(itemDisplayString);
        }
    }

    string GetItem()
    {
        gaveItem = true;
        return PlayerInventory.instance.AddRandomComponent();
    }

    string GetHealth()
    {
        int amount = playerObj.GetComponent<Health>().Heal(10);
        if(amount > 0)
        {
            gaveItem = true;
            return amount.ToString() + " health";
        }
        return "";
    }

    string GetCharges()
    {
        return "";
    }

    string GetGold()
    {
        gaveItem = true;
        const int minGold = 10;
        const int maxGold = 40;
        int amount = Random.Range(minGold, maxGold) + Random.Range(minGold, maxGold);
        CoinHolder holder =playerObj.GetComponent<CoinHolder>();
        for(int i = 0; i < amount; i++)
        {
            holder.AddCoin();
        }
        return amount.ToString() + " gold";
    }

    void DisplayString(string s)
    {
        string display = "You got " + s + "!";
        text.text = display;
    }
}
