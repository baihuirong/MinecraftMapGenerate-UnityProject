using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using Uniblocks;

public class BlockManager : MonoBehaviour {

	public int range = 10;

	private ushort blockID = 0;
	private Transform selectedBlockEffect;
	// Use this for initialization
	void Start () {
		selectedBlockEffect = GameObject.Find("selected block graphics").transform;
		selectedBlockEffect.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
		SelectBlockID();

		VoxelInfo info = Engine.VoxelRaycast(Camera.main.transform.position, Camera.main.transform.forward, 10, false);
		if (info != null)
		{
			//print(info.index);
			if( Input.GetMouseButtonDown(0))
			{
				Voxel.DestroyBlock(info);
			}
			if (Input.GetMouseButtonDown(1))
			{
				VoxelInfo newInfo = new VoxelInfo(info.adjacentIndex, info.chunk);
				Voxel.PlaceBlock(newInfo, blockID);
			}
		}
		UpdateSelectedBlockEffect(info);
	}

	private void SelectBlockID()
	{
		for(ushort i = 1; i < 10; i++)
		{
			if (Input.GetKeyDown(i.ToString()))
			{
				blockID = i;
			}
		}
	}
	private void UpdateSelectedBlockEffect(VoxelInfo info)
	{
		if (info != null)
		{
			selectedBlockEffect.gameObject.SetActive(true);
			selectedBlockEffect.position = info.chunk.VoxelIndexToPosition(info.index);
		}
		else
		{
			selectedBlockEffect.gameObject.SetActive(false);
		}
	}
}
