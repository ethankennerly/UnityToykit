namespace FineGameDesign.Utils
{
    public class ASingleton<T> where T : new()
    {
        private static T s_Instance;

        /// <summary>
        /// Useful to prevent accidentally recreating instance during teardown.
        /// </summary>
        public static bool InstanceExists()
        {
            return s_Instance != null;
        }

        public static T instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = new T();
                }
                return s_Instance;
            }
        }
    }
}
