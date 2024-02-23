using UnityEngine;

public class PoseScript : MonoBehaviour
{
    [SerializeField] float poseIndex;
    Animator poseAnim;

    void Awake()
    {
        poseAnim = GetComponent<Animator>();
        poseAnim.SetFloat("Pose", poseIndex);
    }

}
