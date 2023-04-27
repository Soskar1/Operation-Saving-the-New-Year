using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    [SerializeField] private GameObject loot;

    public void DropLoot()
    {
        Instantiate(loot, transform.position, Quaternion.identity);
    }
}
