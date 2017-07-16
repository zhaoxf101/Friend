using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace TIM.T_KERNEL.DbTableCache
{
	public class DllComponent : Component
	{
		private List<DllModule> m_modules = new List<DllModule>();

		private List<ModuleAttribute> m_moduleAttributes = new List<ModuleAttribute>();

		private DllComponent m_instance = null;

		public List<DllModule> Modules
		{
			get
			{
				return this.m_modules;
			}
			set
			{
				this.m_modules = value;
			}
		}

		public List<ModuleAttribute> ModuleAttributes
		{
			get
			{
				return this.m_moduleAttributes;
			}
			set
			{
				this.m_moduleAttributes = value;
			}
		}

		public DllComponent Instance
		{
			get
			{
				return this.m_instance;
			}
			set
			{
				this.m_instance = value;
			}
		}

		public DllComponent()
		{
		}

		public DllComponent(string componentFullFileName)
		{
			Assembly assembly = Assembly.LoadFrom(componentFullFileName);
			string str = "TIM." + Path.GetFileNameWithoutExtension(componentFullFileName) + ".DllComponent";
			bool flag = !(assembly.GetType(str) != null);
			if (!flag)
			{
				this.m_instance = (DllComponent)assembly.CreateInstance(str);
			}
		}

		private void DefineModule(int moduleId, string moduleName, ModuleType moduleType, string url, string wfbId, Type moduleObjType, string attributes)
		{
			DllModule dllModule = new DllModule();
			dllModule.MdId = moduleId;
			dllModule.MdName = moduleName;
			dllModule.Type = moduleType;
			dllModule.Url = url;
			dllModule.WfbId = wfbId;
			dllModule.CallObjectType = moduleObjType;
			dllModule.Attribute = attributes;
			this.m_modules.Add(dllModule);
		}

		public void DefinePageMoudle(int moduleId, string moduleName, ModuleType moduleType, string url)
		{
			this.DefineModule(moduleId, moduleName, moduleType, url, string.Empty, null, string.Empty);
		}

		public void DefineDataModule(int moduleId, string moduleName, ModuleType moduleType, Type moduleObjType)
		{
			this.DefineModule(moduleId, moduleName, moduleType, string.Empty, string.Empty, moduleObjType, string.Empty);
		}

		public void DefineTimerModule(int moduleId, string moduleName, ModuleType moduleType, string attributes)
		{
			this.DefineModule(moduleId, moduleName, moduleType, string.Empty, string.Empty, null, attributes);
		}

		public void DefineWfPageModule(int moduleId, string wfbId, ModuleType moduleType, string url)
		{
			this.DefineModule(moduleId, string.Empty, moduleType, url, wfbId, null, string.Empty);
		}

		public void DefinePortalPageModule(int moduleId, ModuleType moduleType, string url)
		{
			this.DefineModule(moduleId, string.Empty, moduleType, url, string.Empty, null, string.Empty);
		}

		public void DefineModuleAttribute(int mdId, string attrName, string attrValue)
		{
			this.ModuleAttributes.Add(new ModuleAttribute
			{
				MdId = mdId,
				AttributeName = attrName,
				AttributeValue = attrValue
			});
		}
	}
}
