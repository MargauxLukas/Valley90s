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
        gameObject.SetActive(false);
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

    public void OpenMap()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public GameObject PutBalise(Vector2 position)
    {
        position = new Vector2(100, 0) + position*0.1f;
        return Instantiate(prefab, position, Quaternion.identity, dynamics);
    }

    public void RemoveBalise(GameObject toRemove)
    {
        toRemove.SetActive(false);
    }
}
