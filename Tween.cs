using UnityEngine;

public class Tween: MonoBehaviour
{
	private Vector3 posTo;
	private Vector3 posFrom;
	private Vector3 posDif;
	private bool move = false;
	private float time;
	public float duration = 1f;
	public enum modes { linear, sine, exponential, quadratic, cubic, quartic, quintic, circular, elastic, nearest, test, remapCurve1, remapCurve2, remapCurve3, remapCurve4, remapCurve5, remapCurve6, remapCurve7, remapCurve8 }
	public modes mode;
	public AnimationCurve remapCurve1;
	public AnimationCurve remapCurve2;
	public AnimationCurve remapCurve3;
	public AnimationCurve remapCurve4;
	public AnimationCurve remapCurve5;
	public AnimationCurve remapCurve6;
	public AnimationCurve remapCurve7;
	public AnimationCurve remapCurve8;

	public AnimationCurve xCurve;
	public AnimationCurve yCurve;

	// Use this for initialization
	void Start()
	{
		posFrom = this.transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetMouseButtonUp(0))
		{
			Vector3 mousePos = Input.mousePosition;
			posTo = mousePos;
			posFrom = transform.position;
			posDif = posTo - posFrom;
			move = true;
			time = 0;
		}

		if(move)
		{
			transform.position = Move(time, posFrom, posDif, duration);
			time += Time.deltaTime;
		}
		if(time > duration)
		{
			time = 0f;
			move = false;
			posFrom = transform.position;
		}
	}

	Vector3 Move(float t, Vector2 b, Vector2 c, float d)
	{
		Vector2 pos = transform.position;
		//t=current time
		//b=start value
		//c=change value
		//d=duration
		switch(mode)
		{
			case modes.linear:
				pos = c * t / d + b;
				break;
			case modes.sine:
				pos = -c / 2 * (Mathf.Cos(Mathf.PI * t / d) - 1) + b;
				break;
			case modes.exponential:
				t /= d / 2;
				if(t < 1) return c / 2 * Mathf.Pow(2, 10 * (t - 1)) + b;
				t--;
				pos = c / 2 * (-Mathf.Pow(2, -10 * t) + 2) + b;
				break;
			case modes.quadratic:
				t /= d / 2;
				if(t < 1) return c / 2 * t * t + b;
				t--;
				pos = -c / 2 * (t * (t - 2) - 1) + b;
				break;
			case modes.cubic:
				t /= d / 2;
				if(t < 1) return c / 2 * t * t * t + b;
				t -= 2;
				pos = c / 2 * (t * t * t + 2) + b;
				break;
			case modes.quartic:
				t /= d / 2;
				if(t < 1) return c / 2 * t * t * t * t + b;
				t -= 2;
				pos = -c / 2 * (t * t * t * t - 2) + b;
				break;
			case modes.quintic:
				t /= d / 2;
				if(t < 1) return c / 2 * t * t * t * t * t + b;
				t -= 2;
				pos = c / 2 * (t * t * t * t * t + 2) + b;
				break;
			case modes.circular:
				t /= d / 2;
				if(t < 1)
				{
					pos = -c / 2 * (Mathf.Sqrt(1 - t * t) - 1) + b;
				}
				else
				{
					t -= 2;
					pos = c / 2 * (Mathf.Sqrt(1 - t * t) + 1) + b;
				}
				break;
			case modes.elastic:
				if((t /= d / 2) == 2)
				{
					pos = b + c;
				}
				else
				{
					float p = d * (.3f * 1.5f);
					float s = p / 4;
					if(t < 1f)
					{
						pos = -.5f * (c * Mathf.Pow(2, 10 * (t -= 1)) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p)) + b;
					}
					else
					{
						pos = c * Mathf.Pow(2, -10 * (t -= 1)) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p) * .5f + c + b;
					}
				}
				break;
			case modes.nearest:
				pos = c * Mathf.Round(t / d) + b;
				break;
			case modes.test:
				pos = c * Mathf.Ceil(t / d) + b;
				break;
			case modes.remapCurve1:
				pos = UseRemapCurve(remapCurve1, b, c, t, d);
				break;
			case modes.remapCurve2:
				pos = UseRemapCurve(remapCurve2, b, c, t, d);
				break;
			case modes.remapCurve3:
				pos = UseRemapCurve(remapCurve3, b, c, t, d);
				break;
			case modes.remapCurve4:
				pos = UseRemapCurve(remapCurve4, b, c, t, d);
				break;
			case modes.remapCurve5:
				pos = UseRemapCurve(remapCurve5, b, c, t, d);
				break;
			case modes.remapCurve6:
				pos = UseRemapCurve(remapCurve6, b, c, t, d);
				break;
			case modes.remapCurve7:
				pos = UseRemapCurve(remapCurve7, b, c, t, d);
				break;
			case modes.remapCurve8:
				pos = UseRemapCurve(remapCurve8, b, c, t, d);
				break;
			default:
				pos = c * t / d + b; //defaults to linear interpolation
				break;
		}

		Vector2 posDiffNow = b - pos;
		float posPercent = Mathf.Abs(posDiffNow.x / c.x);
		Vector2 posNext = b + Vector2.Scale(c, new Vector2(xCurve.Evaluate(posPercent), yCurve.Evaluate(posPercent)));
		Debug.Log(posNext);

		return posNext;
	}

	Vector3 UseRemapCurve(AnimationCurve rc, Vector3 b, Vector3 c, float t, float d)
	{
		float percent;
		percent = rc.Evaluate(t / d);
		return b + Vector3.Scale(c, new Vector3(percent, percent, percent));
	}
}
