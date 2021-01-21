using System;
using System.Collections.Generic;
using UnityEngine;
using Decawave.Common;

namespace Decawave
{
    namespace HighLevel
    {
        /// <summary>
        /// Contains basic math functions used for RF localization
        /// </summary>
        public class RF
        {
            /// <summary>
            /// Given the positions of each anchor and distances from those anchors, will return your position in RF space
            /// </summary>
            /// <param name="idsToPositions">Dictionary associating anchor ids with positions</param>
            /// <param name="r0">Distance from anchor</param>
            /// <param name="r1">Distance from anchor</param>
            /// <param name="r2">Distance from anchor</param>
            /// <param name="r3">Distance from anchor</param>
            /// <returns>Corrosponding position in RF space</returns>
            private static Vector3 Trilaterate4(Dictionary<int, Vector3> idsToPositions, AnchorData r0, AnchorData r1, AnchorData r2, AnchorData r3)
            {
                return Vector3.zero; // TODO
            }

            /// <summary>
            /// Given a sufficient, and supported amount of positions, will attempt to perform trilateration and throw HighLevelException on failure
            /// </summary>
            /// <param name="idsToPositions"></param>
            /// <param name="rs"></param>
            /// <returns></returns>
            public static Vector3 Trilaterate(Dictionary<int, Vector3> idsToPositions, params AnchorData[] rs)
            {
                return Vector3.zero; ; // TODO
            }

            /// <summary>
            /// Given transforms derived from various localization method, will attempt to fuse them with Mallesh Dasari's algorithm
            /// </summary>
            /// <param name="rfDerived"></param>
            /// <param name="vioDerived"></param>
            /// <returns></returns>
            public static Transform toFusion(Transform rfDerived, Transform vioDerived)
            {
                return null; // TODO (Use average for now)
            }
        }

        /// <summary>
        /// Contains basic math functions used for VIO localization
        /// </summary>
        public class VIO
        {
            /// <summary>
            /// Given the transforms of the RF and VIO origins, as well as position in VIO, maps vioPosition to RF space
            /// </summary>
            /// <param name="rfOrigin">Transform of RF Origin</param>
            /// <param name="vioOrigin">Transform of VIO origin</param>
            /// <param name="vioPosition">Position in VIO space</param>
            /// <returns>Position in RF space</returns>
            public static Vector3 positionToRF(Transform rfOrigin, Transform vioOrigin, Vector3 vioPosition)
            {
                return Vector3.zero; // TODO
            }

            /// <summary>
            /// Given the transforms of the RF and VIO origins, as well as orientation in VIO, maps vioRotation to RF space
            /// </summary>
            /// <param name="rfOrigin">Transform of RF Origin</param>
            /// <param name="vioOrigin">Transform of VIO origin</param>
            /// <param name="vioRotation">Orientation in VIO space</param>
            /// <returns>Position in RF space</returns>
            public static Vector3 rotationToRF(Transform rfOrigin, Transform vioOrigin, Quaternion vioRotation)
            {
                return Vector3.zero; // TODO
            }

            /// <summary>
            /// Given the transforms of the RF and VIO origins, as well as transform in VIO, maps vioTransform to RF space
            /// </summary>
            /// <param name="rfOrigin"></param>
            /// <param name="vioOrigin"></param>
            /// <param name="vioTransform"></param>
            /// <returns></returns>
            public static Transform transformToRF(Transform rfOrigin, Transform vioOrigin, Transform vioTransform)
            {
                return null; // TODO
            }
        }

        /// <summary>
        /// Thrown on failure of Decawave high level interface
        /// </summary>
        public class HighLevelException : Decawave.Common.DecawaveException
        {
            public HighLevelException(string message) : base(message)
            {

            }
        }
    }
}
