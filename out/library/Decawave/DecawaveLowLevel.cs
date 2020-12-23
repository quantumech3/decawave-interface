using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Decawave
{
    namespace LowLevel
    {
        /// <summary>
        /// Contains methods that provide a direct serial interface on Android
        /// </summary>
        public class Serial
        {
            private static bool javaSideIsInitialized = false;
            private static AndroidJavaClass javaSerialInterface;

            /// <summary>
            /// Vendor ID of all Decawaves
            /// </summary>
            public const int VENDOR_ID = 0x1366;

            /// <summary>
            /// Product ID of all Decawaves
            /// </summary>
            public const int PRODUCT_ID = 0x0105;

            private class UnityCallback : AndroidJavaProxy
            {
                public UnityCallback() : base("com.arlazertag.decawavelowlevel.UnityCallback") { }

                public void onException(AndroidJavaObject exception)
                {
                    throw new LowLevelException(exception.Call<string>("getString")); // TODO test this
                }
            }

            private static AndroidJavaObject getUnityContext()
            {
                AndroidJavaObject unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject unityActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
                AndroidJavaObject unityContext = unityActivity.Call<AndroidJavaObject>("getApplicationContext");

                return unityContext;
            }

            /// <summary>
            /// Returns true if the library has been initialized else false
            /// </summary>
            public static bool JavaSideIsInitialized()
            {
                return javaSideIsInitialized;
            }

            /// <summary>
            /// Attempts to initialize the internals of the Java component of the DecawaveLowLevel interface.
            /// </summary>
            /// <returns><code>true</code> on success, else <code>false</code></returns>
            public static void InitializeJavaSide()
            {
                javaSerialInterface = new AndroidJavaClass("com.arlazertag.decawavelowlevel.SerialInterface");

                // Inject c# callbacks
                javaSerialInterface.CallStatic("setCallbackListener", javaSerialInterface);

                // Attempt to initialize Java side
                if(!javaSideIsInitialized)
                    javaSideIsInitialized = javaSerialInterface.CallStatic<bool>("initializeJavaSide", getUnityContext(), VENDOR_ID, PRODUCT_ID);

                Debug.Log(javaSideIsInitialized); // TODO get rid of this
            }

            /// <returns><code>true</code> if permissions have already been granted, else <code>false</code></returns>
            public static bool HasUSBPermission()
            {
                return false; // TODO
            }

            /// <summary>
            /// Attempts to request USB permissions from Android OS. Throws exception if already initialized
            /// </summary>
            /// <returns><code>true</code> if successfully initialied, else false</returns>
            /// <exception cref="Decawave.LowLevel.LowLevelException"></exception>
            public static bool RequestUSBPermission()
            {
                return false; // TODO
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns><code>true</code> if port has already been opened, else <code>false</code></returns>
            public static bool PortIsOpen()
            {
                return false; // TODO
            }

            /// <summary>
            /// Attempts to open the port on the phones default COM port. Throws exception if already opened
            /// </summary>
            /// <returns><code>true</code> if port is successfully opened, else <code>false</code></returns>
            /// <exception cref="Decawave.LowLevel.LowLevelException"></exception>
            public static bool OpenPort()
            {
                return false;
            }

            /// <summary>
            /// Writes data to serial if possible, else throws DecawaveLowLevelException
            /// </summary>
            /// <param name="data">Data to be written to serial</param>
            public static void Write(string data)
            {
                // TODO
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns>data sent from Decawave as a string if possible, else throws LowLevelException</returns>
            /// <exception cref="Decawave.LowLevel.LowLevelException"></exception>
            public static string ReadAll()
            {
                return null; // TODO
            }
        }

        /// <summary>
        /// Contains static methods to initialize and get distances from RF anchors
        /// </summary>
        public class Interface
        {
            /// <summary>
            /// Read only attribute that returns a list of AnchorData objects containing the ID of each anchor and each associated distance if possible, else throws DecawaveLowLevelException
            /// </summary>
            /// <exception cref="LowLevelException"></exception>
            public static Common.AnchorData[] anchors
            {
                get
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Thrown on failure of Decawave low level interface
        /// </summary>
        public class LowLevelException : Decawave.Common.DecawaveException
        {
            public LowLevelException(string message) : base(message)
            {

            }
        }
    }
}
