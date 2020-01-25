using System;
namespace SecondOpinion {
    public static class App {


        private static QbProvider qbProvider;
        public static QbProvider QbProvider {
            get {
                if (qbProvider == null) {
                    qbProvider = new QbProvider(() => { });
                }
                return qbProvider;
            }
            private set { qbProvider = value; }
        }

        
    }
}
