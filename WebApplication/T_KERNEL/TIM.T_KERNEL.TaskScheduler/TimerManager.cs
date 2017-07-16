using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using TIM.T_KERNEL.DbTableCache;

namespace TIM.T_KERNEL.TaskScheduler
{
	internal sealed class TimerManager
	{
		private static readonly TimerManager _TaskManager = new TimerManager();

		private ConcurrentDictionary<TimerCategory, TimerThread> _TimerThreads;

		public static TimerManager Instance
		{
			get
			{
				return TimerManager._TaskManager;
			}
		}

		private TimerManager()
		{
			this._TimerThreads = new ConcurrentDictionary<TimerCategory, TimerThread>();
		}

		internal static void Initialize()
		{
			TimerThread timerThread = new TimerThread(TimerCategory.SessionTask, 600000, 300000);
			TimerManager._TaskManager._TimerThreads.TryAdd(TimerCategory.SessionTask, timerThread);
			TimerThread timerThread2 = new TimerThread(TimerCategory.TableCacheTask, 300000, 60000);
			TimerManager._TaskManager._TimerThreads.TryAdd(TimerCategory.TableCacheTask, timerThread2);
			TimerThread timerThread3 = new TimerThread(TimerCategory.TimeModuleTask, 60000, 60000);
			TimerManager._TaskManager._TimerThreads.TryAdd(TimerCategory.TimeModuleTask, timerThread3);
		}

		public void AddTask(TimerCategory timerCategory, string taskName, Type taskPlan, string userId, string dbId)
		{
			TimerThread timerThread = this._TimerThreads[timerCategory];
			TaskInfo taskInfo = new TaskInfo(taskName, taskPlan, userId, dbId);
			timerThread.AddTaskInfo(taskInfo);
			timerThread.Start();
		}

		public static void RunTask(ITask task, string userId, string dbId)
		{
			LogicSession logicSession = new LogicSession(userId, LogicSessionType.S);
			User user = UserUtils.GetUser(userId);
			bool flag = user != null;
			if (flag)
			{
				logicSession.UserName = user.UserName;
			}
			logicSession.DbId = dbId;
			LogicContext current = LogicContext.Current;
			current.SetLogicSession(logicSession);
			current.UserId = userId;
			current.SetDatabase(dbId);
			current.AmId = task.MdId;
			current.MdId = task.MdId;
			current.ComId = task.ComId;
			try
			{
				task.Execute();
			}
			finally
			{
			}
		}

		public void Stop()
		{
			foreach (KeyValuePair<TimerCategory, TimerThread> keyValuePair in this._TimerThreads)
			{
				keyValuePair.Value.Dispose();
			}
			this._TimerThreads.Clear();
		}
	}
}
