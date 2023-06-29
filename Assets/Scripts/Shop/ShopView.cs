using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ShopView : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform contentTransform;
    [SerializeField] private RectTransform contentRectTransform;

    [Header("Settings")]
    [SerializeField] private List<GameObject> elements;
    [SerializeField] private float offset;

    public GameObject Add(GameObject element)
    {
        GameObject createdElement = Instantiate(element, this.contentTransform);

        if(this.elements.Count == 0)
        {
            this.elements.Add(createdElement);
            return createdElement;
        }
        ShopElements elementMeta = createdElement.GetComponent<ShopElements>();
        GameObject lastElement = this.elements.Last();

        Vector3 lastElementPosition = lastElement.transform.localPosition;

        Vector3 newElementPosition = new Vector3
        {
            x = lastElementPosition.x,
            y = lastElementPosition.y - elementMeta.Height() - offset,
            z = lastElementPosition.z
        };
        createdElement.transform.localPosition = newElementPosition;
        this.elements.Add(createdElement);
        return createdElement;
    }
}
