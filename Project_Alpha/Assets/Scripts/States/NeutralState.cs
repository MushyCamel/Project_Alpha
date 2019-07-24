using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "States/NeutralState")]
public class NeutralState : BaseState
{
    public ShootingState shootingState;


    public override async Task Enter(GameObject player)
        {

            await new WaitForEndOfFrame();
            Debug.Log("entered state " + nameof(NeutralState));
        }

        public override async void Actions(GameObject player)
        {
            if (Input.GetButton("Fire1"))
            {
                TriggerState(shootingState);
            }

            Stop();

        }

        public override async Task Leave(GameObject player)
        {
            await new WaitForEndOfFrame();
            Debug.Log("leaving State");

        }
    }
