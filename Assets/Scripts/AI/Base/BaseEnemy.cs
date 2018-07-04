using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BaseEnemy : PoolObject {
	public override void OnObjectReuse()
	{
		gameObject.SetActive(true);
		Initialized();
	}

	protected override void Destroy()
	{
		gameObject.SetActive(false);
	}

	

	public virtual void Initialized(){}

	#region Flashing
	private Material _mat;
	private IEnumerator _flashCoroutine;
	protected void doFlash(){
		if(!_mat) _mat = GetComponent<SpriteRenderer>().material;

		if(_flashCoroutine != null)
		{	
			StopCoroutine(_flashCoroutine);
		}

		_flashCoroutine = Flashing();
		StartCoroutine(Flashing());
	}	

	private IEnumerator Flashing()
	{
		_mat.SetFloat("_FlashAmount", 1.0f);
		_mat.SetFloat("_IsFlash2", 0.0f);
		yield return new WaitForSeconds(0.05f);
		_mat.SetFloat("_FlashAmount", 0.0f);
		yield return new WaitForSeconds(0.05f);
		_mat.SetFloat("_FlashAmount", 1.0f);
		_mat.SetFloat("_IsFlash2", 1.0f);
		yield return new WaitForSeconds(0.05f);
		_mat.SetFloat("_FlashAmount", 0.0f);
		yield return new WaitForSeconds(0.05f);
		_mat.SetFloat("_FlashAmount", 1.0f);
		_mat.SetFloat("_IsFlash2", 0.0f);
		yield return new WaitForSeconds(0.05f);
		_mat.SetFloat("_FlashAmount", 0.0f);
	}

	#endregion
}
