using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof (CircleCollider2D))]
public class Shop : InRangeActive {
	public Text priceText;
	public Text itemText;
	public bool canHaveComponents = false;
	public bool canHaveHealth = true;
	public bool canHaveCharges = true;

	public float componentPrice;
	public float fullHealPrice;
	public float fullChargePrice;

	delegate void GiveItem();
	GiveItem giveItem;

	float price;
	
	bool roomSet = false;
    public AudioSource notEnoughGold;
    public AudioSource purchase;
	void Update()
	{

		if(transform.hasChanged && transform.parent != null && !roomSet)
		{
			roomSet = true;
			transform.parent.gameObject.GetComponent<Room>().RoomEntered += PickItem;
		}
		if (playerInside && Input.GetKeyDown(KeyCode.E))
		{
			CoinHolder holder = player.gameObject.GetComponent<CoinHolder>();
			if(holder.GetCoinCount() >= price)
			{
				holder.RemoveCoin((int)price);
                purchase.Play();
                giveItem();
				gameObject.SetActive(false);
			}
            else
            {
                notEnoughGold.Play();
            }

		}
	}

	void Start () {

	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            CoinHolder holder = player.gameObject.GetComponent<CoinHolder>();
            outLineColor = holder.GetCoinCount() >= price ? new Color(0, 1, 0) : new Color(1, 0, 0);
            SetOutline(other);
        }
    }

    void PickItem(Room r)
	{
		if( canHaveComponents || canHaveHealth || canHaveCharges)
		{
			bool haveItem = false;
			while(!haveItem)
			{
				int itemType = Random.Range (0,3);
				switch(itemType)
                {
                    case 0:
                        if (!canHaveComponents)
                        {
                            continue;
                        }
                        price = componentPrice;
                        giveItem = GiveSpellComponent;
                        spellComponent = PlayerInventory.instance.AddComponentToStore();
                        ISpellComponent component = ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent(spellComponent, PlayerInventory.instance.componentsInStore[spellComponent]));
                        itemText.text = component.GetTitle() + "\n" + component.GetToolTip();
                        haveItem = true;
                        break;
                    case 1:
                        if (!canHaveHealth)
                        {
                            continue;
                        }
                        price = fullHealPrice;
                        giveItem = Heal;
                        itemText.text = "Full Healh";
                        haveItem = true;
                        break;
                    case 2:
                        if (!canHaveCharges)
                        {
                            continue;
                        }
                        price = fullChargePrice;
                        giveItem = RechargeSpells;
                        itemText.text = "Recharge Spells";
                        haveItem = true;
                        break;
                    default:
					break;

				}
			}
			priceText.text = price.ToString();
		}
	}

	string spellComponent;
	void GiveSpellComponent()
	{
		PlayerInventory.instance.AddComponent (spellComponent, true);
	}

    void RechargeSpells()
    {
        Shoot playerShoot = player.gameObject.transform.GetChild(1).GetChild(0).GetComponent<Shoot>();
        playerShoot.RechargeSpells();
        return;
    }

	void Heal()
	{
		player.GetComponent<Health> ().Heal (1000);
	}
}
