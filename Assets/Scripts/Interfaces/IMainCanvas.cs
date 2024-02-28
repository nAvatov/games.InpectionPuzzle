using UnityEngine;

public interface IMainCommonCanvas {
    public Canvas CommonCanvas { get; }
    public void ChangeRenderDisplay(int displayID);
}