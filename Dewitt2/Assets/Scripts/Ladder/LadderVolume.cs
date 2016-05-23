using UnityEngine;
using System.Collections;

public class LadderVolume : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		setPlayerCanClimb(other, true);
	}

	void OnTriggerExit(Collider other)
	{
		setPlayerCanClimb(other, false);
	}

	void setPlayerCanClimb(Collider other, bool canClimb)
	{
		if (other.tag == "Player")
		{
			Player player = other.GetComponent<Player>();
			if (player)
				player.setCanClimbLadder(canClimb);
		}
	}
}