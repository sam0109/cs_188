/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
==============================================================================*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Vuforia
{
    /// <summary>
    /// This class serves both as an augmentation definition for an ImageTarget in the editor
    /// as well as a tracked image target result at runtime
    /// </summary>
    public class ImageTargetBehaviour : ImageTargetAbstractBehaviour
    {
		public GameObject TreeO;
		public GameObject House;
		public GameObject Necro;
		public GameObject Wall;
		public GameObject Blob;
		public Dropdown DropdownButton;


		public void Start()
		{

			Necro.SetActive(false);			
			House.SetActive(false);
			TreeO.SetActive (false);
			Wall.SetActive (false);

		}

		public void SetModel(int model)
		{
			switch (model)
			{
			case 1:
				House.SetActive (true);
				Blob.SetActive (false);
				break;
			case 2:
				Necro.SetActive (true);
				Blob.SetActive (false);
				break;
			case 3:
				TreeO.SetActive (true);
				Blob.SetActive (false);
				break;
			case 4:
				Wall.SetActive (true);
				Blob.SetActive (false);
				break;
			default:
				break;
			}
		}

		public void Update()
		{
			
		}

		
    }
}
