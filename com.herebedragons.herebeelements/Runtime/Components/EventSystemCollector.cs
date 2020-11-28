﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HereBeElements
{
    public class EventSystemCollector: MonoBehaviour
    {
        public static event Action<GameObject, GameObject> OnSelectionUpdated;
        private static void DispatchSelectionUpdated(GameObject newSelectedGameObject, GameObject previousSelectedGameObject)
        {
            if (OnSelectionUpdated != null)
            {
                OnSelectionUpdated.Invoke(newSelectedGameObject, previousSelectedGameObject);
            }
        }

        private void Awake()
        {
            eventSystem = GetComponent<EventSystem>();
        }

        private EventSystem eventSystem;
 
        private GameObject m_LastSelectedGameObject;
 
        private void Update()
        {
            var currentSelectedGameObject = eventSystem.currentSelectedGameObject;
            if (currentSelectedGameObject != m_LastSelectedGameObject)
            {
                DispatchSelectionUpdated(currentSelectedGameObject, m_LastSelectedGameObject);
                m_LastSelectedGameObject = currentSelectedGameObject;
            }
        }
    }
    
}