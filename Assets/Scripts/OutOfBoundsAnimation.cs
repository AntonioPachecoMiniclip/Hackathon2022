using System.Collections;
using UnityEngine;

public class OutOfBoundsAnimation : MonoBehaviour
{
    private Rigidbody rigidbody3D;
    private MeshRenderer model3D;
    private PlayerController playerController;
    private MeshRenderer model2D;
    
    private void Start()
    {
        rigidbody3D = GetComponent<Rigidbody>();
        model3D = GetComponent<MeshRenderer>();
        rigidbody3D.isKinematic = true;
    }

    public void StartAnimation(PlayerController playerController, MeshRenderer model2D)
    {
        this.playerController = playerController; 
        this.model2D = model2D;
        Vector2 velocity = playerController.PlayerMovement.Rb.velocity;
        model2D.enabled = false;
        
        rigidbody3D.isKinematic = false;
        model3D.enabled = true;
        rigidbody3D.AddTorque(velocity, ForceMode.Impulse);

        StartCoroutine(CheckForAnimationEnd());
    }

    private IEnumerator CheckForAnimationEnd()
    {
        yield return new WaitUntil(() => !playerController.IsOutOfBounds);
        EndAnimation();
    }

    private void EndAnimation()
    {
        playerController.PlayerMovement.Rb.velocity = Vector2.zero;
        model2D.enabled = true;
        
        rigidbody3D.isKinematic = true;
        rigidbody3D.constraints = RigidbodyConstraints.FreezeRotation;
        model3D.enabled = false;

        Transform transform3D = transform;
        Transform transform2D = model2D.transform;
        transform3D.position = transform2D.position;
        transform3D.rotation = transform2D.rotation;
    }
}