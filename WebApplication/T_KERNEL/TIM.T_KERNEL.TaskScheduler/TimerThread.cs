using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace TIM.T_KERNEL.TaskScheduler
{
	internal sealed class TimerThread : IDisposable
	{
		private TimerCategory m_name = TimerCategory.None;

		private int m_dueTimeMilliseconds = 0;

		private int m_periodMilliseconds = 0;

		private ConcurrentDictionary<string, TaskInfo> m_tasks = new ConcurrentDictionary<string, TaskInfo>();

		private Timer _timer;

		public TimerCategory Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		public int DueTimeMilliseconds
		{
			get
			{
				return this.m_dueTimeMilliseconds;
			}
			set
			{
				this.m_dueTimeMilliseconds = value;
			}
		}

		public int PeriodMilliseconds
		{
			get
			{
				return this.m_periodMilliseconds;
			}
			set
			{
				this.m_periodMilliseconds = value;
			}
		}

		internal ConcurrentDictionary<string, TaskInfo> Tasks
		{
			get
			{
				return this.m_tasks;
			}
			set
			{
				this.m_tasks = value;
			}
		}

		internal TimerThread(TimerCategory name, int dueTimeMilliseconds, int periodMilliseconds)
		{
			this.Name = name;
			this.DueTimeMilliseconds = dueTimeMilliseconds;
			this.PeriodMilliseconds = periodMilliseconds;
		}

		internal void Start()
		{
			bool flag = this._timer != null;
			if (!flag)
			{
				this._timer = new Timer(new TimerCallback(this.TimerTaskCallback), null, this.DueTimeMilliseconds, this.PeriodMilliseconds);
			}
		}

		private void TimerTaskCallback(object sender)
		{
			bool flag = this._timer == null || AppRuntime.IsStopping;
			if (!flag)
			{
				this._timer.Change(-1, -1);
				GlobalCulture.SetContextCulture();
				LogicContext.Current.SetDatabase(AppConfig.DefaultDbId);
				foreach (KeyValuePair<string, TaskInfo> keyValuePair in this.Tasks)
				{
					this.ExecTask(keyValuePair.Value);
				}
				bool flag2 = this._timer == null;
				if (!flag2)
				{
					this._timer.Change(this.DueTimeMilliseconds, this.PeriodMilliseconds);
				}
			}
		}

		private void ExecTask(TaskInfo taskInfo)
		{
			ITask taskObject = this.GetTaskObject(taskInfo);
			LogicSession logicSession = new LogicSession(taskInfo.UserId, LogicSessionType.S);
			logicSession.DbId = taskInfo.DbId;
			LogicContext current = LogicContext.Current;
			current.SetLogicSession(logicSession);
			current.UserId = taskInfo.UserId;
			current.SetDatabase(taskInfo.DbId);
			current.AmId = taskObject.MdId;
			current.MdId = taskObject.MdId;
			current.ComId = taskObject.ComId;
			try
			{
				taskObject.Execute();
			}
			catch (Exception ex_7D)
			{
			}
			finally
			{
			}
		}

		private ITask GetTaskObject(TaskInfo taskInfo)
		{
			bool flag = taskInfo.TaskObject == null;
			if (flag)
			{
				taskInfo.TaskObject = (Activator.CreateInstance(taskInfo.TaskType) as ITask);
			}
			return taskInfo.TaskObject;
		}

		internal void AddTaskInfo(TaskInfo taskInfo)
		{
			bool flag = this.Tasks.ContainsKey(taskInfo.TaskName);
			if (!flag)
			{
				this.Tasks.TryAdd(taskInfo.TaskName, taskInfo);
			}
		}

		public void Dispose()
		{
			bool flag = this._timer == null;
			if (!flag)
			{
				lock (this)
				{
					this._timer.Dispose();
					this._timer = null;
				}
			}
		}
	}
}
