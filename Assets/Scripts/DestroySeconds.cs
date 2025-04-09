using UnityEngine;


public class DestroySeconds :  MonoBehaviour
{
    [SerializeField]
    private float secondsToDestroy = 5f;
    private void Start()
    {
        Destroy(gameObject, secondsToDestroy);
    }

}
