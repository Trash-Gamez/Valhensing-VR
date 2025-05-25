using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Weapon : MonoBehaviour
{
    public float damage = 20f;
    public InputActionReference fireActionReference; // Asignar desde el inspector

    void Update()
    {
        if (fireActionReference != null && fireActionReference.action.WasPressedThisFrame())
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Le di a: " + hit.transform.name);

            EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage((int)damage);
            }
        }
        else
        {
            Debug.Log("No le di a nada.");
        }
    }
}