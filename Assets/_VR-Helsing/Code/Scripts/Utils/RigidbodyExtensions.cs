using UnityEngine;

namespace _VR_Helsing.Utils
{
    public static class RigidbodyExtensions
    {
        /// <summary>
        /// Gets the local angular velocity of rigidbody transform itself
        /// </summary>
        /// <param name="rb">The rigidbody that contains the global angular velocity</param>
        /// <returns>Local Angular Velocity of Rigidbody projected in transform</returns>
        public static Vector3 GetLocalAngularVelocity(this Rigidbody rb)
        {
            Vector3 localAngVel = Vector3.zero
                ;
            var globalAngularVelocity = rb.angularVelocity; //Velocidad angular GLOBAL (esto ultimo no lo sabia xd)
            /* PROYECTAMOS (mucho ojo que no sabia que el dot era proyeccion)
             * Proyectamos, la velocidad angular del mundo, SOBRE la direccion local del eje
             * Esto nos dará como resultado la velocidad angular local en x (es la que buscamos para la animacion de rotar)
             */

            //                                        Velocidad angular global       eje local de el rigidbody en x      Conv a grados
            //                                                  |                            |                           |
            localAngVel.x = Vector3.Dot(globalAngularVelocity, rb.transform.right) * Mathf.Rad2Deg;
            localAngVel.z = Vector3.Dot(globalAngularVelocity, rb.transform.forward) * Mathf.Rad2Deg;
            localAngVel.y = Vector3.Dot(globalAngularVelocity, rb.transform.up) * Mathf.Rad2Deg;
            return localAngVel;
        }
        
        /// <summary>
        /// Get the local angular velocity of <param>localTransform</param>
        /// </summary>
        /// <param name="rb">The rigidbody that contains angular velocity</param>
        /// <param name="localTransform">The transform where to project global angular velocity</param>
        /// <returns>Local Angular Velocity of Rigidbody projected in transform</returns>
        public static Vector3 GetLocalAngularVelocity(this Rigidbody rb, Transform localTransform)
        {
            Vector3 localAngVel = Vector3.zero
                ;
            var globalAngularVelocity = rb.angularVelocity; //Velocidad angular GLOBAL (esto ultimo no lo sabia xd)
            /* PROYECTAMOS (mucho ojo que no sabia que el dot era proyeccion)
             * Proyectamos, la velocidad angular del mundo, SOBRE la direccion local del eje
             * Esto nos dará como resultado la velocidad angular local en x (es la que buscamos para la animacion de rotar)
             */

            //                                        Velocidad angular global       eje local de el rigidbody en x      Conv a grados
            //                                                  |                            |                           |
            localAngVel.x = Vector3.Dot(globalAngularVelocity, localTransform.right) * Mathf.Rad2Deg;
            localAngVel.z = Vector3.Dot(globalAngularVelocity, localTransform.forward) * Mathf.Rad2Deg;
            localAngVel.y = Vector3.Dot(globalAngularVelocity, localTransform.up) * Mathf.Rad2Deg;
            return localAngVel;
        }
    }
}