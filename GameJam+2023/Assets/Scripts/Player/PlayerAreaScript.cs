using UnityEngine;

public class PlayerAreaScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<UnitBase>(out UnitBase unit))
        {
            var newDamage = new Damage() { damageValue = unit.CrashDamage + unit.CurrentHealth };
            EventManager.instance.OnEnemyCrashPlayer(newDamage, unit);
        }
    }

}