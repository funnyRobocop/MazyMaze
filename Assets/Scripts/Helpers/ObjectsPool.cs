using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ObjectsPool<T> : List<T> where T : class
{

	public bool IsEmpty()
	{
		if (this.Count == 0)
		{
			return true;
		}

		return false;
	}

	public T GetObjectFromPool()
	{
		if (this.Count > 0) 
		{
			T lastPoolObject = this[this.Count - 1];
			Remove(lastPoolObject);

			return lastPoolObject;
		}

		return null;
	}
  
	public void SendObjectToPool(T poolObject)
	{
		Add (poolObject);
	}
}
