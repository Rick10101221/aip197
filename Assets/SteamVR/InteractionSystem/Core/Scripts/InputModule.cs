//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Makes the hand act as an input module for Unity's event system
//
//=============================================================================

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Valve.VR;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	public class InputModule : BaseInputModule
	{

        // REMOVED STANDALONE INPUT MODULE COMPONENT BECAUSE INPUTMODULE INHERITS

        public Camera m_Camera;
        public SteamVR_Input_Sources m_targetSource;
        public SteamVR_Action_Boolean m_ClickAction;

        private GameObject m_CurrentObject = null;
        private PointerEventData m_Data = null;

        private GameObject submitObject;

		//-------------------------------------------------
		private static InputModule _instance;
		public static InputModule instance
		{
			get
			{
				if ( _instance == null )
					_instance = GameObject.FindObjectOfType<InputModule>();

				return _instance;
			}
		}

        protected override void Awake()
        {
            base.Awake();

            m_Data = new PointerEventData(eventSystem);
        }

        //-------------------------------------------------
        public override bool ShouldActivateModule()
		{
			if ( !base.ShouldActivateModule() )
				return false;

			return submitObject != null;
		}


		//-------------------------------------------------
		public void HoverBegin( GameObject gameObject )
		{
			PointerEventData pointerEventData = new PointerEventData( eventSystem );
			ExecuteEvents.Execute( gameObject, pointerEventData, ExecuteEvents.pointerEnterHandler );
		}


		//-------------------------------------------------
		public void HoverEnd( GameObject gameObject )
		{
			PointerEventData pointerEventData = new PointerEventData( eventSystem );
			pointerEventData.selectedObject = null;
			ExecuteEvents.Execute( gameObject, pointerEventData, ExecuteEvents.pointerExitHandler );
		}


		//-------------------------------------------------
		public void Submit( GameObject gameObject )
		{
			submitObject = gameObject;
		}


		//-------------------------------------------------
		public override void Process()
		{
			if ( submitObject )
			{
				BaseEventData data = GetBaseEventData();
				data.selectedObject = submitObject;
				ExecuteEvents.Execute( submitObject, data, ExecuteEvents.submitHandler );

				submitObject = null;
			}

            // Reset Data, set Camera
            m_Data.Reset();
            m_Data.position = new Vector2(m_Camera.pixelWidth / 2, m_Camera.pixelHeight / 2);

            // Raycast
            eventSystem.RaycastAll(m_Data, m_RaycastResultCache);
            m_Data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
            m_CurrentObject = m_Data.pointerCurrentRaycast.gameObject;

            // Clear
            m_RaycastResultCache.Clear();

            // Hover
            HandlePointerExitAndEnter(m_Data, m_CurrentObject);

            // Press
            if (m_ClickAction.GetStateDown(m_targetSource))
                ProcessPress(m_Data);

            // Release
            if (m_ClickAction.GetStateUp(m_targetSource))
                ProcessRelease(m_Data);
        }

        public PointerEventData GetData()
        {
            return m_Data;
        }

        private void ProcessPress(PointerEventData data)
        {

        }

        private void ProcessRelease(PointerEventData data)
        {

        }
    }
}
