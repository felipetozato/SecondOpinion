using System;

namespace SecondOpinion {
    public static class DisposeHelper {
        public static void Dispose<T> (ref T disposable) where T : IDisposable {
            if (disposable != null) {
                disposable.Dispose();
                disposable = default(T);
            }
        }
    }
}