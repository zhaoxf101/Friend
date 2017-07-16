using System;
using System.Web.Optimization;

namespace TIM.T_KERNEL.Optimization
{
	internal class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-2.8.3.js", new IItemTransform[0]));
			BundleTable.EnableOptimizations = true;
		}
	}
}
