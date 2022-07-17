using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    #region Inspector
    
    #endregion

    

    public virtual void InitializeHealth()
    {

    }

    public virtual void TakeDamage(int amount)
    {
        
    }

    public virtual void HealDamage(int amount)
    {
        
    }

    public virtual void AddArmour(int amount)
    {
        
    }

    public virtual void ResetArmour()
    {
        
    }

    public virtual void CheckDeath()
    {

    }

    public virtual void OnDeath()
    {

    }
}
