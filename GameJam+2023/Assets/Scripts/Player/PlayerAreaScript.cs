using UnityEngine;

public class PlayerAreaScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<UnitBase>(out UnitBase unit))
        {
            EventManager.instance.OnEnemyCrashPlayer(unit);//not working
        }
    }

}