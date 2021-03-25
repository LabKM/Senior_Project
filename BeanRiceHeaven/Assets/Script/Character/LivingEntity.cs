using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    protected delegate void Behavior();
    public float MaxHp;
    public float hp { protected set; get; }
    public bool dead { protected set; get; }

    public virtual void Start()
    {
        hp = MaxHp;
    }
    public void TakeHit(float damage, RaycastHit hit)
    {
        TakeHit(damage);
    }
    public void TakeHit(float damage)
    {
        hp -= damage;
        if (hp <= 0 && !dead)
        {
            Die();
        }
    }
    public void Heal(float healPoint)
    {
        if (!dead)
        {
            hp += healPoint;
            hp = Math.Min(hp, MaxHp);
        }
    }
    public void Restore()
    {
        dead = false;
        hp = MaxHp;
    }
    protected virtual void Die()
    {
        dead = true;
        Destroy(gameObject);
    }
    protected virtual void idle()
    {
    }
}
