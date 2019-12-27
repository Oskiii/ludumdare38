using UnityEngine;
using System;
using System.Threading;
using System.Collections.Generic;

public static class MyExtensions
{
	public static void Shuffle<T>(this IList<T> list)
	{
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}

	public static double GetLuminance(this Color color){
		return (0.2126 * color.r + 0.7152 * color.b + 0.0722 * color.b);
	}

	public static float Scale(this float value, float min, float max, float minScale, float maxScale){
		return (float)(minScale + (double)(value - min)/(max-min) * (maxScale - minScale));
	}

	public static int Scale(this int value, int min, int max, int minScale, int maxScale){
		return Mathf.RoundToInt ((float)(minScale + (double)(value - min)/(max-min) * (maxScale - minScale)));
	}

	public static Color ToColor(this int HexVal)
	{
		byte R = (byte)((HexVal >> 16) & 0xFF);
		byte G = (byte)((HexVal >> 8) & 0xFF);
		byte B = (byte)((HexVal) & 0xFF);
		return new Color(R, G, B, 255);
	}

	public static List<AudioSource> playAtPointAudioSources;
	public static void PlayAtPoint(this AudioClip clip, Vector3 position,  float pitch = 1, float volume = 1) {
		var audioSources = playAtPointAudioSources ?? (playAtPointAudioSources = new List<AudioSource>());
		AudioSource availableAudioSource = audioSources.Find(audioSource => !audioSource.isPlaying);
		if (!availableAudioSource) {
			audioSources.Add(availableAudioSource = new GameObject().AddComponent<AudioSource>());
			availableAudioSource.gameObject.hideFlags = HideFlags.HideInHierarchy;
		}
		availableAudioSource.clip = clip;
		availableAudioSource.transform.position = position;
		availableAudioSource.volume = volume;
		availableAudioSource.pitch = pitch;
		availableAudioSource.Play();       
	}

	public static List<T> Splice<T>(this List<T> list, int index, int count)
      {
          List<T> range = list.GetRange(index, count);
          list.RemoveRange(index, count);
          return range;
      }

}

public static class ThreadSafeRandom
{
	[ThreadStatic] private static System.Random Local;

	public static System.Random ThisThreadsRandom
	{
		get { return Local ?? (Local = new System.Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
	}
}
