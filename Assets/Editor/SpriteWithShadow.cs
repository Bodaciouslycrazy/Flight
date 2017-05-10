using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpriteWithShadow : MonoBehaviour {

	[MenuItem("Create/Shadow-Sprite", false, -1)]
	[MenuItem("GameObject/Create Shadow-Sprite", false, -1)]
	static void CreateCustomGO(MenuCommand menuCommand)
	{
		GameObject Pref = Resources.Load<GameObject>("Sprite");

		GameObject go = Instantiate(Pref);
		// Ensure it gets reparented if this was a context click (otherwise does nothing)
		GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
		// Register the creation in the undo system
		Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
		Selection.activeObject = go;
	}
}
