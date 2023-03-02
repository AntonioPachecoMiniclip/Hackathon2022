// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.Leaderboards
{
	/// <summary>
	/// Input parameters for the <see cref="LeaderboardsInterface.CopyLeaderboardUserScoreByIndex" /> function.
	/// </summary>
	public class CopyLeaderboardUserScoreByIndexOptions
	{
		/// <summary>
		/// Index of the sorted leaderboard user score to retrieve from the cache.
		/// </summary>
		public uint LeaderboardUserScoreIndex { get; set; }

		/// <summary>
		/// Name of the stat used to rank the leaderboard.
		/// </summary>
		public string StatName { get; set; }
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	internal struct CopyLeaderboardUserScoreByIndexOptionsInternal : ISettable, System.IDisposable
	{
		private int m_ApiVersion;
		private uint m_LeaderboardUserScoreIndex;
		private System.IntPtr m_StatName;

		public uint LeaderboardUserScoreIndex
		{
			set
			{
				m_LeaderboardUserScoreIndex = value;
			}
		}

		public string StatName
		{
			set
			{
				Helper.TryMarshalSet(ref m_StatName, value);
			}
		}

		public void Set(CopyLeaderboardUserScoreByIndexOptions other)
		{
			if (other != null)
			{
				m_ApiVersion = LeaderboardsInterface.CopyleaderboarduserscorebyindexApiLatest;
				LeaderboardUserScoreIndex = other.LeaderboardUserScoreIndex;
				StatName = other.StatName;
			}
		}

		public void Set(object other)
		{
			Set(other as CopyLeaderboardUserScoreByIndexOptions);
		}

		public void Dispose()
		{
			Helper.TryMarshalDispose(ref m_StatName);
		}
	}
}