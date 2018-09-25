using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreath : Bullet {
	public float growth = 1f;
	public int frame = 25;
	private Vector3 _scale = Vector3.zero, _scaleGrowth;
	private float _time, _growth = 1f;

	protected override void VAwake() {
		_scale = transform.localScale;
		_time = frame * 0.125f;
		lifetime = _time;
	}

	protected override void VUpdate() {
		if(_growth < growth) {
			_growth = Mathf.Lerp(_growth, growth + 0.00001f, _time * Time.deltaTime);
			_scaleGrowth = _scale + (_scale * _growth);
		} else {
			Destroy();
		}

		// Debug.Log(_growth);

		transform.localScale = Vector3.Lerp(transform.localScale, _scaleGrowth, Time.deltaTime);
	}
}
