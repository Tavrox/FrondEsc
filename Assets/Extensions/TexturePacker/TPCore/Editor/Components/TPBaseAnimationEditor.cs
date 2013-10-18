////////////////////////////////////////////////////////////////////////////////
//  
// @module Texture Packer Plugin for Unity3D 
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class TPBaseAnimationEditor : Editor {

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------
	
	public void DrawAnimationInfo() {
		EditorGUILayout.Separator();
		
		int totalFrames = tpAnim.lastFrameIndex + 1;
		float duration = totalFrames * (1f / tpAnim.frameRate);
		EditorGUILayout.HelpBox("Total Frames: " + totalFrames + ", Duration: " + duration + " sec", MessageType.Info);
		
		EditorGUILayout.Separator();
	}
	
	public void DrawBaseAnimationGUI() {
		EditorGUI.BeginChangeCheck();
		tpAnim.currentFrame = EditorGUILayout.IntSlider("Current Frame", tpAnim.currentFrame, 0, tpAnim.lastFrameIndex);
		if (EditorGUI.EndChangeCheck ()) {
			tpAnim.OnEditorFrameChange ();
		}
		

		tpAnim.frameRate = EditorGUILayout.IntSlider("Frame Rate", tpAnim.frameRate, 1, 50);


	

		tpAnim.PlayOnStart = EditorGUILayout.Toggle ("Play On Start", tpAnim.PlayOnStart);
		tpAnim.Loop = EditorGUILayout.Toggle ("Loop", tpAnim.Loop);

	}
	
	public void DrawButtonsSection() {

		EditorGUI.BeginChangeCheck();
		tpAnim.useCollider = EditorGUILayout.Toggle ("Use Collider", tpAnim.useCollider);
		if (EditorGUI.EndChangeCheck ()) {
			tpAnim.OnColliderSettingsChange();
		}
		
		EditorGUILayout.Separator();
		
		EditorGUI.BeginChangeCheck();
		
		tpAnim.pivotCenterX = EditorGUILayout.Slider("Pivot Center X", tpAnim.pivotCenterX, 0f, 1f);
		tpAnim.pivotCenterY = EditorGUILayout.Slider("Pivot Center Y", tpAnim.pivotCenterY, 0f, 1f);
		
		if (EditorGUI.EndChangeCheck ()) {
			tpAnim.OnEditorFrameChange ();
		}
		
		
		EditorGUILayout.Separator();
		EditorGUILayout.Separator();

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		
		
		if(GUILayout.Button(new GUIContent("Add"),   GUILayout.Width(80))) {
			EditorWindow.GetWindow<TexturePackerEditor>();
	    }
		
		
		if(GUILayout.Button(new GUIContent("Clear"),   GUILayout.Width(80))) {
			tpAnim.ClearFrames();
			tpAnim.OnEditorFrameChange ();
	    }
		
		if(GUILayout.Button(new GUIContent("Update"),   GUILayout.Width(80))) {
			tpAnim.OnEditorFrameChange ();
	    }
		
		
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Separator();
	}
	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------
	
	public TPBaseAnimation tpAnim {
		get {
			return target as TPBaseAnimation;
		}
	}
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------
	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------
	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------


}
