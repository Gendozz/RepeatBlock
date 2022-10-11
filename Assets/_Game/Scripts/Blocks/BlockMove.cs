using DG.Tweening;
using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class BlockMove
{
    readonly BlockView _view;

    public Settings _settings = null;


    public BlockMove(Settings settings, BlockView view)
    {
        _settings = settings;
        _view = view;
    }

    public async void Move()
    {

        await UniTask.Delay(TimeSpan.FromSeconds(1));
        //Debug.Log($"BlockMove was said to move {_view.gameObject.name} from {_view.gameObject.transform.position}");

        _view.transform
            .DOMoveY(_settings.yHeight, _settings.moveDuration);
            //.From();
    }


    [Serializable]
    public class Settings
    {
        public float yHeight;
        public float moveDuration;
    }
}
