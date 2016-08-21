using System.Collections.Generic/*<List>*/;
using UnityEngine/*<Mathf>*/;

namespace Finegamedesign.Utils
{
	// Record and playback real-time actions.
	public sealed class Journal
	{
		public string action = null;
		public string actionNow = null;
		public int milliseconds = 0;
		public float seconds = 0.0f;
		public bool isPlayback = false;
		public int playbackIndex = 0;
		public List<int> playbackDelays = new List<int>();
		public List<string> playbackActions = new List<string>();
		public string commandNow = null;

		// Example: TestAnagramJournal.cs
		public void Update(float deltaSeconds)
		{
			actionNow = null;
			seconds += deltaSeconds;
			milliseconds = (int)(Mathf.Round(seconds * 1000.0f));
			if (isPlayback)
			{
				commandNow = UpdatePlayback();
			}
		}

		private string UpdatePlayback()
		{
			string command = null;
			if (playbackIndex < DataUtil.Length(playbackDelays))
			{
				int delay = playbackDelays[playbackIndex];
				if (delay <= milliseconds)
				{
					command = playbackActions[playbackIndex];
					playbackIndex++;
					seconds -= delay / 1000.0f;
				}
			}
			return command;
		}

		// Example: TestAnagramJournal.cs
		public void Record(string act)
		{
			action = act;
			actionNow = act;
			if (!isPlayback)
			{
				playbackActions.Add(act);
				playbackDelays.Add(milliseconds);
				seconds = 0.0f;
			}
		}

		// Example: TestAnagramJournal.cs
		public void Read(string historyTsv)
		{
			string[][] table = StringUtil.ParseCsv(historyTsv, "\t");
			DataUtil.Clear(playbackDelays);
			DataUtil.Clear(playbackActions);
			if ("delay" != table[0][0] || "action" != table[0][1])
			{
				throw new System.InvalidOperationException("Expected delay and action as headers."
					+ " Got " + table[0][0] + ", " + table[0][1]);
			}
			for (int rowIndex = 1; rowIndex < DataUtil.Length(table); rowIndex++)
			{
				string[] row = table[rowIndex];
				int delay = StringUtil.ParseInt(row[0]);
				playbackDelays.Add(delay);
				string action = row[1];
				playbackActions.Add(action);
			}
		}

		// Example: TestAnagramJournal.cs
		public string Write()
		{
			string historyTsv = "delay\taction";
			for (int index = 0; index < DataUtil.Length(playbackDelays); index++)
			{
				historyTsv += "\n" + playbackDelays[index].ToString();
				historyTsv += "\t" + playbackActions[index];
			}
			return historyTsv;
		}

		public void StartPlayback()
		{
			playbackIndex = 0;
			isPlayback = true;
		}

		public bool IsPlaying()
		{
			return isPlayback && playbackIndex < DataUtil.Length(playbackDelays);
		}

		public void ReadAndPlay(string historyTsv)
		{
			if (null != historyTsv && "" != historyTsv)
			{
				Read(historyTsv);
				StartPlayback();
			}
		}
	}
}
