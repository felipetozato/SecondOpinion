using System;
using System.Reactive.Disposables;
using ReactiveUI;

using UIKit;

namespace SecondOpinion.iOS.Views
{
    public abstract class BaseViewController<T> : ReactiveViewController<T> where T : class
    {

        private CompositeDisposable subscriptionDisposables;

        public BaseViewController() : base() {
            ViewModel = (T)Activator.CreateInstance(typeof(T));
            CommonInit();
        }

        public BaseViewController(Foundation.NSObjectFlag flag) : base(flag) {
            ViewModel = (T)Activator.CreateInstance(typeof(T));
            CommonInit();
        }

        public BaseViewController(Foundation.NSCoder c) : base(c) {
            ViewModel = (T)Activator.CreateInstance(typeof(T));
            CommonInit();
        }

        public BaseViewController(IntPtr ptr) : base(ptr) {
            ViewModel = (T)Activator.CreateInstance(typeof(T));
            CommonInit();
        }

        public BaseViewController(string controllerName, Foundation.NSBundle bundle, params object[] viewModelParameters) : base(controllerName, bundle) {
            ViewModel = (T)Activator.CreateInstance(typeof(T), viewModelParameters);
            CommonInit();
        }

        public BaseViewController(string controllerName, Foundation.NSBundle bundle) : base(controllerName, bundle) {
            ViewModel = (T)Activator.CreateInstance(typeof(T));
            CommonInit();
        }

        /// <summary>
        /// Views the did disappear.
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
			subscriptionDisposables.Clear();
			subscriptionDisposables.Dispose();
        }

		/// <summary>
		/// Shoulds dispose when dissapearing.
		/// </summary>
		/// <param name="disposable">Disposable.</param>
		public void ShouldDispose(IDisposable disposable)
		{
			subscriptionDisposables.Add(disposable);
		}

        /// <summary>
        /// Shoulds the dispose.
        /// </summary>
        /// <param name="disposableArray">Disposable array.</param>
        public void ShouldDispose(params IDisposable[] disposableArray) {
            Array.ForEach(disposableArray, subscriptionDisposables.Add);
        }

        protected void CommonInit() {
            subscriptionDisposables = new CompositeDisposable();
        }
    }
}

