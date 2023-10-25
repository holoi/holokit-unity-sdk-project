// SPDX-FileCopyrightText: Copyright 2023 Holo Interactive <dev@holoi.com>
// SPDX-FileContributor: Sizheng Hao <sizheng@holoi.com>
// SPDX-License-Identifier: MIT

#if UNITY_IOS
using UnityEngine;
using HoloInteractive.XR.HoloKit.iOS;

namespace HoloInteractive.XR.HoloKit.Samples.GlowingOrbs
{
    public class OrbSpawner : MonoBehaviour
    {
        [SerializeField] private HandGestureRecognitionManager m_HandGestureRecognitionManager;

        [SerializeField] private Transform m_SpawnHandJoint;

        [SerializeField] private GameObject m_OrbPrefab;

        [SerializeField] private float m_DistOffset = 0.3f;

        [SerializeField] private float m_InitialForce = 5f;

        [SerializeField] private float m_Lifetime = 6f;

        private Transform m_CenterEyePose;

        private void Start()
        {
            m_CenterEyePose = FindObjectOfType<HoloKitCameraManager>().transform;

            // Register the callback
            m_HandGestureRecognitionManager.OnHandGestureChanged += OnHandGestureChanged;
        }

        private void OnHandGestureChanged(HandGesture handGesture)
        {
            if (handGesture == HandGesture.Five)
            {
                // Instantiate orb
                var direction = (m_SpawnHandJoint.position - m_CenterEyePose.position).normalized;
                GameObject orb = Instantiate(m_OrbPrefab, m_SpawnHandJoint.position + m_DistOffset * direction, Quaternion.identity);

                // Add initial velocity
                orb.GetComponent<Rigidbody>().AddForce(m_InitialForce * direction);
                Destroy(orb, m_Lifetime);
            }
        }
    }
}
#endif
