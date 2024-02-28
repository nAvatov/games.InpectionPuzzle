using System;
using UnityEngine;

public class UI_MainCommonCanvas : IMainCommonCanvas {
    public Canvas CommonCanvas => throw new NotImplementedException();

    public void ChangeRenderDisplay(int displayID) {
        CommonCanvas.targetDisplay = displayID;
    }
}