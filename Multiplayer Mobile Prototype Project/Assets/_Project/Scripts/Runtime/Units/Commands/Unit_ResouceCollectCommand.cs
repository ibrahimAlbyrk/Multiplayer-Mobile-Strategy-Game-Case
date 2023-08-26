using Core.Runtime.NETWORK;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Core.Runtime.Units.Commands
{
    using Motors;
    using Animations;
    using Collectible;
    using Animations.Event;
    
    public class Unit_ResourceCollectCommand : Unit_ResourceCollect_Command
    {
        private const float _CollectCountDown = 1f;
        private float _timer;

        private bool _addedEvent;

        private ResourceCollectible _collectible;
        
        public override bool Execute(Transform obj1, ResourceCollectible target)
        {
            if (target == null) return true;
            
            if(_collectible == null)
                _collectible = target;
            
            if (!_addedEvent)
            {
                _unit.GetAnimator().AddEvent("Collect", CollectEvent);

                _addedEvent = true;
            }
            
            Unit_LocomotionMotor.LookTransform(obj1, target.transform);
            
            if (_timer >= _CollectCountDown)
            {
                
                _timer = 0f;
            }
            else
                _timer += Time.fixedDeltaTime;
            
            var isCompleted = target.ResourceAmount < 1;
            
            if(isCompleted)
            {
                if (_addedEvent)
                {
                    _unit.GetAnimator().RemoveEvent("Collect", CollectEvent);
                    _addedEvent = false;
                }

                if (target.gameObject != null)
                {
                    var eventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                    
                    PhotonNetwork.RaiseEvent(NETWORK_EventCode.DESTROY_OBJECT_EVENT_CODE,
                        target.gameObject.GetComponent<PhotonView>().ViewID,
                        eventOptions,
                        SendOptions.SendReliable);
                }
                
            }
            
            _unit.GetAnimator().SetBool(AnimationParams.WORKING_PARAM, !isCompleted);

            return isCompleted;
        }

        public override void End()
        {
            _unit.GetAnimator().SetBool(AnimationParams.WORKING_PARAM, false); 
        }

        private void CollectEvent()
        {
            _collectible.ResourceAmount--;
        }
    }
}