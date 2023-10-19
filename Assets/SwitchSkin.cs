using UnityEngine;

public class ISwitchSkin
    {
    public void SwitchSkin(GameObject current, GameObject next)
    {
        current.SetActive(false);
        next.SetActive(true);
    }
}
