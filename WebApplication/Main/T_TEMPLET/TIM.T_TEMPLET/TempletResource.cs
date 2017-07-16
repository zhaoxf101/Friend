using System;

namespace TIM.T_TEMPLET
{
	public class TempletResource
	{
		internal const string NoDeletePermission = "当前用户没有此记录的删除权限！";

		internal const string FieldNoNullOrZero = " 【{0}】不允许为空或零！\r";

		internal const string FieldNoNull = " 【{0}】不允许为空！\r";

		internal const string FieldOverLength = " 【{0}】值超出设置限定长度({1})！ \r";

		internal const string FieldOverLengthWithText = " 【{0}】值超出设置限定长度(4000)！ \r";

		internal const string WorkflowSubmitActionFail = "事务提交操作失败！ 提示信息：\r {0}";

		internal const string WorkflowSubmitNoPermission = "当前事务您没有流程提交权限！";

		internal const string WorkflowWithdrawActionFail = "事务撤回操作失败！ 提示信息：\r {0}";

		internal const string WorkflowWithdrawNoPermission = "当前事务您没有流程撤回权限！";

		internal const string WorkflowBackActionFail = "事务退回操作失败！ 提示信息：\r {0}";

		internal const string WorkflowDeliverToActionFail = "事务转交操作失败！ 提示信息：\r {0}";

		internal const string WorkflowVetoActionFail = "事务否决操作失败！ 提示信息：\r {0}";

		internal const string WorkflowNoSubmitWithWfpEnd = "当前记录处于流程结束状态，不能进行提交操作！";

		internal const string WorkflowSubmitNextPromptMessage = "<table><tr><td>流程提交成功！</td></tr><tr><td>下一步骤:{0}</td></tr><tr><td>待处理人:{1}</td></tr></table>";

		internal const string WorkflowSubmitIdlePromptMessage = "<table><tr><td>流程提交成功！</td></tr><tr><td>等待其他人会签！</td></tr><tr><td>待会签人:{0}</td></tr></table>";

		internal const string WorkflowSubmitEndPromptMessage = "<table><tr><td>流程提交成功！</td></tr><tr><td>下一步骤:{0}</td></tr></table>";

		internal const string BeNullFromWorkflowFieldRight = "该业务流程当前步骤存在域权限不允许为空配置，请打开维护页面后再提交工作流！";

		internal const string DFS_InsertDfsGroupFail = "文件服务器异常！";

		internal const string DFS_InsertDfsFileFail = "文件服务器异常！";

		internal const string DFS_OpenFileFail = "文件已被他人删除，请刷新后查看！";

		internal const string DFS_DeleteFileFail = "文件已被他人删除，请刷新后查看！";

		internal const string DFS_DeleteDfsGroupFail = "文件已被他人删除，无需再操作！";
	}
}
