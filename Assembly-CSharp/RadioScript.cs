﻿using System;
using UnityEngine;

// Token: 0x02000387 RID: 903
public class RadioScript : MonoBehaviour
{
	// Token: 0x06001991 RID: 6545 RVA: 0x000F87AC File Offset: 0x000F69AC
	private void Update()
	{
		if (base.transform.parent == null)
		{
			if (this.CooldownTimer > 0f)
			{
				this.CooldownTimer = Mathf.MoveTowards(this.CooldownTimer, 0f, Time.deltaTime);
				if (this.CooldownTimer == 0f)
				{
					this.Prompt.enabled = true;
				}
			}
			else
			{
				UISprite uisprite = this.Prompt.Circle[0];
				if (uisprite.fillAmount == 0f)
				{
					uisprite.fillAmount = 1f;
					if (!this.On)
					{
						this.Prompt.Label[0].text = "     Turn Off";
						this.MyRenderer.material.mainTexture = this.OnTexture;
						this.RadioNotes.SetActive(true);
						this.MyAudio.Play();
						this.On = true;
					}
					else
					{
						this.CooldownTimer = 1f;
						this.TurnOff();
					}
				}
			}
			if (this.On && this.Victim == null && this.AlarmDisc != null)
			{
				AlarmDiscScript component = UnityEngine.Object.Instantiate<GameObject>(this.AlarmDisc, base.transform.position + Vector3.up, Quaternion.identity).GetComponent<AlarmDiscScript>();
				component.SourceRadio = this;
				component.NoScream = true;
				component.Radio = true;
			}
		}
		else if (this.Prompt.enabled)
		{
			this.Prompt.enabled = false;
			this.Prompt.Hide();
		}
		if (this.Delinquent)
		{
			this.Proximity = 0;
			this.ID = 1;
			while (this.ID < 6)
			{
				if (this.StudentManager.Students[75 + this.ID] != null && Vector3.Distance(base.transform.position, this.StudentManager.Students[75 + this.ID].transform.position) < 1.1f)
				{
					if (!this.StudentManager.Students[75 + this.ID].Alarmed && !this.StudentManager.Students[75 + this.ID].Threatened && this.StudentManager.Students[75 + this.ID].Alive)
					{
						this.Proximity++;
					}
					else
					{
						this.Proximity = -100;
						this.ID = 5;
						this.MyAudio.Stop();
						this.Jukebox.ClubDip = 0f;
					}
				}
				this.ID++;
			}
			if (this.Proximity > 0)
			{
				if (!this.MyAudio.isPlaying)
				{
					this.MyAudio.Play();
				}
				float num = Vector3.Distance(this.Prompt.Yandere.transform.position, base.transform.position);
				if (num < 11f)
				{
					this.Jukebox.ClubDip = Mathf.MoveTowards(this.Jukebox.ClubDip, (10f - num) * 0.2f * this.Jukebox.Volume, Time.deltaTime);
					if (this.Jukebox.ClubDip < 0f)
					{
						this.Jukebox.ClubDip = 0f;
					}
					if (this.Jukebox.ClubDip > this.Jukebox.Volume)
					{
						this.Jukebox.ClubDip = this.Jukebox.Volume;
						return;
					}
				}
			}
			else if (this.MyAudio.isPlaying)
			{
				this.MyAudio.Stop();
				this.Jukebox.ClubDip = 0f;
			}
		}
	}

	// Token: 0x06001992 RID: 6546 RVA: 0x000F8B58 File Offset: 0x000F6D58
	public void TurnOff()
	{
		this.Prompt.Label[0].text = "     Turn On";
		this.Prompt.enabled = false;
		this.Prompt.Hide();
		this.MyRenderer.material.mainTexture = this.OffTexture;
		this.RadioNotes.SetActive(false);
		this.CooldownTimer = 1f;
		this.MyAudio.Stop();
		this.Victim = null;
		this.On = false;
	}

	// Token: 0x04002747 RID: 10055
	public StudentManagerScript StudentManager;

	// Token: 0x04002748 RID: 10056
	public JukeboxScript Jukebox;

	// Token: 0x04002749 RID: 10057
	public GameObject RadioNotes;

	// Token: 0x0400274A RID: 10058
	public GameObject AlarmDisc;

	// Token: 0x0400274B RID: 10059
	public AudioSource MyAudio;

	// Token: 0x0400274C RID: 10060
	public Renderer MyRenderer;

	// Token: 0x0400274D RID: 10061
	public Texture OffTexture;

	// Token: 0x0400274E RID: 10062
	public Texture OnTexture;

	// Token: 0x0400274F RID: 10063
	public StudentScript Victim;

	// Token: 0x04002750 RID: 10064
	public PromptScript Prompt;

	// Token: 0x04002751 RID: 10065
	public float CooldownTimer;

	// Token: 0x04002752 RID: 10066
	public bool Delinquent;

	// Token: 0x04002753 RID: 10067
	public bool On;

	// Token: 0x04002754 RID: 10068
	public int Proximity;

	// Token: 0x04002755 RID: 10069
	public int ID;
}
