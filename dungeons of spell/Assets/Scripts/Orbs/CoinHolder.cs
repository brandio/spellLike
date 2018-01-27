using UnityEngine;
using System.Collections;

public class CoinHolder : MonoBehaviour {

    int coins = 0;

    public delegate void GainCoinHandler(CoinHolder coinHolder);
    public event GainCoinHandler GainedCoin;

    public int GetCoinCount ()
    {
        return coins;
    }

    public void AddCoin()
    {
        coins++;
        if(GainedCoin != null)
        {
            GainedCoin(this);
        }
    }

	public void RemoveCoin(int removeAmount)
	{
		coins = coins - removeAmount;
		if (coins < 0) {
			coins = 0;

		}
	}
}
