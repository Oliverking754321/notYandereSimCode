﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020000D1 RID: 209
public class ArmDetectorScript : MonoBehaviour
{
	// Token: 0x06000A1C RID: 2588 RVA: 0x0005078C File Offset: 0x0004E98C
	private void Start()
	{
		this.DemonDress.SetActive(false);
	}

	// Token: 0x06000A1D RID: 2589 RVA: 0x0005079C File Offset: 0x0004E99C
	private void Update()
	{
		if (!this.SummonDemon)
		{
			for (int i = 1; i < this.ArmArray.Length; i++)
			{
				if (this.ArmArray[i] != null && this.ArmArray[i].transform.parent != this.LimbParent)
				{
					this.ArmArray[i] = null;
					if (i != this.ArmArray.Length - 1)
					{
						this.Shuffle(i);
					}
					this.Arms--;
					Debug.Log("Decrement arm count?");
				}
			}
			if (this.Arms > 9)
			{
				this.Yandere.Character.GetComponent<Animation>().CrossFade(this.Yandere.IdleAnim);
				this.Yandere.CanMove = false;
				this.SummonDemon = true;
				this.MyAudio.Play();
				this.Arms = 0;
			}
		}
		if (!this.SummonFlameDemon)
		{
			this.CorpsesCounted = 0;
			this.Sacrifices = 0;
			int num = 0;
			while (this.CorpsesCounted < this.Police.Corpses)
			{
				RagdollScript ragdollScript = this.Police.CorpseList[num];
				if (ragdollScript != null)
				{
					this.CorpsesCounted++;
					if (ragdollScript.Burned && ragdollScript.Sacrifice && !ragdollScript.Dragged && !ragdollScript.Carried)
					{
						this.Sacrifices++;
					}
				}
				num++;
			}
			if (this.Sacrifices > 4 && !this.Yandere.Chased && this.Yandere.Chasers == 0)
			{
				this.Yandere.Character.GetComponent<Animation>().CrossFade(this.Yandere.IdleAnim);
				this.Yandere.CanMove = false;
				this.SummonFlameDemon = true;
				this.MyAudio.Play();
			}
		}
		if (!this.SummonEmptyDemon && this.Bodies > 10 && !this.Yandere.Chased && this.Yandere.Chasers == 0)
		{
			this.Yandere.Character.GetComponent<Animation>().CrossFade(this.Yandere.IdleAnim);
			this.Yandere.CanMove = false;
			this.SummonEmptyDemon = true;
			this.MyAudio.Play();
		}
		if (this.SummonDemon)
		{
			if (this.Phase == 1)
			{
				if (this.ArmArray[1] != null)
				{
					for (int j = 1; j < 11; j++)
					{
						if (this.ArmArray[j] != null)
						{
							UnityEngine.Object.Instantiate<GameObject>(this.SmallDarkAura, this.ArmArray[j].transform.position, Quaternion.identity);
							UnityEngine.Object.Destroy(this.ArmArray[j]);
						}
					}
				}
				this.Timer += Time.deltaTime;
				if (this.Timer > 1f)
				{
					this.Timer = 0f;
					this.Phase++;
				}
			}
			else if (this.Phase == 2)
			{
				this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime));
				this.Jukebox.Volume = Mathf.MoveTowards(this.Jukebox.Volume, 0f, Time.deltaTime);
				if (this.Darkness.color.a == 1f)
				{
					SchoolGlobals.SchoolAtmosphere = 0f;
					this.StudentManager.SetAtmosphere();
					this.Yandere.transform.eulerAngles = new Vector3(0f, 180f, 0f);
					this.Yandere.transform.position = new Vector3(12f, 0.1f, 26f);
					this.DemonSubtitle.text = "...revenge...at last...";
					this.BloodProjector.SetActive(true);
					this.DemonSubtitle.color = new Color(this.DemonSubtitle.color.r, this.DemonSubtitle.color.g, this.DemonSubtitle.color.b, 0f);
					this.Skull.Prompt.Hide();
					this.Skull.Prompt.enabled = false;
					this.Skull.enabled = false;
					this.MyAudio.clip = this.DemonLine;
					this.MyAudio.Play();
					this.Phase++;
				}
			}
			else if (this.Phase == 3)
			{
				this.DemonSubtitle.transform.localPosition = new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f));
				this.DemonSubtitle.color = new Color(this.DemonSubtitle.color.r, this.DemonSubtitle.color.g, this.DemonSubtitle.color.b, Mathf.MoveTowards(this.DemonSubtitle.color.a, 1f, Time.deltaTime));
				if (this.DemonSubtitle.color.a == 1f && Input.GetButtonDown("A"))
				{
					this.Phase++;
				}
			}
			else if (this.Phase == 4)
			{
				this.DemonSubtitle.transform.localPosition = new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f));
				this.DemonSubtitle.color = new Color(this.DemonSubtitle.color.r, this.DemonSubtitle.color.g, this.DemonSubtitle.color.b, Mathf.MoveTowards(this.DemonSubtitle.color.a, 0f, Time.deltaTime));
				if (this.DemonSubtitle.color.a == 0f)
				{
					this.MyAudio.clip = this.DemonMusic;
					this.MyAudio.loop = true;
					this.MyAudio.Play();
					this.DemonSubtitle.text = string.Empty;
					this.Phase++;
				}
			}
			else if (this.Phase == 5)
			{
				this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime));
				if (this.Darkness.color.a == 0f)
				{
					this.Yandere.Character.GetComponent<Animation>().CrossFade("f02_demonSummon_00");
					this.Phase++;
				}
			}
			else if (this.Phase == 6)
			{
				this.Timer += Time.deltaTime;
				if (this.Timer > (float)this.ArmsSpawned)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.DemonArm, this.SpawnPoints[this.ArmsSpawned].position, Quaternion.identity);
					gameObject.transform.parent = this.Yandere.transform;
					gameObject.transform.LookAt(this.Yandere.transform);
					gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y + 180f, gameObject.transform.localEulerAngles.z);
					this.ArmsSpawned++;
					gameObject.GetComponent<DemonArmScript>().IdleAnim = ((this.ArmsSpawned % 2 == 1) ? "DemonArmIdleOld" : "DemonArmIdle");
				}
				if (this.ArmsSpawned == 10)
				{
					this.Yandere.CanMove = true;
					this.Yandere.IdleAnim = "f02_demonIdle_00";
					this.Yandere.WalkAnim = "f02_demonWalk_00";
					this.Yandere.RunAnim = "f02_demonRun_00";
					this.Yandere.Demonic = true;
					this.SummonDemon = false;
				}
			}
		}
		if (this.SummonFlameDemon)
		{
			if (this.Phase == 1)
			{
				foreach (RagdollScript ragdollScript2 in this.Police.CorpseList)
				{
					if (ragdollScript2 != null && ragdollScript2.Burned && ragdollScript2.Sacrifice && !ragdollScript2.Dragged && !ragdollScript2.Carried)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.SmallDarkAura, ragdollScript2.Prompt.transform.position, Quaternion.identity);
						UnityEngine.Object.Destroy(ragdollScript2.gameObject);
						this.Yandere.NearBodies--;
						this.Police.Corpses--;
					}
				}
				this.Phase++;
			}
			else if (this.Phase == 2)
			{
				this.Timer += Time.deltaTime;
				if (this.Timer > 1f)
				{
					this.Timer = 0f;
					this.Phase++;
				}
			}
			else if (this.Phase == 3)
			{
				this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime));
				this.Jukebox.Volume = Mathf.MoveTowards(this.Jukebox.Volume, 0f, Time.deltaTime);
				if (this.Darkness.color.a == 1f)
				{
					this.Yandere.transform.eulerAngles = new Vector3(0f, 180f, 0f);
					this.Yandere.transform.position = new Vector3(12f, 0.1f, 26f);
					this.DemonSubtitle.text = "You have proven your worth. Very well. I shall lend you my power.";
					this.DemonSubtitle.color = new Color(1f, 0f, 0f, 0f);
					this.Skull.Prompt.Hide();
					this.Skull.Prompt.enabled = false;
					this.Skull.enabled = false;
					this.MyAudio.clip = this.FlameDemonLine;
					this.MyAudio.Play();
					this.Phase++;
				}
			}
			else if (this.Phase == 4)
			{
				this.DemonSubtitle.transform.localPosition = new Vector3(UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f));
				this.DemonSubtitle.color = new Color(this.DemonSubtitle.color.r, this.DemonSubtitle.color.g, this.DemonSubtitle.color.b, Mathf.MoveTowards(this.DemonSubtitle.color.a, 1f, Time.deltaTime));
				if (this.DemonSubtitle.color.a == 1f && Input.GetButtonDown("A"))
				{
					this.Phase++;
				}
			}
			else if (this.Phase == 5)
			{
				this.DemonSubtitle.transform.localPosition = new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f));
				this.DemonSubtitle.color = new Color(this.DemonSubtitle.color.r, this.DemonSubtitle.color.g, this.DemonSubtitle.color.b, Mathf.MoveTowards(this.DemonSubtitle.color.a, 0f, Time.deltaTime));
				if (this.DemonSubtitle.color.a == 0f)
				{
					this.DemonDress.SetActive(true);
					this.Yandere.MyRenderer.sharedMesh = this.FlameDemonMesh;
					this.RiggedAccessory.SetActive(true);
					this.Yandere.FlameDemonic = true;
					this.Yandere.Stance.Current = StanceType.Standing;
					this.Yandere.Sanity = 100f;
					this.Yandere.MyRenderer.materials[0].mainTexture = this.Yandere.FaceTexture;
					this.Yandere.MyRenderer.materials[1].mainTexture = this.Yandere.NudePanties;
					this.Yandere.MyRenderer.materials[2].mainTexture = this.Yandere.NudePanties;
					this.DebugMenu.UpdateCensor();
					this.MyAudio.clip = this.DemonMusic;
					this.MyAudio.loop = true;
					this.MyAudio.Play();
					this.DemonSubtitle.text = string.Empty;
					this.Phase++;
				}
			}
			else if (this.Phase == 6)
			{
				this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime));
				if (this.Darkness.color.a == 0f)
				{
					this.Yandere.Character.GetComponent<Animation>().CrossFade("f02_demonSummon_00");
					this.Phase++;
				}
			}
			else if (this.Phase == 7)
			{
				this.Timer += Time.deltaTime;
				if (this.Timer > 5f)
				{
					this.MyAudio.PlayOneShot(this.FlameActivate);
					this.RightFlame.SetActive(true);
					this.LeftFlame.SetActive(true);
					this.Phase++;
				}
			}
			else if (this.Phase == 8)
			{
				this.Timer += Time.deltaTime;
				if (this.Timer > 10f)
				{
					this.Yandere.CanMove = true;
					this.Yandere.IdleAnim = "f02_demonIdle_00";
					this.Yandere.WalkAnim = "f02_demonWalk_00";
					this.Yandere.RunAnim = "f02_demonRun_00";
					this.SummonFlameDemon = false;
				}
			}
		}
		if (this.SummonEmptyDemon)
		{
			if (this.Phase == 1)
			{
				if (this.BodyArray[1] != null)
				{
					for (int l = 1; l < 12; l++)
					{
						if (this.BodyArray[l] != null)
						{
							UnityEngine.Object.Instantiate<GameObject>(this.SmallDarkAura, this.BodyArray[l].transform.position, Quaternion.identity);
							UnityEngine.Object.Destroy(this.BodyArray[l]);
						}
					}
				}
				this.Timer += Time.deltaTime;
				if (this.Timer > 1f)
				{
					this.Timer = 0f;
					this.Phase++;
					return;
				}
			}
			else if (this.Phase == 2)
			{
				this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime));
				this.Jukebox.Volume = Mathf.MoveTowards(this.Jukebox.Volume, 0f, Time.deltaTime);
				if (this.Darkness.color.a == 1f)
				{
					this.Yandere.transform.eulerAngles = new Vector3(0f, 180f, 0f);
					this.Yandere.transform.position = new Vector3(12f, 0.1f, 26f);
					this.DemonSubtitle.text = "At last...it is time to reclaim our rightful place.";
					this.BloodProjector.SetActive(true);
					this.DemonSubtitle.color = new Color(this.DemonSubtitle.color.r, this.DemonSubtitle.color.g, this.DemonSubtitle.color.b, 0f);
					this.Skull.Prompt.Hide();
					this.Skull.Prompt.enabled = false;
					this.Skull.enabled = false;
					this.MyAudio.clip = this.EmptyDemonLine;
					this.MyAudio.Play();
					this.Phase++;
					return;
				}
			}
			else if (this.Phase == 3)
			{
				this.DemonSubtitle.transform.localPosition = new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f));
				this.DemonSubtitle.color = new Color(this.DemonSubtitle.color.r, this.DemonSubtitle.color.g, this.DemonSubtitle.color.b, Mathf.MoveTowards(this.DemonSubtitle.color.a, 1f, Time.deltaTime));
				if (this.DemonSubtitle.color.a == 1f && Input.GetButtonDown("A"))
				{
					this.Phase++;
					return;
				}
			}
			else if (this.Phase == 4)
			{
				this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime));
				if (this.Darkness.color.a == 1f)
				{
					GameGlobals.EmptyDemon = true;
					SceneManager.LoadScene("LoadingScene");
				}
			}
		}
	}

	// Token: 0x06000A1E RID: 2590 RVA: 0x00051B0C File Offset: 0x0004FD0C
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.parent == this.LimbParent)
		{
			PickUpScript component = other.gameObject.GetComponent<PickUpScript>();
			if (component != null)
			{
				BodyPartScript bodyPart = component.BodyPart;
				if (bodyPart.Sacrifice && (bodyPart.Type == 3 || bodyPart.Type == 4))
				{
					bool flag = true;
					for (int i = 1; i < 11; i++)
					{
						if (this.ArmArray[i] == other.gameObject)
						{
							flag = false;
						}
					}
					if (flag)
					{
						this.Arms++;
						if (this.Arms < this.ArmArray.Length)
						{
							this.ArmArray[this.Arms] = other.gameObject;
						}
					}
				}
			}
		}
		if (other.transform.parent != null && other.transform.parent.parent != null && other.transform.parent.parent.parent != null)
		{
			StudentScript component2 = other.transform.parent.parent.parent.gameObject.GetComponent<StudentScript>();
			if (component2 != null && component2.Ragdoll.Sacrifice && component2.Armband.activeInHierarchy)
			{
				bool flag2 = true;
				for (int j = 1; j < 11; j++)
				{
					if (this.BodyArray[j] == other.gameObject)
					{
						flag2 = false;
					}
				}
				if (flag2)
				{
					this.Bodies++;
					if (this.Bodies < this.BodyArray.Length)
					{
						this.BodyArray[this.Bodies] = other.gameObject;
					}
				}
			}
		}
	}

	// Token: 0x06000A1F RID: 2591 RVA: 0x00051CC0 File Offset: 0x0004FEC0
	private void OnTriggerExit(Collider other)
	{
		PickUpScript component = other.gameObject.GetComponent<PickUpScript>();
		if (component != null && component.BodyPart && other.gameObject.GetComponent<BodyPartScript>().Sacrifice && (other.gameObject.name == "FemaleRightArm(Clone)" || other.gameObject.name == "FemaleLeftArm(Clone)" || other.gameObject.name == "MaleRightArm(Clone)" || other.gameObject.name == "MaleLeftArm(Clone)" || other.gameObject.name == "SacrificialArm(Clone)"))
		{
			this.Arms--;
			Debug.Log("Decrement arm count again?");
		}
		if (other.transform.parent != null && other.transform.parent.parent != null && other.transform.parent.parent.parent != null)
		{
			StudentScript component2 = other.transform.parent.parent.parent.gameObject.GetComponent<StudentScript>();
			if (component2 != null && component2.Ragdoll.Sacrifice && component2.Armband.activeInHierarchy)
			{
				this.Bodies--;
			}
		}
	}

	// Token: 0x06000A20 RID: 2592 RVA: 0x00051E34 File Offset: 0x00050034
	private void Shuffle(int Start)
	{
		for (int i = Start; i < this.ArmArray.Length - 1; i++)
		{
			this.ArmArray[i] = this.ArmArray[i + 1];
		}
	}

	// Token: 0x06000A21 RID: 2593 RVA: 0x00051E68 File Offset: 0x00050068
	private void ShuffleBodies(int Start)
	{
		for (int i = Start; i < this.BodyArray.Length - 1; i++)
		{
			this.BodyArray[i] = this.BodyArray[i + 1];
		}
	}

	// Token: 0x04000A2C RID: 2604
	public StudentManagerScript StudentManager;

	// Token: 0x04000A2D RID: 2605
	public DebugMenuScript DebugMenu;

	// Token: 0x04000A2E RID: 2606
	public JukeboxScript Jukebox;

	// Token: 0x04000A2F RID: 2607
	public YandereScript Yandere;

	// Token: 0x04000A30 RID: 2608
	public PoliceScript Police;

	// Token: 0x04000A31 RID: 2609
	public SkullScript Skull;

	// Token: 0x04000A32 RID: 2610
	public UILabel DemonSubtitle;

	// Token: 0x04000A33 RID: 2611
	public UISprite Darkness;

	// Token: 0x04000A34 RID: 2612
	public Transform LimbParent;

	// Token: 0x04000A35 RID: 2613
	public Transform[] SpawnPoints;

	// Token: 0x04000A36 RID: 2614
	public GameObject[] BodyArray;

	// Token: 0x04000A37 RID: 2615
	public GameObject[] ArmArray;

	// Token: 0x04000A38 RID: 2616
	public GameObject RiggedAccessory;

	// Token: 0x04000A39 RID: 2617
	public GameObject BloodProjector;

	// Token: 0x04000A3A RID: 2618
	public GameObject SmallDarkAura;

	// Token: 0x04000A3B RID: 2619
	public GameObject DemonDress;

	// Token: 0x04000A3C RID: 2620
	public GameObject RightFlame;

	// Token: 0x04000A3D RID: 2621
	public GameObject LeftFlame;

	// Token: 0x04000A3E RID: 2622
	public GameObject DemonArm;

	// Token: 0x04000A3F RID: 2623
	public bool SummonEmptyDemon;

	// Token: 0x04000A40 RID: 2624
	public bool SummonFlameDemon;

	// Token: 0x04000A41 RID: 2625
	public bool SummonDemon;

	// Token: 0x04000A42 RID: 2626
	public Mesh FlameDemonMesh;

	// Token: 0x04000A43 RID: 2627
	public int CorpsesCounted;

	// Token: 0x04000A44 RID: 2628
	public int ArmsSpawned;

	// Token: 0x04000A45 RID: 2629
	public int Sacrifices;

	// Token: 0x04000A46 RID: 2630
	public int Phase = 1;

	// Token: 0x04000A47 RID: 2631
	public int Bodies;

	// Token: 0x04000A48 RID: 2632
	public int Arms;

	// Token: 0x04000A49 RID: 2633
	public float Timer;

	// Token: 0x04000A4A RID: 2634
	public AudioClip FlameDemonLine;

	// Token: 0x04000A4B RID: 2635
	public AudioClip FlameActivate;

	// Token: 0x04000A4C RID: 2636
	public AudioClip DemonMusic;

	// Token: 0x04000A4D RID: 2637
	public AudioClip DemonLine;

	// Token: 0x04000A4E RID: 2638
	public AudioClip EmptyDemonLine;

	// Token: 0x04000A4F RID: 2639
	public AudioSource MyAudio;
}
