using System;
using UniRx;
using UnityEngine;

namespace Controllers.Main
{
    public class InputController : IDisposable
    {
        private readonly IPathRecorderController _pathRecorderController;
        private CompositeDisposable _clicksOnScreenSubscription;
        private IDisposable _clickOnScreenSubscription;

        public InputController(IPathRecorderController pathRecorderController)
        {
            _pathRecorderController = pathRecorderController;
            SubscribeToClicksOnScreen();
        }
        
        private void SubscribeToClicksOnScreen()
        {
            _clicksOnScreenSubscription = new CompositeDisposable();

            Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButton(0))
                .Subscribe(_ => _pathRecorderController.RecordTouchPath())
                .AddTo(_clicksOnScreenSubscription);
        }
        
        public void Dispose() => _clickOnScreenSubscription?.Dispose();
    }
}