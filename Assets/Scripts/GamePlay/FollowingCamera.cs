using UnityEngine;
using Cinemachine;

namespace HDV
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class FollowingCamera : MonoBehaviour
    {
        private CinemachineVirtualCamera virtualCamera;
        [SerializeField]
        private Transform followingObject;
        [SerializeField]
        private float distance, followSpeed;
        [SerializeField]
        private Vector3 angle;
        private EventVector3 screenYDir = new EventVector3();

        public EventVector3 ScreenYDir => screenYDir;

        public Transform FollowingObject => followingObject;

        private void Awake()
        {
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        public void SetTarget(Transform target)
        {
            /*virtualCamera.LookAt = target;
            virtualCamera.Follow = target;*/
            followingObject = target;
            transform.localEulerAngles = angle;
            transform.position = followingObject.position + (-transform.forward * distance);
        }

        public void ReleaseTarget(Transform target)
        {
            if (followingObject != null && followingObject == target)
                followingObject = null;
        }

        private void LateUpdate()
        {
            if (FollowingObject == null)
                return;

            transform.localEulerAngles = angle;

            var targetPos = FollowingObject.position + (-transform.forward * distance);
            var cameraPos = transform.position;
            if (Vector3.Distance(cameraPos, targetPos) < 0.01f)
                transform.position = targetPos;
            else
                transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);

            var down = Vector3.Dot(-Vector3.up, transform.forward);
            screenYDir.Value = (transform.forward + down * Vector3.up).normalized;
        }

        private void OnDrawGizmos()
        {
            if (FollowingObject == null) return;
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, FollowingObject.position);
            Gizmos.color = Color.red;
            var down = Vector3.Dot(-Vector3.up, transform.forward);
            screenYDir.Value = transform.forward + down * Vector3.up;
            Gizmos.DrawLine(transform.position, transform.position - down * Vector3.up);
            Gizmos.DrawLine(transform.position, transform.position + screenYDir.Value);
            Gizmos.DrawLine(transform.position, transform.position + transform.forward);
        }
    }
}