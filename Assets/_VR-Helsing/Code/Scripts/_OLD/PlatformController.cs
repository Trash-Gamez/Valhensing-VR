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
            AudioManager.Instance.SwampMusic("MainMenuMusic");          
        }
        public void StartNextPoint(float delay)
        {
            
            StartCoroutine(MoveToNextPoint(wayPoints[actualIndex].transform.position,delay));
        }

        IEnumerator MoveToNextPoint(Vector3 nextPosition,float delay)
        {
            yield return new WaitForSeconds(delay);
            PlayAudio(wayPoints[actualIndex].canRotate);
            while (canMove)
            {
                if (wayPoints[actualIndex].canRotate)
                {
                    RotateToNextPoint(nextPosition);
                }
                transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

                if (transform.position == nextPosition)
                {

                    canMove = false;

                }

                yield return null;
            }
            if (wayPoints[actualIndex].Time > 0)
            {
                AudioManager.Instance.PlaySound3D("CarritoStop", transform.position);
                AudioManager.Instance.StopAmbient();
            }
            yield return new WaitForSeconds(wayPoints[actualIndex].Time);
            

            NextPoint(wayPoints[actualIndex].canContinue);
        }

        void RotateToNextPoint(Vector3 nextPoint)
        {
       
            Vector3 targetDirection = nextPoint - transform.position;

       
            float singleStep = rotationSpeed * Time.deltaTime;

        
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        
            UnityEngine.Debug.DrawRay(transform.position, newDirection, Color.red);

        
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        void NextPoint(bool canContinue)
        {
            actualIndex++;
            
            if (!canContinue)
            {
                AudioManager.Instance.PlaySound3D("CarritoStop", transform.position);
                AudioManager.Instance.StopAmbient();

                return;
            }
            canMove = true;
            StartCoroutine(MoveToNextPoint(wayPoints[actualIndex].transform.position, 0));
        }

        public void RestartMovement(float delay)
        { 
            if (canMove) return;
            canMove = true;
            StartCoroutine(MoveToNextPoint(wayPoints[actualIndex].transform.position, delay));
        }

        private void PlayAudio(bool canRotate)
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
            AudioManager.Instance.PlaySound3D("CarritoStop", transform.position);
            AudioManager.Instance.StopAmbient();
        }
    
    }
}
