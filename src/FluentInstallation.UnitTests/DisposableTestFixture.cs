using System;
using System.Collections.Generic;

namespace FluentInstallation
{
    public abstract class DisposableTestFixture : IDisposable
    {
        protected DisposableTestFixture()
        {
            Disposables = new List<IDisposable>();
        }

        private List<IDisposable> Disposables { get; set; }


        public void Dispose()
        {
            Disposables.ForEach(disposable => disposable.Dispose());
        }

        public void RegisterDisposable(Action action)
        {
            Disposables.Add(new DisposableAction(action));
        }
    }
}