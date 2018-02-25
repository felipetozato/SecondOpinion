﻿using System;
using System.Reactive.Disposables;
using ReactiveUI;

using UIKit;

namespace SecondOpinion.iOS.Views
{
    public abstract class BaseViewController<T> : ReactiveViewController<T> where T : class
    {

        private readonly CompositeDisposable subscriptionDisposables;

        public BaseViewController() : base() {
            subscriptionDisposables = new CompositeDisposable();
            ViewModel = (T)Activator.CreateInstance(typeof(T));
        }

        public BaseViewController(Foundation.NSObjectFlag flag) : base(flag) {
            subscriptionDisposables = new CompositeDisposable();
            ViewModel = (T)Activator.CreateInstance(typeof(T));
        }

        public BaseViewController(Foundation.NSCoder c) : base(c) {
            subscriptionDisposables = new CompositeDisposable();
            ViewModel = (T)Activator.CreateInstance(typeof(T));
        }

        public BaseViewController(IntPtr ptr) : base(ptr) {
            subscriptionDisposables = new CompositeDisposable();
            ViewModel = (T)Activator.CreateInstance(typeof(T));
        }

        public BaseViewController(string controllerName, Foundation.NSBundle bundle, params object[] viewModelParameters) : base(controllerName, bundle) {
            subscriptionDisposables = new CompositeDisposable();
            ViewModel = (T)Activator.CreateInstance(typeof(T), viewModelParameters);
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
    }
}

