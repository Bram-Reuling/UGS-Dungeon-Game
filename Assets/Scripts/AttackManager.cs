using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private Player player;

    [SerializeField, Range(0, 10)]
    private int damage = 5;

    public List<int> ticks = new List<int>();

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    public void ApplyDamage(int t)
    {
        if (ticks.Count <= 0)
        {
            ticks.Add(t);
            StartCoroutine(Damage());
        }
        else
        {
            ticks.Add(t);
        }
    }

    IEnumerator Damage()
    {
        while(ticks.Count > 0)
        {
            for (int i = 0; i < ticks.Count; i++)
            {
                ticks[i]--;
            }
            player.playerHealth -= damage;
            ticks.RemoveAll(i => i == 0);
            yield return new WaitForSeconds(0.75f);
        }
    }
}
