using UnityEngine;

public class SingleBehaviour<T> : MonoBehaviour where T:Component
{
    private static T _instance;

    public static T GetInstance
    {
        get
        {
            return _instance;
        }
    }

    protected virtual  void Awake()
    {
        if (_instance == null)
        {
            //search first loaded
            _instance = FindObjectOfType<T>();

            // double check
            if (_instance == null)
            {
                var name = nameof(T);
                Debug.LogError($"[Add the following component to make the system work '{name}']");
            }
        }
    }
}
