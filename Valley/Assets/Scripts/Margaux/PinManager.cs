using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform dynamics;

    [SerializeField] public float viewRange;
    [SerializeField] public float blockingRange;

    public static PinManager instance;

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        instance = this;

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var flattenMouse = new Vector3(mousePos.x, mousePos.y, 0);

            Instantiate(prefab, flattenMouse, Quaternion.identity, dynamics);
        }
    }
}
