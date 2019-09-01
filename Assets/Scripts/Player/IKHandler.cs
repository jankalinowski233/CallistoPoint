using UnityEngine;

[RequireComponent(typeof(Animator))]
[ExecuteInEditMode]
public class IKHandler : MonoBehaviour
{
    Animator m_anim;
    PlayerController m_player;

    [Header("SMG")]
    public Transform m_leftHandSMGTransform;
    public Transform m_rightHandSMGTransform;

    [Header("Heavy rifle")]
    [Space(5f)]
    public Transform m_leftHandRifleTransform;
    public Transform m_rightHandRifleTransform;

    [Header("Sniper rifle")]
    [Space(5f)]
    public Transform m_leftHandSniperTransform;
    public Transform m_rightHandSniperTransform;

    [Header("Grenade throw")]
    [Space(5f)]
    public Transform m_grenadeThrowLeftHandTransform;

    private void Awake()
    {
        m_anim = GetComponent<Animator>();
        m_player = GetComponent<PlayerController>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if(m_player.m_bIsMeleeAttacking == false)
        {
            if(m_player.m_bIsThrowingGrenade == false)
            {
                switch (PlayerController.m_instance.m_iCurrentWeapon)
                {
                    case 0: //SMG
                        if (m_leftHandSMGTransform != null)
                        {
                            m_anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                            m_anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                            m_anim.SetIKPosition(AvatarIKGoal.LeftHand, m_leftHandSMGTransform.position);
                            m_anim.SetIKRotation(AvatarIKGoal.LeftHand, m_leftHandSMGTransform.rotation);
                        }

                        if (m_rightHandSMGTransform != null)
                        {
                            m_anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                            m_anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                            m_anim.SetIKPosition(AvatarIKGoal.RightHand, m_rightHandSMGTransform.position);
                            m_anim.SetIKRotation(AvatarIKGoal.RightHand, m_rightHandSMGTransform.rotation);
                        }
                        break;
                    case 1: //Heavy rifle
                        if (m_leftHandRifleTransform != null)
                        {
                            m_anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                            m_anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                            m_anim.SetIKPosition(AvatarIKGoal.LeftHand, m_leftHandRifleTransform.position);
                            m_anim.SetIKRotation(AvatarIKGoal.LeftHand, m_leftHandRifleTransform.rotation);
                        }

                        if (m_rightHandRifleTransform != null)
                        {
                            m_anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                            m_anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                            m_anim.SetIKPosition(AvatarIKGoal.RightHand, m_rightHandRifleTransform.position);
                            m_anim.SetIKRotation(AvatarIKGoal.RightHand, m_rightHandRifleTransform.rotation);
                        }
                        break;
                    case 2: //Sniper rifle
                        if (m_leftHandSniperTransform != null)
                        {
                            m_anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                            m_anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                            m_anim.SetIKPosition(AvatarIKGoal.LeftHand, m_leftHandSniperTransform.position);
                            m_anim.SetIKRotation(AvatarIKGoal.LeftHand, m_leftHandSniperTransform.rotation);
                        }

                        if (m_rightHandSniperTransform != null)
                        {
                            m_anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                            m_anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                            m_anim.SetIKPosition(AvatarIKGoal.RightHand, m_rightHandSniperTransform.position);
                            m_anim.SetIKRotation(AvatarIKGoal.RightHand, m_rightHandSniperTransform.rotation);
                        }
                        break;
                }
            }
            
        }
        
    }
}
