using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Generic PriorityQueue
// Uses a min heap
// see https://visualstudiomagazine.com/Articles/2012/11/01/Priority-Queues-with-C.aspx?Page=2 for more info
public class PriorityQueue<T> where T : System.IComparable<T>, System.IEquatable<T>
{
	//Private Members
	//-------------------
	//-------------------
	List<T> m_Data;
	
	//Public functions
	//-------------------
	//-------------------
	public PriorityQueue()
	{
		m_Data = new List<T>();
	}
	
	//-------------------
	public int size()
	{
		return m_Data.Count;
	}
	
	//-------------------
	public void EnQueue(T item)
	{
		m_Data.Add(item);	
		int childIndex = m_Data.Count - 1;
		while (childIndex > 0)	
		{
			int parentIndex = (childIndex - 1) / 2;       
			if(m_Data[childIndex].CompareTo(m_Data[parentIndex]) >= 0)
			{
					break;
			}
				T temp = m_Data[childIndex];
				m_Data[childIndex] = m_Data[parentIndex];
				m_Data[parentIndex] = temp;
				childIndex = parentIndex;
		}
	 }
			
	//-------------------
	public T DeQueue()
	{
		if(m_Data.Count == 0)
		{
			return default(T);
		}
				
		T item = m_Data[0];
		int lastIndex = m_Data.Count - 1;
		m_Data[0] = m_Data[lastIndex];
		m_Data.RemoveAt(lastIndex);
				
		--lastIndex;
		int parentIndex = 0;
		while(true)
		{
			int childIndex = parentIndex * 2 + 1;
				
			if(childIndex > lastIndex)
			{
				break;
			}
					
			int rightChild = childIndex + 1;
					
			if( rightChild <= lastIndex && m_Data[rightChild].CompareTo(m_Data[childIndex]) < 0 )
			{	
				childIndex = rightChild;
			}
					
			if(m_Data[parentIndex].CompareTo(m_Data[childIndex]) <= 0)
			{
				break;
			}
					
			T temp = m_Data[parentIndex];
			m_Data[parentIndex] = m_Data[childIndex];
			m_Data[childIndex] = temp;
			parentIndex = childIndex;
			}
		return item;
	}

	public void IncreasePriority(int index)
	{
		int childIndex = index;
		while (childIndex > 0)
		{
			int parentIndex = (childIndex - 1) / 2;       
			if(m_Data[childIndex].CompareTo(m_Data[parentIndex]) < 0)
			{
				break;
			}
			T temp = m_Data[childIndex];
			m_Data[childIndex] = m_Data[parentIndex];
			m_Data[parentIndex] = temp;
			childIndex = parentIndex;
		}
	}

	//-------------------
	public bool Contains(T item)
	{
		return m_Data.Contains(item);
	}
			
	//-------------------
	public T FindItem(T item)
	{
		return m_Data[m_Data.IndexOf(item)];
	}

    //-------------------
    public int IndexOf(T item)
	{
		return m_Data.IndexOf (item);
	}

    //-------------------
    public int Size()
    {
        return m_Data.Count;
    }
};
