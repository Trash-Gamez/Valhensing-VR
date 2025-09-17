using System.Collections;
using UnityEngine;

namespace _VanHelsingVR
{
    public class PlatformController : MonoBehaviour
    {
        // Start is called before the first frame update

        private bool canMove = true;

        [SerializeField] private float speed;
        [SerializeField] private float rotationSpeed=0.01f;

        [SerializeField] private WayPoint[] wayPoints;
        private int actualIndex = 0;

        private void Start()
        {
            if(AudioManager.Instance)
                AudioManager.Instance.SwampMusic("MainMenuMusic");          
        }
        
        public void StartNextPoint(float delay)
        {
            AudioManager.Instance.SwampMusic("GameplayMusic03");
            StartCoroutine(MoveToCurrentPoint(wayPoints[actualIndex].transform.position,delay));
        }

        IEnumerator MoveToCurrentPoint(Vector3 nextPosition,float delay)
        {
            yield return new WaitForSeconds(delay);
            PlayPlatformAudio(wayPoints[actualIndex].canRotate);
            
            while (canMove)
            {
                if (wayPoints[actualIndex].canRotate)
                    RotateToNextPoint(nextPosition);
                
                //Mueve la plataforma
                transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

                if (Vector3.Distance(transform.position, nextPosition) <= Mathf.Epsilon) //Llegó a la plataforma
                    canMove = false;

                yield return null;
            }
            
            var newIndex = actualIndex + 1;
            if (newIndex >= wayPoints.Length) //Si no hay más waypoints ni lo intentes
            {
                StopPlatform();
                yield break;
            }
            
            if (wayPoints[actualIndex].Time > 0)
                StopPlatform();
            
            //Espera el tiempo que el waypoint necesite
            yield return new WaitForSeconds(wayPoints[actualIndex].Time);

            if(!wayPoints[actualIndex].canContinue)
                StopPlatform();
                
            //Espera a que se pueda continuar la plataforma
            yield return new WaitUntil(() => wayPoints[actualIndex].canContinue);
            
            NextPoint();
        }

        private void StopPlatform()
        {
            AudioManager.Instance.PlaySound3D("CarritoStop", transform.position);
            AudioManager.Instance.StopAmbient();
        }

        void RotateToNextPoint(Vector3 nextPoint)
        {
            Vector3 targetDirection = nextPoint - transform.position;

            float singleStep = rotationSpeed * Time.deltaTime;

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        void NextPoint()
        {
            actualIndex++;
            canMove = true;
            StartCoroutine(MoveToCurrentPoint(wayPoints[actualIndex].transform.position, 0));
        }

        private void PlayPlatformAudio(bool canRotate)
        {
            if (canRotate)
            {
                if (AudioManager.Instance.IsAmbientPlaying("Carrito_01")) return;
                AudioManager.Instance.PlayAmbient("Carrito_01", 0);
            }
            else
            {
                if (AudioManager.Instance.IsAmbientPlaying("Carrito_02")) return;
                AudioManager.Instance.PlayAmbient("Carrito_02", 0);
            }
        }
        
       public void OnPlayerDead()
        {
            StopAllCoroutines();
            StopPlatform();
        }
    }
}
