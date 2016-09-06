using UnityEngine;
using System.Collections.Generic;
using System.Linq;



public class InventoryHandler : MonoBehaviour {


	private List<Item> m_inventory;

	// Use this for initialization
	void Start () {
		m_inventory = new List<Item>();
	}

	public void AddItem(Item item) {
		if (item != null)
			m_inventory.Add(item);
	}

	public void RemoveItem(string id) {
		int idx = m_inventory.FindIndex(item => item.id.Equals(id));
		if (idx >= 0)
			m_inventory.RemoveAt(idx);
		else
			Debug.Log("Error removing item: "+id);
	}
		
	public List<Item> Items { get{return m_inventory;} }
	public Item FindItem(string id) {
		return m_inventory.Find(i => i.id.Equals(id));
	}
		

}
