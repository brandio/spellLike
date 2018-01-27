using UnityEngine;
using System.Collections;

public interface PathingObject {
	 PathFindingNode GetPosition();

    Transform GetTransform();
}
