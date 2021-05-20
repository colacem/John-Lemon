using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CapsuleCollider))]
public class Observer : MonoBehaviour
{
    public Transform player;
    private bool isPlayerInRange;
    public GameEnding gameEnding;
    private void OnTriggerEnter(Collider other) 
    {
        if (other.transform==player) 
        {
            isPlayerInRange=true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform==player)
        {
            isPlayerInRange=false;
        }    
    }
    private void Update() 
    {
        //Verificar si no hay pared ni nada en medio del player con el enemigo con RayCast
        if (isPlayerInRange)
        {
            //Le sumo 1 metro en altura vector3.up ya que el player tiene el punto de origen en el suelo.
            Vector3 direction=player.position-transform.position+Vector3.up;
            Ray ray = new Ray(transform.position,direction);

            Debug.DrawRay(transform.position,direction,Color.green,Time.deltaTime,true);

            RaycastHit raycastHit;
            if (Physics.Raycast(ray,out raycastHit)) //Devuelve true cuando el rayo choca contra algo
            {
                if (raycastHit.collider.transform==player)
                {
                    gameEnding.CatchPlayer();
                }
            }
        }
    }

    //Dibujar Gizmos por programación. Solo se muestran en el desarrollo y no se traspasan al Game
    private void OnDrawGizmos() 
    {
        Gizmos.color=Color.green;
        Gizmos.DrawSphere(transform.position,0.1f);
        Gizmos.color=Color.magenta;
        Gizmos.DrawLine(transform.position,player.position+Vector3.up);
        
    }
    
    
}
