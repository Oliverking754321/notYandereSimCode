﻿using System;

namespace Pathfinding.Voxels
{
	// Token: 0x020005CB RID: 1483
	internal struct VoxelPolygonClipper
	{
		// Token: 0x06002813 RID: 10259 RVA: 0x001B9B54 File Offset: 0x001B7D54
		private void Init()
		{
			if (this.clipPolygonCache == null)
			{
				this.clipPolygonCache = new float[21];
				this.clipPolygonIntCache = new int[21];
			}
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x001B9B78 File Offset: 0x001B7D78
		public int ClipPolygon(float[] vIn, int n, float[] vOut, float multi, float offset, int axis)
		{
			this.Init();
			float[] array = this.clipPolygonCache;
			for (int i = 0; i < n; i++)
			{
				array[i] = multi * vIn[i * 3 + axis] + offset;
			}
			int num = 0;
			int j = 0;
			int num2 = n - 1;
			while (j < n)
			{
				bool flag = array[num2] >= 0f;
				bool flag2 = array[j] >= 0f;
				if (flag != flag2)
				{
					int num3 = num * 3;
					int num4 = j * 3;
					int num5 = num2 * 3;
					float num6 = array[num2] / (array[num2] - array[j]);
					vOut[num3] = vIn[num5] + (vIn[num4] - vIn[num5]) * num6;
					vOut[num3 + 1] = vIn[num5 + 1] + (vIn[num4 + 1] - vIn[num5 + 1]) * num6;
					vOut[num3 + 2] = vIn[num5 + 2] + (vIn[num4 + 2] - vIn[num5 + 2]) * num6;
					num++;
				}
				if (flag2)
				{
					int num7 = num * 3;
					int num8 = j * 3;
					vOut[num7] = vIn[num8];
					vOut[num7 + 1] = vIn[num8 + 1];
					vOut[num7 + 2] = vIn[num8 + 2];
					num++;
				}
				num2 = j;
				j++;
			}
			return num;
		}

		// Token: 0x06002815 RID: 10261 RVA: 0x001B9C94 File Offset: 0x001B7E94
		public int ClipPolygonY(float[] vIn, int n, float[] vOut, float multi, float offset, int axis)
		{
			this.Init();
			float[] array = this.clipPolygonCache;
			for (int i = 0; i < n; i++)
			{
				array[i] = multi * vIn[i * 3 + axis] + offset;
			}
			int num = 0;
			int j = 0;
			int num2 = n - 1;
			while (j < n)
			{
				bool flag = array[num2] >= 0f;
				bool flag2 = array[j] >= 0f;
				if (flag != flag2)
				{
					vOut[num * 3 + 1] = vIn[num2 * 3 + 1] + (vIn[j * 3 + 1] - vIn[num2 * 3 + 1]) * (array[num2] / (array[num2] - array[j]));
					num++;
				}
				if (flag2)
				{
					vOut[num * 3 + 1] = vIn[j * 3 + 1];
					num++;
				}
				num2 = j;
				j++;
			}
			return num;
		}

		// Token: 0x06002816 RID: 10262 RVA: 0x001B9D4C File Offset: 0x001B7F4C
		public int ClipPolygon(Int3[] vIn, int n, Int3[] vOut, int multi, int offset, int axis)
		{
			this.Init();
			int[] array = this.clipPolygonIntCache;
			for (int i = 0; i < n; i++)
			{
				array[i] = multi * vIn[i][axis] + offset;
			}
			int num = 0;
			int j = 0;
			int num2 = n - 1;
			while (j < n)
			{
				bool flag = array[num2] >= 0;
				bool flag2 = array[j] >= 0;
				if (flag != flag2)
				{
					double rhs = (double)array[num2] / (double)(array[num2] - array[j]);
					vOut[num] = vIn[num2] + (vIn[j] - vIn[num2]) * rhs;
					num++;
				}
				if (flag2)
				{
					vOut[num] = vIn[j];
					num++;
				}
				num2 = j;
				j++;
			}
			return num;
		}

		// Token: 0x04004311 RID: 17169
		private float[] clipPolygonCache;

		// Token: 0x04004312 RID: 17170
		private int[] clipPolygonIntCache;
	}
}
