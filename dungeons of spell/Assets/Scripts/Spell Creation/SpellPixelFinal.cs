using UnityEngine;
using System.Collections;

public class SpellPixelFinal  {
	public IInk m_ink;
	public Vector2 m_position;
    public float damage { get; set; }

	public SpellPixelFinal(IInk ink, Vector2 position)
	{
        if(position == null || ink == null)
        {
            Debug.LogError("Failed To Make Pixel with ink and position " + ink + " " + position);
        }
		m_position = position;
		m_ink = ink;
	}
}
