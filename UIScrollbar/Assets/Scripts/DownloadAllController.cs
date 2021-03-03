using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class DownloadAllController : MonoBehaviour
{
    [SerializeField]
    private EventListener _downloadListener;
    [SerializeField]
    private EventDispatcher _allDownloadDispatcher;

    [SerializeField]
    private int countDownload;

    private int check = 0;
    private void OnEnable() {
        _downloadListener.OnEventHappened += CheckCount;
    }

    private void CheckCount() {
        check++;
        if(check >= countDownload) {
            _allDownloadDispatcher.Dispatch();
        }
    }
}
