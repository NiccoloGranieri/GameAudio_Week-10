// proximityTwo2Float.cs - Script to send a float to a PD patch determined by the
//						player's proximityTwo to a specific GameObject.
// -----------------------------------------------------------------------------
// Copyright (c) 2018 Niall Moody
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// -----------------------------------------------------------------------------
// note: changed the system for scaling the proximityTwo output to make it dependant on
// the Sensor Bounds object, for variable sizing -- jan5.2021, yann seznec

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Script to send a float to a PD patch determined by the player's proximityTwo to
/// a specific GameObject.
public class Proximity2FloatNew : MonoBehaviour
{

	// The Pd patch we'll be communicating with.
	public LibPdInstance pdPatch;
	// We'll use the transform of the red sphere to judge the player's proximityTwo.
	public Transform targetLocation;
	public Transform sensorBounds;

	private Transform player;

	void Start()
	{
		this.player = GameObject.FindWithTag("Player").transform;
	}

	/// All our calculations for this class take place in MonoBehaviour's
	/// Update() function.
	void Update()
	{
		//Get the distance between the sphere and the main camera.
		float proximityTwo = Vector3.Distance(targetLocation.position, this.player.position);
		float sensorDistance = sensorBounds.localScale.x;
		//We want proximityTwo to be in the range 0 -> 1.
		// calculate the proximityTwo based on the size of the sensor area 
		proximityTwo /= sensorDistance/2;
		//We also want the pitch to increase as we get closer to the sphere,
		//so we invert proximityTwo.
		proximityTwo = 1.0f - proximityTwo;
		
		if(proximityTwo < 0.0f)
			proximityTwo = 0.0f;

		proximityTwo /= 10.0f;
		//Send our frequency value to the PD patch.
		//Like in Button2Bang.cs/ButtonExample.pd, all we need to be able to
		//send floats to our PD patch is a named receive object in the patch (in
		//this case, named proximityTwo). We can then use the SendFloat() function
		//to send our float value to that named receive object.
		pdPatch.SendFloat("proximityTwo", proximityTwo);
	}
}