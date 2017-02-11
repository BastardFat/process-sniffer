using System;
using System.Reflection;

namespace BastardFat.ProcessSniffer.Tools
{
    public abstract class Singleton<T> where T : class
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                    lock (syncRoot)
                        if (instance == null)
                            instance = (T) typeof(T).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
                                                                    null,
                                                                    new Type[0],
                                                                    new ParameterModifier[0]).Invoke(null);
                return instance;
            }
        }
        private static object syncRoot = new object();
    }
}
