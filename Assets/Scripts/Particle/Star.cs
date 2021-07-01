
using UnityEngine;


public class Star : MonoBehaviour
{
    private float _lifeTime = 0.2f;

    private void Awake()
    {
        Destroy(gameObject, _lifeTime);
    }
}
